using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[Keyless]
public partial class VwGetCompanyMembership
{
    public long CompanyMemberShipId { get; set; }

    public long MembershipTypeFk { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string MembershipTypeName { get; set; } = null!;

    public bool MembershipTypeIsActive { get; set; }

    public long CompanyId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string CompanyName { get; set; } = null!;

    public Guid CompanyClient { get; set; }

    public Guid CompanySecret { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Subdomain { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public bool CompanyIsActive { get; set; }

    public bool IsSystemCompany { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string DefaultCultureFk { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string CountryFk { get; set; } = null!;

    public long ApplicationId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string ApplicationName { get; set; } = null!;

    public bool ApplicationIsActive { get; set; }

    public Guid ApplicationClient { get; set; }

    public Guid ApplicationSecret { get; set; }
}
