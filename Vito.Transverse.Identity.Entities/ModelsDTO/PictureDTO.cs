using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record PictureDTO
{
    [Required]
    public long CompanyFk { get; set; }

    [Required]
    [StringLength(75)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }

    
    public long? EntityFk { get; set; }

    [Required]
    public long FileTypeFk { get; set; }

    [Required]
    public long PictureCategoryFk { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public byte[]? BinaryPicture { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 5)")]
    public decimal PictureSize { get; set; }

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

    //Estensions
    public string PictureCategoryNameTranslationKey { get;  set; }=null!;
    public string FileTypeNameTranslationKey { get; set; } = null!; 
    public string CompanyNameTranslationKey { get; set; } = null!;  
    public long CompanyId { get;  set; }
    public string EntityName { get;  set; } = null!;
    public string EntitySchemaName { get;  set; } = null!;
}
