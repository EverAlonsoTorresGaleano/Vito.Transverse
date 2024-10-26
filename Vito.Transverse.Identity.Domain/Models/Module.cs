using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Module
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual ICollection<UserRolePermission> UserRolePermissions { get; set; } = new List<UserRolePermission>();
}
