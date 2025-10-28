namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record PictureDTO
{
    public long CompanyFk { get; set; }

    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long EntityFk { get; set; }

    public long FileTypeFk { get; set; }

    public long PictureCategoryFk { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public byte[] BinaryPicture { get; set; } = null!;

    public decimal PictureSize { get; set; }


    public string PictureCategoryNameTranslationKey { get;  set; }=null!;
    public string FileTypeNameTranslationKey { get; set; } = null!; 
    public string CompanyNameTranslationKey { get; set; } = null!;  
    public long CompanyId { get;  set; }
    public string EntityName { get;  set; } = null!;
    public string EntitySchemaName { get;  set; } = null!;
}
