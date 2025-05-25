using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.Models;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;

public interface ISocialNetworksRepository
{
    Task<List<NotificationTemplateDTO>> GetNotificationTemplateListAsync(Expression<Func<NotificationTemplate, bool>> filters, DataBaseServiceContext? context = null);

    Task<NotificationDTO?> CreateNewNotificationsync(NotificationDTO newRecord, DataBaseServiceContext? context = null);

}
