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
public class SecurityService(ISecurityRepository _securityRepository, ICultureService _cultureService, ILocalizationService _localizationService, IOptions<IdentityServiceServerSettingsOptions> _jwtIdentityServerOptions, ILogger<ISecurityService> _logger) : ISecurityService
{
    private readonly IdentityServiceServerSettingsOptions _jwtIdentityServerOptionsValues = _jwtIdentityServerOptions.Value;

    #region Public Methods
    /// <see cref="ISecurityService.CreateAuthenticationTokenAsync(TokenRequestDTO, DeviceInformationDTO)"/>
    public async Task<TokenResponseDTO> CreateAuthenticationTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation)
    {
        try
        {


            UserDTO? userInfoDTO = default;
            var grantType = Enum.Parse<TokenGrantTypeEnum>(requestBody.grant_type, true);
            TokenResponseDTO tokenResponse = default!;
            switch (grantType)
            {
                case TokenGrantTypeEnum.AuthorizationCode:
                    userInfoDTO = await _securityRepository.TokenValidateAuthorizationCode(Guid.Parse(requestBody.company_id), Guid.Parse(requestBody.company_secret), Guid.Parse(requestBody.application_id), Guid.Parse(requestBody.application_secret), requestBody.scope, deviceInformation);
                    break;
                case TokenGrantTypeEnum.ClientCredentials:
                    userInfoDTO = await _securityRepository.TokenValidateClientCredentials(Guid.Parse(requestBody.company_id), Guid.Parse(requestBody.company_secret), Guid.Parse(requestBody.application_id), Guid.Parse(requestBody.application_secret), requestBody.user_id, requestBody.user_secret, requestBody.scope, deviceInformation);
                    break;
                case TokenGrantTypeEnum.RefreshToken:
                    break;
            }

            var logginSuccesStatusList = new List<ActionTypeEnum>()
        {
            ActionTypeEnum.ActionType_LoginSuccessByClientCredentials,
            ActionTypeEnum.ActionType_LoginSuccessByAuthorizationCode
        };

            if (userInfoDTO is not null && logginSuccesStatusList.Contains(userInfoDTO!.ActionStatus!.Value))
            {
                List<Claim> claimList = CreateAuthenticationClaims(userInfoDTO);
                tokenResponse = await CreateJwtTokenAsync(requestBody, claimList, userInfoDTO);
            }
            else
            {
                tokenResponse = await CreateEmptyJwtTokenAsync(requestBody, userInfoDTO);
            }
            return tokenResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(CreateAuthenticationTokenAsync));
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
    private Task<TokenResponseDTO> CreateEmptyJwtTokenAsync(TokenRequestDTO requestBody, UserDTO userInfoDTO)
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
    private List<Claim> CreateAuthenticationClaims(UserDTO userInfo)
    {
        List<Claim> claimList =
        [
            new (ClaimTypes.System, userInfo.ApplicationName!), //Application name
            new (ClaimTypes.Webpage, userInfo.CompanyName!), //Company Name
            new (ClaimTypes.Sid, userInfo.Id.ToString()), //USer Id
            new (ClaimTypes.Name, $"{userInfo.Name} {userInfo.LastName}"),
            new (ClaimTypes.Email, userInfo.Email!),
            new (ClaimTypes.Role, userInfo.RoleName ),
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
    private Task<TokenResponseDTO> CreateJwtTokenAsync(TokenRequestDTO requestBody, List<Claim> claimList, UserDTO userInfoDTO)
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
            scope = requestBody.scope!,
            application_id = requestBody.application_id,
            company_id = requestBody.company_id,
            user_id = requestBody.user_id,
            user_avatar = []
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
