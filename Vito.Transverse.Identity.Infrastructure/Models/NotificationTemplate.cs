using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[PrimaryKey("NotificationTemplateGroupId", "CultureFk")]
public partial class NotificationTemplate
{
    public long Id { get; set; }

    [Key]
    public long NotificationTemplateGroupId { get; set; }

    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string CultureFk { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public string SubjectTemplateText { get; set; } = null!;

    public string? MessageTemplateText { get; set; }

    public bool IsHtml { get; set; }

    [ForeignKey("CultureFk")]
    [InverseProperty("NotificationTemplates")]
    public virtual Culture CultureFkNavigation { get; set; } = null!;

    [InverseProperty("NotificationTemplate")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
