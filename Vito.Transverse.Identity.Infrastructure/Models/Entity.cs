using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Entity
{
    [Key]
    public long Id { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string SchemaName { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string EntityName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsSystemEntity { get; set; }

    [InverseProperty("EntityFkNavigation")]
    public virtual ICollection<AuditRecord> AuditRecords { get; set; } = new List<AuditRecord>();

    [InverseProperty("EntityFkNavigation")]
    public virtual ICollection<CompanyEntityAudit> CompanyEntityAudits { get; set; } = new List<CompanyEntityAudit>();

    [InverseProperty("EntityFkNavigation")]
    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
}
