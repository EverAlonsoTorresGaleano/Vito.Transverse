using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class GeneralTypeItem
{
    public long ListItemGroupFk { get; set; }

    public int? OrderIndex { get; set; }

    [Key]
    public long Id { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    public bool IsEnabled { get; set; }

    [InverseProperty("ActionTypeFkNavigation")]
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    [InverseProperty("AuditTypeFkNavigation")]
    public virtual ICollection<AuditRecord> AuditRecords { get; set; } = new List<AuditRecord>();

    [InverseProperty("AuditTypeFkNavigation")]
    public virtual ICollection<CompanyEntityAudit> CompanyEntityAudits { get; set; } = new List<CompanyEntityAudit>();

    [ForeignKey("ListItemGroupFk")]
    [InverseProperty("GeneralTypeItems")]
    public virtual GeneralTypeGroup ListItemGroupFkNavigation { get; set; } = null!;

    [InverseProperty("NotificationTypeFkNavigation")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("FileTypeFkNavigation")]
    public virtual ICollection<Picture> PictureFileTypeFkNavigations { get; set; } = new List<Picture>();

    [InverseProperty("PictureCategoryFkNavigation")]
    public virtual ICollection<Picture> PicturePictureCategoryFkNavigations { get; set; } = new List<Picture>();

    [InverseProperty("SequenceTypeFkNavigation")]
    public virtual ICollection<Sequence> Sequences { get; set; } = new List<Sequence>();
}
