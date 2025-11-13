using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Language
{
    public string Id { get; set; } = null!;

    public string NameTranslationKey { get; set; } = null!;

    public virtual ICollection<Culture> Cultures { get; set; } = new List<Culture>();
}
