using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[PrimaryKey("CompanyFk", "ApplicationFk")]
[Index("Id", Name = "IX_CompanyMemberships", IsUnique = true)]
public partial class CompanyMembership
{
    public long Id { get; set; }

    [Key]
    public long CompanyFk { get; set; }

    [Key]
    public long ApplicationFk { get; set; }

    public long MembershipTypeFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("ApplicationFk")]
    [InverseProperty("CompanyMemberships")]
    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    [ForeignKey("CompanyFk")]
    [InverseProperty("CompanyMemberships")]
    public virtual Company CompanyFkNavigation { get; set; } = null!;

    [InverseProperty("CompanyMembershipFkNavigation")]
    public virtual ICollection<CompanyMembershipPermission> CompanyMembershipPermissions { get; set; } = new List<CompanyMembershipPermission>();

    [ForeignKey("MembershipTypeFk")]
    [InverseProperty("CompanyMemberships")]
    public virtual MembershipType MembershipTypeFkNavigation { get; set; } = null!;
}
