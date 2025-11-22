using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Country
{
    [Key]
    [StringLength(2)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    public int? UtcHoursDifference { get; set; }

    [InverseProperty("CountryFkNavigation")]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    [InverseProperty("CountryFkNavigation")]
    public virtual ICollection<Culture> Cultures { get; set; } = new List<Culture>();
}
