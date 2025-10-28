using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Applications;

public interface IApplicationsRepository
{

    Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId, DataBaseServiceContext? context = null);

    Task<List<ApplicationDTO>> GetAllApplicationListAsync(Expression<Func<Application, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ApplicationDTO>> GetApplicationListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null);


    Task<RoleDTO> GetRolePermissionListAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ModuleDTO>> GetModuleListAsync(Expression<Func<Module, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<EndpointDTO>> GetEndpointsListAsync(Expression<Func<Endpoint, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ComponentDTO>> GetComponentListAsync(Expression<Func<Component, bool>> filters, DataBaseServiceContext? context = null);

}
