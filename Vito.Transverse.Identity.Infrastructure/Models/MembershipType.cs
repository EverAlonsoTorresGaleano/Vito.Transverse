using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class MembershipType
{
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

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("MembershipTypeFkNavigation")]
    public virtual ICollection<CompanyMembership> CompanyMemberships { get; set; } = new List<CompanyMembership>();

    [InverseProperty("MembershipTypeFkNavigation")]
    public virtual ICollection<MembersipPriceHistory> MembersipPriceHistories { get; set; } = new List<MembersipPriceHistory>();
}
