using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class GeneralTypeGroup
{
    [Key]
    public long Id { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    public bool IsSystemType { get; set; }

    [InverseProperty("ListItemGroupFkNavigation")]
    public virtual ICollection<GeneralTypeItem> GeneralTypeItems { get; set; } = new List<GeneralTypeItem>();
}
