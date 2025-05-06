using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class ListItem
{
    public long ListItemGroupFk { get; set; }

    public int? OrderIndex { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ListItemGroup ListItemGroupFkNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
