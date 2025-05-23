using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Security;

public interface ISecurityRepository
{
    Task<UserDTOToken?> TokenValidateAuthorizationCodeAsync(Guid companyClient, Guid companySecret, Guid applicationClient, Guid applicationSecret, string? scope, DeviceInformationDTO deviceInformation);


    Task<UserDTOToken?> TokenValidateClientCredentialsAsync(Guid companyClient, Guid companySecret, Guid applicationClient, Guid applicationSecret, string? userName, string? password, string? scope, DeviceInformationDTO deviceInformation);





    Task<bool> AddNewActivityLogAsync(long companyId, long? applicationId, long? userId,long? roleId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);

    
    Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, long companyId, long userId, DataBaseServiceContext? context = null);


    Task<CompanyApplicationsDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<CompanyApplicationsDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);


    Task<UserDTO> CreateNewUserAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> ChangeUserPasswordAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> UpdateLastUserAccessAsync(long id, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);

    Task<bool> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> ActivateAccountAsync(Guid companyClientId, long userId, Guid activationId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null!);



    //TODO
    Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId, DataBaseServiceContext? context = null);

    Task<List<ApplicationDTO>> GetAllApplicationListAsync(Expression<Func<Application, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ApplicationDTO>> GetApplicationListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<CompanyDTO>> GetAllCompanyListAsync(Expression<Func<Company, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<RoleDTO>> GetRoleListAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null);

    Task<RoleDTO> GetRolePermissionListAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ModuleDTO>> GetModuleListAsync(Expression<Func<Module, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<EndpointDTO>> GetEndpointsListAsync(Expression<Func<Endpoint, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ComponentDTO>> GetComponentListAsync(Expression<Func<Component, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<UserRoleDTO>> GetUserRolesListAsync(Expression<Func<UserRole, bool>> filters, DataBaseServiceContext? context = null);

    Task<UserDTO> GetUserPermissionListAsync(Expression<Func<User, bool>> filters, DataBaseServiceContext? context = null);


}
