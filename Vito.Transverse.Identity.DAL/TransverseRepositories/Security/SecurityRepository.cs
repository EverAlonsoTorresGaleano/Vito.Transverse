using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;
using Vito.Transverse.Identity.DAL.TransverseServices.Audit;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;
using Vito.Transverse.Identity.Domain.Options;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Security;

public class SecurityRepository(IDataBaseContextFactory _dataBaseContextFactory, ICultureRepository _cultureRepository, ISocialNetworksRepository _socialNetworksRepository, IAuditRepository _auditRepository, IOptions<IdentityServiceServerSettingsOptions> _identityServiceOptions, IOptions<EncryptionSettingsOptions> _encryptionSettingsOptions, ILogger<SecurityRepository> _logger) : ISecurityRepository
{
    private IdentityServiceServerSettingsOptions identityServiceOptionsValues = _identityServiceOptions.Value;
    private EncryptionSettingsOptions _encryptionSettingsOptions = _encryptionSettingsOptions.Value;

    #region Public Methods
    public async Task<UserDTOToken?> TokenValidateAuthorizationCodeAsync(Guid companyClient, Guid companySecret, Guid applicationClient, Guid applicationSecret, string? scope, DeviceInformationDTO deviceInformation)
    {
        UserDTOToken? userInfoDTO = default;
        DataBaseServiceContext? context = default;

        context = _dataBaseContextFactory.GetDbContext();
        //To retry if fail
        //var executionStrategy = context.Database.CreateExecutionStrategy();
        //await executionStrategy.ExecuteAsync(async () =>
        //{
        //    using var transactionScope = context.Database.BeginTransactionAsync();
        try
        {

            userInfoDTO = await TokenValidateCompany(companyClient, companySecret, applicationClient, applicationSecret, null, null, scope, deviceInformation, context);
            //while (transactionScope.Result.GetDbTransaction().Connection is null)
            //{
            //    transactionScope.Wait();
            //}
            //await transactionScope.Result.CommitAsync();

        }
        catch (Exception ex)
        {
            //transactionScope.Result.Rollback();
            _logger.LogError(ex, message: nameof(UpdateLastUserAccessAsync));
        }
        // });

        return userInfoDTO;
    }

    public async Task<UserDTOToken?> TokenValidateClientCredentialsAsync(Guid companyClient, Guid companySecret, Guid applicationClient, Guid applicationSecret, string? userName, string? password, string? scope, DeviceInformationDTO deviceInformation)
    {
        UserDTOToken? userInfoDTO = default;
        DataBaseServiceContext? context = default;

        context = _dataBaseContextFactory.GetDbContext();

        // var transactionScope = await context.Database.BeginTransactionAsync();
        //To retry if fail
        //var executionStrategy = context.Database.CreateExecutionStrategy();
        //await executionStrategy.Execute(async () =>
        //{
        //    using var transactionScope = context.Database.BeginTransactionAsync();
        try
        {
            //transactionScope.Start();
            userInfoDTO = await TokenValidateCompany(companyClient, companySecret, applicationClient, applicationSecret, userName, password, scope, deviceInformation, context);
            //    userInfoDTO1.ConfigureAwait(true);
            //    while (userInfoDTO1.Status != TaskStatus.RanToCompletion)
            //    {
            //        userInfoDTO1.Wait();
            //   }

            //var x = transactionScope.GetDbTransaction();
            //while (transactionScope.Status!= TaskStatus.RanToCompletion)
            //{
            //    transactionScope.ConfigureAwait(true);
            //    //x = transactionScope.GetDbTransaction();
            //}
            //while (transactionScope.ConfigureAwait(true);
            //.Status != TaskStatus.RanToCompletion)
            //    {
            //        transactionScope.Wait();
            //    }
            //transactionScope.CommitAsync();

        }
        catch (Exception ex)
        {
            //transactionScope.RollbackAsync();
            _logger.LogError(ex, message: nameof(UpdateLastUserAccessAsync));
            throw;
        }
        // });

        return userInfoDTO;
    }

    public async Task<bool> UpdateLastUserAccessAsync(long id, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null)
    {
        var savedSuccesfuly = false;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);

            var userInfo = await context.Users.FirstAsync(x => x.Id.Equals(id));
            var userInfoBackup = userInfo.CloneEntity();
            //var userInfoBackup = context.Entry(userInfo).OriginalValues.ToObject() as User;

            userInfo.LastAccess = _cultureRepository.UtcNow().DateTime;
            if (actionStatus == OAuthActionTypeEnum.OAuthActionType_LoginFail_UserSecretInvalid)
            {
                var retryCount = userInfo.RetryCount;
                userInfo.RetryCount++;
                if (retryCount >= identityServiceOptionsValues.MaxUserFailRetrys)
                {
                    userInfo.IsLocked = true;
                    userInfo.LockedDate = _cultureRepository.UtcNow().DateTime;
                }
            }
            else
            {
                userInfo.RetryCount = 0;
            }
            var resultValue = await context.SaveChangesAsync();
            await _auditRepository.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoBackup, userInfo, userInfo.Id.ToString(), deviceInformation, context);


            savedSuccesfuly = resultValue > 0;


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(UpdateLastUserAccessAsync));
            throw;
        }

        return savedSuccesfuly;
    }

    public async Task<bool> AddNewActivityLogAsync(long companyId, long? applicationId, long? userId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null)
    {
        var savedSuccesfuly = false;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            ActivityLog userTrace = new()
            {
                CompanyFk = companyId,
                UserFk = userId!.Value,
                ActionTypeFk = (int)actionStatus,
                Browser = deviceInformation.Browser!,
                CultureId = deviceInformation.CultureId!,
                DeviceName = deviceInformation.Name!,
                DeviceType = deviceInformation.DeviceType!,
                Engine = deviceInformation.Engine!,
                EventDate = _cultureRepository.UtcNow().DateTime,
                IpAddress = deviceInformation.IpAddress!,
                Platform = deviceInformation.Platform!,
                EndPointUrl = deviceInformation.EndPointUrl!,
                Method = deviceInformation.Method!,
                JwtToken = deviceInformation.JwtToken!,

                AddtionalInformation = deviceInformation.AddtionalInfo!.Serialize(),
            };
            context.ActivityLogs.Add(userTrace);
            var resultValue = await context.SaveChangesAsync();
            await _auditRepository.NewRowAuditAsync(companyId, userId ?? FrameworkConstants.UserId_UserUnknown, userTrace, userTrace.TraceId.ToString(), deviceInformation, context);
            savedSuccesfuly = resultValue > 0;

            //Send Activation Email
            //List<KeyValuePair<string, string>> emailTemplateParams = new()
            //    {
            //         new KeyValuePair<string, string>("EMAIL","email@email.com"),
            //         new KeyValuePair<string, string>("ACTIVATION_LINK","www.casasdemiciudad.com/activateuser?activationId=EB2D4FFC-DC34-435F-8983-ECD42481143F"),
            //    };
            //await _socialNetworksRepository.SendNotificationByTemplate(companyId, NotificationTypeEnum.Email, (int)NotificationTemplatesEnum.ActivationEmail, emailTemplateParams, ["eeatg844@hotmail.com"], null, null, _cultureRepository.GetCurrentCultureId(), context);


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(AddNewActivityLogAsync));
            throw;
        }

        return savedSuccesfuly;
    }

    public async Task<ApplicationDTO> CreateNewApplicationAsync(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, long companyId, long userId, DataBaseServiceContext? context = null)
    {
        bool savedSuccesfuly = false;
        var aplicationInfo = applicationInfoDTO.ToApplication();

        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            aplicationInfo.ApplicationClient = Guid.NewGuid();
            aplicationInfo.ApplicationSecret = Guid.NewGuid();
            context.Applications.Add(aplicationInfo);
            await context.SaveChangesAsync();
            await _auditRepository.NewRowAuditAsync(companyId, userId, aplicationInfo, aplicationInfo.Id.ToString(), deviceInformation, context);
            OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_CreateNewApplication;
            savedSuccesfuly = await AddNewActivityLogAsync(companyId, aplicationInfo.Id, userId, deviceInformation, actionStatus);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewApplicationAsync));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context);

        }
        return aplicationInfo.ToApplicationDTO();
    }

    public async Task<CompanyDTO> CreateNewCompanyAsync(CompanyDTO companyInfoDTO, DeviceInformationDTO deviceInformation, long userId, DataBaseServiceContext? context = null)
    {
        bool savedSuccesfuly = false;
        var companyInfo = companyInfoDTO.ToCompany();

        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);

            companyInfo.CompanyClient = Guid.NewGuid();
            companyInfo.CompanySecret = Guid.NewGuid();
            context.Companies.Add(companyInfo);
            await context.SaveChangesAsync();
            await _auditRepository.NewRowAuditAsync(companyInfo.Id, userId, companyInfo, companyInfo.Id.ToString(), deviceInformation, context);
            OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_CreateNewCompany;
            savedSuccesfuly = await AddNewActivityLogAsync(companyInfo.Id, null, userId, deviceInformation, actionStatus);
            var newPersonDTO = new PersonDTO()
            {
                //TODO
            };
            //newPersonDTO = await CreateNewPerson(newPersonDTO, deviceInformation);
            var newUserDTO = new UserDTO()
            {
                //TODO

            };
            newUserDTO = await CreateNewUserAsync(newUserDTO, deviceInformation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewCompanyAsync));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context);

        }
        return companyInfo.ToCompanyDTO();
    }

    //public async Task<PersonDTO> CreateNewPerson(PersonDTO personInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    //{
    //    bool savedSuccesfuly = false;
    //    var personInfo = personInfoDTO.ToPerson();
    //    PersonDTO personInfoDTOReturn = new();
    //    try
    //    {
    //        context = _dataBaseContextFactory.GetDbContext(context);
    //        context.Persons.Add(personInfo);
    //        await context.SaveChangesAsync();
    //await _auditRepository.NewRowAuditAsync(companyInfo.Id, userId, companyInfo, companyInfo.Id.ToString(), deviceInformation, context);
    //        personInfoDTOReturn = personInfo.ToPersonDTO();
    //        OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_CreateNewPerson;
    //        savedSuccesfuly = await AddNewActivityLog(personInfoDTOReturn.CompanyFk!.Value, null, FrameworkConstants.UserId_UserUnknown, deviceInformation, actionStatus);

    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, message: nameof(CreateNewPerson));
    //    }
    //    finally
    //    {
    //        _dataBaseContextFactory.DisposeDbContext(context);
    //    }
    //    return personInfoDTOReturn;
    //}

    public async Task<UserDTO> CreateNewUserAsync(UserDTO userInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool savedSuccesfuly = false;
        var userInfo = userInfoDTO.ToUser();
        UserDTOToken userDTOReturn = new();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            context.Users.Add(userInfo);
            await context.SaveChangesAsync();
            await _auditRepository.NewRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfo, userInfo.Id.ToString(), deviceInformation, context);
            userDTOReturn = userInfo.ToUserDTOToken();
            OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_CreateNewUser;
            savedSuccesfuly = await AddNewActivityLogAsync(userInfo.CompanyFkNavigation.Id, null, userDTOReturn.Id, deviceInformation, actionStatus);
            await SendActivationEmailAsync(userInfo.Id, userInfo.CompanyFk, deviceInformation, context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewUserAsync));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context);
        }
        return userDTOReturn;
    }

    public async Task<bool> ChangeUserPasswordAsync(UserDTO userInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool savedSuccesfuly = false;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var userInfo = await context.Users.FirstOrDefaultAsync(u => u.Id == userInfoDTO.Id && u.CompanyFk.Equals(userInfoDTO.CompanyFk));
            if (userInfo is not null)
            {
                //var userInfoBackup = (User)context.Entry(userInfo).OriginalValues.ToObject();
                var userInfoBackup = userInfo.CloneEntity();

                if (userInfo.Password.Equals(userInfoDTO.Password))
                {
                    throw new InvalidOperationException("");
                }

                userInfo.IsActive = true;
                userInfo.EmailValidated = true;
                userInfo.IsLocked = false;
                userInfo.RequirePasswordChange = true;
                userInfo.RetryCount = 0;
                userInfo.LastAccess = _cultureRepository.UtcNow().DateTime;
                await context.SaveChangesAsync();
                await _auditRepository.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoBackup, userInfo, userInfo.Id.ToString(), deviceInformation, context);

                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_ChangeUserPassword;

                savedSuccesfuly = await AddNewActivityLogAsync(userInfo.CompanyFkNavigation.Id, null, userInfo.Id, deviceInformation, actionStatus);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ChangeUserPasswordAsync));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context);
        }
        return savedSuccesfuly;
    }

    public async Task<bool> UpdateCompanyApplicationsAsync(CompanyDTO companyInfo, List<ApplicationDTO> applicationInfoList, long userId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool savedSuccesfuly = false;

        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var companyInfoDb = await context.Companies.FirstOrDefaultAsync(x => x.Id == companyInfo.Id);
            var companyInfoDbBackup = (Company)context.Entry(companyInfoDb!).OriginalValues.ToObject();
            //TODO
            await context.SaveChangesAsync();
            await _auditRepository.UpdateRowAuditAsync(companyInfo.Id, userId, companyInfoDbBackup, companyInfoDb!, companyInfo.Id.ToString(), deviceInformation, context);

            savedSuccesfuly = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(UpdateCompanyApplicationsAsync));
            throw;
        }

        return savedSuccesfuly;
    }

    public async Task<bool> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool emailSent = false;

        try
        {

            context = _dataBaseContextFactory.GetDbContext(context);
            //TODO send email with teplate logic
            var userInfo = await context.Users
                .Include(x => x.CompanyFkNavigation).FirstOrDefaultAsync(u => u.Id == userId && u.CompanyFk.Equals(companyId));
            if (userInfo is not null)
            {
                //var userInfoDTOBackup = (User)context.Entry(userInfo).OriginalValues.ToObject();
                var userInfoDTOBackup = userInfo.CloneEntity();
                // var companyId = userInfo.CompanyFkNavigation.Id;

                userInfo.IsActive = false;
                userInfo.EmailValidated = false;
                userInfo.IsLocked = false;
                userInfo.RequirePasswordChange = true;
                userInfo.RetryCount = 0;
                userInfo.LastAccess = _cultureRepository.UtcNow().DateTime;
                await context.SaveChangesAsync();
                await _auditRepository.UpdateRowAuditAsync(userInfo.CompanyFk, userInfo.Id, userInfoDTOBackup, userInfo, userInfo.Id.ToString(), deviceInformation, context);

                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_SendActivationEmail;

                emailSent = await AddNewActivityLogAsync(companyId, null, userInfo.Id, deviceInformation, actionStatus);

                //Send Activation Email
                List<KeyValuePair<string, string>> emailTemplateParams = new()
                {
                     new (EmailTemplateParametersEnum.EMAIL.ToString(),userInfo?.Email!),
                     new (EmailTemplateParametersEnum.USER_ID.ToString(),userInfo?.Id.ToString()!),
                     new (EmailTemplateParametersEnum.APPLICATION_CLIENTID.ToString(),userInfo?.CompanyFkNavigation.CompanyClient.ToString()!),
                     new (EmailTemplateParametersEnum.ACTIVATION_ID.ToString(),userInfo?.ActivationId.ToString()!),
                };
                emailSent = await _socialNetworksRepository.SendNotificationByTemplateAsync(companyId, NotificationTypeEnum.NotificationType_Email, (int)NotificationTemplatesEnum.ActivationEmail, emailTemplateParams, [userInfo?.Email], null, null, _cultureRepository.GetCurrentCultureId(), context);

            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(SendActivationEmailAsync));
        }

        return emailSent;
    }

    public async Task<bool> ActivateAccountAsync(Guid companyClientId, long userId, Guid activationId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null!)
    {
        bool savedSuccesfuly = false;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var userInfo = await context.Users
                .Include(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(u => u.Id == userId && u.CompanyFkNavigation.CompanyClient.Equals(companyClientId) && u.ActivationId.Equals(activationId));
            if (userInfo is not null)
            {
                //var userInfoDTOBackup = (User)context.Entry(userInfo).OriginalValues.ToObject();
                var userInfoDTOBackup = userInfo.CloneEntity();
                userInfo.IsActive = true;
                userInfo.EmailValidated = true;
                userInfo.IsLocked = false;
                userInfo.RequirePasswordChange = true;
                userInfo.RetryCount = 0;
                userInfo.LastAccess = _cultureRepository.UtcNow().DateTime;
                await context.SaveChangesAsync();
                await _auditRepository.UpdateRowAuditAsync(userInfo.CompanyFkNavigation.Id, userInfo.Id, userInfoDTOBackup, userInfo, userInfo.Id.ToString(), deviceInformation, context);

                OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_ActivateUser;

                savedSuccesfuly = await AddNewActivityLogAsync(userInfo.CompanyFkNavigation.Id, null, userId, deviceInformation, actionStatus);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ActivateAccountAsync));
        }
        return savedSuccesfuly;
    }







    public async Task<List<ApplicationDTO>> GetAllApplicationListAsync(DataBaseServiceContext? context = null)
    {
        List<ApplicationDTO> applicationDTOList = new();

        try
        {
            context = _dataBaseContextFactory.CreateDbContext();

            var appicationList = await context.Applications
                .Include(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation).ToListAsync();
            applicationDTOList = appicationList.ToApplicationDTOList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllApplicationListAsync));
        }

        return applicationDTOList;
    }


    public async Task<List<ApplicationDTO>> GetApplicationListAsync(long? companyId, DataBaseServiceContext? context = null)
    {
        List<ApplicationDTO> applicationDTOList = new();

        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var companyMembershipList = await context.CompanyMemberships
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(x => companyId == null || x.CompanyFk == companyId).ToListAsync();
            applicationDTOList = companyMembershipList.ToApplicationDTOList().OrderBy(x => x.CompanyId).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetApplicationListAsync));
        }

        return applicationDTOList;
    }

    public async Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipAsync(long? companyId, DataBaseServiceContext? context = null)
    {
        List<CompanyMembershipsDTO> applicationDTOList = new();

        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var companyMembershipsList = await context.CompanyMemberships
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.MembershipTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(x => companyId == null || x.CompanyFk == companyId).ToListAsync();
            applicationDTOList = companyMembershipsList.ToCompanyMembershipsDTOList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetApplicationListAsync));
        }

        return applicationDTOList;
    }

    public async Task<List<CompanyDTO>> GetAllCompanyListAsync(DataBaseServiceContext? context = null)
    {
        List<CompanyDTO> companyDTOList = new();

        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var companyList = await context.Companies

                .Include(x => x.CountryFkNavigation)
                .Include(x => x.DefaultCultureFkNavigation)
                .ThenInclude(x => x.LanguageFkNavigation)
                .ToListAsync();
            companyDTOList = companyList.ToCompanyDTOList().OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllCompanyListAsync));
        }

        return companyDTOList;
    }

    public async Task<List<RoleDTO>> GetRoleListAsync(long? companyId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<RoleDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Roles
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(x => companyId == null || x.CompanyFk == companyId).ToListAsync();
            returnList = bdList.ToRoleDTOList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetRoleListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<RoleDTO> GetRolePermissionListAsync(long roleId, DataBaseServiceContext? context = null)
    {
        var returnList = new RoleDTO();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Roles
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.RolePermissions)
                .ThenInclude(x => x.ModuleFkNavigation)
                .ThenInclude(x => x.Endpoints)
                .ThenInclude(x => x.Components).FirstOrDefaultAsync(x => x.Id == roleId);
            returnList = bdList.ToRoleDTO();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetRolePermissionListAsync));
            throw;
        }

        return returnList!;
    }

    public async Task<List<ModuleDTO>> GetModuleListAsync(long? applicationId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<ModuleDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Modules
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(x => applicationId == null || x.ApplicationFk == applicationId).ToListAsync();
            returnList = bdList.ToModuleDTOList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetModuleListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<EndpointDTO>> GetEndpointsListAsync(long moduleId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<EndpointDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Endpoints
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(x => x.ModuleFk == moduleId).ToListAsync();
            returnList = bdList.ToEndpointDTOList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetEndpointsListAsync));
            throw;
        }

        return returnList;
    }

    /// <summary>
    ///  Get Role permissions If have vaue on EndPointFk  check enpoint  if use wildcard  EndPointFk=null is like * that means have permission on all module end points
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<EndpointDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var endpointPermissionList = await context.RolePermissions
               .Include(x => x.ModuleFkNavigation)
               .ThenInclude(x => x.Endpoints)
               .ThenInclude(x => x.Components)
               .Where(r => r.RoleFk == roleId
               && r.RoleFkNavigation.IsActive == true
               && r.ModuleFkNavigation.IsActive == true
               && r.EndpointFkNavigation!.IsActive == true).Select(x => x.EndpointFkNavigation).ToListAsync();

            var endpointModulesWildCardPermissionList = await context.RolePermissions
               .Include(x => x.ModuleFkNavigation)
               .ThenInclude(x => x.Endpoints)
               .ThenInclude(x => x.Components)
               .Where(r => r.RoleFk == roleId
               && r.RoleFkNavigation.IsActive == true
               && r.ModuleFkNavigation.IsActive == true
               && r.EndpointFk == null).Select(x => x.ModuleFkNavigation.Endpoints).ToListAsync();

            endpointModulesWildCardPermissionList.ForEach(itemModule =>
            {
                endpointPermissionList.AddRange(itemModule.ToList());
            });
            endpointPermissionList = endpointPermissionList.Distinct().ToList();

            returnList = endpointPermissionList!.ToEndpointDTOList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetEndpointsListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<ComponentDTO>> GetComponentListAsync(long endpointId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<ComponentDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Components
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(x => x.EndpointFk == endpointId).ToListAsync();
            returnList = bdList.ToComponentDTOList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList(); ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetComponentListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<UserRoleDTO>> GetUserRolesListAsync(long userId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<UserRoleDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.UserRoles
                .Include(x => x.RoleFkNavigation.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.RoleFkNavigation.CompanyFkNavigation)
                .Where(x => x.UserFk == userId).ToListAsync();
            returnList = bdList.ToUserRoleDTOList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.RoleFk).ToList(); ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetUserRolesListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<UserDTO> GetUserPermissionListAsync(long userId, DataBaseServiceContext? context = null)
    {
        var returnValue = new UserDTO();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.RoleFkNavigation)
                    .ThenInclude(x => x.ApplicationFkNavigation)
                    .ThenInclude(x => x.ApplicationOwners)
                    .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.RoleFkNavigation)
                    .ThenInclude(x => x.RolePermissions)
                    .ThenInclude(x => x.ModuleFkNavigation)
                    .ThenInclude(x => x.Endpoints)
                    .ThenInclude(x => x.Components)
                .FirstOrDefaultAsync(x => x.Id == userId);
            returnValue = bdList!.ToUserDTO()!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetUserPermissionListAsync));
            throw;
        }

        return returnValue;
    }


    public async Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(long? companyId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<CompanyEntityAuditDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.CompanyEntityAudits
                .Include(x => x.AuditEntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(x => companyId == null || x.CompanyFk == companyId).ToListAsync();
            returnList = bdList.ToCompanyEntityAuditDTOList().OrderBy(x => x.CompanyFk).ThenBy(x => x.AuditEntityName).ThenBy(x => x.AuditTypeFk).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetCompanyEntityAuditsListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<AuditRecordDTO>> GetAuditRecordsListAsync(long? companyId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<AuditRecordDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.AuditRecords
                .Include(x => x.AuditEntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.UserFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(x => companyId == null || x.CompanyFk == companyId).ToListAsync();
            returnList = bdList.ToAuditRecordDTOList().OrderByDescending(x => x.CreationDate).ToList(); ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAuditRecordsListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<ActivityLogDTO>> GetActivityLogListAsync(long? companyId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<ActivityLogDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.ActivityLogs
                .Include(x => x.UserFkNavigation)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.ActionTypeFkNavigation)
                .Where(x => companyId == null || x.CompanyFk == companyId).ToListAsync();
            returnList = bdList.ToActivityLogDTOList().OrderByDescending(x => x.EventDate).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetActivityLogListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<NotificationDTO>> GetNotificationsListAsync(long? companyId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<NotificationDTO>();
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Notifications
                .Include(x => x.NotificationTemplate)
                .ThenInclude(x => x.CultureFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.NotificationTypeFkNavigation)
                .Where(x => companyId == null || x.CompanyFk == companyId).ToListAsync();
            returnList = bdList.ToNotificationDTOList().OrderByDescending(x => x.CreationDate).ToList(); ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetNotificationsListAsync));
            throw;
        }

        return returnList;
    }

    #endregion

    #region Private Methods
    private async Task<UserDTOToken?> TokenValidateCompany(Guid companyClient, Guid companySecret, Guid applicationClient, Guid? applicationSecret, string? userName, string? password, string? scope, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserDTOToken? userInfoDTO = default;
        var scopeRequest = !String.IsNullOrEmpty(scope) ? scope : deviceInformation.EndPointUrl;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);

            OAuthActionTypeEnum actionStatus = default;
            Company? companyInfo = await context.Companies.Include(x => x.CompanyMemberships).FirstOrDefaultAsync(c => c.CompanyClient.Equals(companyClient) && c.IsActive == true);


            if (companyInfo is null)
            {
                actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_CompanyNotFound;
            }
            else
            {
                var applicationInfo = context.Applications.FirstOrDefault(x => x.ApplicationClient.Equals(applicationClient) && x.IsActive == true);


                if (applicationInfo is null)
                {
                    actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_ApplicationNoFound;
                }
                else
                {
                    var companyApplicationMembership = context.CompanyMemberships.Include(x => x.CompanyMembershipPermissions).FirstOrDefault(x => x.ApplicationFk.Equals(applicationInfo.Id) && x.CompanyFk.Equals(companyInfo.Id));
                    if (companyApplicationMembership is null)
                    {
                        actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_CompanyMembershipDoesNotExist;
                    }
                    else
                    {

                        var companyApplicationPermissions = companyApplicationMembership.CompanyMembershipPermissions.ToList();
                        var isValid = companyInfo.CompanyClient.Equals(companyClient) && companyInfo.CompanySecret.Equals(companySecret);
                        if (!isValid)
                        {
                            actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_CompanySecretInvalid;
                        }
                        else
                        {
                            userInfoDTO = await TokenValidateApplication(companyInfo.Id, companySecret, applicationInfo.Id, applicationSecret, userName, password, scopeRequest, deviceInformation.Method, deviceInformation, context);
                        }
                    }
                }
            }
            var actionsListToTrace = new List<OAuthActionTypeEnum>
            {
                OAuthActionTypeEnum.OAuthActionType_LoginFail_CompanyMembershipDoesNotExist,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_CompanyNotFound,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_CompanySecretInvalid,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_ApplicationNoFound
            };
            if (actionsListToTrace.Contains(actionStatus))
            {
                var userTraceAddedSuccessfully = AddNewActivityLogAsync(companyInfo?.Id ?? FrameworkConstants.Company_DefaultId, null, FrameworkConstants.UserId_UserUnknown, deviceInformation, actionStatus, context);
                userInfoDTO = new() { ActionStatus = actionStatus };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(TokenValidateCompany));
            throw;
        }


        return userInfoDTO;
    }


    private async Task<UserDTOToken?> TokenValidateApplication(long companyId, Guid companySecret, long applicationId, Guid? applicationSecret, string? userName, string? password, string? scope, string? method, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserDTOToken? userInfoDTO = default;
        OAuthActionTypeEnum actionStatus = default;

        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            Application? applicationInfo = await context.Applications.FirstOrDefaultAsync(a => a.Id.Equals(applicationId) && a.IsActive == true);
            if (applicationInfo is null)
            {
                actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_ApplicationNoFound;
            }
            else
            {
                var isValid = !applicationSecret.HasValue ? true : applicationInfo.ApplicationSecret.Equals(applicationSecret);
                if (!isValid)
                {
                    actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_ApplicationSecretInvalid;
                }
                else
                {
                    userInfoDTO = await TokenValidateUser(companyId, companySecret, applicationId, applicationSecret, !string.IsNullOrEmpty(userName) ? userName : FrameworkConstants.Username_UserApi, password, scope, method, deviceInformation, context);
                }
            }
            var actionsListToTrace = new List<OAuthActionTypeEnum>
            {
                OAuthActionTypeEnum.OAuthActionType_LoginFail_ApplicationNoFound,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_ApplicationSecretInvalid,

            };

            if (actionsListToTrace.Contains(actionStatus))
            {
                var userTraceAddedSuccessfully = AddNewActivityLogAsync(companyId, applicationId, FrameworkConstants.UserId_UserUnknown, deviceInformation, actionStatus, context);
                userInfoDTO = new() { ActionStatus = actionStatus };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(TokenValidateApplication));
            throw;
        }


        return userInfoDTO;
    }


    private async Task<UserDTOToken?> TokenValidateUser(long companyId, Guid companySecret, long applicationId, Guid? applicationSecret, string? userName, string? password, string? scope, string? method, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserDTOToken? userInfoDTO = default;
        OAuthActionTypeEnum actionStatus = default;

        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            UserRole? userRoleInfo = await context.UserRoles
                .Include(x => x.RoleFkNavigation)
                .Include(x => x.RoleFkNavigation.RolePermissions)
                .Include(x => x.RoleFkNavigation.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.UserFkNavigation).FirstOrDefaultAsync(u => u.CompanyFk.Equals(companyId) && u.ApplicationFk.Equals(applicationId)
                 && u.UserFkNavigation.UserName.Equals(userName)
                 && u.IsActive == true
                 && u.UserFkNavigation.IsActive == true
                 && u.UserFkNavigation.IsLocked == false);

            if (userRoleInfo is null)
            {
                //User do no exist
                actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_UserNotFound;
            }
            else
            {
                var isValid = password is null ? true : userRoleInfo.UserFkNavigation.Password.Equals(password);
                if (!isValid)
                {
                    //psw invalid
                    actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_UserSecretInvalid;
                }
                else
                {
                    var endpointList = await GetEndpointsListByRoleIdAsync(userRoleInfo.RoleFk);
                    var endpointInfo = endpointList.FirstOrDefault(x => (x.EndpointUrl.Equals(scope) && x.Method.Equals(method)) && x.IsActive);

                    if (endpointInfo is null)
                    {
                        //Do nos have Acces to resource
                        actionStatus = OAuthActionTypeEnum.OAuthActionType_LoginFail_UserUnauthorized;
                    }
                    else
                    {
                        //user valid
                        actionStatus = userRoleInfo.UserFkNavigation.UserName.Equals(FrameworkConstants.Username_UserApi) ? OAuthActionTypeEnum.OAuthActionType_LoginSuccessByAuthorizationCode : OAuthActionTypeEnum.OAuthActionType_LoginSuccessByClientCredentials;
                        Application applicationInfo = userRoleInfo.RoleFkNavigation.ApplicationFkNavigation;
                        userInfoDTO = userRoleInfo.UserFkNavigation.ToUserDTOToken(applicationInfo, actionStatus);
                    }
                }
                var userUpdatedSuccessfully = await UpdateLastUserAccessAsync(userRoleInfo.UserFkNavigation.Id, deviceInformation, actionStatus, context);
            }
            var userInfoDToId = userRoleInfo?.UserFkNavigation is not null ? userRoleInfo?.UserFkNavigation.Id : FrameworkConstants.UserId_UserUnknown;
            var userTraceAddedSuccessfully = AddNewActivityLogAsync(companyId, applicationId, userInfoDToId, deviceInformation, actionStatus, context);
            var actionsListToTrace = new List<OAuthActionTypeEnum>
            {
                OAuthActionTypeEnum.OAuthActionType_LoginFail_UserNotFound,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_UserSecretInvalid,
                OAuthActionTypeEnum.OAuthActionType_LoginFail_UserUnauthorized
            };

            if (actionsListToTrace.Contains(actionStatus))
            {
                userInfoDTO = new() { ActionStatus = actionStatus };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(TokenValidateUser));
            throw;
        }


        return userInfoDTO;
    }




    #endregion
}