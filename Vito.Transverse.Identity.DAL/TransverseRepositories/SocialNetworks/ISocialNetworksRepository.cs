using System.Linq.Expressions;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.Models;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;

public interface ISocialNetworksRepository
{
    Task<List<NotificationTemplateDTO>> GetNotificationTemplateListAsync(Expression<Func<NotificationTemplate, bool>> filters, DataBaseServiceContext? context = null);

    Task<bool> SendNotificationByTemplateAsync(long companyId, NotificationTypeEnum type, long templateId, List<KeyValuePair<string, string>> templateParameters, List<string> emailList, List<string>? emailListCC = null, List<string>? emailListBCC = null, string? cultureId = null, DataBaseServiceContext? context = null);

    Task<bool> SendNotificationAsync(NotificationDTO emailInfo, DataBaseServiceContext? context = null);
}
