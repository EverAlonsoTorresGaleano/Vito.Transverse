using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.DAL.DataBaseContext;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;

public interface ISocialNetworksRepository
{
    Task<NotificationTemplateDTO> GetNotificationTeamplete(string id);

    Task<bool> SendNotificationByTemplate(NotificationTypeEnum type, long templateId, List<KeyValuePair<string, string>> templateParameters, List<string> emailList, List<string>? emailListCC = null, List<string>? emailListBCC = null, string? cultureId = null, DataBaseServiceContext? context = null);

    Task<bool> SendNotification(NotificationDTO emailInfo, DataBaseServiceContext? context = null);
}
