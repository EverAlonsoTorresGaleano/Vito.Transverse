using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Module
{
    public long ApplicationFk { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public string DescriptionTranslationKey { get; set; } = null!;

    public long? PositionIndex { get; set; }

    public bool IsActive { get; set; }

    public bool IsVisible { get; set; }

    public bool IsApi { get; set; }

    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    public virtual ICollection<Endpoint> Endpoints { get; set; } = new List<Endpoint>();

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
