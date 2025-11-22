using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[Keyless]
public partial class VwGetAuditRecord
{
    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    public long CompanyFk { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    public long UserFk { get; set; }

    [StringLength(30)]
    public string UserName { get; set; } = null!;

    public long AuditTypeFk { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string Expr1 { get; set; } = null!;

    public long EntityFk { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string SchemaName { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string EntityName { get; set; } = null!;

    public bool IsSystemEntity { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string HostName { get; set; } = null!;

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

    [Column(TypeName = "text")]
    public string AuditChanges { get; set; } = null!;
}
