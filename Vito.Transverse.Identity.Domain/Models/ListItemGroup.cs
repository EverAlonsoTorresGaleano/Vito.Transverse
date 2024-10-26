using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class ListItemGroup
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public bool IsSystemType { get; set; }

    public virtual ICollection<ListItem> ListItems { get; set; } = new List<ListItem>();
}
