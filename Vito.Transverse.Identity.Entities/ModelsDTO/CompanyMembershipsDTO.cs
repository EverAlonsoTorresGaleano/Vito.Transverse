using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record CompanyMembershipsDTO
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public long Id { get; set; }

    [Required]
    [Key]
    public long CompanyFk { get; set; }

    [Required]
    [Key]
    public long ApplicationFk { get; set; }

    [Required]
    public long MembershipTypeFk { get; set; }    

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


    //Extension
    public string MembershipTypeNameTranslationKey { get; set; } = null!;
    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string CompanyNameTranslationKey { get; set; } = null!;
    public string DescriptionTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}
