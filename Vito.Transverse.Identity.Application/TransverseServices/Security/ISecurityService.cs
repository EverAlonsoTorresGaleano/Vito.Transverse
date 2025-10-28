using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Entities.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Security;


public interface ISecurityService
{
    Task<TokenResponseDTO> NewJwtTokenAsync(TokenRequestDTO requestBody, DeviceInformationDTO deviceInformation);






    
    
    
    
 



 









    
}