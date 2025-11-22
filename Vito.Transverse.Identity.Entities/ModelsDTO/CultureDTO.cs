using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public partial class CultureDTO
{
    [Required]
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [NotMapped]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [Required]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryFk { get; set; } = null!;

    [Required]
    [StringLength(2)]
    [Unicode(false)]
    public string LanguageFk { get; set; } = null!;

    [Required]
    public bool IsEnabled { get; set; }

    //Localization
    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationValue { get; set; } = null!;


}
