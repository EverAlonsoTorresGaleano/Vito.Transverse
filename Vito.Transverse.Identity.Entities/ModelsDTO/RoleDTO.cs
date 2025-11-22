using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record RoleDTO
{
    [Required]
    public long CompanyFk { get; set; }

    [Required]
    public long ApplicationFk { get; set; }

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

    //Extension
    public List<ModuleDTO> Modules { get; set; } = new();

    public string CompanyNameTranslationKey { get; set; } = null!;
    public string ApplicationNameTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}