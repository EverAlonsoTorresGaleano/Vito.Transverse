using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record MembershipTypeDTO
{
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

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Required]
    public bool IsActive { get; set; }


    //Create Update Time and User

    [NotMapped]
    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    [NotMapped]
    public long CreatedByUserFk { get; set; }


    [NotMapped]
    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    [NotMapped]
    public long? LastUpdateByUserFk { get; set; }


    //Localization
    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationValue { get; set; } = null!;

    [Required]
    [StringLength(85)]
    [Unicode(false)]
    public string DescriptionTranslationValue { get; set; } = null!;


}

