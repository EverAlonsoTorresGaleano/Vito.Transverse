namespace Vito.Transverse.Identity.Entities.Options;

public record TwilioSettingsOptions
{
    public const string SectionName = "TwilioSettings";
    public string AccountSid { get; set; } = "";
    public string AuthToken { get; set; } = "";
    public string FromPhoneNumber { get; set; } = "";
}