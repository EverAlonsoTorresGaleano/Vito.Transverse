using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Notification
{
    public string? NotificationTemplateFk { get; set; }

    public string? CultureFk { get; set; }

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

    public long NotificationTypeFk { get; set; }

    public virtual Culture? CultureFkNavigation { get; set; }

    public virtual NotificationTemplate? NotificationTemplateFkNavigation { get; set; }

    public virtual ListItem NotificationTypeFkNavigation { get; set; } = null!;
}
