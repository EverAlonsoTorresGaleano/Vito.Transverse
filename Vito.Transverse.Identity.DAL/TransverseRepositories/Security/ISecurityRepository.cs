using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Security;

/// <summary>
/// Handle of the security Database Repositories
/// </summary>
public interface ISecurityRepository
{
    /// <summary>
    /// Get Token with GrantType=AuthorizationCode
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="companySecret"></param>
    /// <param name="applicationId"></param>
    /// <param name="applicationSecret"></param>
    /// <param name="deviceInformation"></param>
    /// <returns></returns>
    Task<UserDTO?> TokenValidateAuthorizationCode(Guid companyId, Guid companySecret, Guid applicationId, Guid applicationSecret, DeviceInformationDTO deviceInformation);

    /// <summary>
    /// Get Token with GrantType=ClientCredentials

    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="companySecret"></param>
    /// <param name="applicationId"></param>
    /// <param name="applicationSecret"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="deviceInformation"></param>
    /// <returns></returns>
    Task<UserDTO?> TokenValidateClientCredentials(Guid companyId, Guid companySecret, Guid applicationId, Guid applicationSecret, string? userName, string? password, DeviceInformationDTO deviceInformation);

    /// <summary>
    /// Add new Record for Access Action Logging
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="applicationId"></param>
    /// <param name="userId"></param>
    /// <param name="deviceInformation"></param>
    /// <param name="actionStatus"></param>
    /// <returns></returns>
    Task<bool> AddNewActivityLog(Guid companyId, Guid? applicationId, long? userId, DeviceInformationDTO deviceInformation, ActionTypeEnum actionStatus, DataBaseServiceContext? context = null);

    /// <summary>
    /// Update user logging information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="deviceInformation"></param>
    /// <param name="actionStatus"></param>
    /// <returns></returns>
    Task<bool> UpdateLastUserAccess(long id, DeviceInformationDTO deviceInformation, ActionTypeEnum actionStatus, DataBaseServiceContext? context = null);


    Task<ApplicationDTO> CreateNewApplication(ApplicationDTO applicationInfo, DeviceInformationDTO deviceInformation, Guid companyId, long userId, DataBaseServiceContext? context = null);

    /// <summary>
    /// Create a new company and asociate a new person and new api-user to enable acces
    /// </summary>
    /// <param name="companyInfo"></param>
    /// <returns></returns>
    Task<CompanyDTO> CreateNewCompany(CompanyDTO companyInfo, DeviceInformationDTO deviceInformation, long userId, DataBaseServiceContext? context = null);

    Task<PersonDTO> CreateNewPerson(PersonDTO personInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<UserDTO> CreateNewUser(UserDTO userInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> ChangeUserPassword(UserDTO userInfo, DeviceInformationDTO deviceInformation);

    Task<bool> UpdateCompanyApplications(CompanyDTO companyInfo, List<ApplicationDTO> applicationInfoList, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> SendActivationEmail(UserDTO userInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> ActivateAccount(Guid companyId, long userId, Guid activationId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<List<ApplicationDTO>> GetAllApplicationList();

    Task<List<CompanyDTO>> GetAllCompanyList();
}
