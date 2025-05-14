namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public class ApplicationDTO
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public Guid ApplicationClient { get; set; }

    public Guid ApplicationSecret { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public byte[]? Avatar { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }





    public long CompanyId { get;  set; }
    public string CompanyNameTranslationKey { get; set; } = null!;
}
