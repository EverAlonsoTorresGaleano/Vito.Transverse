using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Country
{
    public string Id { get; set; } = null!;

    public string NameTranslationKey { get; set; } = null!;

    public int? UtcHoursDifference { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<Culture> Cultures { get; set; } = new List<Culture>();
}
