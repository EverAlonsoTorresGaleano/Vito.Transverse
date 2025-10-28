using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Vito.Framework.Common.Models.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.Extensions;
using  Vito.Transverse.Identity.Infrastructure.Models;


namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.SocialNetworks;

public class SocialNetworksRepository(IDataBaseContextFactory dataBaseContextFactory,  ILogger<SocialNetworksRepository> logger) : ISocialNetworksRepository
{

    public async Task<List<NotificationTemplateDTO>> GetNotificationTemplateListAsync(Expression<Func<NotificationTemplate, bool>> filters, DataBaseServiceContext? context = null)
    {
       List< NotificationTemplateDTO> notificationTemplateInfoDTOList = default!;
        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var notificationTemplateInfo = await context.NotificationTemplates.Where(filters).ToListAsync();
            notificationTemplateInfoDTOList = notificationTemplateInfo!.ToNotificationTemplateDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetNotificationTemplateListAsync));
        }

        return notificationTemplateInfoDTOList;
    }

    public async Task<NotificationDTO?> CreateNewNotificationsync(NotificationDTO newRecord, DataBaseServiceContext? context = null)
    {
        NotificationDTO? savedRecord = null;
        var newRecordDb = newRecord.ToNotification();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Notifications.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToNotificationDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewNotificationsync));
        }
        return savedRecord;
    }


}
