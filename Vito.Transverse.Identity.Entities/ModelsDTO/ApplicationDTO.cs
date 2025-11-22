using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record ApplicationDTO
{
    [Required]
    [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
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

    
    [NotMapped]
    public Guid ApplicationClient { get; set; }

    
    [NotMapped]
    public Guid ApplicationSecret { get; set; }
  
    public byte[]? Avatar { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public long OwnerFk { get; set; }

    [Required]
    public long ApplicationLicenseTypeFk { get; set; }

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

    //Estensions
    public long? ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
    public string ApplicationOwnerDescriptionTranslationKey { get; set; } = null!;

}
