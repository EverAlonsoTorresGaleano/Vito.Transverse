using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class GeneralTypeItem
{
    public long ListItemGroupFk { get; set; }

    public int? OrderIndex { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<AuditRecord> AuditRecords { get; set; } = new List<AuditRecord>();

    public virtual ICollection<CompanyEntityAudit> CompanyEntityAudits { get; set; } = new List<CompanyEntityAudit>();

    public virtual GeneralTypeGroup ListItemGroupFkNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Picture> PictureFileTypeFkNavigations { get; set; } = new List<Picture>();

    public virtual ICollection<Picture> PicturePictureCategoryFkNavigations { get; set; } = new List<Picture>();
}
