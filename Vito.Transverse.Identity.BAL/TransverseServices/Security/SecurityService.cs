using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.Helpers;
using Vito.Transverse.Identity.BAL.TransverseServices.Audit;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Security;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Security;


/// <see cref="ISecurityService"/>
public class SecurityService(ISecurityRepository _securityRepository, ICultureService _cultureService, IAuditService auditService, ICachingServiceMemoryCache _cachingService, IOptions<IdentityServiceServerSettingsOptions> _jwtIdentityServerOptions, ILogger<ISecurityService> _logger) : ISecurityService
{
    private readonly IdentityServiceServerSettingsOptions _jwtIdentityServerOptionsValues = _jwtIdentityServerOptions.Value;

    #region Public Methods
    public async Task<TokenResponseDTO> NewJwtTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation)
    {
        try
        {
            UserDTOToken? userInfoDTO = default;
            var grantType = Enum.Parse<TokenGrantTypeEnum>(requestBody.grant_type, true);
            TokenResponseDTO tokenResponse = default!;
            switch (grantType)
            {
                case TokenGrantTypeEnum.AuthorizationCode:
                    userInfoDTO = await _securityRepository.TokenValidateAuthorizationCodeAsync(Guid.Parse(requestBody.company_id), Guid.Parse(requestBody.company_secret), Guid.Parse(requestBody.application_id), Guid.Parse(requestBody.application_secret), requestBody.scope, deviceInformation);
                    break;
                case TokenGrantTypeEnum.ClientCredentials:
                    userInfoDTO = await _securityRepository.TokenValidateClientCredentialsAsync(Guid.Parse(requestBody.company_id), Guid.Parse(requestBody.company_secret), Guid.Parse(requestBody.application_id), Guid.Parse(requestBody.application_secret), requestBody.user_id, requestBody.user_secret, requestBody.scope, deviceInformation);
                    break;
                case TokenGrantTypeEnum.RefreshToken:
                    break;
            }

            var logginSuccesStatusList = new List<OAuthActionTypeEnum>()
            {
                OAuthActionTypeEnum.OAuthActionType_LoginSuccessByClientCredentials,
                OAuthActionTypeEnum.OAuthActionType_LoginSuccessByAuthorizationCode
            };

            if (userInfoDTO is not null && logginSuccesStatusList.Contains(userInfoDTO!.ActionStatus!.Value))
            {
                List<Claim> claimList = JwtTokenHelper.ToClaimsList(userInfoDTO);
                tokenResponse = await JwtTokenHelper.CreateJwtTokenAsync(requestBody, claimList, userInfoDTO, _jwtIdentityServerOptionsValues, _cultureService);
            }
            else
            {
                tokenResponse = await JwtTokenHelper.CreateEmptyJwtTokenAsync(requestBody, userInfoDTO!);
            }
            return tokenResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(NewJwtTokenAsync));
            throw;
        }
    }





    public async Task<ApplicationDTO> CreateNewApplicationAsync(ApplicationDTO applicationInfoDTO, DeviceInformationDTO deviceInformation, long companyId, long userId)
    {
        try
        {
            var returnValue = await _securityRepository.CreateNewApplicationAsync(applicationInfoDTO, deviceInformation, companyId, userId);
            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewApplicationAsync));
            throw;
        }
    }

    public async Task<CompanyApplicationsDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO companyApplicationInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await _securityRepository.CreateNewCompanyAsync(companyApplicationInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewCompanyAsync));
            throw;
        }
    }

    public async Task<CompanyApplicationsDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO companyApplicationInfo, DeviceInformationDTO deviceInformation)
    {
        CompanyApplicationsDTO? returnValue = null;
        try
        {
            returnValue = await _securityRepository.UpdateCompanyApplicationsAsync(companyApplicationInfo, deviceInformation);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(UpdateCompanyApplicationsAsync));
            throw;
        }
        return returnValue;
    }

    public async Task<UserDTO?> CreateNewUserAsync(long companyId, UserDTO userInfo, DeviceInformationDTO deviceInformation)
    {
        UserDTO? returnValue = null;
        try
        {
            returnValue = await _securityRepository.CreateNewUserAsync(userInfo, deviceInformation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewUserAsync));
            throw;
        }
        return returnValue;
    }

    public async Task<bool?> ChangeUserPasswordAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation)
    {
        bool? returnValue = null;
        try
        {
            returnValue = await _securityRepository.ChangeUserPasswordAsync(userInfo, deviceInformation);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ChangeUserPasswordAsync));
            throw;
        }
        return returnValue;
    }



    public async Task<bool?> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation)
    {
        bool? returnValue = null;
        try
        {
            returnValue = await _securityRepository.SendActivationEmailAsync(companyId, userId, deviceInformation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(SendActivationEmailAsync));
            throw;
        }
        return returnValue;

    }

    public async Task<bool?> ActivateAccountAsync(string activationToken, DeviceInformationDTO deviceInformation)
    {
        bool? returnValue = null;
        try
        {
            var activationTokenList = ValidateEmailActivationToken(activationToken);
            Guid companyClientId = Guid.Parse(activationTokenList.First());
            long userId = long.Parse(activationTokenList[1]);
            Guid activationId = Guid.Parse(activationTokenList.Last());

            returnValue = await _securityRepository.ActivateAccountAsync(companyClientId, userId, activationId, deviceInformation);
            if (returnValue == false)
            {
                throw new Exception(TransverseExceptionEnum.ActivateAccount_InvalidToken.ToString());
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ActivateAccountAsync));
            throw;
        }
        return returnValue;
    }



    public async Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation)
    {
        EndpointDTO? endpointInfo = null!;
        try
        {
            var roleId = deviceInformation.RoleId;
            if (roleId is not null)
            {
                var endpointList = await GetEndpointsListByRoleIdAsync(roleId.Value);
                endpointInfo = endpointList.FirstOrDefault(x => (x.EndpointUrl.Equals(deviceInformation.EndPointUrl, StringComparison.InvariantCultureIgnoreCase) && x.Method.Equals(deviceInformation.Method, StringComparison.InvariantCultureIgnoreCase)) && x.IsActive);
                var actionType = endpointInfo is null ? OAuthActionTypeEnum.OAuthActionType_ApiRequestUnauthorized : OAuthActionTypeEnum.OAuthActionType_ApiRequestSuccessfully;
                await AddNewActivityLogAsync(deviceInformation, actionType);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ValidateEndpointAuthorizationAsync));
        }
        return endpointInfo!;
    }



    //public async Task<AuditRecordDTO?> NewRowAuditAsync(object entity, string entityIndex, DeviceInformationDTO deviceInformation)
    //{
    //    AuditRecordDTO? returnValue = null!;
    //    try
    //    {
    //        var tokenDTO = deviceInformation.JwtToken!.DecodeJwtTokenSync();
    //        var companyId = tokenDTO!.GetJwtTokenClaimLong(CustomClaimTypes.CompanyId.ToString());
    //        var userId = tokenDTO!.GetJwtTokenClaimLong(CustomClaimTypes.UserId.ToString());
    //        returnValue = await auditService.NewRowAuditAsync(companyId!.Value!, userId!.Value!, entity, entityIndex, deviceInformation, true);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, message: nameof(AddNewActivityLogAsync));
    //    }
    //    return returnValue!;
    //}

    public async Task<bool?> AddNewActivityLogAsync(DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus)
    {
        bool? returnValue = null!;
        try
        {
            var companyId = deviceInformation.CompanyId;
            var applicationId = deviceInformation.ApplicationId;
            var userId = deviceInformation.UserId;
            var roleId = deviceInformation.RoleId;
            returnValue = await _securityRepository.AddNewActivityLogAsync(companyId!.Value, applicationId, userId, roleId, deviceInformation, actionStatus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(AddNewActivityLogAsync));
        }
        return returnValue!;
    }


    public async Task<List<ApplicationDTO>> GetAllApplicationListAsync()
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<ApplicationDTO>>(CacheItemKeysEnum.AllApplicationList.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetAllApplicationListAsync(x => x.Id > 0);
                _cachingService.SetCacheData(CacheItemKeysEnum.AllApplicationList.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllApplicationListAsync));
            throw;
        }
    }

    public async Task<List<ApplicationDTO>> GetApplicationListAsync(long? companyId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<ApplicationDTO>>(CacheItemKeysEnum.ApplicationListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetApplicationListAsync(x => companyId == null || x.CompanyFk == companyId);
                _cachingService.SetCacheData(CacheItemKeysEnum.ApplicationListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllApplicationListAsync));
            throw;
        }
    }


    public async Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipAsync(long? companyId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<CompanyMembershipsDTO>>(CacheItemKeysEnum.CompanyMemberhipListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetCompanyMemberhipListAsync(x => companyId == null || x.CompanyFk == companyId);
                _cachingService.SetCacheData(CacheItemKeysEnum.CompanyMemberhipListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllApplicationListAsync));
            throw;
        }
    }


    public async Task<List<CompanyDTO>> GetAllCompanyListAsync()
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<CompanyDTO>>(CacheItemKeysEnum.AllCompanyList.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetAllCompanyListAsync(x => x.Id > 0);
                _cachingService.SetCacheData(CacheItemKeysEnum.AllCompanyList.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllCompanyListAsync));
            throw;
        }

    }

    public async Task<List<RoleDTO>> GetRoleListAsync(long? companyId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<RoleDTO>>(CacheItemKeysEnum.RoleListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetRoleListAsync(x => companyId == null || x.CompanyFk == companyId);
                _cachingService.SetCacheData(CacheItemKeysEnum.RoleListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetRoleListAsync));
            throw;
        }
    }

    public async Task<RoleDTO> GetRolePermissionListAsync(long roleId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<RoleDTO>(CacheItemKeysEnum.RolePermissionListByRoleId + roleId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetRolePermissionListAsync(x => x.Id == roleId);
                _cachingService.SetCacheData(CacheItemKeysEnum.RolePermissionListByRoleId + roleId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetRolePermissionListAsync));
            throw;
        }
    }

    public async Task<List<ModuleDTO>> GetModuleListAsync(long? applicationId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<ModuleDTO>>(CacheItemKeysEnum.ModuleListByApplicationId + applicationId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetModuleListAsync(x => applicationId == null || x.ApplicationFk == applicationId);
                _cachingService.SetCacheData(CacheItemKeysEnum.ModuleListByApplicationId + applicationId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetModuleListAsync));
            throw;
        }
    }

    public async Task<List<EndpointDTO>> GetEndpointsListAsync(long moduleId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<EndpointDTO>>(CacheItemKeysEnum.EndpointListByModuleId + moduleId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetEndpointsListAsync(x => x.ModuleFk == moduleId);
                _cachingService.SetCacheData(CacheItemKeysEnum.EndpointListByModuleId + moduleId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetEndpointsListAsync));
            throw;
        }
    }

    public async Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<EndpointDTO>>(CacheItemKeysEnum.EndpointListByRoleId + roleId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetEndpointsListByRoleIdAsync(roleId);
                _cachingService.SetCacheData(CacheItemKeysEnum.EndpointListByRoleId + roleId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetEndpointsListByRoleIdAsync));
            throw;
        }
    }


    public async Task<List<ComponentDTO>> GetComponentListAsync(long endpointId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<ComponentDTO>>(CacheItemKeysEnum.ComponentListByEndpointId + endpointId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetComponentListAsync(x => x.EndpointFk == endpointId);
                _cachingService.SetCacheData(CacheItemKeysEnum.ComponentListByEndpointId + endpointId.ToString(), returnList);
            }
            return returnList;


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetComponentListAsync));
            throw;
        }
    }

    public async Task<List<UserRoleDTO>> GetUserRolesListAsync(long userId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<UserRoleDTO>>(CacheItemKeysEnum.UserRoleListByUserId + userId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetUserRolesListAsync(x => x.UserFk == userId);
                _cachingService.SetCacheData(CacheItemKeysEnum.UserRoleListByUserId + userId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetUserRolesListAsync));
            throw;
        }
    }

    public async Task<UserDTO> GetUserPermissionListAsync(long userId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<UserDTO>(CacheItemKeysEnum.UserPermissionListByUserId + userId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetUserPermissionListAsync(x => x.Id == userId);
                _cachingService.SetCacheData(CacheItemKeysEnum.UserPermissionListByUserId + userId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetUserPermissionListAsync));
            throw;
        }
    }


    #endregion


    #region Private Methods


    private List<string> ValidateEmailActivationToken(string activationToken)
    {
        try
        {
            var activationTokenList = activationToken.Split("@").ToList();
            if (activationTokenList.Count != 3)
            {
                throw new Exception(TransverseExceptionEnum.ActivateAccount_InvalidToken.ToString());
            }
            Guid companyClientId = Guid.Parse(activationTokenList.First());
            long userId = long.Parse(activationTokenList[1]);
            Guid activationId = Guid.Parse(activationTokenList.Last());
            return activationTokenList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ValidateEmailActivationToken));
            throw new Exception(TransverseExceptionEnum.UserPermissionException_ModuleFromApplicationNotFound.ToString());
        }

    }

    #endregion

    //decodejw token on react/NodeJS
    //    import jwt_decode from 'jwt-decode';

    //var token = 'eyJ0eXAiO.../// jwt token';

    //    var decoded = jwt_decode(token);
    //    console.log(decoded);

}
