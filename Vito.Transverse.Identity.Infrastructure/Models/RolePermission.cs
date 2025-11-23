using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class RolePermission
{
    public long RoleFk { get; set; }

    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    [Key]
    public long Id { get; set; }

    public long ModuleFk { get; set; }

    public long? EndpointFk { get; set; }

    public long? ComponentFk { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string? PropertyValue { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Obs { get; set; }

    
    public bool? CanView { get; set; }

    public bool? CanCreate { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }

    [ForeignKey("ComponentFk")]
    [InverseProperty("RolePermissions")]
    public virtual Component? ComponentFkNavigation { get; set; }

    [ForeignKey("EndpointFk")]
    [InverseProperty("RolePermissions")]
    public virtual Endpoint? EndpointFkNavigation { get; set; }

    [ForeignKey("ModuleFk")]
    [InverseProperty("RolePermissions")]
    public virtual Module ModuleFkNavigation { get; set; } = null!;

    [ForeignKey("RoleFk")]
    [InverseProperty("RolePermissions")]
    public virtual Role RoleFkNavigation { get; set; } = null!;
}
