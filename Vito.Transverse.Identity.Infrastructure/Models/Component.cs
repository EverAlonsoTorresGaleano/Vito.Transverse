using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Component
{
    public long ApplicationFk { get; set; }

    public long EndpointFk { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public string DescriptionTranslationKey { get; set; } = null!;

    public string ObjectId { get; set; } = null!;

    public string ObjectName { get; set; } = null!;

    public string ObjectPropertyName { get; set; } = null!;

    public string DefaultPropertyValue { get; set; } = null!;

    public long? PositionIndex { get; set; }

    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    public virtual Endpoint EndpointFkNavigation { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
