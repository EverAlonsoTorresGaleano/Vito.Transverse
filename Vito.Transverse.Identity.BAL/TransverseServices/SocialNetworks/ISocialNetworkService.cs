using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;

namespace Vito.Transverse.Identity.BAL.TransverseServices.SocialNetworks;

public interface ISocialNetworkService
{

    Task<List<NotificationTemplateDTO>?> GetNotificationTemplateListAsync();
    Task<NotificationDTO?> SendNotificationByTemplateAsync(long companyId, NotificationTypeEnum type, long templateId, List<KeyValuePair<string, string>> templateParameters, List<string> emailList, List<string>? emailListCC = null, List<string>? emailListBCC = null, string? cultureId = null);

    Task<NotificationDTO?> SendNotificationAsync(NotificationDTO emailInfo);
}
