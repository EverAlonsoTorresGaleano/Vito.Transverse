using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.Security;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Security;

/// <summary>
/// Bussiness Logic for Security
/// </summary>
public interface ISecurityService
{
    /// <summary>
    /// Authenticate 
    /// </summary>
    /// <param name="requestBody"></param>
    /// <param name="deviceInformation"></param>
    /// <returns></returns>
    Task<TokenResponseDTO> CreateAuthenticationTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation);
}