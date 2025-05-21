using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Role
{
    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public string DescriptionTranslationKey { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    public virtual Company CompanyFkNavigation { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
