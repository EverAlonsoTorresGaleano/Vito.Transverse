using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Notification
{
    public long CompanyFk { get; set; }

    public long NotificationTemplateGroupFk { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string CultureFk { get; set; } = null!;

    public long NotificationTypeFk { get; set; }

    [Key]
    public long Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string Sender { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Receiver { get; set; } = null!;

    [Column("CC")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Cc { get; set; }

    [Column("BCC")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Bcc { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Subject { get; set; } = null!;

    [Unicode(false)]
    public string Message { get; set; } = null!;

    public bool IsSent { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SentDate { get; set; }

    public bool IsHtml { get; set; }

    [ForeignKey("CompanyFk")]
    [InverseProperty("Notifications")]
    public virtual Company CompanyFkNavigation { get; set; } = null!;

    [ForeignKey("CultureFk")]
    [InverseProperty("Notifications")]
    public virtual Culture CultureFkNavigation { get; set; } = null!;

    [ForeignKey("NotificationTemplateGroupFk, CultureFk")]
    [InverseProperty("Notifications")]
    public virtual NotificationTemplate NotificationTemplate { get; set; } = null!;

    [ForeignKey("NotificationTypeFk")]
    [InverseProperty("Notifications")]
    public virtual GeneralTypeItem NotificationTypeFkNavigation { get; set; } = null!;
}
