namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public  class NotificationDTO1
{
    public long CompanyFk { get; set; }

    public long NotificationTemplateGroupFk { get; set; }

    public string CultureFk { get; set; } = null!;

    public long NotificationTypeFk { get; set; }

    public long Id { get; set; }

    public DateTime CreationDate { get; set; }

    public string Sender { get; set; } = null!;

    public string Receiver { get; set; } = null!;

    public string? Cc { get; set; }

    public string? Bcc { get; set; }

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public bool IsSent { get; set; }

    public DateTime? SentDate { get; set; }

    public bool IsHtml { get; set; }



    public string CompanyNameTranslationKey { get;  set; }
    public string NotificationTypeNameTranslationKey { get;  set; }
    public string NotificationTemplateName { get;  set; }
    public string CultureNameTranslationKey { get;  set; }
}
