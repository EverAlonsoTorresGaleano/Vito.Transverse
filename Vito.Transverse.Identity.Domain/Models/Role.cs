using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Role
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public virtual ICollection<UserRolePermission> UserRolePermissions { get; set; } = new List<UserRolePermission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
