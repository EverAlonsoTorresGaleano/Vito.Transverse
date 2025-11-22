using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[PrimaryKey("UserFk", "RoleFk")]
public partial class UserRole
{
    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    [Key]
    public long UserFk { get; set; }

    [Key]
    public long RoleFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long CreatedByUserFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("RoleFk")]
    [InverseProperty("UserRoles")]
    public virtual Role RoleFkNavigation { get; set; } = null!;

    [ForeignKey("UserFk")]
    [InverseProperty("UserRoles")]
    public virtual User UserFkNavigation { get; set; } = null!;
}
