using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Page
{
    public long ModuleFk { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public bool? IsEnabled { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    public virtual Module ModuleFkNavigation { get; set; } = null!;

    public virtual ICollection<UserRolePermission> UserRolePermissions { get; set; } = new List<UserRolePermission>();
}
