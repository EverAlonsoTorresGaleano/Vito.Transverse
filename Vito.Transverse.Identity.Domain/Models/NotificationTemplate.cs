using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class NotificationTemplate
{
    public long Id { get; set; }

    public string CultureFk { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string SubjectTemplateText { get; set; } = null!;

    public string? MessageTemplateText { get; set; }

    public bool IsHtml { get; set; }

    public virtual Culture CultureFkNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
