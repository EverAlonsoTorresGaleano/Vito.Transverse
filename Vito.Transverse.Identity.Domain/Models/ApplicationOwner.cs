using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class ApplicationOwner
{
    public long Id { get; set; }

    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long? ApplicationLicenseTypeFk { get; set; }

    public DateTime CreatedDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    public virtual ApplicationLicenseType? ApplicationLicenseTypeFkNavigation { get; set; }

    public virtual Company CompanyFkNavigation { get; set; } = null!;
}
