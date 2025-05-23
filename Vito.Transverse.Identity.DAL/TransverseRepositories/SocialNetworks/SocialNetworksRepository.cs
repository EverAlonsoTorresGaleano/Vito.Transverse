using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.Models;


namespace Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;

public class SocialNetworksRepository(IDataBaseContextFactory _dataBaseContextFactory, ICultureRepository _cultureRepository, IOptions<EmailSettingsOptions> _emailSettingsOptions, ILogger<SocialNetworksRepository> _logger) : ISocialNetworksRepository
{
    EmailSettingsOptions _emailSettingsOptionsValues => _emailSettingsOptions.Value;

    public async Task<List<NotificationTemplateDTO>> GetNotificationTemplateListAsync(Expression<Func<NotificationTemplate, bool>> filters, DataBaseServiceContext? context = null)
    {
       List< NotificationTemplateDTO> notificationTemplateInfoDTOList = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var notificationTemplateInfo = await context.NotificationTemplates.Where(filters).ToListAsync();
            notificationTemplateInfoDTOList = notificationTemplateInfo!.ToNotificationTemplateDTO();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetNotificationTemplateListAsync));
        }

        return notificationTemplateInfoDTOList;
    }

    //TODO MOve to service
    public async Task<bool> SendNotificationByTemplateAsync(long companyId, NotificationTypeEnum type, long templateId, List<KeyValuePair<string, string>> templateParameters, List<string> emailList, List<string>? emailListCC = null, List<string>? emailListBCC = null, string? cultureId = null, DataBaseServiceContext? context = null)
    {
        bool notificationSent = false;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            cultureId = cultureId ?? _cultureRepository.GetCurrentCultureId();

            var templateInfo = await context.NotificationTemplates.FirstOrDefaultAsync(x => x.Id.Equals(templateId));
            if (templateInfo != null)
            {
                NotificationDTO notificationInfoDTO = new()
                {
                    CompanyFk = companyId,
                    NotificationTemplateGroupFk = templateInfo.NotificationTemplateGroupId,
                    NotificationTypeFk = (long)type,
                    CreationDate = _cultureRepository.UtcNow().DateTime,
                    Receiver = emailList,
                    Cc = emailListCC,
                    Bcc = emailListBCC,
                    CultureFk = cultureId,
                    IsHtml = templateInfo.IsHtml,
                    IsSent = false,
                    Subject = templateInfo.SubjectTemplateText?.ReplaceParameterOnString(templateParameters)!,
                    Message = templateInfo.MessageTemplateText?.ReplaceParameterOnString(templateParameters)!,
                };
                notificationSent = await SendNotificationAsync(notificationInfoDTO, context);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(SendNotificationByTemplateAsync));
            throw;
        }

        return notificationSent;
    }

    //TODO move to service
    public async Task<bool> SendNotificationAsync(NotificationDTO notificationInfoDTO, DataBaseServiceContext? context = null)
    {

        bool notificationSent = false;
        try
        {
            context = _dataBaseContextFactory.GetDbContext(context);
            notificationInfoDTO.Sender = _emailSettingsOptionsValues.SenderEmail;
            var notificationInfo = notificationInfoDTO.ToNotification();
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
            notificationInfo.CreationDate = _cultureRepository.UtcNow().DateTime;

            notificationInfo.IsSent = notificationIsSent;
            notificationInfo.SentDate = notificationIsSent ? _cultureRepository.UtcNow().DateTime : null;
            context.Notifications.Add(notificationInfo);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(SendNotificationAsync));
            throw;
        }

        return notificationSent;
    }


    //TODO  Move to service
    private async Task<bool> SendEmailNotification(NotificationDTO notificationInfoDTO)
    {
        bool emailSent = false;
        try
        {
            NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(_emailSettingsOptionsValues.UserName, _emailSettingsOptionsValues.Password);

            SmtpClient mySmtpClient = new(_emailSettingsOptionsValues.ServerName)
            {
                Port = _emailSettingsOptionsValues.Port,
                EnableSsl = _emailSettingsOptionsValues.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = basicAuthenticationInfo,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var mailMessage = new MailMessage();

            mailMessage.From = new(_emailSettingsOptionsValues.SenderEmail, notificationInfoDTO.Sender);

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
            _logger.LogError(ex, nameof(SendEmailNotification));
        }
        return emailSent;
    }

}
