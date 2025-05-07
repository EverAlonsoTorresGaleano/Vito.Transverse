using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class GeneralTypeGroup
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public bool IsSystemType { get; set; }

    public virtual ICollection<GeneralTypeItem> GeneralTypeItems { get; set; } = new List<GeneralTypeItem>();
}
