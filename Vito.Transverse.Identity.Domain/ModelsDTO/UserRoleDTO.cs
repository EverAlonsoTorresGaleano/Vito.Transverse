namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public class UserRoleDTO
{
    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long UserFk { get; set; }

    public long RoleFk { get; set; }

    public DateTime CreatedDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public bool IsActive { get; set; }




    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string RoleNameTranslationKey { get; set; } = null!;
    public string CompanyNameTranslationKey { get; set; } = null!;
}
