using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record CompanyDTO
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    public Guid CompanyClient { get; set; }

  
    [NotMapped]
    public Guid CompanySecret { get; set; }

    [Required]
    [StringLength(150)]
    [Unicode(false)]
    public string Subdomain { get; set; } = null!;

    [Required]
    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(5)]
    [Unicode(false)]
    public string DefaultCultureFk { get; set; } = null!;

    [Required]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryFk { get; set; } = null!;

    [Required]
    public bool IsSystemCompany { get; set; }

    public byte[]? Avatar { get; set; }


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

    //Extensions
    public string CountryNameTranslationKey { get; set; } = null!;
    public string LanguageNameTranslationKey { get; set; } = null!;
}