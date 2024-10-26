using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class UserRolePermission
{
    public long UserRoleFk { get; set; }

    public long Id { get; set; }

    public long ModuleFk { get; set; }

    public long? PageFk { get; set; }

    public long? ComponentFk { get; set; }

    public bool? IsEnabled { get; set; }

    public bool? IsVisible { get; set; }

    public virtual Component? ComponentFkNavigation { get; set; }

    public virtual Module ModuleFkNavigation { get; set; } = null!;

    public virtual Page? PageFkNavigation { get; set; }

    public virtual Role UserRoleFkNavigation { get; set; } = null!;
}
