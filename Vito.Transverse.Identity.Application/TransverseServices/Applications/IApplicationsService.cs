using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Applications;

public interface  IApplicationsService
{
    Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO newRecord, DeviceInformationDTO deviceInformation);

    Task<List<ApplicationDTO>> GetAllApplicationListAsync();

    Task<List<ApplicationDTO>> GetApplicationListAsync(long? companyId);
    Task<ApplicationDTO> GetApplicationByIdAsync(long applicationId);



    Task<RoleDTO> GetRolePermissionListAsync(long roleId);

    Task<List<ModuleDTO>> GetModuleListAsync(long? applicationId);

    Task<List<EndpointDTO>> GetEndpointsListAsync(long moduleId);

    Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId);

    Task<List<ComponentDTO>> GetComponentListAsync(long endpointId);

    Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation);


}
