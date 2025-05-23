namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public record CompanyDTO
{
    public long Id { get; set; }
    public string NameTranslationKey { get; set; } = null!;
    public string DescriptionTranslationKey { get; set; } = null!;
    public Guid CompanyClient { get; set; }
    public Guid CompanySecret { get; set; }
    public DateTime CreationDate { get; set; }
    public long CreatedByUserFk { get; set; }
    public string Subdomain { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string DefaultCultureFk { get; set; } = null!;
    public string CountryFk { get; set; } = null!;
    public bool IsSystemCompany { get; set; }
    public byte[]? Avatar { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public long? LastUpdateByUserFk { get; set; }
    public bool IsActive { get; set; }
    public string CountryNameTranslationKey { get; set; } = null!;
    public string LanguageNameTranslationKey { get; set; } = null!;
}