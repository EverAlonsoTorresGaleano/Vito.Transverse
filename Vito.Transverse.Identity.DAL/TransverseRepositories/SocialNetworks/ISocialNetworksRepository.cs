using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.DAL.DataBaseContext;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;

public interface ISocialNetworksRepository
{
    Task<NotificationTemplateDTO> GetNotificationTeamplete(string id);
    Task<bool> SendNotification(NotificationDTO emailInfo, DataBaseServiceContext? transactionContext = null);
}
