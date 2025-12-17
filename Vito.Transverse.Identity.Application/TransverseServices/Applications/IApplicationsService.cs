using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Application.TransverseServices.Applications;

public interface IApplicationsService
{
    Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO newRecord, DeviceInformationDTO deviceInformation);

    Task<List<ApplicationDTO>> GetAllApplicationListAsync();

    Task<List<ApplicationDTO>> GetApplicationListAsync(long? companyId);
    Task<ApplicationDTO> GetApplicationByIdAsync(long applicationId);
    Task<ApplicationDTO?> UpdateApplicationAsync(ApplicationDTO recordToUpdate, DeviceInformationDTO deviceInformation);
    Task<bool> DeleteApplicationAsync(long applicationId, DeviceInformationDTO deviceInformation);

    Task<RoleDTO> GetRolePermissionListAsync(long roleId);
    Task<List<RoleDTO>> GetRoleListByApplicationIdAsync(long applicationId);

    Task<List<ModuleDTO>> GetModuleListAsync(long? applicationId);
    Task<ModuleDTO?> GetModuleByIdAsync(long moduleId);
    Task<ModuleDTO?> CreateNewModuleAsync(ModuleDTO newRecord, DeviceInformationDTO deviceInformation);
    Task<ModuleDTO?> UpdateModuleByIdAsync(long moduleId, ModuleDTO moduleInfo, DeviceInformationDTO deviceInformation);
    Task<bool> DeleteModuleByIdAsync(long moduleId, DeviceInformationDTO deviceInformation);

    Task<List<EndpointDTO>> GetEndpointsListAsync(long moduleId);

    Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId);

    Task<List<ComponentDTO>> GetComponentListAsync(long endpointId);

    Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation);

    Task<EndpointDTO?> GetEndpointByIdAsync(long endpointId);
    Task<EndpointDTO?> CreateNewEndpointAsync(EndpointDTO newRecord, DeviceInformationDTO deviceInformation);
    Task<EndpointDTO?> UpdateEndpointByIdAsync(long endpointId, EndpointDTO endpointInfo, DeviceInformationDTO deviceInformation);
    Task<bool> DeleteEndpointByIdAsync(long endpointId, DeviceInformationDTO deviceInformation);

    Task<ComponentDTO?> GetComponentByIdAsync(long componentId);
    Task<ComponentDTO?> CreateNewComponentAsync(ComponentDTO newRecord, DeviceInformationDTO deviceInformation);
    Task<ComponentDTO?> UpdateComponentByIdAsync(long componentId, ComponentDTO componentInfo, DeviceInformationDTO deviceInformation);
    Task<bool> DeleteComponentByIdAsync(long componentId, DeviceInformationDTO deviceInformation);
    Task<List<ListItemDTO>> GetAllApplicationListItemAsync(long? companyId, bool useGuid = false);
    Task<List<ListItemDTO>> GetModuleListItemAsync(long value);
    Task<List<ListItemDTO>> GetEndpointsListItemAsync(long moduleId);
    Task<List<ListItemDTO>> GetComponentListItemAsync(long endpointId);
    Task<List<ListItemDTO>> GetAllLicenseTypesListItemAsync();
}
