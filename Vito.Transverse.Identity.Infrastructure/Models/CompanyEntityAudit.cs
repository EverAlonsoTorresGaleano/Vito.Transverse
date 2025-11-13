using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class CompanyEntityAudit
{
    public long CompanyFk { get; set; }

    public long Id { get; set; }

    public long EntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? UpdatedByUserFk { get; set; }

    public bool IsActive { get; set; }

    public virtual GeneralTypeItem AuditTypeFkNavigation { get; set; } = null!;

    public virtual Company CompanyFkNavigation { get; set; } = null!;

    public virtual Entity EntityFkNavigation { get; set; } = null!;
}
