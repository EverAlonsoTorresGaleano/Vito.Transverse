using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public class CultureTranslationDTO
{
    [Required]
    [Key]
    public long ApplicationFk { get; set; }

    [Required]
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string CultureFk { get; set; } = null!;

    [Required]
    [Key]
    [StringLength(85)]
    [Unicode(false)]
    public string TranslationKey { get; set; } = null!;

    [Required]
    [StringLength(250)]
    [Unicode(false)]
    public string TranslationValue { get; set; } = null!;




    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string LanguageNameTranslationKey { get; set; } = null!;
}

