using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Domain.Extensions;


namespace Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;

public class SocialNetworksRepository(IDataBaseContextFactory _dataBaseContextFactory, ICultureRepository _cultureRepository, IOptions<EmailSettingsOptions> _emailSettingsOptions, ILogger<SocialNetworksRepository> _logger) : ISocialNetworksRepository
{
    EmailSettingsOptions _emailSettingsOptionsValues => _emailSettingsOptions.Value;

    public async Task<NotificationTemplateDTO> GetNotificationTeamplete(string id)
    {
        DataBaseServiceContext context = default!;
        NotificationTemplateDTO notificationTemplateInfoDTO = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var notificationTemplateInfo = await context.NotificationTemplates.FirstOrDefaultAsync(t => t.Id.Equals(id));
            notificationTemplateInfoDTO = notificationTemplateInfo!.ToNotificationTemplateDTO();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetNotificationTeamplete));
        }
        finally
        {
            context.Dispose();
        }
        return notificationTemplateInfoDTO;
    }


    public async Task<bool> SendNotificationByTemplate(NotificationTypeEnum type, long templateId, List<KeyValuePair<string, string>> templateParameters, List<string> emailList, List<string>? emailListCC = null, List<string>? emailListBCC = null, string? cultureId = null, DataBaseServiceContext? context = null)
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
                    NotificationTemplateFk = templateInfo.Id,
                    NotificationTypeFk = (long)type,
                    CreationDate = _cultureRepository.UtcNow().DateTime,
                    Receiver = emailList,
                    Cc = emailListCC,
                    Bcc = emailListBCC,
                    CultureFk = cultureId,
                    IsHtml = templateInfo.IsHtml,
                    IsSent = false,
                    Subject = templateInfo.SubjectTemplateText?.ReplaceParameterOnString(templateParameters),
                    Message = templateInfo.MessageTemplateText?.ReplaceParameterOnString(templateParameters),
                };
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(SendNotificationByTemplate));
            throw;
        }

        return notificationSent;
    }

    public async Task<bool> SendNotification(NotificationDTO notificationInfoDTO, DataBaseServiceContext? context = null)
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
                case NotificationTypeEnum.Email:
                    try
                    {
                        notificationIsSent = await SendEmailNotification(notificationInfoDTO);
                    }
                    catch (Exception exMail)
                    {
                        _logger.LogError(exMail, nameof(SendNotification));
                    }

                    break;

                case NotificationTypeEnum.SMS:
                    break;
            }
            notificationInfo.CreationDate = _cultureRepository.UtcNow().DateTime;

            notificationInfo.IsSent = notificationIsSent;
            notificationInfo.SentDate = notificationIsSent ? _cultureRepository.UtcNow().DateTime : null;
            context.Notifications.Add(notificationInfo);
            await context.SaveChangesAsync();
            notificationSent = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(SendNotification));
            throw;
        }

        return notificationSent;
    }


    private async Task<bool> SendEmailNotification(NotificationDTO notificationInfoDTO)
    {
        bool emailSent = false;
        try
        {
            SmtpClient mySmtpClient = new SmtpClient(_emailSettingsOptionsValues.ServerName);
            mySmtpClient.Port = _emailSettingsOptionsValues.Port;
            mySmtpClient.EnableSsl = _emailSettingsOptionsValues.EnableSsl;
            mySmtpClient.UseDefaultCredentials = false;
            NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(_emailSettingsOptionsValues.UserName, _emailSettingsOptionsValues.Password);
            mySmtpClient.Credentials = basicAuthenticationInfo;


            var mailMessage = new MailMessage();

            mailMessage.From = new(_emailSettingsOptionsValues.SenderEmail, notificationInfoDTO.Sender);

            notificationInfoDTO.Receiver?.ForEach(mailMessage.To.Add);

            notificationInfoDTO.Cc?.ForEach(mailMessage.To.Add);

            notificationInfoDTO.Bcc?.ForEach(mailMessage.To.Add);

            mailMessage.Subject = notificationInfoDTO.Subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            mailMessage.Body = notificationInfoDTO.Message;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;

            await mySmtpClient.SendMailAsync(mailMessage);

            emailSent = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(SendEmailNotification));
            throw;
        }
        return emailSent;
    }

}
