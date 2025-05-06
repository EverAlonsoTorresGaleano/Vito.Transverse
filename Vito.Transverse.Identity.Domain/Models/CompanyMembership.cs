using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class CompanyMembership
{
    public long Id { get; set; }

    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long MembershipTypeFk { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    public virtual Company CompanyFkNavigation { get; set; } = null!;

    public virtual ICollection<CompanyMembershipPermission> CompanyMembershipPermissions { get; set; } = new List<CompanyMembershipPermission>();

    public virtual MembershipType MembershipTypeFkNavigation { get; set; } = null!;
}
