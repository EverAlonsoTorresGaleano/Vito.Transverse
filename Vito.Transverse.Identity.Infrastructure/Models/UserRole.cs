using System;
using System.Collections.Generic;

namespace  Vito.Transverse.Identity.Infrastructure.Models;

public partial class UserRole
{
    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long UserFk { get; set; }

    public long RoleFk { get; set; }

    public DateTime CreatedDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public bool IsActive { get; set; }

    public virtual Role RoleFkNavigation { get; set; } = null!;

    public virtual User UserFkNavigation { get; set; } = null!;
}
