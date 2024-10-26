using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Security;

/// <see cref="ISecurityRepository"/>
public class SecurityRepository(IDataBaseContextFactory _dataBaseContextFactory, ICultureRepository _cultureRepository, IOptions<IdentityServiceServerSettingsOptions> _identityServiceOptions, ISocialNetworksRepository _socialNetworksRepository, ILogger<SecurityRepository> _logger) : ISecurityRepository
{
    private IdentityServiceServerSettingsOptions identityServiceOptionsValues = _identityServiceOptions.Value;

    #region Public Methods
    /// <see cref="ISecurityRepository.TokenValidateAuthorizationCode(Guid, Guid, Guid, Guid, DeviceInformationDTO)"/>
    public async Task<UserDTO?> TokenValidateAuthorizationCode(Guid companyId, Guid companySecret, Guid applicationId, Guid applicationSecret, DeviceInformationDTO deviceInformation)
    {
        UserDTO? userInfoDTO = default;
        userInfoDTO = await TokenValidateCompany(companyId, companySecret, applicationId, applicationSecret, null, null, deviceInformation);
        return userInfoDTO;
    }

    /// <see cref="ISecurityRepository.TokenValidateClientCredentials(Guid, Guid, Guid, Guid, string?, string?, DeviceInformationDTO)"/>
    public async Task<UserDTO?> TokenValidateClientCredentials(Guid companyId, Guid companySecret, Guid applicationId, Guid applicationSecret, string? userName, string? password, DeviceInformationDTO deviceInformation)
    {

        UserDTO? userInfoDTO = default;
        userInfoDTO = await TokenValidateCompany(companyId, companySecret, applicationId, applicationSecret, userName, password, deviceInformation);
        return userInfoDTO;
    }

    /// <see cref="ISecurityRepository.UpdateLastUserAccess(long, DeviceInformationDTO, ActionTypeEnum)"/>
    public async Task<bool> UpdateLastUserAccess(long id, DeviceInformationDTO deviceInformation, ActionTypeEnum actionStatus, DataBaseServiceContext? transactionContext = null)
    {
        var savedSuccesfuly = false;
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);

            var userInfo = await context.Users.FirstAsync(x => x.Id.Equals(id));
            userInfo.LastAccess = _cultureRepository.UtcNow().DateTime;
            if (actionStatus == ActionTypeEnum.ActionType_LoginFail_UserSecretInvalid)
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
            savedSuccesfuly = resultValue > 0;


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(UpdateLastUserAccess));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }
        return savedSuccesfuly;
    }

    /// <see cref="ISecurityRepository.AddNewActivityLog(Guid, Guid?, long?, DeviceInformationDTO, ActionTypeEnum)"/>
    public async Task<bool> AddNewActivityLog(Guid companyId, Guid? applicationId, long? userId, DeviceInformationDTO deviceInformation, ActionTypeEnum actionStatus, DataBaseServiceContext? transactionContext = null)
    {
        var savedSuccesfuly = false;
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);

            ActivityLog userTrace = new()
            {
                CompanyFk = companyId,
                ApplicationFk = applicationId!.Value,
                UserFk = userId!.Value,
                ActionTypeFk = (int)actionStatus,
                Browser = deviceInformation.Browser!,
                CultureId = deviceInformation.CultureId!,
                DeviceName = deviceInformation.Name!,
                DeviceType = deviceInformation.DeviceType!,
                Engine = deviceInformation.Engine!,
                EventDateTime = _cultureRepository.UtcNow().DateTime,
                IpAddress = deviceInformation.IpAddress!,
                Platform = deviceInformation.Platform!,
                AddtionalInformation = deviceInformation.AddtionalInfo!.Serialize(),
            };
            context.ActivityLogs.Add(userTrace);
            var resultValue = await context.SaveChangesAsync();
            savedSuccesfuly = resultValue > 0;


            var notificationInfo = new NotificationDTO()
            {
                Subject = "Vito Transverse",
                Message = "Email from Vito.Transverse",
                Receiver = ["eeatg844@hotmail.com"],
                NotificationTypeFk = (long)NotificationTypeEnum.Email

            };
            await _socialNetworksRepository.SendNotification(notificationInfo);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(AddNewActivityLog));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }
        return savedSuccesfuly;
    }

    public async Task<ApplicationDTO> CreateNewApplication(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, Guid companyId, long userId, DataBaseServiceContext? transactionContext = null)
    {
        bool savedSuccesfuly = false;
        var aplicationInfo = applicationInfoDTO.ToApplication();
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            aplicationInfo.Id = Guid.NewGuid();
            aplicationInfo.Secret = Guid.NewGuid();
            context.Applications.Add(aplicationInfo);
            await context.SaveChangesAsync();
            ActionTypeEnum actionStatus = ActionTypeEnum.ActionType_CreateNewApplication;
            savedSuccesfuly = await AddNewActivityLog(companyId, aplicationInfo.Id, userId, deviceInformation, actionStatus);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewApplication));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);

        }
        return aplicationInfo.ToApplicationDTO();
    }

    public async Task<CompanyDTO> CreateNewCompany(CompanyDTO companyInfoDTO, DeviceInformationDTO deviceInformation, long userId, DataBaseServiceContext? transactionContext = null)
    {
        bool savedSuccesfuly = false;
        var companyInfo = companyInfoDTO.ToCompany();
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);

            companyInfo.Id = Guid.NewGuid();
            companyInfo.Secret = Guid.NewGuid();
            context.Companies.Add(companyInfo);
            await context.SaveChangesAsync();
            ActionTypeEnum actionStatus = ActionTypeEnum.ActionType_CreateNewCompany;
            savedSuccesfuly = await AddNewActivityLog(companyInfo.Id, null, userId, deviceInformation, actionStatus);
            var newPersonDTO = new PersonDTO()
            {

            };
            newPersonDTO = await CreateNewPerson(newPersonDTO, deviceInformation);
            var newUserDTO = new UserDTO()
            {

            };
            newUserDTO = await CreateNewUser(newUserDTO, deviceInformation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewCompany));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);

        }
        return companyInfo.ToCompanyDTO();
    }



    public async Task<PersonDTO> CreateNewPerson(PersonDTO personInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        bool savedSuccesfuly = false;
        var personInfo = personInfoDTO.ToPerson();
        PersonDTO personInfoDTOReturn = new();
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            context.Persons.Add(personInfo);
            await context.SaveChangesAsync();
            personInfoDTOReturn = personInfo.ToPersonDTO();
            ActionTypeEnum actionStatus = ActionTypeEnum.ActionType_CreateNewPerson;
            savedSuccesfuly = await AddNewActivityLog(personInfoDTOReturn.CompanyFk!.Value, null, FrameworkConstants.UserId_UserUnknown, deviceInformation, actionStatus);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewPerson));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }
        return personInfoDTOReturn;
    }

    public async Task<UserDTO> CreateNewUser(UserDTO userInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        bool savedSuccesfuly = false;
        var userInfo = userInfoDTO.ToUser();
        UserDTO userDTOReturn = new();
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            context.Users.Add(userInfo);
            await context.SaveChangesAsync();
            userDTOReturn = userInfo.ToUserDTO();
            ActionTypeEnum actionStatus = ActionTypeEnum.ActionType_CreateNewUser;
            savedSuccesfuly = await AddNewActivityLog(userInfo.CompanyFk, null, userDTOReturn.Id, deviceInformation, actionStatus);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewUser));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }
        return userDTOReturn;
    }

    public async Task<List<ApplicationDTO>> GetAllApplicationList()
    {
        List<ApplicationDTO> applicationDTOList = new();
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var appicationList = await context.Applications.ToListAsync();
            applicationDTOList = appicationList.ToApplicationDTOList();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllApplicationList));
        }
        finally
        {
            context.Dispose();
        }
        return applicationDTOList;
    }

    public async Task<List<CompanyDTO>> GetAllCompanyList()
    {
        List<CompanyDTO> companyDTOList = new();
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var companyList = await context.Companies.ToListAsync();
            companyDTOList = companyList.ToCompanyDTOList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllCompanyList));
        }
        finally
        {
            context.Dispose();
        }
        return companyDTOList;
    }

    public async Task<bool> ChangeUserPassword(UserDTO userInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        bool savedSuccesfuly = false;
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            var userInfo = await context.Users.FirstOrDefaultAsync(u => u.Id == userInfoDTO.Id && u.CompanyFk.Equals(userInfoDTO.CompanyFk));
            if (userInfo is not null)
            {
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
                ActionTypeEnum actionStatus = ActionTypeEnum.ActionType_ChangeUserPassword;

                savedSuccesfuly = await AddNewActivityLog(userInfo.CompanyFk, null, userInfo.Id, deviceInformation, actionStatus);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ChangeUserPassword));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }
        return savedSuccesfuly;
    }

    public async Task<bool> UpdateCompanyApplications(CompanyDTO companyInfo, List<ApplicationDTO> applicationInfoList, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        bool savedSuccesfuly = false;
        DataBaseServiceContext? context = default;
        try
        {
            //TODO
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            await context.SaveChangesAsync();
            savedSuccesfuly = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(UpdateCompanyApplications));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }
        return savedSuccesfuly;
    }

    public async Task<bool> SendActivationEmail(UserDTO userInfoDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        bool savedSuccesfuly = false;
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            //TODO send email with teplate logic
            var userInfo = await context.Users.FirstOrDefaultAsync(u => u.Id == userInfoDTO.Id && u.CompanyFk.Equals(userInfoDTO.CompanyFk));
            if (userInfo is not null)
            {
                userInfo.IsActive = true;
                userInfo.EmailValidated = true;
                userInfo.IsLocked = false;
                userInfo.RequirePasswordChange = true;
                userInfo.RetryCount = 0;
                userInfo.LastAccess = _cultureRepository.UtcNow().DateTime;
                await context.SaveChangesAsync();
                ActionTypeEnum actionStatus = ActionTypeEnum.ActionType_SendActivationEmail;

                savedSuccesfuly = await AddNewActivityLog(userInfoDTO.CompanyFk, null, userInfoDTO.Id, deviceInformation, actionStatus);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(SendActivationEmail));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }
        return savedSuccesfuly;
    }

    public async Task<bool> ActivateAccount(Guid companyId, long userId, Guid activationId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        bool savedSuccesfuly = false;
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            var userInfo = await context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.CompanyFk.Equals(companyId) && u.ActivationId.Equals(activationId));
            if (userInfo is not null)
            {
                userInfo.IsActive = true;
                userInfo.EmailValidated = true;
                userInfo.IsLocked = false;
                userInfo.RequirePasswordChange = true;
                userInfo.RetryCount = 0;
                userInfo.LastAccess = _cultureRepository.UtcNow().DateTime;
                await context.SaveChangesAsync();
                ActionTypeEnum actionStatus = ActionTypeEnum.ActionType_ActivateUser;

                savedSuccesfuly = await AddNewActivityLog(companyId, null, userId, deviceInformation, actionStatus);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ActivateAccount));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }
        return savedSuccesfuly;
    }
    #endregion

    #region Private Methods
    private async Task<UserDTO?> TokenValidateCompany(Guid companyId, Guid companySecret, Guid applicationId, Guid? applicationSecret, string? userName, string? password, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        UserDTO? userInfoDTO = default;
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);

            ActionTypeEnum actionStatus = default;
            Company? companyInfo = await context.Companies.Include(x => x.ApplicationFks).FirstOrDefaultAsync(c => c.Id.Equals(companyId) && c.IsActive == true);


            if (companyInfo is null)
            {
                actionStatus = ActionTypeEnum.ActionType_LoginFail_CompanyNotFound;
            }
            else
            {
                var companyApplicationPermissions = companyInfo.ApplicationFks.FirstOrDefault(x => x.Id.Equals(applicationId));
                if (companyApplicationPermissions is null)
                {
                    actionStatus = ActionTypeEnum.ActionType_LoginFail_ApplicationNoFound;
                }
                else
                {
                    var isValid = companyInfo.Secret.Equals(companySecret);
                    if (!isValid)
                    {
                        actionStatus = ActionTypeEnum.ActionType_LoginFail_CompanySecretInvalid;
                    }
                    else
                    {
                        userInfoDTO = await TokenValidateApplication(companyId, companySecret, applicationId, applicationSecret, userName, password, deviceInformation, context);
                    }
                }
            }
            var actionsListToTrace = new List<ActionTypeEnum>
            {
                ActionTypeEnum.ActionType_LoginFail_CompanyNotFound,
                ActionTypeEnum.ActionType_LoginFail_CompanySecretInvalid,
                ActionTypeEnum.ActionType_LoginFail_ApplicationNoFound
            };
            if (actionsListToTrace.Contains(actionStatus))
            {
                var userTraceAddedSuccessfully = AddNewActivityLog(companyId, applicationId, FrameworkConstants.UserId_UserUnknown, deviceInformation, actionStatus, context);
                userInfoDTO = new() { ActionStatus = actionStatus };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(TokenValidateCompany));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }

        return userInfoDTO;
    }


    private async Task<UserDTO?> TokenValidateApplication(Guid companyId, Guid companySecret, Guid applicationId, Guid? applicationSecret, string? userName, string? password, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        UserDTO? userInfoDTO = default;
        ActionTypeEnum actionStatus = default;
        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            Application? applicationInfo = await context.Applications.FirstOrDefaultAsync(a => a.Id.Equals(applicationId) && a.IsActive == true);
            if (applicationInfo is null)
            {
                actionStatus = ActionTypeEnum.ActionType_LoginFail_ApplicationNoFound;
            }
            else
            {
                var isValid = !applicationSecret.HasValue ? true : applicationInfo.Secret.Equals(applicationSecret);
                if (!isValid)
                {
                    actionStatus = ActionTypeEnum.ActionType_LoginFail_ApplicationSecretInvalid;
                }
                else
                {
                    userInfoDTO = await TokenValidateUser(companyId, companySecret, applicationId, applicationSecret, !string.IsNullOrEmpty(userName) ? userName : FrameworkConstants.Username_UserApi, password, deviceInformation, context);
                }
            }
            var actionsListToTrace = new List<ActionTypeEnum>
        {
            ActionTypeEnum.ActionType_LoginFail_ApplicationNoFound,
            ActionTypeEnum.ActionType_LoginFail_ApplicationSecretInvalid,

        };

            if (actionsListToTrace.Contains(actionStatus))
            {
                var userTraceAddedSuccessfully = AddNewActivityLog(companyId, applicationId, FrameworkConstants.UserId_UserUnknown, deviceInformation, actionStatus, context);
                userInfoDTO = new() { ActionStatus = actionStatus };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(TokenValidateApplication));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }

        return userInfoDTO;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="companySecret"></param>
    /// <param name="applicationId"></param>
    /// <param name="applicationSecret"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="deviceInformation"></param>
    /// <returns></returns>
    private async Task<UserDTO?> TokenValidateUser(Guid companyId, Guid companySecret, Guid applicationId, Guid? applicationSecret, string? userName, string? password, DeviceInformationDTO deviceInformation, DataBaseServiceContext? transactionContext = null)
    {
        UserDTO? userInfoDTO = default;
        ActionTypeEnum actionStatus = default;

        DataBaseServiceContext? context = default;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(transactionContext);
            User? userInfo = await context.Users.Include(x => x.PersonFkNavigation).Include(x => x.RoleFkNavigation).Include(x => x.CompanyFkNavigation)
             .FirstOrDefaultAsync(u => u.CompanyFk.Equals(companyId)
                 && u.UserName.Equals(userName)
                 && u.IsActive == true
                 && u.IsLocked == false);

            if (userInfo is null)
            {
                //User do no exist
                actionStatus = ActionTypeEnum.ActionType_LoginFail_UserNotFound;
            }
            else
            {
                var isValid = password is null ? true : userInfo.Password.Equals(password);
                if (!isValid)
                {
                    //psw invalid
                    actionStatus = ActionTypeEnum.ActionType_LoginFail_UserSecretInvalid;
                }
                else
                {
                    //user valid
                    actionStatus = userInfo.UserName.Equals(FrameworkConstants.Username_UserApi) ? ActionTypeEnum.ActionType_LoginSuccessByAuthorizationCode : ActionTypeEnum.ActionType_LoginSuccessByClientCredentials;
                    Application applicationInfo = userInfo!.CompanyFkNavigation.ApplicationFks.First(x => x.Id.Equals(applicationId));
                    userInfoDTO = userInfo.ToUserDTO(applicationInfo.Id, applicationInfo.Name, actionStatus);
                }
                var userUpdatedSuccessfully = await UpdateLastUserAccess(userInfo.Id, deviceInformation, actionStatus, context);
            }
            var userInfoDToId = userInfo is not null ? userInfo.Id : FrameworkConstants.UserId_UserUnknown;
            var userTraceAddedSuccessfully = AddNewActivityLog(companyId, applicationId, userInfoDToId, deviceInformation, actionStatus, context);
            var actionsListToTrace = new List<ActionTypeEnum>
        {
            ActionTypeEnum.ActionType_LoginFail_UserNotFound,
            ActionTypeEnum.ActionType_LoginFail_UserSecretInvalid,
        };

            if (actionsListToTrace.Contains(actionStatus))
            {
                userInfoDTO = new() { ActionStatus = actionStatus };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(TokenValidateUser));
        }
        finally
        {
            _dataBaseContextFactory.DisposeDbContext(context, transactionContext);
        }

        return userInfoDTO;
    }
    #endregion
}