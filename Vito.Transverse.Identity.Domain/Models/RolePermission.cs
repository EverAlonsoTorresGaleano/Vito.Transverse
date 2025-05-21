using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class RolePermission
{
    public long RoleFk { get; set; }

    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long Id { get; set; }

    public long ModuleFk { get; set; }

    public long? EndpointFk { get; set; }

    public long? ComponentFk { get; set; }

    public string? PropertyValue { get; set; }

    public virtual Component? ComponentFkNavigation { get; set; }

    public virtual Endpoint? EndpointFkNavigation { get; set; }

    public virtual Module ModuleFkNavigation { get; set; } = null!;

    public virtual Role RoleFkNavigation { get; set; } = null!;
}
