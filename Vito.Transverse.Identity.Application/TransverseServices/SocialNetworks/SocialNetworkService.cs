using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Application.TransverseServices.Culture;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.SocialNetworks;
using Vito.Transverse.Identity.Entities.Enums;

namespace  Vito.Transverse.Identity.Application.TransverseServices.SocialNetworks;

public class SocialNetworkService(ISocialNetworksRepository socialNetworksRepository, ICultureService cultureService, ICachingServiceMemoryCache cachingService, ILogger<SocialNetworkService> logger, IOptions<EmailSettingsOptions> emailSettingsOptions) : ISocialNetworkService
{
    private EmailSettingsOptions emailSettingsOptionsValues = emailSettingsOptions.Value;
    public async Task<List<NotificationTemplateDTO>?> GetNotificationTemplateListAsync()
    {
        List<NotificationTemplateDTO>? returnList = null;
        try
        {
            returnList = cachingService.GetCacheDataByKey<List<NotificationTemplateDTO>>(CacheItemKeysEnum.NotificationTemplates.ToString());
            if (returnList == null)
            {
                returnList = await socialNetworksRepository.GetNotificationTemplateListAsync(x => x.Id > 0);
                cachingService.SetCacheData(CacheItemKeysEnum.NotificationTemplates.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetNotificationTemplateListAsync));
        }
        return returnList;
    }


    public async Task<NotificationDTO?> SendNotificationByTemplateAsync(long companyId, NotificationTypeEnum type, long templateId, List<KeyValuePair<string, string>> templateParameters, List<string> emailList, List<string>? emailListCC = null, List<string>? emailListBCC = null, string? cultureId = null)
    {
        NotificationDTO? recodSaved = null;
        try
        {

            var templateInfoList = await GetNotificationTemplateListAsync();
            var templateInfo = templateInfoList!.FirstOrDefault(x => x.Id.Equals(templateId));
            if (templateInfo != null)
            {
                NotificationDTO notificationInfoDTO = new()
                {
                    CompanyFk = companyId,
                    NotificationTemplateGroupFk = templateInfo.NotificationTemplateGroupId,
                    NotificationTypeFk = (long)type,
                    CreationDate = cultureService.UtcNow().DateTime,
                    Receiver = emailList,
                    Cc = emailListCC,
                    Bcc = emailListBCC,
                    CultureFk = cultureId!,
                    IsHtml = templateInfo.IsHtml,
                    IsSent = false,
                    Subject = templateInfo.SubjectTemplateText?.ReplaceParameterOnString(templateParameters)!,
                    Message = templateInfo.MessageTemplateText?.ReplaceParameterOnString(templateParameters)!,
                };
                recodSaved = await SendNotificationAsync(notificationInfoDTO);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(SendNotificationByTemplateAsync));
            throw;
        }

        return recodSaved;
    }


    public async Task<NotificationDTO?> SendNotificationAsync(NotificationDTO notificationInfoDTO)
    {

        NotificationDTO? recordSaved = null;
        try
        {
            notificationInfoDTO.Sender = emailSettingsOptionsValues.SenderEmail;

            var notificationType = Enum.Parse<NotificationTypeEnum>(notificationInfoDTO.NotificationTypeFk.ToString(), true);
            var notificationIsSent = false;
            //TODO push on Queue  AzFx Queue Based Function that send Email
            switch (notificationType)
            {
                case NotificationTypeEnum.NotificationType_Email:
                    notificationIsSent = await SendEmailNotification(notificationInfoDTO);
                    break;
                case NotificationTypeEnum.NotificationType_SMS:

                    break;
            }
            notificationInfoDTO.CreationDate = cultureService.UtcNow().DateTime;

            notificationInfoDTO.IsSent = notificationIsSent;
            notificationInfoDTO.SentDate = notificationIsSent ? cultureService.UtcNow().DateTime : null;
            recordSaved = await socialNetworksRepository.CreateNewNotificationsync(notificationInfoDTO);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(SendNotificationAsync));
            throw;
        }

        return recordSaved;
    }


    private async Task<bool> SendEmailNotification(NotificationDTO notificationInfoDTO)
    {
        bool emailSent = false;
        try
        {
            NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(emailSettingsOptionsValues.UserName, emailSettingsOptionsValues.Password);

            SmtpClient mySmtpClient = new(emailSettingsOptionsValues.ServerName)
            {
                Port = emailSettingsOptionsValues.Port,
                EnableSsl = emailSettingsOptionsValues.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = basicAuthenticationInfo,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var mailMessage = new MailMessage();

            mailMessage.From = new(emailSettingsOptionsValues.SenderEmail, notificationInfoDTO.Sender);

            notificationInfoDTO.Receiver?.ForEach(mailMessage.To.Add);

            notificationInfoDTO.Cc?.ForEach(mailMessage.To.Add);

            notificationInfoDTO.Bcc?.ForEach(mailMessage.To.Add);

            mailMessage.Subject = notificationInfoDTO.Subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            mailMessage.Body = notificationInfoDTO.Message;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = notificationInfoDTO.IsHtml;

            await mySmtpClient.SendMailAsync(mailMessage);

            emailSent = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(SendEmailNotification));
        }
        return emailSent;
    }

}
