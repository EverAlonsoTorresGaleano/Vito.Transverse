namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public record ActivityLogDTO
{

    public long CompanyFk { get; set; }

    public long UserFk { get; set; }

    public long TraceId { get; set; }

    public DateTime EventDate { get; set; }

    public string DeviceName { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public long ActionTypeFk { get; set; }

    public string IpAddress { get; set; } = null!;

    public string Browser { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Engine { get; set; } = null!;

    public string CultureId { get; set; } = null!;

    public string EndPointUrl { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string JwtToken { get; set; } = null!;

    public string AddtionalInformation { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string CompanyNameTranslationKey { get; set; } = null!;
    public string ActionTypeNameTranslationKey { get; set; } = null!;
    public string CompanyDescriptionTranslationKey { get; set; } = null!;
}
