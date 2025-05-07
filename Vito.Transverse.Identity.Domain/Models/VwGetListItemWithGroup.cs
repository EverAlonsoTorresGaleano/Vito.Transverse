using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class VwGetListItemWithGroup
{
    public long ListItenGroupId { get; set; }

    public string ListItemGroupName { get; set; } = null!;

    public bool ListItemGroupIsSystemType { get; set; }

    public long ListItemId { get; set; }

    public string ListItemName { get; set; } = null!;

    public int? ListItemIndex { get; set; }

    public bool ListItemIsEnabled { get; set; }
}
