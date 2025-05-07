using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Application
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid ApplicationClient { get; set; }

    public Guid ApplicationSecret { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public byte[]? Avatar { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CompanyMembership> CompanyMemberships { get; set; } = new List<CompanyMembership>();

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    public virtual ICollection<CultureTranslation> CultureTranslations { get; set; } = new List<CultureTranslation>();

    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Sequence> Sequences { get; set; } = new List<Sequence>();
}
