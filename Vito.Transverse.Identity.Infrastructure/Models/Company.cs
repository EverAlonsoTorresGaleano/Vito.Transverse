using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Company
{
    [Key]
    public long Id { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationKey { get; set; } = null!;

    public Guid CompanyClient { get; set; }

    public Guid CompanySecret { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Subdomain { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string DefaultCultureFk { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string CountryFk { get; set; } = null!;

    public bool IsSystemCompany { get; set; }

    public byte[]? Avatar { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("OwnerFkNavigation")]
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    [InverseProperty("CompanyFkNavigation")]
    public virtual ICollection<AuditRecord> AuditRecords { get; set; } = new List<AuditRecord>();

    [InverseProperty("CompanyFkNavigation")]
    public virtual ICollection<CompanyEntityAudit> CompanyEntityAudits { get; set; } = new List<CompanyEntityAudit>();

    [InverseProperty("CompanyFkNavigation")]
    public virtual ICollection<CompanyMembership> CompanyMemberships { get; set; } = new List<CompanyMembership>();

    [ForeignKey("CountryFk")]
    [InverseProperty("Companies")]
    public virtual Country CountryFkNavigation { get; set; } = null!;

    [ForeignKey("DefaultCultureFk")]
    [InverseProperty("Companies")]
    public virtual Culture DefaultCultureFkNavigation { get; set; } = null!;

    [InverseProperty("CompanyFkNavigation")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("CompanyFkNavigation")]
    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    [InverseProperty("CompanyFkNavigation")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    [InverseProperty("CompanyFkNavigation")]
    public virtual ICollection<Sequence> Sequences { get; set; } = new List<Sequence>();

    [InverseProperty("CompanyFkNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
