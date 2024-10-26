using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class NotificationTemplate
{
    public string CultureFk { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string TemplateText { get; set; } = null!;

    public virtual Culture CultureFkNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
