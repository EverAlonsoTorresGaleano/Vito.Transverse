namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public partial class CompanyEntityAuditDTO
{
    public long CompanyFk { get; set; }

    public long Id { get; set; }

    public long EntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? UpdatedByUserFk { get; set; }

    public bool IsActive { get; set; }



    public string CompanyNameTranslationKey { get; set; } = null!;
    public string AuditTypeNameTranslationKey { get; set; } = null!;
    public string EntitySchemaName { get; set; } = null!;
    public string EntityName { get; set; } = null!;
}
