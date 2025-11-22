using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Endpoint
{
    public long ApplicationFk { get; set; }

    public long ModuleFk { get; set; }

    [Key]
    public long Id { get; set; }

    public long? PositionIndex { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? IconName { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationKey { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string EndpointUrl { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? Method { get; set; }

    public bool IsActive { get; set; }

    public bool IsVisible { get; set; }

    public bool IsApi { get; set; }

    [ForeignKey("ApplicationFk")]
    [InverseProperty("Endpoints")]
    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    [InverseProperty("EndpointFkNavigation")]
    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    [ForeignKey("ModuleFk")]
    [InverseProperty("Endpoints")]
    public virtual Module ModuleFkNavigation { get; set; } = null!;

    [InverseProperty("EndpointFkNavigation")]
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
