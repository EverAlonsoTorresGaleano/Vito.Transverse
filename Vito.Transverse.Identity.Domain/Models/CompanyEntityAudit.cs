using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class CompanyEntityAudit
{
    public long CompanyFk { get; set; }

    public long Id { get; set; }

    public long AuditEntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    public virtual AuditEntity AuditEntityFkNavigation { get; set; } = null!;

    public virtual GeneralTypeItem AuditTypeFkNavigation { get; set; } = null!;

    public virtual Company CompanyFkNavigation { get; set; } = null!;
}
