using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Role
{
    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    [Key]
    public long Id { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationKey { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    [ForeignKey("ApplicationFk")]
    [InverseProperty("Roles")]
    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    [ForeignKey("CompanyFk")]
    [InverseProperty("Roles")]
    public virtual Company CompanyFkNavigation { get; set; } = null!;

    [InverseProperty("RoleFkNavigation")]
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    [InverseProperty("RoleFkNavigation")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
