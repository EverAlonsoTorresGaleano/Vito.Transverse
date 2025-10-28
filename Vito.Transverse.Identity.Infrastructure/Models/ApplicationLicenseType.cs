using System;
using System.Collections.Generic;

namespace  Vito.Transverse.Identity.Infrastructure.Models;

public partial class ApplicationLicenseType
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public string DescriptionTranslationKey { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public byte[]? LicenseFile { get; set; }

    public virtual ICollection<ApplicationOwner> ApplicationOwners { get; set; } = new List<ApplicationOwner>();
}
