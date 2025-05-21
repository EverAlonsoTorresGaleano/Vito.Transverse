using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Security;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Security;


/// <see cref="ISecurityService"/>
public class SecurityService(ISecurityRepository _securityRepository, ICultureService _cultureService, ICachingServiceMemoryCache _cachingService, IOptions<IdentityServiceServerSettingsOptions> _jwtIdentityServerOptions, ILogger<ISecurityService> _logger) : ISecurityService
{
    private readonly IdentityServiceServerSettingsOptions _jwtIdentityServerOptionsValues = _jwtIdentityServerOptions.Value;

    #region Public Methods
    public async Task<TokenResponseDTO> CreateAuthenticationTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation)
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
                List<Claim> claimList = CreateAuthenticationClaims(userInfoDTO);
                tokenResponse = await CreateJwtTokenAsync(requestBody, claimList, userInfoDTO);
            }
            else
            {
                tokenResponse = await CreateEmptyJwtTokenAsync(requestBody, userInfoDTO!);
            }
            return tokenResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateAuthenticationTokenAsync));
            throw;
        }
    }


    private TokenDTO DecodeJwtSync(string tokenBearer)
    {
        var jwtToken = tokenBearer!.Replace(FrameworkConstants.TokenBearerPrefix, string.Empty).Trim();
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);
        var keyId = token.Header.Kid;
        var audience = token.Audiences.ToList();
        var claims = token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();
        return new TokenDTO(
            keyId,
            token.Issuer,
            audience,
            claims,
            token.ValidTo,
            token.SignatureAlgorithm,
            token.RawData,
            token.Subject,
            token.ValidFrom,
            token.EncodedHeader,
            token.EncodedPayload
        );
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

    public async Task<CompanyDTO> CreateNewCompanyAsync(CompanyDTO companyInfo, DeviceInformationDTO deviceInformation, long userId)
    {
        try
        {
            var returnValue = await _securityRepository.CreateNewCompanyAsync(companyInfo, deviceInformation, userId);
            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateNewCompanyAsync));
            throw;
        }
    }

    public async Task<UserDTO?> CreateNewUserAsync(UserDTO userInfo, long companyId, DeviceInformationDTO deviceInformation)
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

    public async Task<bool?> UpdateCompanyApplicationsAsync(CompanyDTO companyInfo, List<ApplicationDTO> applicationInfoList, long userId, DeviceInformationDTO deviceInformation)
    {
        bool? returnValue = null;
        try
        {
            returnValue = await _securityRepository.UpdateCompanyApplicationsAsync(companyInfo, applicationInfoList, userId, deviceInformation);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(UpdateCompanyApplicationsAsync));
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
            var activationTokenList = ValidateActivationToken(activationToken);
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

    private List<string> ValidateActivationToken(string activationToken)
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
            _logger.LogError(ex, message: nameof(ValidateActivationToken));
            throw new Exception(TransverseExceptionEnum.UserPermissionException_ModuleFromApplicationNotFound.ToString());
        }

    }

    public async Task<EndpointDTO?> ValidateEndpointAuthorizationAsync(DeviceInformationDTO deviceInformation)
    {
        EndpointDTO? endpointInfo = null!;
        try
        {
            var tokenDTO = DecodeJwtSync(deviceInformation.JwtToken!);

            var roleIdString = tokenDTO.Claims.First(x => x.Type.Equals(CustomClaimTypes.RoleId.ToString())).Value;
            if (long.TryParse(roleIdString, out var roleId))
            {
                endpointInfo = await ValidateAuthorizationEndpointByRoleIdAsync(roleId, deviceInformation.EndPointUrl!, deviceInformation.Method!);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ValidateEndpointAuthorizationAsync));
        }
        return endpointInfo!;
    }

    public async Task<EndpointDTO?> ValidateAuthorizationEndpointByRoleIdAsync(long roleId, string endpointUrl, string method)
    {
        var endpointList = await GetEndpointsListByRoleIdAsync(roleId);
        var endpointInfo = endpointList.FirstOrDefault(x => (x.EndpointUrl.Equals(endpointUrl) && x.Method.Equals(method)) && x.IsActive);
        return endpointInfo;

    }


    public async Task<bool?> AddNewActivityLogAsync(DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus)
    {
        bool? returnValue = null!;
        try
        {
            var tokenDto = DecodeJwtSync(deviceInformation.JwtToken!);
            var companyId = tokenDto.Claims.First(x => x.Type.Equals(CustomClaimTypes.CompanyId.ToString())).Value;
            var applicationId = tokenDto.Claims.First(x => x.Type.Equals(CustomClaimTypes.ApplicationId.ToString())).Value;
            var userId = tokenDto.Claims.First(x => x.Type.Equals(ClaimTypes.Sid.ToString())).Value;
            returnValue = await _securityRepository.AddNewActivityLogAsync(long.Parse(companyId), long.Parse(applicationId), long.Parse(userId), deviceInformation, actionStatus);
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
                returnList = await _securityRepository.GetAllApplicationListAsync();
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
                returnList = await _securityRepository.GetApplicationListAsync(companyId);
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
                returnList = await _securityRepository.GetCompanyMemberhipAsync(companyId);
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
                returnList = await _securityRepository.GetAllCompanyListAsync();
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
                returnList = await _securityRepository.GetRoleListAsync(companyId);
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
                returnList = await _securityRepository.GetRolePermissionListAsync(roleId);
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
                returnList = await _securityRepository.GetModuleListAsync(applicationId);
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
                returnList = await _securityRepository.GetEndpointsListAsync(moduleId);
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
                returnList = await _securityRepository.GetComponentListAsync(endpointId);
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
                returnList = await _securityRepository.GetUserRolesListAsync(userId);
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
                returnList = await _securityRepository.GetUserPermissionListAsync(userId);
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

    public async Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(long? companyId)
    {
        try
        {
            var returnList = _cachingService.GetCacheDataByKey<List<CompanyEntityAuditDTO>>(CacheItemKeysEnum.CompanyEntityAuditListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await _securityRepository.GetCompanyEntityAuditsListAsync(companyId);
                _cachingService.SetCacheData(CacheItemKeysEnum.CompanyEntityAuditListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetCompanyEntityAuditsListAsync));
            throw;
        }
    }

    public async Task<List<AuditRecordDTO>> GetAuditRecordsListAsync(long? companyId)
    {
        try
        {
            var returnValue = await _securityRepository.GetAuditRecordsListAsync(companyId);
            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAuditRecordsListAsync));
            throw;
        }
    }


    public async Task<List<ActivityLogDTO>> GetActivityLogListAsync(long? companyId)
    {
        try
        {
            var returnList = await _securityRepository.GetActivityLogListAsync(companyId);
            return returnList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetActivityLogListAsync));
            throw;
        }
    }

    public async Task<List<NotificationDTO>> GetNotificationsListAsync(long? companyId)
    {
        try
        {
            var returnList = await _securityRepository.GetNotificationsListAsync(companyId);
            return returnList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetNotificationsListAsync));
            throw;
        }
    }
    #endregion


    #region Private Methods
    /// <summary>
    /// Create Empty Jwt Token
    /// </summary>
    /// <param name="requestBody"></param>
    /// <param name="userInfoDTO"></param>
    /// <returns></returns>
    private Task<TokenResponseDTO> CreateEmptyJwtTokenAsync(TokenRequestDTO requestBody, UserDTOToken userInfoDTO)
    {
        TokenResponseDTO response = new()
        {
            access_token = null,
            token_type = FrameworkConstants.TokenBearerPrefix,
            issued_at = null,
            expires_in = null,
            status = userInfoDTO?.ActionStatus.ToString(),
            scope = requestBody.scope!,
            application_id = requestBody.application_id,
            company_id = requestBody.company_id,
            user_id = requestBody.user_id,
            user_avatar = []
        };
        return Task.FromResult(response);
    }

    /// <summary>
    /// Create Authenticaton Claims
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    private List<Claim> CreateAuthenticationClaims(UserDTOToken userInfo)
    {
        List<Claim> claimList =
        [
            new (CustomClaimTypes.ApplicationOwnerId.ToString(),userInfo.ApplicationOwnerId.ToString()!),
            new (CustomClaimTypes.ApplicationOwnerName.ToString(),userInfo.ApplicationOwnerNameTranslationKey!.ToString()!),
            new (CustomClaimTypes.ApplicationId.ToString(), userInfo.ApplicationId.ToString()!),
            new (CustomClaimTypes.ApplicationName.ToString(), userInfo.ApplicationNameTranslationKey!.ToString()!),
            new (CustomClaimTypes.CompanyId.ToString(), userInfo.CompanyFk.ToString()!),
            new (CustomClaimTypes.CompanyName.ToString(), userInfo.CompanyNameTranslationKey!),
            new (ClaimTypes.Sid, userInfo.Id.ToString()),
            new (ClaimTypes.Name, userInfo.UserName),
            new (ClaimTypes.GivenName, $"{userInfo.Name} {userInfo.LastName}"),
            new (ClaimTypes.Email, userInfo.Email!),
            new (CustomClaimTypes.RoleId.ToString()    , userInfo.RoleId.ToString()! ),
            new (CustomClaimTypes.RoleName.ToString(), userInfo.RoleName! ),
        ];
        return claimList;
    }

    /// <summary>
    /// Create Jwt Token
    /// </summary>
    /// <param name="requestBody"></param>
    /// <param name="claimList"></param>
    /// <param name="userInfoDTO"></param>
    /// <returns></returns>
    private Task<TokenResponseDTO> CreateJwtTokenAsync(TokenRequestDTO requestBody, List<Claim> claimList, UserDTOToken userInfoDTO)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = CreateJwtAuthenticationTokenDescriptor(claimList);
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var access_token = tokenHandler.WriteToken(securityToken);
        TokenResponseDTO response = new()
        {
            access_token = access_token,
            token_type = FrameworkConstants.TokenBearerPrefix,
            issued_at = tokenDescriptor.IssuedAt!.Value.Ticks,
            expires_in = _jwtIdentityServerOptionsValues.TokenLifeTimeMinutes * FrameworkConstants.MinuteOnSeconds,
            status = userInfoDTO.ActionStatus.ToString(),
#if DEBUG
            scope = $"{userInfoDTO.RoleId}-{userInfoDTO.RoleName}",
#else
            scope = requestBody.scope!,
#endif
            application_id = requestBody.application_id,
            company_id = requestBody.company_id,
            user_id = requestBody.user_id,
            user_avatar = [],

        };
        return Task.FromResult(response);
    }

    /// <summary>
    /// Create Token descriptor
    /// </summary>
    /// <param name="claimList"></param>
    /// <returns></returns>
    private SecurityTokenDescriptor CreateJwtAuthenticationTokenDescriptor(List<Claim> claimList)
    {
        var securityKey = new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_jwtIdentityServerOptionsValues!.Key!)),
                    SecurityAlgorithms.HmacSha256Signature
                );

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _jwtIdentityServerOptionsValues.Issuer,
            Audience = _jwtIdentityServerOptionsValues.Audience,
            Subject = new ClaimsIdentity(claimList.ToArray()),
            IssuedAt = _cultureService.UtcNow().DateTime,
            Expires = _cultureService.UtcNow().DateTime.AddMinutes(_jwtIdentityServerOptionsValues.TokenLifeTimeMinutes),
            SigningCredentials = securityKey,

        };
        return tokenDescriptor;
    }



    #endregion

    //decodejw token on react/NodeJS
    //    import jwt_decode from 'jwt-decode';

    //var token = 'eyJ0eXAiO.../// jwt token';

    //    var decoded = jwt_decode(token);
    //    console.log(decoded);

}
