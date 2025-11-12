using System;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Applications;

public interface IApplicationsRepository
{

    Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<ApplicationDTO?> UpdateApplicationAsync(ApplicationDTO applicationInfoDTO, DataBaseServiceContext? context = null);

    Task<bool> DeleteApplicationAsync(long applicationId, long updatedByUserId, DateTime actionDate, DataBaseServiceContext? context = null);

    Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId, DataBaseServiceContext? context = null);

    Task<List<ApplicationDTO>> GetAllApplicationListAsync(Expression<Func<Application, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ApplicationDTO>> GetApplicationListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null);


    Task<RoleDTO> GetRolePermissionListAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null);
    Task<List<RoleDTO>> GetRoleListByApplicationIdAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ModuleDTO>> GetModuleListAsync(Expression<Func<Module, bool>> filters, DataBaseServiceContext? context = null);
    Task<ModuleDTO?> CreateNewModuleAsync(ModuleDTO moduleInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);
    Task<ModuleDTO?> UpdateModuleByIdAsync(ModuleDTO moduleInfo, DataBaseServiceContext? context = null);
    Task<bool> DeleteModuleByIdAsync(long moduleId, long updatedByUserId, DateTime actionDate, DataBaseServiceContext? context = null);

    Task<List<EndpointDTO>> GetEndpointsListAsync(Expression<Func<Endpoint, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ComponentDTO>> GetComponentListAsync(Expression<Func<Component, bool>> filters, DataBaseServiceContext? context = null);

    Task<EndpointDTO?> CreateNewEndpointAsync(EndpointDTO endpointInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);
    Task<EndpointDTO?> UpdateEndpointByIdAsync(EndpointDTO endpointInfo, DataBaseServiceContext? context = null);
    Task<bool> DeleteEndpointByIdAsync(long endpointId, long updatedByUserId, DateTime actionDate, DataBaseServiceContext? context = null);

    Task<ComponentDTO?> CreateNewComponentAsync(ComponentDTO componentInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);
    Task<ComponentDTO?> UpdateComponentByIdAsync(ComponentDTO componentInfo, DataBaseServiceContext? context = null);
    Task<bool> DeleteComponentByIdAsync(long componentId, long updatedByUserId, DateTime actionDate, DataBaseServiceContext? context = null);
}
