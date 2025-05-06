using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class VwGetCompanyMembership
{
    public long CompanyMemberShipId { get; set; }

    public long MembershipTypeFk { get; set; }

    public string MembershipTypeName { get; set; } = null!;

    public bool MembershipTypeIsActive { get; set; }

    public long CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Subdomain { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool CompanyIsActive { get; set; }

    public bool IsSystemCompany { get; set; }

    public string DefaultCultureFk { get; set; } = null!;

    public string CountryFk { get; set; } = null!;

    public long ApplicationId { get; set; }

    public string ApplicationName { get; set; } = null!;

    public bool ApplicationIsActive { get; set; }
}
