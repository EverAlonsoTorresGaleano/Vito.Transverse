using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class MembershipType
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CompanyMembership> CompanyMemberships { get; set; } = new List<CompanyMembership>();

    public virtual ICollection<MembersipPriceHistory> MembersipPriceHistories { get; set; } = new List<MembersipPriceHistory>();
}
