namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public record CompanyMembershipsDTO
{
    public long Id { get; set; }

    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long MembershipTypeFk { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }


    public string MembershipTypeNameTranslationKey { get; set; } = null!;
    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string CompanyNameTranslationKey { get; set; } = null!;
    public string DescriptionTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}
