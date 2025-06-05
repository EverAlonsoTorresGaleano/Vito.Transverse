using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.Security;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.Helpers;
using Vito.Transverse.Identity.BAL.TransverseServices.Audit;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.SocialNetworks;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Security;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Security;



public class SecurityService(ISecurityRepository securityRepository, ICultureService cultureService, IAuditService auditService, ICachingServiceMemoryCache cachingService, ISocialNetworkService socialNetworkService, IDataBaseContextFactory dataBaseContextFactory, IOptions<IdentityServiceServerSettingsOptions> identityServerOptions, ILogger<ISecurityService> logger) : ISecurityService
{
    private readonly IdentityServiceServerSettingsOptions identityServerOptionsValues = identityServerOptions.Value;

    #region Public Methods
    public async Task<TokenResponseDTO> NewJwtTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation)
    {
        try
        {
            UserDTOToken? userInfoDTO = default;
            var grantType = Enum.Parse<TokenGrantTypeEnum>(requestBody.grant_type, true);
            TokenResponseDTO tokenResponse = default!;

            userInfoDTO = await NewJwtTokenAsync_ValidateCompanyApplicationInformation(Guid.Parse(requestBody.company_id), Guid.Parse(requestBody.company_secret), Guid.Parse(requestBody.application_id), Guid.Parse(requestBody.application_secret), requestBody.user_id ?? null, requestBody.user_secret ?? null, requestBody.scope, deviceInformation);


            var logginSuccesStatusList = new List<OAuthActionTypeEnum>()
            {
                OAuthActionTypeEnum.OAuthActionType_LoginSuccessByClientCredentials,
                OAuthActionTypeEnum.OAuthActionType_LoginSuccessByAuthorizationCode
            };

            if (userInfoDTO is not null && logginSuccesStatusList.Contains(userInfoDTO!.ActionStatus!.Value))
            {
                List<Claim> claimList = JwtTokenHelper.ToClaimsList(userInfoDTO);
                tokenResponse = await JwtTokenHelper.CreateJwtTokenAsync(requestBody, claimList, userInfoDTO, identityServerOptionsValues, cultureService);
            }
            else
            {
                tokenResponse = await JwtTokenHelper.CreateEmptyJwtTokenAsync(requestBody, userInfoDTO!);
            }
            return tokenResponse;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(NewJwtTokenAsync));
            throw;
        }
    }



    private async Task<UserDTOToken?> NewJwtTokenAsync_ValidateCompanyApplicationInformation(Guid companyClient, Guid companySecret, Guid applicationClient, Guid? applicationSecret, string? userName, string? password, string? scope, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserDTOToken? userInfo = default;
        var scopeRequest = !String.IsNullOrEmpty(scope) ? scope : deviceInformation.EndPointUrl;
        try
        {
            var contextTx = dataBaseContextFactory.GetDbContext(context);
            OAuthActionTypeEnum actionStatus = default;

            var companyInfoList = await securityRepository.GetAllCompanyListAsync(x => (
                        x.CompanyClient.Equals(companyClient)
                        && x.CompanySecret.Equals(companySecret)
                        && x.IsActive == true),
                        contextTx);
            var companyInfo = companyInfoList.FirstOrDefault();
            if (companyInfo is null)
            {
                actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_Company_ClientOrSecretNotFound;
            }
            else
            {
                var applicationInfoList = await securityRepository.GetAllApplicationListAsync(x => (
                        x.ApplicationClient.Equals(applicationClient)
                        && x.ApplicationSecret.Equals(applicationSecret)
                        && x.IsActive == true),
                        contextTx);
                var applicationInfo = applicationInfoList.FirstOrDefault();
                if (applicationInfo is null)
                {
                    actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_Application_ClientOrSecretNoFound;
                }
                else
                {
                    var companyMembershipList = await securityRepository.GetCompanyMemberhipListAsync(x => (
                                x.ApplicationFk == applicationInfo.Id
                                && x.CompanyFk == companyInfo.Id
                                && x.IsActive == true),
                                contextTx);

                    var companyMembershipInfo = companyMembershipList.FirstOrDefault();
                    if (companyMembershipInfo is null)
                    {
                        actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_CompanyMembershipNotFound;
                    }
                    else
                    {
                        userInfo = await NewJwtTokenAsync_ValidateUserInformation(companyInfo.Id, applicationInfo.Id, !string.IsNullOrEmpty(userName) ? userName : FrameworkConstants.Username_UserApi, password, scopeRequest, deviceInformation.Method, deviceInformation, contextTx);

                    }
                }
            }
            var actionsListToTrace = new List<OAuthActionTypeEnum>
            {
                OAuthActionTypeEnum.OAuthActionType_LoginFail_CompanyMembershipNotFound,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_Company_ClientOrSecretNotFound,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_Application_ClientOrSecretNoFound
            };
            if (actionsListToTrace.Contains(actionStatus))
            {
                var userTraceAddedSuccessfully = AddNewActivityLogAsync(companyInfo?.Id, companyInfo!.Id, userInfo!.Id, userInfo.RoleId, deviceInformation, actionStatus, contextTx);
                userInfo = new() { ActionStatus = actionStatus };
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(NewJwtTokenAsync_ValidateCompanyApplicationInformation));
            throw;
        }

        return userInfo;
    }

    private async Task<UserDTOToken?> NewJwtTokenAsync_ValidateUserInformation(long companyId, long applicationId, string? userName, string? password, string? scope, string? method, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserDTOToken? userInfoDTO = default;
        OAuthActionTypeEnum actionStatus = default;

        try
        {
            var contextTx = dataBaseContextFactory.GetDbContext(context);
            var userRoleInfoList = await securityRepository.GetUserRolesListAsync(x => (
                     x.CompanyFk == companyId
                     && x.ApplicationFk == applicationId
                     && (
                        (x.UserFkNavigation.UserName.Equals(userName)
                        && x.UserFkNavigation.Password.Equals(password))
                        || (x.UserFkNavigation.UserName.Equals(FrameworkConstants.Username_UserApi)
                            && password == null)
                        )

                     && x.IsActive == true
                     && x.UserFkNavigation.IsActive == true
                     && x.UserFkNavigation.IsLocked == false)
                     , contextTx);
            var userRoleInfo = userRoleInfoList.FirstOrDefault();
            deviceInformation.RoleId = userRoleInfo!.RoleFk;
            deviceInformation.ApplicationId = userRoleInfo.ApplicationFk;

            if (userRoleInfo is null)
            {
                //User do no exist
                actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_User_LoginOrPasswordInvalid;
            }
            else
            {
                var endpointList = await GetEndpointsListByRoleIdAsync(userRoleInfo.RoleFk);
                var endpointInfo = endpointList.FirstOrDefault(x => (x.EndpointUrl.Equals(scope) && x.Method.Equals(method)) && x.IsActive);

                if (endpointInfo is null)
                {
                    //Do nos have Acces to resource
                    actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_UserUnauthorized;
                }
                else
                {
                    //user valid
                    actionStatus = userRoleInfo.UserName.Equals(FrameworkConstants.Username_UserApi) ? OAuthActionTypeEnum.OAuthActionType_LoginSuccessByAuthorizationCode : OAuthActionTypeEnum.OAuthActionType_LoginSuccessByClientCredentials;

                    var userInfoList = await securityRepository.GetUserListAsync(x => x.UserName.Equals(userName), contextTx);
                    var userInfo = userInfoList.FirstOrDefault();

                    var applicationInfoList = await GetAllApplicationListAsync();
                    var applicationInfo = applicationInfoList.FirstOrDefault(x => x.Id == applicationId);
                    userInfoDTO = userInfo!.ToUserDTOToken(applicationInfo, userRoleInfo, actionStatus);
                    //deviceInformation.CompanyId = userInfo!.CompanyFk;
                }

                var userUpdatedSuccessfully = await UpdateLastUserAccessAsync(userRoleInfo.UserFk, deviceInformation, actionStatus, contextTx);
            }
            var userInfoId = userRoleInfo?.UserFk is not null ? userRoleInfo?.UserFk : FrameworkConstants.UserId_UserUnknown;
            var userInfoRoleId = userRoleInfo?.RoleFk is not null ? userRoleInfo?.RoleFk : FrameworkConstants.RoleId_UserUnknown;

            var userTraceAddedSuccessfully = await AddNewActivityLogAsync(companyId, applicationId, userInfoId, userInfoRoleId, deviceInformation, actionStatus, contextTx);
            var actionsListToTrace = new List<OAuthActionTypeEnum>
            {
                OAuthActionTypeEnum.OAuthActionType_LoginFail_User_LoginOrPasswordInvalid,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_UserUnauthorized
            };

            if (actionsListToTrace.Contains(actionStatus))
            {
                userInfoDTO = new() { ActionStatus = actionStatus };
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(NewJwtTokenAsync_ValidateUserInformation));
            throw;
        }
        return userInfoDTO;
    }

    public async Task<UserDTO?> UpdateLastUserAccessAsync(long userId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null)
    {
        UserDTO? savedSuccesfuly = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);

            var userList = await GetUserListAsync(null);
            var userInfo = userList.FirstOrDefault(x => x.Id == userId);

            var userInfoBackup = userInfo!.CloneEntity();

            userInfo!.LastAccess = cultureService.UtcNow().DateTime;
            if (actionStatus == OAuthActionTypeEnum.OAuthActionType_LoginFail_User_LoginOrPasswordInvalid)
            {
                var retryCount = userInfo.RetryCount;
                userInfo.RetryCount++;
                if (retryCount >= identityServerOptionsValues.MaxUserFailRetrys)
                {
                    userInfo.IsLocked = true;
                    userInfo.LockedDate = cultureService.UtcNow().DateTime;
                }
            }
            else
            {
                userInfo.RetryCount = 0;
            }
            savedSuccesfuly = await securityRepository.UpdateUserAsync(userInfo, context);

            await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoBackup!, userInfo, userInfo.Id.ToString(), deviceInformation);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateLastUserAccessAsync));
            throw;
        }

        return savedSuccesfuly;
    }

    public async Task<ActivityLogDTO?> AddNewActivityLogAsync(long? companyId, long? applicationId, long? userId, long? roleId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null)
    {
        ActivityLogDTO? savedRecord = null;
        try
        {
            var contextTx = dataBaseContextFactory.GetDbContext(context);
            ActivityLogDTO activityLogDb = new()
            {
                CompanyFk = companyId!.Value,
                UserFk = userId!.Value,
                ActionTypeFk = (int)actionStatus,
                Browser = deviceInformation.Browser!,
                CultureId = deviceInformation.CultureId!,
                DeviceName = deviceInformation.HostName!,
                DeviceType = deviceInformation.DeviceType!,
                Engine = deviceInformation.Engine!,
                EventDate = cultureService.UtcNow().DateTime,
                IpAddress = deviceInformation.IpAddress!,
                Platform = deviceInformation.Platform!,
                EndPointUrl = deviceInformation.EndPointUrl!,
                Method = deviceInformation.Method!,
                QueryString = deviceInformation.QueryString!,
                Referer = deviceInformation.Referer!,
                UserAgent = deviceInformation.UserAgent!,
                ApplicationId = applicationId ?? deviceInformation.ApplicationId!.Value,
                RoleId = roleId ?? deviceInformation.RoleId!.Value,

            };
            savedRecord = await securityRepository.AddNewActivityLogAsync(activityLogDb, contextTx);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(AddNewActivityLogAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<ActivityLogDTO?> AddNewActivityLogAsync(DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null)
    {
        ActivityLogDTO? returnValue = null!;
        try
        {
            var companyId = deviceInformation.CompanyId;
            var applicationId = deviceInformation.ApplicationId;
            var userId = deviceInformation.UserId;
            var roleId = deviceInformation.RoleId;
            returnValue = await AddNewActivityLogAsync(companyId!.Value, applicationId, userId, roleId, deviceInformation, actionStatus, context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(AddNewActivityLogAsync));
        }
        return returnValue!;
    }



    public async Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        ApplicationDTO? savedRecord = null;
        try
        {
            newRecord.ApplicationClient = Guid.NewGuid();
            newRecord.ApplicationSecret = Guid.NewGuid();
            savedRecord = await securityRepository.CreateNewApplicationAsync(newRecord, deviceInformation); ;
            var savedAuditRecord = await auditService.NewRowAuditAsync(deviceInformation.CompanyId!.Value, deviceInformation.UserId!.Value, savedRecord!, savedRecord!.Id.ToString(), deviceInformation, true);
            OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_CreateNewApplication;
            var savedActivityLog = await AddNewActivityLogAsync(deviceInformation, actionStatus);
            return savedRecord;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewApplicationAsync));
            throw;
        }
    }

    public async Task<CompanyDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await securityRepository.CreateNewCompanyAsync(newRecord, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCompanyAsync));
            throw;
        }
    }

    public async Task<CompanyDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO companyApplicationInfo, DeviceInformationDTO deviceInformation)
    {
        CompanyDTO? returnValue = null;
        try
        {
            returnValue = await securityRepository.UpdateCompanyApplicationsAsync(companyApplicationInfo, deviceInformation);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyApplicationsAsync));
            throw;
        }
        return returnValue;
    }

    public async Task<UserDTO?> CreateNewUserAsync(long companyId, UserDTO userInfo, DeviceInformationDTO deviceInformation)
    {
        UserDTO? returnValue = null;
        try
        {
            returnValue = await securityRepository.CreateNewUserAsync(userInfo, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewUserAsync));
            throw;
        }
        return returnValue;
    }

    public async Task<UserDTO?> ChangeUserPasswordAsync(UserDTO recordToUpdate, DeviceInformationDTO deviceInformation)
    {
        UserDTO? savedRecord = null;
        try
        {
            var userInfoList = await securityRepository.GetUserListAsync(x => (
                          x.Id == recordToUpdate.Id
                          && x.CompanyFk.Equals(recordToUpdate.CompanyFk)
                          && x.Password.Equals(recordToUpdate.Password)));
            var userInfo = userInfoList.FirstOrDefault();
            var userInfoBackup = userInfo.CloneEntity();
            if (userInfo is not null)
            {
                userInfo.IsActive = true;
                userInfo.EmailValidated = true;
                userInfo.IsLocked = false;
                userInfo.RequirePasswordChange = true;
                userInfo.RetryCount = 0;
                userInfo.LastAccess = cultureService.UtcNow().DateTime;
                userInfo.Password = recordToUpdate.NewPassword1;
                savedRecord = await securityRepository.UpdateUserAsync(userInfo);

                var savedAuditRecord = await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoBackup!, userInfo, userInfo.Id.ToString(), deviceInformation);
                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_ChangeUserPassword;
                var savedActivityLog = await AddNewActivityLogAsync(deviceInformation, actionStatus);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(ChangeUserPasswordAsync));
        }
        return savedRecord;
    }





    public async Task<UserDTO?> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation)
    {
        UserDTO? savedRecord = null;

        try
        {
            var userList = await GetUserListAsync(companyId);
            var userInfo = userList.FirstOrDefault(u => u.Id == userId);
            if (userInfo is not null)
            {
                var userInfoDTOBackup = userInfo.CloneEntity();

                userInfo.IsActive = false;
                userInfo.EmailValidated = false;
                userInfo.IsLocked = false;
                userInfo.RequirePasswordChange = true;
                userInfo.RetryCount = 0;
                userInfo.LastAccess = cultureService.UtcNow().DateTime;
                savedRecord = await securityRepository.UpdateUserAsync(userInfo);
                var savedAuditRecord = await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoDTOBackup!, userInfo, userInfo.Id.ToString(), deviceInformation);

                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_SendActivationEmail;

                var savedActivityLog = await AddNewActivityLogAsync(companyId, null, userInfo.Id, null, deviceInformation, actionStatus);

                //Send Activation Email
                List<KeyValuePair<string, string>> emailTemplateParams = new()
                {
                     new (EmailTemplateParametersEnum.EMAIL.ToString(),userInfo?.Email!),
                     new (EmailTemplateParametersEnum.FULL_NAME.ToString(),$"{userInfo?.Name!} {userInfo?.LastName!}"),
                     new (EmailTemplateParametersEnum.USER_ID.ToString(),userInfo?.Id.ToString()!),
                     new (EmailTemplateParametersEnum.APPLICATION_CLIENTID.ToString(),userInfo?.CompanyClient.ToString()!),
                     new (EmailTemplateParametersEnum.ACTIVATION_ID.ToString(),userInfo?.ActivationId.ToString()!),
                };
                var savedNOtification = await socialNetworkService.SendNotificationByTemplateAsync(companyId, NotificationTypeEnum.NotificationType_Email, (int)NotificationTemplatesEnum.ActivationEmail, emailTemplateParams, [userInfo?.Email], null, null, cultureService.GetCurrectCulture().Name);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(SendActivationEmailAsync));
        }

        return savedRecord;

    }

    public async Task<bool?> ActivateAccountAsync(string activationToken, DeviceInformationDTO deviceInformation)
    {
        UserDTO? savedRecord = null;
        try
        {
            var activationTokenList = ValidateEmailActivationToken(activationToken);
            Guid companyClientId = Guid.Parse(activationTokenList.First());
            long userId = long.Parse(activationTokenList[1]);
            Guid activationId = Guid.Parse(activationTokenList.Last());


            var userInfoList = await securityRepository.GetUserListAsync(x => (
                        x.Id == userId
                        && x.CompanyFkNavigation.CompanyClient.Equals(companyClientId)
                        && x.ActivationId.Equals(activationId)));
            var userInfo = userInfoList.FirstOrDefault();
            var userInfoDTOBackup = userInfo.CloneEntity();
            if (userInfo is not null)
            {
                userInfo.IsActive = true;
                userInfo.EmailValidated = true;
                userInfo.IsLocked = false;
                userInfo.RequirePasswordChange = true;
                userInfo.RetryCount = 0;
                userInfo.LastAccess = cultureService.UtcNow().DateTime;
                savedRecord = await securityRepository.UpdateUserAsync(userInfo);

                var savedAuditRecord = await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoDTOBackup!, userInfo, userInfo.Id.ToString(), deviceInformation);
                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_ActivateUser;
                var savedActivityLog = await AddNewActivityLogAsync(userInfo.CompanyFk, null, userId, null, deviceInformation, actionStatus);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(ActivateAccountAsync));
        }
        return savedRecord != null;
    }



    public async Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation)
    {
        EndpointDTO? endpointInfo = null!;
        try
        {
            var roleId = deviceInformation.RoleId;
            if (roleId is not null)
            {
                var endpointList = await GetEndpointsListByRoleIdAsync(roleId.Value);
                endpointInfo = endpointList.FirstOrDefault(x => (x.EndpointUrl.Equals(deviceInformation.EndPointUrl, StringComparison.InvariantCultureIgnoreCase) && x.Method.Equals(deviceInformation.Method, StringComparison.InvariantCultureIgnoreCase)) && x.IsActive);
                var actionType = endpointInfo is null ? OAuthActionTypeEnum.OAuthActionType_ApiRequestUnauthorized : OAuthActionTypeEnum.OAuthActionType_ApiRequestSuccessfully;
                var savedActivityLog = await AddNewActivityLogAsync(deviceInformation, actionType);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(ValidateEndpointAuthorizationAsync));
            throw;
        }
        return endpointInfo!;
    }








    public async Task<List<ApplicationDTO>> GetAllApplicationListAsync()
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<ApplicationDTO>>(CacheItemKeysEnum.AllApplicationList.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetAllApplicationListAsync(x => x.Id > 0);
                cachingService.SetCacheData(CacheItemKeysEnum.AllApplicationList.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllApplicationListAsync));
            throw;
        }
    }

    public async Task<List<ApplicationDTO>> GetApplicationListAsync(long? companyId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<ApplicationDTO>>(CacheItemKeysEnum.ApplicationListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetApplicationListAsync(x => companyId == null || x.CompanyFk == companyId && x.IsActive == true);
                cachingService.SetCacheData(CacheItemKeysEnum.ApplicationListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllApplicationListAsync));
            throw;
        }
    }


    public async Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipAsync(long? companyId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<CompanyMembershipsDTO>>(CacheItemKeysEnum.CompanyMemberhipListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetCompanyMemberhipListAsync(x => companyId == null || x.CompanyFk == companyId);
                cachingService.SetCacheData(CacheItemKeysEnum.CompanyMemberhipListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllApplicationListAsync));
            throw;
        }
    }


    public async Task<List<CompanyDTO>> GetAllCompanyListAsync()
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<CompanyDTO>>(CacheItemKeysEnum.AllCompanyList.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetAllCompanyListAsync(x => x.Id > 0);
                cachingService.SetCacheData(CacheItemKeysEnum.AllCompanyList.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllCompanyListAsync));
            throw;
        }

    }

    public async Task<List<RoleDTO>> GetRoleListAsync(long? companyId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<RoleDTO>>(CacheItemKeysEnum.RoleListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetRoleListAsync(x => companyId == null || x.CompanyFk == companyId);
                cachingService.SetCacheData(CacheItemKeysEnum.RoleListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRoleListAsync));
            throw;
        }
    }

    public async Task<RoleDTO> GetRolePermissionListAsync(long roleId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<RoleDTO>(CacheItemKeysEnum.RolePermissionListByRoleId + roleId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetRolePermissionListAsync(x => x.Id == roleId);
                cachingService.SetCacheData(CacheItemKeysEnum.RolePermissionListByRoleId + roleId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRolePermissionListAsync));
            throw;
        }
    }

    public async Task<List<ModuleDTO>> GetModuleListAsync(long? applicationId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<ModuleDTO>>(CacheItemKeysEnum.ModuleListByApplicationId + applicationId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetModuleListAsync(x => applicationId == null || x.ApplicationFk == applicationId);
                cachingService.SetCacheData(CacheItemKeysEnum.ModuleListByApplicationId + applicationId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetModuleListAsync));
            throw;
        }
    }

    public async Task<List<EndpointDTO>> GetEndpointsListAsync(long moduleId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<EndpointDTO>>(CacheItemKeysEnum.EndpointListByModuleId + moduleId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetEndpointsListAsync(x => x.ModuleFk == moduleId);
                cachingService.SetCacheData(CacheItemKeysEnum.EndpointListByModuleId + moduleId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEndpointsListAsync));
            throw;
        }
    }

    public async Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<EndpointDTO>>(CacheItemKeysEnum.EndpointListByRoleId + roleId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetEndpointsListByRoleIdAsync(roleId);
                cachingService.SetCacheData(CacheItemKeysEnum.EndpointListByRoleId + roleId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEndpointsListByRoleIdAsync));
            throw;
        }
    }


    public async Task<List<ComponentDTO>> GetComponentListAsync(long endpointId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<ComponentDTO>>(CacheItemKeysEnum.ComponentListByEndpointId + endpointId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetComponentListAsync(x => x.EndpointFk == endpointId);
                cachingService.SetCacheData(CacheItemKeysEnum.ComponentListByEndpointId + endpointId.ToString(), returnList);
            }
            return returnList;


        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetComponentListAsync));
            throw;
        }
    }

    public async Task<List<UserRoleDTO>> GetUserRolesListAsync(long userId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<UserRoleDTO>>(CacheItemKeysEnum.UserRoleListByUserId + userId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetUserRolesListAsync(x => x.UserFk == userId);
                cachingService.SetCacheData(CacheItemKeysEnum.UserRoleListByUserId + userId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserRolesListAsync));
            throw;
        }
    }

    public async Task<UserDTO> GetUserPermissionListAsync(long userId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<UserDTO>(CacheItemKeysEnum.UserPermissionListByUserId + userId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetUserPermissionListAsync(x => x.Id == userId);
                cachingService.SetCacheData(CacheItemKeysEnum.UserPermissionListByUserId + userId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserPermissionListAsync));
            throw;
        }
    }

    public async Task<List<UserDTO>> GetUserListAsync(long? companyId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<UserDTO>>(CacheItemKeysEnum.UserListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await securityRepository.GetUserListAsync(x => companyId == null || x.CompanyFk == companyId);
                cachingService.SetCacheData(CacheItemKeysEnum.UserListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserPermissionListAsync));
            throw;
        }
    }


    #endregion


    #region Private Methods


    private List<string> ValidateEmailActivationToken(string activationToken)
    {
        try
        {
            var activationTokenList = activationToken.Split("@").ToList();
            if (activationTokenList.Count != 3)
            {
                throw new Exception(TransverseExceptionEnum.ActivateAccount_InvalidToken.ToString());
            }
            Guid companyClientId = Guid.Parse(activationTokenList.First());
            long userId = long.Parse(activationTokenList[1]);
            Guid activationId = Guid.Parse(activationTokenList.Last());
            return activationTokenList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(ValidateEmailActivationToken));
            throw new Exception(TransverseExceptionEnum.UserPermissionException_ModuleFromApplicationNotFound.ToString());
        }

    }

    #endregion

    //decodejw token on react/NodeJS
    //    import jwt_decode from 'jwt-decode';

    //var token = 'eyJ0eXAiO.../// jwt token';

    //    var decoded = jwt_decode(token);
    //    console.log(decoded);

}
