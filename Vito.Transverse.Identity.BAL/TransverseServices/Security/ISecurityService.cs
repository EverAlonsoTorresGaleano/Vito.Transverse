using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Security;


public interface ISecurityService
{
    Task<TokenResponseDTO> CreateAuthenticationTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation);

    Task<ApplicationDTO> CreateNewApplicationAsync(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, long companyId, long userId);

    Task<CompanyDTO> CreateNewCompanyAsync(CompanyDTO companyInfo, DeviceInformationDTO deviceInformation, long userId);

    //Task<PersonDTO> CreateNewPerson(PersonDTO personInfo, DeviceInformationDTO deviceInformation);

    Task<UserDTO?> CreateNewUserAsync(UserDTO userInfo, long companyId, DeviceInformationDTO deviceInformation);

    Task<bool?> ChangeUserPasswordAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation);

    Task<bool?> UpdateCompanyApplicationsAsync(CompanyDTO companyInfo, List<ApplicationDTO> applicationInfoList, long userId, DeviceInformationDTO deviceInformation);

    Task<bool?> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation);

    Task<bool?> ActivateAccountAsync(string activationToken, DeviceInformationDTO deviceInformation);

    Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation);

    Task<EndpointDTO?> ValidateAuthorizationEndpointByRoleIdAsync(long roleId, string endpointUrl, string method);

    Task<bool?> AddNewActivityLogAsync( DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus);

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

    Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(long? companyId);
    Task<List<AuditRecordDTO>> GetAuditRecordsListAsync(long? companyId);

    Task<List<ActivityLogDTO>> GetActivityLogListAsync(long? companyId);

    Task<List<NotificationDTO>> GetNotificationsListAsync(long? companyId);
}