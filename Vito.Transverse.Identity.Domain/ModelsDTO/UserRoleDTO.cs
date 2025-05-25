namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public record UserRoleDTO
{
    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long UserFk { get; set; }

    public long RoleFk { get; set; }

    public DateTime CreatedDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public bool IsActive { get; set; }



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
