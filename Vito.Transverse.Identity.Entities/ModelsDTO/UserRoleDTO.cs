using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record UserRoleDTO
{
    [Required]
    public long CompanyFk { get; set; }

    [Required]
    public long ApplicationFk { get; set; }

    [Required]
    [Key]
    public long UserFk { get; set; }

    [Required]
    [Key]
    public long RoleFk { get; set; }

    [Required]
    public bool IsActive { get; set; }

    //Create Update Time and User

    [NotMapped]
    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [NotMapped]
    public long CreatedByUserFk { get; set; }


    [NotMapped]
    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    [NotMapped]
    public long? LastUpdateByUserFk { get; set; }



    //Extension
    public string UserName { get; set; } = null!;
    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string RoleNameTranslationKey { get; set; } = null!;
    public string CompanyNameTranslationKey { get; set; } = null!;
    public string RoleDescriptionTranslationKey { get; set; } = null!;
    public string CompanyDescriptionTranslationKey { get; set; } = null!;
    public string ApplicationDescriptionTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}
