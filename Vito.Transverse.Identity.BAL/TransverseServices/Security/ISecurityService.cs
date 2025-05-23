using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Security;


public interface ISecurityService
{
    Task<TokenResponseDTO> NewJwtTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation);

    Task<ApplicationDTO> CreateNewApplicationAsync(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, long companyId, long userId);

    Task<CompanyApplicationsDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation);

    Task<CompanyApplicationsDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation);

    Task<UserDTO?> CreateNewUserAsync(long companyId, UserDTO userInfo, DeviceInformationDTO deviceInformation);

    Task<bool?> ChangeUserPasswordAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation);


    Task<bool?> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation);

    Task<bool?> ActivateAccountAsync(string activationToken, DeviceInformationDTO deviceInformation);

    Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation);

    Task<bool?> AddNewActivityLogAsync(DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus);

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


}