using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class CompanyMembershipPermission
{
    public long CompanyMembershipFk { get; set; }

    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long Id { get; set; }

    public long ModuleFk { get; set; }

    public long PageFk { get; set; }

    public long? ComponentFk { get; set; }

    public string? PropertyValue { get; set; }

    public virtual CompanyMembership CompanyMembershipFkNavigation { get; set; } = null!;
}
