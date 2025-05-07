namespace Vito.Transverse.Identity.Domain.Options;

public record EncryptionSettingsOptions
{
    public const string SectionName = "EncryptionSettings";
    public string EncryptionKey { get; set; } = "";
    public string EncryptionKernel{ get; set; } = "";
}