using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[PrimaryKey("ApplicationFk", "CultureFk", "TranslationKey")]
[Index("ApplicationFk", Name = "IX_CultureTranslations")]
public partial class CultureTranslation
{
    [Key]
    public long ApplicationFk { get; set; }

    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string CultureFk { get; set; } = null!;

    [Key]
    [StringLength(85)]
    [Unicode(false)]
    public string TranslationKey { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string TranslationValue { get; set; } = null!;

    [ForeignKey("ApplicationFk")]
    [InverseProperty("CultureTranslations")]
    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    [ForeignKey("CultureFk")]
    [InverseProperty("CultureTranslations")]
    public virtual Culture CultureFkNavigation { get; set; } = null!;
}
