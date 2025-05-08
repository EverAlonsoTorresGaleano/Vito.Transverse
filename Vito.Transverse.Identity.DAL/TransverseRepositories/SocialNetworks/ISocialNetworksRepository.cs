using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.DAL.DataBaseContext;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;

public interface ISocialNetworksRepository
{
    Task<NotificationTemplateDTO> GetNotificationTemplateByIdAsync(string id);

    Task<bool> SendNotificationByTemplateAsync(long companyId,NotificationTypeEnum type, long templateId, List<KeyValuePair<string, string>> templateParameters, List<string> emailList, List<string>? emailListCC = null, List<string>? emailListBCC = null, string? cultureId = null, DataBaseServiceContext? context = null);

    Task<bool> SendNotificationAsync(NotificationDTO emailInfo, DataBaseServiceContext? context = null);
}
