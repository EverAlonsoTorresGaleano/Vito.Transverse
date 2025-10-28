using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Application.TransverseServices.Culture;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.Helpers;

public static class JwtTokenHelper
{
    /// <summary>
    /// Create Empty Jwt Token
    /// </summary>
    /// <param name="requestBody"></param>
    /// <param name="userInfoDTO"></param>
    /// <returns></returns>
    public static Task<TokenResponseDTO> CreateEmptyJwtTokenAsync(this TokenRequestDTO requestBody, UserDTOToken userInfoDTO)
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
    /// Create Jwt Token
    /// </summary>
    /// <param name="requestBody"></param>
    /// <param name="claimList"></param>
    /// <param name="userInfoDTO"></param>
    /// <returns></returns>
    public static Task<TokenResponseDTO> CreateJwtTokenAsync(this TokenRequestDTO requestBody, List<Claim> claimList, UserDTOToken userInfoDTO, IdentityServiceServerSettingsOptions _jwtIdentityServerOptionsValues, ICultureService cultureService)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = CreateJwtTokenDescriptor(claimList, _jwtIdentityServerOptionsValues, cultureService);
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
    /// Create Token descriptor _jwtIdentityServerOptionsValues.TokenLifeTimeMinutes
    /// </summary>
    /// <param name="claimList"></param>
    /// <returns></returns>
    public static SecurityTokenDescriptor CreateJwtTokenDescriptor(List<Claim> claimList, IdentityServiceServerSettingsOptions _jwtIdentityServerOptionsValues, ICultureService _cultureService)
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

    /// <summary>
    /// Create Authenticaton Claims
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static List<Claim> ToClaimsList(this UserDTOToken userInfo)
    {
        List<Claim> claimList =
        [
            new (CustomClaimTypes.ApplicationOwnerId.ToString(),userInfo.ApplicationOwnerId.ToString()!),
            new (CustomClaimTypes.ApplicationOwnerName.ToString(),userInfo.ApplicationOwnerNameTranslationKey!.ToString()!),
            new (CustomClaimTypes.ApplicationId.ToString(), userInfo.ApplicationId.ToString()!),
            new (CustomClaimTypes.ApplicationName.ToString(), userInfo.ApplicationNameTranslationKey!.ToString()!),
            new (CustomClaimTypes.CompanyId.ToString(), userInfo.CompanyFk.ToString()!),
            new (CustomClaimTypes.CompanyName.ToString(), userInfo.CompanyNameTranslationKey!),
            new (CustomClaimTypes.UserId.ToString(), userInfo.Id.ToString()),
            new (CustomClaimTypes.UserName.ToString(), userInfo.UserName),
            new (CustomClaimTypes.GivenName.ToString(), $"{userInfo.Name} {userInfo.LastName}"),
            new (CustomClaimTypes.Email.ToString(), userInfo.Email!),
            new (CustomClaimTypes.RoleId.ToString()    , userInfo.RoleId.ToString()! ),
            new (CustomClaimTypes.RoleName.ToString(), userInfo.RoleName! ),
        ];
        return claimList;
    }

    //public static TokenDTO DecodeJwtTokenSync(this string tokenBearer)
    //{
    //    var jwtToken = tokenBearer!.Replace(FrameworkConstants.TokenBearerPrefix, string.Empty).Trim();
    //    var handler = new JwtSecurityTokenHandler();
    //    var token = handler.ReadJwtToken(jwtToken);
    //    var keyId = token.Header.Kid;
    //    var audience = token.Audiences.ToList();
    //    var claims = token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();
    //    return new TokenDTO(
    //        keyId,
    //        token.Issuer,
    //        audience,
    //        claims,
    //        token.ValidTo,
    //        token.SignatureAlgorithm,
    //        token.RawData,
    //        token.Subject,
    //        token.ValidFrom,
    //        token.EncodedHeader,
    //        token.EncodedPayload
    //    );
    //}


    //public static string GetJwtTokenClaim(this TokenDTO tokenDTO, string claimType)
    //{
    //    var claimItem = tokenDTO.Claims.FirstOrDefault(x => x.Type.Equals(claimType, StringComparison.InvariantCultureIgnoreCase));
    //    var claimValue = claimItem.Value;
    //    return claimValue;
    //}

    //public static long? GetJwtTokenClaimLong(this TokenDTO tokenDTO, string claimType)
    //{
    //    long? returnValue = null;
    //    var claimValue = GetJwtTokenClaim(tokenDTO, claimType);
    //    if (long.TryParse(claimValue, out long idParse))
    //    {
    //        returnValue = idParse;
    //    }
    //    return returnValue;

    //}
}
