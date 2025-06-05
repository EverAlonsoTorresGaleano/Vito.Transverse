using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Security;


public interface ISecurityService
{
    Task<TokenResponseDTO> NewJwtTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation);

    Task<ActivityLogDTO?> AddNewActivityLogAsync(long? companyId, long? applicationId, long? userId, long? roleId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);

    Task<ActivityLogDTO?> AddNewActivityLogAsync(DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);

    Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO newRecord, DeviceInformationDTO deviceInformation);

    Task<CompanyDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO newRecord, DeviceInformationDTO deviceInformation);

    Task<CompanyDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation);

    Task<UserDTO?> CreateNewUserAsync(long companyId, UserDTO newRecord, DeviceInformationDTO deviceInformation);

    Task<UserDTO?> ChangeUserPasswordAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation);

    Task<UserDTO?> UpdateLastUserAccessAsync(long userId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);

    Task<UserDTO?> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation);

    Task<bool?> ActivateAccountAsync(string activationToken, DeviceInformationDTO deviceInformation);

    Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation);



    Task<List<ApplicationDTO>> GetAllApplicationListAsync();

    Task<List<ApplicationDTO>> GetApplicationListAsync(long? companyId);

    Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipAsync(long? companyId);

    Task<List<CompanyDTO>> GetAllCompanyListAsync();

    Task<List<RoleDTO>> GetRoleListAsync(long? companyId);

    Task<RoleDTO> GetRolePermissionListAsync(long roleId);

    Task<List<ModuleDTO>> GetModuleListAsync(long? applicationId);

    Task<List<EndpointDTO>> GetEndpointsListAsync(long moduleId);

    Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId);

    Task<List<ComponentDTO>> GetComponentListAsync(long endpointId);

    Task<List<UserRoleDTO>> GetUserRolesListAsync(long userId);

    Task<UserDTO> GetUserPermissionListAsync(long userId);

    Task<List<UserDTO>> GetUserListAsync(long? companyId);
}