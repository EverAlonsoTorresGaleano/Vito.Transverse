using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[Table("MembersipPriceHistory")]
public partial class MembersipPriceHistory
{
    public long MembershipTypeFk { get; set; }

    [Key]
    public long Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? LastPrice { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? LastIncreasePercentage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    [ForeignKey("MembershipTypeFk")]
    [InverseProperty("MembersipPriceHistories")]
    public virtual MembershipType MembershipTypeFkNavigation { get; set; } = null!;
}
