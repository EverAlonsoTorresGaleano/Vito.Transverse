using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class MembersipPriceHistory
{
    public long MembershipTypeFk { get; set; }

    public long Id { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal Price { get; set; }

    public decimal? LastPrice { get; set; }

    public decimal? LastIncreasePercentage { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public virtual MembershipType MembershipTypeFkNavigation { get; set; } = null!;
}
