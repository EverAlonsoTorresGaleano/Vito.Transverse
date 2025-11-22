using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

//developers
public partial class GeneralTypeItemDTO
{
    [Required]
    public long ListItemGroupFk { get; set; }

    public int? OrderIndex { get; set; }

    [Required]
    [Key]
    public long Id { get; set; }

    [NotMapped]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [Required]
    public bool IsEnabled { get; set; }

    //Localization
    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationValue { get; set; } = null!;
    


}