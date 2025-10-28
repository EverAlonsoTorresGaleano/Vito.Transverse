using Microsoft.Extensions.Logging;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using  Vito.Transverse.Identity.Application.TransverseServices.Audit;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Applications;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Applications;

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




}
