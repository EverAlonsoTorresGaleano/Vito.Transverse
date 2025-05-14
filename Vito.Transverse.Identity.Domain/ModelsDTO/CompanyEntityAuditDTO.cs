namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public partial class CompanyEntityAuditDTO
{
    public long CompanyFk { get; set; }

    public long Id { get; set; }

    public long AuditEntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? UpdatedByUserFk { get; set; }

    public bool IsActive { get; set; }



    public string CompanyNameTranslationKey { get; set; } = null!;
    public string AuditTypeNameTranslationKey { get; set; } = null!;
    public string AuditEntitySchemaName { get; set; } = null!;
    public string AuditEntityName { get; set; } = null!;
}
