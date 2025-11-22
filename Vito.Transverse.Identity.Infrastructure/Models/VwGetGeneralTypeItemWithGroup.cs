using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[Keyless]
public partial class VwGetGeneralTypeItemWithGroup
{
    public long GeneralTypeGroupId { get; set; }

    [StringLength(100)]
    public string GeneralTypeGroupName { get; set; } = null!;

    public bool GeneralTypeGroup { get; set; }

    public bool IsSystemType { get; set; }

    public long GeneralTypeItemId { get; set; }

    [StringLength(100)]
    public string GeneralTypeName { get; set; } = null!;

    public int? GeneralTypeItemIndex { get; set; }

    public bool GeneralTypeItemIsEnabled { get; set; }
}
