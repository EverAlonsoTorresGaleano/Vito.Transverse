using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

//Developers
public record ComponentDTO
{
    [Required]
    public long ApplicationFk { get; set; }

    [Required]
    public long EndpointFk { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }

    [NotMapped]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;


    [NotMapped]
    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationKey { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string ObjectId { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string ObjectName { get; set; } = null!;

    [Required]
    [StringLength(75)]
    [Unicode(false)]
    public string ObjectPropertyName { get; set; } = null!;

    [Required]
    [StringLength(75)]
    [Unicode(false)]
    public string DefaultPropertyValue { get; set; } = null!;

    public long? PositionIndex { get; set; }

    //Localization
    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationValue { get; set; } = null!;

    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationValue { get; set; } = null!;

    //Extensions

    public string ApplicationNameTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}
