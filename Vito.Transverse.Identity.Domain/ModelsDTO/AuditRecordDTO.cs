namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public partial class AuditRecordDTO
{


    public long CompanyFk { get; set; }

    public long Id { get; set; }

    public long UserFk { get; set; }

    public long AuditEntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    public string AuditEntityIndex { get; set; } = null!;

    public string HostName { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public string? DeviceType { get; set; }

    public string Browser { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Engine { get; set; } = null!;

    public string CultureFk { get; set; } = null!;

    public string AuditInfoJson { get; set; } = null!;

    public DateTime CreationDate { get; set; }



    public string auditEntitySchemaName { get; set; } = null!;
    public string AuditEntityName { get; set; } = null!;
    public string AuditTypeNameTranslationKey { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string CompanyNameTranslationKey { get; set; } = null!;
}
