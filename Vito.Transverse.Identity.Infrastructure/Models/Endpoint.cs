using System;
using System.Collections.Generic;

namespace  Vito.Transverse.Identity.Infrastructure.Models;

public partial class Endpoint
{
    public long ApplicationFk { get; set; }

    public long ModuleFk { get; set; }

    public long Id { get; set; }

    public long? PositionIndex { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public string DescriptionTranslationKey { get; set; } = null!;

    public string EndpointUrl { get; set; } = null!;

    public string? Method { get; set; }

    public bool IsActive { get; set; }

    public bool IsVisible { get; set; }

    public bool IsApi { get; set; }

    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    public virtual Module ModuleFkNavigation { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
