using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Security;

public interface ISecurityRepository
{
    Task<UserDTOToken?> TokenValidateAuthorizationCodeAsync(Guid companyClient, Guid companySecret, Guid applicationClient, Guid applicationSecret, string? scope, DeviceInformationDTO deviceInformation);


    Task<UserDTOToken?> TokenValidateClientCredentialsAsync(Guid companyClient, Guid companySecret, Guid applicationClient, Guid applicationSecret, string? userName, string? password, string? scope, DeviceInformationDTO deviceInformation);


    Task<bool> AddNewActivityLogAsync(long companyId, long? applicationId, long? userId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);


    Task<bool> UpdateLastUserAccessAsync(long id, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);


    Task<ApplicationDTO> CreateNewApplicationAsync(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, long companyId, long userId, DataBaseServiceContext? context = null);

    Task<CompanyDTO> CreateNewCompanyAsync(CompanyDTO companyInfo, DeviceInformationDTO deviceInformation, long userId, DataBaseServiceContext? context = null);

    //Task<PersonDTO> CreateNewPerson(PersonDTO personInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<UserDTO> CreateNewUserAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> ChangeUserPasswordAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> UpdateCompanyApplicationsAsync(CompanyDTO companyInfo, List<ApplicationDTO> applicationInfoList, long userId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> ActivateAccountAsync(long companyId, long userId, Guid activationId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null!);

    Task<List<ApplicationDTO>> GetAllApplicationListAsync(DataBaseServiceContext? context = null);

    Task<List<ApplicationDTO>> GetApplicationListAsync(long? companyId, DataBaseServiceContext? context = null);

    Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipAsync(long? companyId, DataBaseServiceContext? context = null);



    Task<List<CompanyDTO>> GetAllCompanyListAsync(DataBaseServiceContext? context = null);

    Task<List<RoleDTO>> GetRoleListAsync(long? companyId, DataBaseServiceContext? context = null);

    Task<RoleDTO> GetRolePermissionListAsync(long roleId, DataBaseServiceContext? context = null);

    Task<List<ModuleDTO>> GetModuleListAsync(long? applicationId, DataBaseServiceContext? context = null);

    Task<List<PageDTO>> GetPageListAsync(long moduleId, DataBaseServiceContext? context = null);

    Task<List<ComponentDTO>> GetComponentListAsync(long pageId, DataBaseServiceContext? context = null);

    Task<List<UserRoleDTO>> GetUserRolesListAsync(long userId, DataBaseServiceContext? context = null);

    Task<UserDTO> GetUserPermissionListAsync(long userId, DataBaseServiceContext? context = null);

    Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(long? companyId, DataBaseServiceContext? context = null);
    Task<List<AuditRecordDTO>> GetAuditRecordsListAsync(long? companyId, DataBaseServiceContext? context = null);

    Task<List<ActivityLogDTO>> GetActivityLogListAsync(long? companyId, DataBaseServiceContext? context = null);

    Task<List<NotificationDTO1>> GetNotificationsListAsync(long? companyId, DataBaseServiceContext? context = null);
}
