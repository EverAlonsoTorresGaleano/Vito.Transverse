using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Component
{
    public long PageFk { get; set; }

    public long Id { get; set; }

    public string ComponentName { get; set; } = null!;

    public string ComponentId { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public virtual Page PageFkNavigation { get; set; } = null!;

    public virtual ICollection<UserRolePermission> UserRolePermissions { get; set; } = new List<UserRolePermission>();
}
