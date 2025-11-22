using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

//REand ONlu
public record ActivityLogDTO
{
    public long CompanyFk { get; set; }

    public long UserFk { get; set; }

    [Key]
    public long TraceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EventDate { get; set; }

    [StringLength(50)]
    public string DeviceName { get; set; } = null!;

    [StringLength(50)]
    public string DeviceType { get; set; } = null!;

    public long ActionTypeFk { get; set; }

    [StringLength(50)]
    public string IpAddress { get; set; } = null!;

    [StringLength(50)]
    public string Browser { get; set; } = null!;

    [StringLength(50)]
    public string Platform { get; set; } = null!;

    [StringLength(50)]
    public string Engine { get; set; } = null!;

    [StringLength(50)]
    public string CultureId { get; set; } = null!;

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


    //Estensions
    public string UserName { get; set; } = null!;
    public string CompanyNameTranslationKey { get; set; } = null!;
    public string ActionTypeNameTranslationKey { get; set; } = null!;
    public string CompanyDescriptionTranslationKey { get; set; } = null!;
}
