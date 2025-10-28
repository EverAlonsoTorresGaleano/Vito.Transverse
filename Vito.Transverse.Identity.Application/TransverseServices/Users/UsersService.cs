using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Application.TransverseServices.Audit;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Application.TransverseServices.Culture;
using  Vito.Transverse.Identity.Application.TransverseServices.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Users;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Users;

public class UsersService(ILogger<UsersService> logger, IUsersRepository usersRepository, ICultureService cultureService, IAuditService auditService, ICachingServiceMemoryCache cachingService, ISocialNetworkService socialNetworkService, IOptions<IdentityServiceServerSettingsOptions> identityServerOptions) : IUsersService
{
    private readonly IdentityServiceServerSettingsOptions identityServerOptionsValues = identityServerOptions.Value;

    public async Task<UserDTO?> UpdateLastUserAccessAsync(long userId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null)
    {
        UserDTO? savedSuccesfuly = null;
        try
        {
            //context = dataBaseContextFactory.GetDbContext(context);

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
            savedSuccesfuly = await usersRepository.UpdateUserAsync(userInfo, context);

            await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoBackup!, userInfo, userInfo.Id.ToString(), deviceInformation);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateLastUserAccessAsync));
            throw;
        }

        return savedSuccesfuly;
    }

    public async Task<List<UserRoleDTO>> GetUserRolesListAsync(long userId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<UserRoleDTO>>(CacheItemKeysEnum.UserRoleListByUserId + userId.ToString());
            if (returnList == null)
            {
                returnList = await usersRepository.GetUserRolesListAsync(x => x.UserFk == userId);
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
                returnList = await usersRepository.GetUserPermissionListAsync(x => x.Id == userId);
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
                returnList = await usersRepository.GetUserListAsync(x => companyId == null || x.CompanyFk == companyId);
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

    public async Task<UserDTO?> CreateNewUserAsync(long companyId, UserDTO userInfo, DeviceInformationDTO deviceInformation)
    {
        UserDTO? returnValue = null;
        try
        {
            returnValue = await usersRepository.CreateNewUserAsync(userInfo, deviceInformation);
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
            var userInfoList = await usersRepository.GetUserListAsync(x =>
                          x.Id == recordToUpdate.Id
                          && x.CompanyFk.Equals(recordToUpdate.CompanyFk)
                          && x.Password.Equals(recordToUpdate.Password));
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
                savedRecord = await usersRepository.UpdateUserAsync(userInfo);

                var savedAuditRecord = await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoBackup!, userInfo, userInfo.Id.ToString(), deviceInformation);
                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_ChangeUserPassword;
                var savedActivityLog = await auditService.AddNewActivityLogAsync(deviceInformation, actionStatus);
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
                savedRecord = await usersRepository.UpdateUserAsync(userInfo);
                var savedAuditRecord = await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoDTOBackup!, userInfo, userInfo.Id.ToString(), deviceInformation);

                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_SendActivationEmail;

                var savedActivityLog = await auditService.AddNewActivityLogAsync(companyId, null, userInfo.Id, null, deviceInformation, actionStatus);

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


            var userInfoList = await usersRepository.GetUserListAsync(x =>
                        x.Id == userId
                        && x.CompanyFkNavigation.CompanyClient.Equals(companyClientId)
                        && x.ActivationId.Equals(activationId));
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
                savedRecord = await usersRepository.UpdateUserAsync(userInfo);

                var savedAuditRecord = await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoDTOBackup!, userInfo, userInfo.Id.ToString(), deviceInformation);
                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_ActivateUser;
                var savedActivityLog = await auditService.AddNewActivityLogAsync(userInfo.CompanyFk, null, userId, null, deviceInformation, actionStatus);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(ActivateAccountAsync));
        }
        return savedRecord != null;
    }


    public async Task<List<RoleDTO>> GetRoleListAsync(long? companyId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<RoleDTO>>(CacheItemKeysEnum.RoleListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await usersRepository.GetRoleListAsync(x => companyId == null || x.CompanyFk == companyId);
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

}
