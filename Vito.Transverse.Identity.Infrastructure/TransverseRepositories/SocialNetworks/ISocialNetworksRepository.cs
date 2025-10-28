using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.SocialNetworks;

public interface ISocialNetworksRepository
{
    Task<List<NotificationTemplateDTO>> GetNotificationTemplateListAsync(Expression<Func<NotificationTemplate, bool>> filters, DataBaseServiceContext? context = null);

    Task<NotificationDTO?> CreateNewNotificationsync(NotificationDTO newRecord, DataBaseServiceContext? context = null);

}
