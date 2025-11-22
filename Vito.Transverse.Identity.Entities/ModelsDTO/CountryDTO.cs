using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public partial class CountryDTO
{
    [Required]
    [Key]
    [StringLength(2)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [NotMapped]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    public int? UtcHoursDifference { get; set; }

    //Localization
    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationValue { get; set; } = null!;
    
}





