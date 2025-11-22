using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record ModuleDTO
{
    [Required]
    public long ApplicationFk { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? IconName { get; set; }

    [NotMapped]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [NotMapped]
    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationKey { get; set; } = null!;

    public long? PositionIndex { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public bool IsVisible { get; set; }

    [Required]
    public bool IsApi { get; set; }


    //Localization
    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationValue { get; set; } = null!;
    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationValue { get; set; } = null!;


    //Extension
    public List<EndpointDTO> Endpoints { get; set; } = new();

    public string ApplicationNameTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}
