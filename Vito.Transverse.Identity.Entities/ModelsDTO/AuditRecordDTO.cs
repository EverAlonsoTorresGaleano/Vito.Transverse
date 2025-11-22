using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

//User Read Only
public record AuditRecordDTO
{
    public long CompanyFk { get; set; }

    [Key]
    public long Id { get; set; }

    public long UserFk { get; set; }

    public long EntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string AuditEntityIndex { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string HostName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string IpAddress { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string DeviceType { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Browser { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Platform { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Engine { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string CultureFk { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string EndPointUrl { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Method { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string QueryString { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string UserAgent { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Referer { get; set; } = null!;

    public long ApplicationId { get; set; }

    public long RoleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    [Column(TypeName = "text")]
    public string AuditChanges { get; set; } = null!;

    //Extensions

    public string auditEntitySchemaName { get; set; } = null!;
    public string AuditEntityName { get; set; } = null!;
    public string AuditTypeNameTranslationKey { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string CompanyNameTranslationKey { get; set; } = null!;
    public string CompanyDescriptionTranslationKey { get; set; } = null!;
}
