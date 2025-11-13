using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.Application.Helpers;
using Vito.Transverse.Identity.Application.TransverseServices.Applications;
using Vito.Transverse.Identity.Application.TransverseServices.Audit;
using Vito.Transverse.Identity.Application.TransverseServices.Culture;
using Vito.Transverse.Identity.Application.TransverseServices.Users;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Applications;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Companies;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Users;
using Vito.Transverse.Identity.Entities.Extensions;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Application.TransverseServices.Security;



public class SecurityService(ILogger<SecurityService> logger, IApplicationsRepository applicationsRepository, ICompaniesRepository companiesRepository, IUsersRepository usersRepository, IApplicationsService applicationsService, IUsersService usersService, ICultureService cultureService, IAuditService auditService, IDataBaseContextFactory dataBaseContextFactory, IOptions<IdentityServiceServerSettingsOptions> identityServerOptions) : ISecurityService
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

            userInfoDTO = await NewJwtTokenAsync_ValidateCompanyApplicationInformation(Guid.Parse(requestBody.company_id), !string.IsNullOrEmpty(requestBody.company_secret) ? Guid.Parse(requestBody.company_secret) : null, Guid.Parse(requestBody.application_id), Guid.Parse(requestBody.application_secret), requestBody.user_id ?? null, requestBody.user_secret ?? null, requestBody.scope, deviceInformation);


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

    private async Task<UserDTOToken?> NewJwtTokenAsync_ValidateCompanyApplicationInformation(Guid companyClient, Guid? companySecret, Guid applicationClient, Guid? applicationSecret, string? userName, string? password, string? scope, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserDTOToken? userInfo = default;
        var scopeRequest = !String.IsNullOrEmpty(scope) ? scope : deviceInformation.EndPointPattern;
        try
        {
            var contextTx = dataBaseContextFactory.GetDbContext(context);
            OAuthActionTypeEnum actionStatus = default;

            var companyInfoList = await companiesRepository.GetAllCompanyListAsync(x => (
                        x.CompanyClient.Equals(companyClient)
                        //&& x.CompanySecret.Equals(companySecret)
                        && x.IsActive == true),
                        contextTx);
            var companyInfo = companyInfoList.FirstOrDefault();
            if (companyInfo is null)
            {
                actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_Company_ClientOrSecretNotFound;
            }
            else
            {
                var applicationInfoList = await applicationsRepository.GetAllApplicationListAsync(x => (
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
                    var companyMembershipList = await companiesRepository.GetCompanyMemberhipListAsync(x => (
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
                var userTraceAddedSuccessfully = auditService.AddNewActivityLogAsync(companyInfo?.Id, companyInfo!.Id, userInfo!.Id, userInfo.RoleId, deviceInformation, actionStatus, contextTx);
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
            var userRoleInfoList = await usersRepository.GetUserRolesListAsync(x => (
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


            if (userRoleInfo is null)
            {
                //User do no exist
                actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_User_LoginOrPasswordInvalid;
            }
            else
            {
                deviceInformation.RoleId = userRoleInfo!.RoleFk;
                deviceInformation.ApplicationId = userRoleInfo.ApplicationFk;
                var endpointList = await applicationsService.GetEndpointsListByRoleIdAsync(userRoleInfo.RoleFk);
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

                    var userInfoList = await usersRepository.GetUserListAsync(x => x.UserName.Equals(userName), contextTx);
                    var userInfo = userInfoList.FirstOrDefault();

                    var applicationInfoList = await applicationsService.GetAllApplicationListAsync();
                    var applicationInfo = applicationInfoList.FirstOrDefault(x => x.Id == applicationId);
                    userInfoDTO = userInfo!.ToUserDTOToken(applicationInfo, userRoleInfo, actionStatus);
                    //deviceInformation.CompanyId = userInfo!.CompanyFk;
                }

                var userUpdatedSuccessfully = await usersService.UpdateLastUserAccessAsync(userRoleInfo.UserFk, deviceInformation, actionStatus, contextTx);
            }
            var userInfoId = userRoleInfo?.UserFk is not null ? userRoleInfo?.UserFk : FrameworkConstants.UserId_UserUnknown;
            var userInfoRoleId = userRoleInfo?.RoleFk is not null ? userRoleInfo?.RoleFk : FrameworkConstants.RoleId_UserUnknown;

            var userTraceAddedSuccessfully = await auditService.AddNewActivityLogAsync(companyId, applicationId, userInfoId, userInfoRoleId, deviceInformation, actionStatus, contextTx);
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


    #endregion

}
