using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Application
{
    [Key]
    public long Id { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationKey { get; set; } = null!;

    public Guid ApplicationClient { get; set; }

    public Guid ApplicationSecret { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public byte[]? Avatar { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public long OwnerFk { get; set; }

    public long ApplicationLicenseTypeFk { get; set; }

    [ForeignKey("ApplicationLicenseTypeFk")]
    [InverseProperty("Applications")]
    public virtual ApplicationLicenseType ApplicationLicenseTypeFkNavigation { get; set; } = null!;

    [InverseProperty("ApplicationFkNavigation")]
    public virtual ICollection<CompanyMembership> CompanyMemberships { get; set; } = new List<CompanyMembership>();

    [InverseProperty("ApplicationFkNavigation")]
    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    [InverseProperty("ApplicationFkNavigation")]
    public virtual ICollection<CultureTranslation> CultureTranslations { get; set; } = new List<CultureTranslation>();

    [InverseProperty("ApplicationFkNavigation")]
    public virtual ICollection<Endpoint> Endpoints { get; set; } = new List<Endpoint>();

    [InverseProperty("ApplicationFkNavigation")]
    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();

    [ForeignKey("OwnerFk")]
    [InverseProperty("Applications")]
    public virtual Company OwnerFkNavigation { get; set; } = null!;

    [InverseProperty("ApplicationFkNavigation")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    [InverseProperty("ApplicationFkNavigation")]
    public virtual ICollection<Sequence> Sequences { get; set; } = new List<Sequence>();
}
