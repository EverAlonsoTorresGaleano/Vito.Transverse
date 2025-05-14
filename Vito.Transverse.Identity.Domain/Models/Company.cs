using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Company
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public Guid CompanyClient { get; set; }

    public Guid CompanySecret { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public string Subdomain { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string DefaultCultureFk { get; set; } = null!;

    public string CountryFk { get; set; } = null!;

    public bool IsSystemCompany { get; set; }

    public byte[]? Avatar { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AuditRecord> AuditRecords { get; set; } = new List<AuditRecord>();

    public virtual ICollection<CompanyEntityAudit> CompanyEntityAudits { get; set; } = new List<CompanyEntityAudit>();

    public virtual ICollection<CompanyMembership> CompanyMemberships { get; set; } = new List<CompanyMembership>();

    public virtual Country CountryFkNavigation { get; set; } = null!;

    public virtual Culture DefaultCultureFkNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Sequence> Sequences { get; set; } = new List<Sequence>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
