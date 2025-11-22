using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Module
{
    public long ApplicationFk { get; set; }

    [Key]
    public long Id { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? IconName { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationKey { get; set; } = null!;

    public long? PositionIndex { get; set; }

    public bool IsActive { get; set; }

    public bool IsVisible { get; set; }

    public bool IsApi { get; set; }

    [ForeignKey("ApplicationFk")]
    [InverseProperty("Modules")]
    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    [InverseProperty("ModuleFkNavigation")]
    public virtual ICollection<Endpoint> Endpoints { get; set; } = new List<Endpoint>();

    [InverseProperty("ModuleFkNavigation")]
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
