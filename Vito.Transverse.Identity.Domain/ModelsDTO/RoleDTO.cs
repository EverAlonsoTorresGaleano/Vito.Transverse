namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public record RoleDTO
{
    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public List<ModuleDTO> Modules { get; set; } = new();



    public string CompanyNameTranslationKey { get; set; } = null!;
    public string ApplicationNameTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}