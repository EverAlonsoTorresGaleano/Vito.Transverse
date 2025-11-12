using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.Application.TransverseServices.Audit;
using Vito.Transverse.Identity.Application.TransverseServices.Caching;
using Vito.Transverse.Identity.Application.TransverseServices.Culture;
using Vito.Transverse.Identity.Application.TransverseServices.SocialNetworks;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Users;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.Extensions;

namespace Vito.Transverse.Identity.Application.TransverseServices.Users;

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

    public async Task<List<UserRoleDTO>> GetUserRolesListAsync(long? userId)
    {
        try
        {
            var cacheKey = CacheItemKeysEnum.UserRoleListByUserId + userId?.ToString() ?? "all";
            var returnList = cachingService.GetCacheDataByKey<List<UserRoleDTO>>(cacheKey);
            if (returnList == null)
            {
                returnList = await usersRepository.GetUserRolesListAsync(x => userId == null || x.UserFk == userId);
                cachingService.SetCacheData(cacheKey, returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserRolesListAsync));
            throw;
        }
    }

    public async Task<UserRoleDTO?> GetUserRoleByIdAsync(long userId, long roleId, long? companyFk, long? applicationFk)
    {
        try
        {
            // If companyFk or applicationFk are not provided, we need to get them from the first matching record
            if (companyFk == null || applicationFk == null)
            {
                var userRoles = await usersRepository.GetUserRolesListAsync(x => x.UserFk == userId && x.RoleFk == roleId);
                var firstMatch = userRoles.FirstOrDefault();
                if (firstMatch == null)
                {
                    return null;
                }
                companyFk = companyFk ?? firstMatch.CompanyFk;
                applicationFk = applicationFk ?? firstMatch.ApplicationFk;
            }

            return await usersRepository.GetUserRoleByIdAsync(userId, roleId, companyFk.Value, applicationFk.Value);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserRoleByIdAsync));
            throw;
        }
    }

    public async Task<UserRoleDTO?> CreateNewUserRoleAsync(UserRoleDTO userRoleInfo, DeviceInformationDTO deviceInformation)
    {
        UserRoleDTO? returnValue = null;
        try
        {
            // Set company and application from device information if not provided
            if (userRoleInfo.CompanyFk == 0 && deviceInformation.CompanyId.HasValue)
            {
                userRoleInfo.CompanyFk = deviceInformation.CompanyId.Value;
            }
            if (userRoleInfo.ApplicationFk == 0 && deviceInformation.ApplicationId.HasValue)
            {
                userRoleInfo.ApplicationFk = deviceInformation.ApplicationId.Value;
            }

            returnValue = await usersRepository.CreateNewUserRoleAsync(userRoleInfo, deviceInformation);

            if (returnValue != null)
            {
                // Clear cache
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.UserRoleListByUserId + userRoleInfo.UserFk.ToString());
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.UserRoleListByUserId + "all");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewUserRoleAsync));
            throw;
        }
        return returnValue;
    }

    public async Task<UserRoleDTO?> UpdateUserRoleByIdAsync(UserRoleDTO userRoleInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var userRoleInfoBackup = await GetUserRoleByIdAsync(userRoleInfo.UserFk, userRoleInfo.RoleFk, userRoleInfo.CompanyFk, userRoleInfo.ApplicationFk);
            if (userRoleInfoBackup == null)
            {
                return null;
            }

            var savedRecord = await usersRepository.UpdateUserRoleByIdAsync(userRoleInfo, deviceInformation);

            if (savedRecord != null)
            {
                await auditService.UpdateRowAuditAsync(userRoleInfo.CompanyFk, deviceInformation.UserId!.Value, userRoleInfoBackup, userRoleInfo, $"{userRoleInfo.UserFk}_{userRoleInfo.RoleFk}", deviceInformation);

                // Clear cache
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.UserRoleListByUserId + userRoleInfo.UserFk.ToString());
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.UserRoleListByUserId + "all");
            }

            return savedRecord;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateUserRoleByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteUserRoleByIdAsync(long userId, long roleId, long? companyFk, long? applicationFk, DeviceInformationDTO deviceInformation)
    {
        bool deleted = false;
        try
        {
            // If companyFk or applicationFk are not provided, we need to get them from the first matching record
            if (companyFk == null || applicationFk == null)
            {
                var userRoles = await usersRepository.GetUserRolesListAsync(x => x.UserFk == userId && x.RoleFk == roleId);
                var firstMatch = userRoles.FirstOrDefault();
                if (firstMatch == null)
                {
                    return false;
                }
                companyFk = companyFk ?? firstMatch.CompanyFk;
                applicationFk = applicationFk ?? firstMatch.ApplicationFk;
            }

            var userRoleToDelete = await usersRepository.GetUserRoleByIdAsync(userId, roleId, companyFk.Value, applicationFk.Value);
            if (userRoleToDelete == null)
            {
                return false;
            }

            var userRoleInfoBackup = userRoleToDelete with { };
            deleted = await usersRepository.DeleteUserRoleByIdAsync(userId, roleId, companyFk.Value, applicationFk.Value, deviceInformation);

            if (deleted)
            {
                await auditService.DeleteRowAuditAsync(companyFk.Value, deviceInformation.UserId!.Value, userRoleInfoBackup, $"{userId}_{roleId}", deviceInformation, true);

                // Clear cache
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.UserRoleListByUserId + userId.ToString());
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.UserRoleListByUserId + "all");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteUserRoleByIdAsync));
            throw;
        }
        return deleted;
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


    public async Task<List<ListItemDTO>> GetUserListItenAsync(long? companyId)
    {
        var listItem = await GetUserListAsync(companyId);
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
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

    public async Task<List<ListItemDTO>> GetRoleListItemAsync(long? companyId)
    {
        var listItem = await GetRoleListAsync(companyId);
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<RoleDTO?> GetRoleByIdAsync(long roleId)
    {
        try
        {
            return await usersRepository.GetRoleByIdAsync(roleId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRoleByIdAsync));
            throw;
        }
    }

    public async Task<RoleDTO?> UpdateRoleByIdAsync(long roleId, RoleDTO roleInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            roleInfo.Id = roleId;
            var roleInfoBackup = await usersRepository.GetRoleByIdAsync(roleId);
            if (roleInfoBackup == null)
            {
                return null;
            }

            var savedRecord = await usersRepository.UpdateRoleAsync(roleInfo);
            if (savedRecord != null)
            {
                await auditService.UpdateRowAuditAsync(roleInfo.CompanyFk, deviceInformation.UserId!.Value, roleInfoBackup, roleInfo, roleId.ToString(), deviceInformation);
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.RoleListByCompanyId + roleInfo.CompanyFk.ToString());
            }
            return savedRecord;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateRoleByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteRoleByIdAsync(long roleId, DeviceInformationDTO deviceInformation)
    {
        bool deleted = false;
        try
        {
            var roleToDelete = await usersRepository.GetRoleByIdAsync(roleId);
            if (roleToDelete == null)
            {
                return false;
            }

            var roleInfoBackup = roleToDelete.CloneEntity();
            deleted = await usersRepository.DeleteRoleAsync(roleId);
            if (deleted)
            {
                await auditService.DeleteRowAuditAsync(roleToDelete.CompanyFk, deviceInformation.UserId!.Value, roleInfoBackup, roleId.ToString(), deviceInformation, true);
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.RoleListByCompanyId + roleToDelete.CompanyFk.ToString());
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteRoleByIdAsync));
            throw;
        }
        return deleted;
    }

    public async Task<UserDTO?> GetUserByIdAsync(long userId)
    {
        try
        {
            var userList = await usersRepository.GetUserListAsync(x => x.Id == userId);
            return userList.FirstOrDefault();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserByIdAsync));
            throw;
        }
    }

    public async Task<UserDTO?> UpdateUserByIdAsync(long userId, UserDTO userInfo, DeviceInformationDTO deviceInformation)
    {
        UserDTO? savedRecord = null;
        try
        {
            var userList = await usersRepository.GetUserListAsync(x => x.Id == userId);
            var existingUser = userList.FirstOrDefault();
            if (existingUser == null)
            {
                return null;
            }

            var userInfoBackup = existingUser.CloneEntity();
            userInfo.Id = userId;
            savedRecord = await usersRepository.UpdateUserAsync(userInfo);

            var savedAuditRecord = await auditService.UpdateRowAuditAsync(userInfo.CompanyFk, userId, userInfoBackup!, userInfo, userId.ToString(), deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateUserByIdAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<bool> DeleteUserByIdAsync(long userId, DeviceInformationDTO deviceInformation)
    {
        bool deleted = false;
        try
        {
            var userList = await usersRepository.GetUserListAsync(x => x.Id == userId);
            var userToDelete = userList.FirstOrDefault();
            if (userToDelete == null)
            {
                return false;
            }

            // Soft delete by setting IsActive to false
            var userInfoBackup = userToDelete.CloneEntity();
            userToDelete.IsActive = false;
            var updatedUser = await usersRepository.UpdateUserAsync(userToDelete);

            if (updatedUser != null)
            {
                await auditService.UpdateRowAuditAsync(userToDelete.CompanyFk, userId, userInfoBackup!, userToDelete, userId.ToString(), deviceInformation);
                deleted = true;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteUserByIdAsync));
            throw;
        }
        return deleted;
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
