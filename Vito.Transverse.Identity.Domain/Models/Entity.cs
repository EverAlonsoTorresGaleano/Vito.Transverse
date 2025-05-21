using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Entity
{
    public long Id { get; set; }

    public string SchemaName { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsSystemEntity { get; set; }

    public virtual ICollection<AuditRecord> AuditRecords { get; set; } = new List<AuditRecord>();

    public virtual ICollection<CompanyEntityAudit> CompanyEntityAudits { get; set; } = new List<CompanyEntityAudit>();

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
}
