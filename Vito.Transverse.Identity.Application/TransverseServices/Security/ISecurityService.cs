using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.Security;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Security;


public interface ISecurityService
{
    Task<TokenResponseDTO> NewJwtTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation);






    
    
    
    
 



 









    
}