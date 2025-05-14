using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Localization;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Security;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Security;


/// <see cref="ISecurityService"/>
public class SecurityService(ISecurityRepository _securityRepository, ICultureService _cultureService, IOptions<IdentityServiceServerSettingsOptions> _jwtIdentityServerOptions, ILogger<ISecurityService> _logger) : ISecurityService
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
        bool? returnValue=null;
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

    public async Task<bool?> ActivateAccountAsync(long companyId, long userId, Guid activationId, DeviceInformationDTO deviceInformation)
    {
        bool? returnValue = null;
        try
        {
            returnValue = await _securityRepository.ActivateAccountAsync(companyId, userId, activationId, deviceInformation);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(ActivateAccountAsync));
            throw;
        }
        return returnValue;
    }

    public async Task<List<ApplicationDTO>> GetAllApplicationListAsync()
    {
        try
        {
            var returnValue = await _securityRepository.GetAllApplicationListAsync();
            return returnValue;
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
            var returnValue = await _securityRepository.GetApplicationListAsync(companyId);
            return returnValue;
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
            var returnValue = await _securityRepository.GetCompanyMemberhipAsync(companyId);
            return returnValue;
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
            var returnValue = await _securityRepository.GetAllCompanyListAsync();
            return returnValue;
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
            var returnValue = await _securityRepository.GetRoleListAsync(companyId);
            return returnValue;
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
            var returnValue = await _securityRepository.GetRolePermissionListAsync(roleId);
            return returnValue;
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
            var returnValue = await _securityRepository.GetModuleListAsync(applicationId);
            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetModuleListAsync));
            throw;
        }
    }

    public async Task<List<PageDTO>> GetPageListAsync(long moduleId)
    {
        try
        {
            var returnValue = await _securityRepository.GetPageListAsync(moduleId);
            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetPageListAsync));
            throw;
        }
    }

    public async Task<List<ComponentDTO>> GetComponentListAsync(long pageId)
    {
        try
        {
            var returnValue = await _securityRepository.GetComponentListAsync(pageId);
            return returnValue;
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
            var returnValue = await _securityRepository.GetUserRolesListAsync(userId);
            return returnValue;
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
            var returnValue = await _securityRepository.GetUserPermissionListAsync(userId);
            return returnValue;
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
            var returnValue = await _securityRepository.GetCompanyEntityAuditsListAsync(companyId);
            return returnValue;
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
            var returnValue = await _securityRepository.GetActivityLogListAsync(companyId);
            return returnValue;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetActivityLogListAsync));
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
            new (ClaimTypes.System, userInfo.ApplicationName!), //Application name
            new (ClaimTypes.Webpage, userInfo.CompanyName!), //Company Name
            new (ClaimTypes.Sid, userInfo.Id.ToString()), //USer Id
            new (ClaimTypes.Name, $"{userInfo.Name} {userInfo.LastName}"),
            new (ClaimTypes.Email, userInfo.Email!),
            new (ClaimTypes.Role, userInfo.RoleName! ),
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
            issued_at = tokenDescriptor.Expires!.Value.AddMinutes(-(double)_jwtIdentityServerOptionsValues!.TokenLifeTimeMinutes).Ticks,
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
            Expires = _cultureService.UtcNow().DateTime.AddMinutes(_jwtIdentityServerOptionsValues.TokenLifeTimeMinutes),
            SigningCredentials = securityKey
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
