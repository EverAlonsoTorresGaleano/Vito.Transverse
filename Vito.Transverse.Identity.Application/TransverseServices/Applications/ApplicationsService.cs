using Microsoft.Extensions.Logging;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.Application.TransverseServices.Audit;
using Vito.Transverse.Identity.Application.TransverseServices.Caching;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Applications;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.Extensions;

namespace Vito.Transverse.Identity.Application.TransverseServices.Applications;

public class ApplicationsService(ILogger<ApplicationsService> logger, IApplicationsRepository applicationsRepository, IAuditService auditService, ICachingServiceMemoryCache cachingService) : IApplicationsService
{

    public async Task<List<ApplicationDTO>> GetAllApplicationListAsync()
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<ApplicationDTO>>(CacheItemKeysEnum.AllApplicationList.ToString());
            if (returnList == null)
            {
                returnList = await applicationsRepository.GetAllApplicationListAsync(x => x.Id > 0);
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


    public async Task<ApplicationDTO?> GetApplicationByIdAsync(long applicationId)
    {
        var returnList = await GetAllApplicationListAsync();
        var returValue = returnList.FirstOrDefault(a => a.Id == applicationId);
        return returValue;
    }

    public async Task<List<ApplicationDTO>> GetApplicationListAsync(long? companyId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<ApplicationDTO>>(CacheItemKeysEnum.ApplicationListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await applicationsRepository.GetApplicationListAsync(x => companyId == null || x.CompanyFk == companyId && x.IsActive == true);
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


    public async Task<List<ListItemDTO>> GetAllApplicationListItemAsync(long? companyId)
    {
        var listItem = await GetApplicationListAsync(companyId);
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }


    public async Task<RoleDTO> GetRolePermissionListAsync(long roleId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<RoleDTO>(CacheItemKeysEnum.RolePermissionListByRoleId + roleId.ToString());
            if (returnList == null)
            {
                returnList = await applicationsRepository.GetRolePermissionListAsync(x => x.Id == roleId);
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

    public async Task<List<RoleDTO>> GetRoleListByApplicationIdAsync(long applicationId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<RoleDTO>>(CacheItemKeysEnum.RoleListByApplicationId + applicationId.ToString());
            if (returnList == null)
            {
                returnList = await applicationsRepository.GetRoleListByApplicationIdAsync(x => x.ApplicationFk == applicationId);
                cachingService.SetCacheData(CacheItemKeysEnum.RoleListByApplicationId + applicationId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRoleListByApplicationIdAsync));
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
                returnList = await applicationsRepository.GetModuleListAsync(x => applicationId == null || x.ApplicationFk == applicationId);
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

    public async Task<ModuleDTO?> GetModuleByIdAsync(long moduleId)
    {
        try
        {
            var returnList = await GetModuleListAsync(null);
            var returnValue = returnList.FirstOrDefault(m => m.Id == moduleId);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetModuleByIdAsync));
            throw;
        }
    }

    public async Task<ModuleDTO?> CreateNewModuleAsync(ModuleDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        ModuleDTO? savedRecord = null;
        try
        {
            savedRecord = await applicationsRepository.CreateNewModuleAsync(newRecord, deviceInformation);
            var savedAuditRecord = await auditService.NewRowAuditAsync(deviceInformation.CompanyId!.Value, deviceInformation.UserId!.Value, savedRecord!, savedRecord!.Id.ToString(), deviceInformation, true);
            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.ModuleListByApplicationId + newRecord.ApplicationFk.ToString());
            return savedRecord;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewModuleAsync));
            throw;
        }
    }

    public async Task<ModuleDTO?> UpdateModuleByIdAsync(long moduleId, ModuleDTO moduleInfo, DeviceInformationDTO deviceInformation)
    {
        ModuleDTO? savedRecord = null;
        try
        {
            var existingRecord = await GetModuleByIdAsync(moduleId);
            if (existingRecord is null)
            {
                return null;
            }

            var userId = deviceInformation.UserId!.Value;
            var applicationId = existingRecord.ApplicationFk;
            var originalRecord = existingRecord with { };
            var utcNow = DateTime.UtcNow;

            var recordPreparedForUpdate = existingRecord with
            {
                NameTranslationKey = moduleInfo.NameTranslationKey,
                DescriptionTranslationKey = moduleInfo.DescriptionTranslationKey,
                PositionIndex = moduleInfo.PositionIndex,
                IsActive = moduleInfo.IsActive,
                IsVisible = moduleInfo.IsVisible,
                IsApi = moduleInfo.IsApi
            };

            savedRecord = await applicationsRepository.UpdateModuleByIdAsync(recordPreparedForUpdate);
            if (savedRecord is null)
            {
                return null;
            }

            await auditService.UpdateRowAuditAsync(deviceInformation.CompanyId!.Value, userId, originalRecord, savedRecord, savedRecord.Id.ToString(), deviceInformation, true);

            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.ModuleListByApplicationId + applicationId.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateModuleByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteModuleByIdAsync(long moduleId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var existingRecord = await GetModuleByIdAsync(moduleId);
            if (existingRecord is null)
            {
                return false;
            }

            var userId = deviceInformation.UserId!.Value;
            var companyId = deviceInformation.CompanyId!.Value;
            var applicationId = existingRecord.ApplicationFk;
            var existingRecordBackup = existingRecord with { };
            var utcNow = DateTime.UtcNow;

            var deleted = await applicationsRepository.DeleteModuleByIdAsync(moduleId, userId, utcNow);
            if (!deleted)
            {
                return false;
            }

            await auditService.DeleteRowAuditAsync(companyId, userId, existingRecordBackup, moduleId.ToString(), deviceInformation, true);

            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.ModuleListByApplicationId + applicationId.ToString());

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteModuleByIdAsync));
            throw;
        }
    }


    public async Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        ApplicationDTO? savedRecord = null;
        try
        {
            newRecord.ApplicationClient = Guid.NewGuid();
            newRecord.ApplicationSecret = Guid.NewGuid();
            savedRecord = await applicationsRepository.CreateNewApplicationAsync(newRecord, deviceInformation); ;
            var savedAuditRecord = await auditService.NewRowAuditAsync(deviceInformation.CompanyId!.Value, deviceInformation.UserId!.Value, savedRecord!, savedRecord!.Id.ToString(), deviceInformation, true);
            OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_CreateNewApplication;
            var savedActivityLog = await auditService.AddNewActivityLogAsync(deviceInformation, actionStatus);
            return savedRecord;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewApplicationAsync));
            throw;
        }
    }

    public async Task<ApplicationDTO?> UpdateApplicationAsync(ApplicationDTO recordToUpdate, DeviceInformationDTO deviceInformation)
    {
        ApplicationDTO? savedRecord = null;
        try
        {
            var existingRecord = await GetApplicationByIdAsync(recordToUpdate.Id);
            if (existingRecord is null)
            {
                return null;
            }

            var userId = deviceInformation.UserId!.Value;
            var companyId = existingRecord.CompanyId;
            var originalRecord = existingRecord with { };
            var utcNow = DateTime.UtcNow;

            var recordPreparedForUpdate = existingRecord with
            {
                NameTranslationKey = recordToUpdate.NameTranslationKey,
                DescriptionTranslationKey = recordToUpdate.DescriptionTranslationKey,
                Avatar = recordToUpdate.Avatar,
                IsActive = recordToUpdate.IsActive,
                LastUpdateByUserFk = userId,
                LastUpdateDate = utcNow
            };

            savedRecord = await applicationsRepository.UpdateApplicationAsync(recordPreparedForUpdate);
            if (savedRecord is null)
            {
                return null;
            }

            await auditService.UpdateRowAuditAsync(companyId, userId, originalRecord, savedRecord, savedRecord.Id.ToString(), deviceInformation, true);

            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.AllApplicationList.ToString());
            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.ApplicationListByCompanyId + companyId.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateApplicationAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteApplicationAsync(long applicationId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var existingRecord = await GetApplicationByIdAsync(applicationId);
            if (existingRecord is null)
            {
                return false;
            }

            var userId = deviceInformation.UserId!.Value;
            var companyId = existingRecord.CompanyId;
            var existingRecordBackup = existingRecord with { };
            var utcNow = DateTime.UtcNow;

            var deleted = await applicationsRepository.DeleteApplicationAsync(applicationId, userId, utcNow);
            if (!deleted)
            {
                return false;
            }

            await auditService.DeleteRowAuditAsync(companyId, userId, existingRecordBackup, applicationId.ToString(), deviceInformation, true);

            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.AllApplicationList.ToString());
            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.ApplicationListByCompanyId + companyId.ToString());

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteApplicationAsync));
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
                returnList = await applicationsRepository.GetEndpointsListAsync(x => x.ModuleFk == moduleId);
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
                returnList = await applicationsRepository.GetEndpointsListByRoleIdAsync(roleId);
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
                returnList = await applicationsRepository.GetComponentListAsync(x => x.EndpointFk == endpointId);
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


    public async Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation)
    {
        EndpointDTO? endpointInfo = null!;
        try
        {
            var roleId = deviceInformation.RoleId;
            if (roleId is not null)
            {
                var endpointList = await GetEndpointsListByRoleIdAsync(roleId.Value);
                endpointInfo = endpointList.FirstOrDefault(x => x.EndpointUrl.Equals(deviceInformation.EndPointUrl, StringComparison.InvariantCultureIgnoreCase) && x.Method.Equals(deviceInformation.Method, StringComparison.InvariantCultureIgnoreCase) && x.IsActive);
                var actionType = endpointInfo is null ? OAuthActionTypeEnum.OAuthActionType_ApiRequestUnauthorized : OAuthActionTypeEnum.OAuthActionType_ApiRequestSuccessfully;
                var savedActivityLog = await auditService.AddNewActivityLogAsync(deviceInformation, actionType);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(ValidateEndpointAuthorizationAsync));
            throw;
        }
        return endpointInfo!;
    }

    public async Task<EndpointDTO?> GetEndpointByIdAsync(long endpointId)
    {
        try
        {
            var returnList = await applicationsRepository.GetEndpointsListAsync(x => x.Id == endpointId);
            var returnValue = returnList.FirstOrDefault();
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEndpointByIdAsync));
            throw;
        }
    }

    public async Task<EndpointDTO?> CreateNewEndpointAsync(EndpointDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        EndpointDTO? savedRecord = null;
        try
        {
            savedRecord = await applicationsRepository.CreateNewEndpointAsync(newRecord, deviceInformation);
            var savedAuditRecord = await auditService.NewRowAuditAsync(deviceInformation.CompanyId!.Value, deviceInformation.UserId!.Value, savedRecord!, savedRecord!.Id.ToString(), deviceInformation, true);
            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.EndpointListByModuleId + newRecord.ModuleFk.ToString());
            return savedRecord;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewEndpointAsync));
            throw;
        }
    }

    public async Task<EndpointDTO?> UpdateEndpointByIdAsync(long endpointId, EndpointDTO endpointInfo, DeviceInformationDTO deviceInformation)
    {
        EndpointDTO? savedRecord = null;
        try
        {
            var existingRecord = await GetEndpointByIdAsync(endpointId);
            if (existingRecord is null)
            {
                return null;
            }

            var userId = deviceInformation.UserId!.Value;
            var moduleId = existingRecord.ModuleFk;
            var originalRecord = existingRecord with { };
            var utcNow = DateTime.UtcNow;

            var recordPreparedForUpdate = existingRecord with
            {
                NameTranslationKey = endpointInfo.NameTranslationKey,
                DescriptionTranslationKey = endpointInfo.DescriptionTranslationKey,
                PositionIndex = endpointInfo.PositionIndex,
                IsActive = endpointInfo.IsActive,
                IsVisible = endpointInfo.IsVisible,
                IsApi = endpointInfo.IsApi,
                EndpointUrl = endpointInfo.EndpointUrl,
                Method = endpointInfo.Method,
            };

            savedRecord = await applicationsRepository.UpdateEndpointByIdAsync(recordPreparedForUpdate);
            if (savedRecord is null)
            {
                return null;
            }

            await auditService.UpdateRowAuditAsync(deviceInformation.CompanyId!.Value, userId, originalRecord, savedRecord, savedRecord.Id.ToString(), deviceInformation, true);

            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.EndpointListByModuleId + moduleId.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateEndpointByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteEndpointByIdAsync(long endpointId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var existingRecord = await GetEndpointByIdAsync(endpointId);
            if (existingRecord is null)
            {
                return false;
            }

            var userId = deviceInformation.UserId!.Value;
            var companyId = deviceInformation.CompanyId!.Value;
            var moduleId = existingRecord.ModuleFk;
            var existingRecordBackup = existingRecord with { };
            var utcNow = DateTime.UtcNow;

            var deleted = await applicationsRepository.DeleteEndpointByIdAsync(endpointId, userId, utcNow);
            if (!deleted)
            {
                return false;
            }

            await auditService.DeleteRowAuditAsync(companyId, userId, existingRecordBackup, endpointId.ToString(), deviceInformation, true);

            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.EndpointListByModuleId + moduleId.ToString());

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteEndpointByIdAsync));
            throw;
        }
    }

    public async Task<ComponentDTO?> GetComponentByIdAsync(long componentId)
    {
        try
        {
            var returnList = await applicationsRepository.GetComponentListAsync(x => x.Id == componentId);
            var returnValue = returnList.FirstOrDefault();
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetComponentByIdAsync));
            throw;
        }
    }

    public async Task<ComponentDTO?> CreateNewComponentAsync(ComponentDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        ComponentDTO? savedRecord = null;
        try
        {
            savedRecord = await applicationsRepository.CreateNewComponentAsync(newRecord, deviceInformation);
            var savedAuditRecord = await auditService.NewRowAuditAsync(deviceInformation.CompanyId!.Value, deviceInformation.UserId!.Value, savedRecord!, savedRecord!.Id.ToString(), deviceInformation, true);
            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.ComponentListByEndpointId + newRecord.EndpointFk.ToString());
            return savedRecord;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewComponentAsync));
            throw;
        }
    }

    public async Task<ComponentDTO?> UpdateComponentByIdAsync(long componentId, ComponentDTO componentInfo, DeviceInformationDTO deviceInformation)
    {
        ComponentDTO? savedRecord = null;
        try
        {
            var existingRecord = await GetComponentByIdAsync(componentId);
            if (existingRecord is null)
            {
                return null;
            }

            var userId = deviceInformation.UserId!.Value;
            var endpointId = existingRecord.EndpointFk;
            var originalRecord = existingRecord with { };
            var utcNow = DateTime.UtcNow;

            var recordPreparedForUpdate = existingRecord with
            {
                NameTranslationKey = componentInfo.NameTranslationKey,
                DescriptionTranslationKey = componentInfo.DescriptionTranslationKey,
                ObjectId = componentInfo.ObjectId,
                ObjectName = componentInfo.ObjectName,
                ObjectPropertyName = componentInfo.ObjectPropertyName,
                DefaultPropertyValue = componentInfo.DefaultPropertyValue,
                PositionIndex = componentInfo.PositionIndex,
            };

            savedRecord = await applicationsRepository.UpdateComponentByIdAsync(recordPreparedForUpdate);
            if (savedRecord is null)
            {
                return null;
            }

            await auditService.UpdateRowAuditAsync(deviceInformation.CompanyId!.Value, userId, originalRecord, savedRecord, savedRecord.Id.ToString(), deviceInformation, true);

            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.ComponentListByEndpointId + endpointId.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateComponentByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteComponentByIdAsync(long componentId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var existingRecord = await GetComponentByIdAsync(componentId);
            if (existingRecord is null)
            {
                return false;
            }

            var userId = deviceInformation.UserId!.Value;
            var companyId = deviceInformation.CompanyId!.Value;
            var endpointId = existingRecord.EndpointFk;
            var existingRecordBackup = existingRecord with { };
            var utcNow = DateTime.UtcNow;

            var deleted = await applicationsRepository.DeleteComponentByIdAsync(componentId, userId, utcNow);
            if (!deleted)
            {
                return false;
            }

            await auditService.DeleteRowAuditAsync(companyId, userId, existingRecordBackup, componentId.ToString(), deviceInformation, true);

            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.ComponentListByEndpointId + endpointId.ToString());

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteComponentByIdAsync));
            throw;
        }
    }

    public async Task<List<ListItemDTO>> GetModuleListItemAsync(long applicationId)
    {
        var listItem = await GetModuleListAsync(applicationId);
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetEndpointsListItemAsync(long moduleId)
    {
        var listItem = await GetEndpointsListAsync(moduleId);
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetComponentListItemAsync(long endpointId)
    {
        var listItem = await GetComponentListAsync(endpointId);
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }
}
