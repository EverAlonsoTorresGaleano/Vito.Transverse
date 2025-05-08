using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class VwGetGeneralTypeItemWithGroup
{
    public long GeneralTypeGroupId { get; set; }

    public string GeneralTypeGroupName { get; set; } = null!;

    public bool GeneralTypeGroup { get; set; }

    public bool IsSystemType { get; set; }

    public long GeneralTypeItemId { get; set; }

    public string GeneralTypeName { get; set; } = null!;

    public int? GeneralTypeItemIndex { get; set; }

    public bool GeneralTypeItemIsEnabled { get; set; }
}
