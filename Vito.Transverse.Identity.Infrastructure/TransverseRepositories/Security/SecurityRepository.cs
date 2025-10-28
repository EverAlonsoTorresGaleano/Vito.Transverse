using Microsoft.Extensions.Logging;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Security;



public class SecurityRepository(ILogger<SecurityRepository> logger, IDataBaseContextFactory dataBaseContextFactory) : ISecurityRepository
{

}