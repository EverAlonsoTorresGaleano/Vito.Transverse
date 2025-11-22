using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Language
{
    [Key]
    [StringLength(2)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [InverseProperty("LanguageFkNavigation")]
    public virtual ICollection<Culture> Cultures { get; set; } = new List<Culture>();
}
