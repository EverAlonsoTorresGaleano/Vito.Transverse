using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class ActivityLog
{
    public long CompanyFk { get; set; }

    public long UserFk { get; set; }

    [Key]
    public long TraceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EventDate { get; set; }

    [StringLength(50)]
    public string DeviceName { get; set; } = null!;

    [StringLength(50)]
    public string DeviceType { get; set; } = null!;

    public long ActionTypeFk { get; set; }

    [StringLength(50)]
    public string IpAddress { get; set; } = null!;

    [StringLength(50)]
    public string Browser { get; set; } = null!;

    [StringLength(50)]
    public string Platform { get; set; } = null!;

    [StringLength(50)]
    public string Engine { get; set; } = null!;

    [StringLength(50)]
    public string CultureId { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string EndPointUrl { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Method { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string QueryString { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string UserAgent { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Referer { get; set; } = null!;

    public long ApplicationId { get; set; }

    public long RoleId { get; set; }

    [ForeignKey("ActionTypeFk")]
    [InverseProperty("ActivityLogs")]
    public virtual GeneralTypeItem ActionTypeFkNavigation { get; set; } = null!;

    [ForeignKey("UserFk")]
    [InverseProperty("ActivityLogs")]
    public virtual User UserFkNavigation { get; set; } = null!;
}
