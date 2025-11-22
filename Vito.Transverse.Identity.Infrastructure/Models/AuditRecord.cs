using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class AuditRecord
{
    public long CompanyFk { get; set; }

    [Key]
    public long Id { get; set; }

    public long UserFk { get; set; }

    public long EntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string AuditEntityIndex { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string HostName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string IpAddress { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string DeviceType { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Browser { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Platform { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Engine { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string CultureFk { get; set; } = null!;

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

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    [Column(TypeName = "text")]
    public string AuditChanges { get; set; } = null!;

    [ForeignKey("AuditTypeFk")]
    [InverseProperty("AuditRecords")]
    public virtual GeneralTypeItem AuditTypeFkNavigation { get; set; } = null!;

    [ForeignKey("CompanyFk")]
    [InverseProperty("AuditRecords")]
    public virtual Company CompanyFkNavigation { get; set; } = null!;

    [ForeignKey("EntityFk")]
    [InverseProperty("AuditRecords")]
    public virtual Entity EntityFkNavigation { get; set; } = null!;

    [ForeignKey("UserFk")]
    [InverseProperty("AuditRecords")]
    public virtual User UserFkNavigation { get; set; } = null!;
}
