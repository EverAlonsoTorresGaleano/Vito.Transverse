using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public partial class CompanyEntityAuditDTO
{
    [Required]
    public long CompanyFk { get; set; }

    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity) ]
    public long Id { get; set; }

    [Required]
    public long EntityFk { get; set; }

    [Required]
    public long AuditTypeFk { get; set; }

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
    public DateTime? LastUpdatedDate { get; set; }

    [NotMapped]
    public long? LastUpdatedByUserFk { get; set; }

    //Extensions
    public string CompanyNameTranslationKey { get; set; } = null!;
    public string AuditTypeNameTranslationKey { get; set; } = null!;
    public string EntitySchemaName { get; set; } = null!;
    public string EntityName { get; set; } = null!;
}
