using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Component
{
    public long ApplicationFk { get; set; }

    public long EndpointFk { get; set; }

    [Key]
    public long Id { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationKey { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string? ObjectId { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string? ObjectName { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string ObjectPropertyName { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string DefaultPropertyValue { get; set; } = null!;

    public long? PositionIndex { get; set; }

    [ForeignKey("ApplicationFk")]
    [InverseProperty("Components")]
    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    [ForeignKey("EndpointFk")]
    [InverseProperty("Components")]
    public virtual Endpoint EndpointFkNavigation { get; set; } = null!;

    [InverseProperty("ComponentFkNavigation")]
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
