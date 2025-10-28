namespace Vito.Transverse.Identity.Entities.Options;

public record EncryptionSettingsOptions
{
    public const string SectionName = "EncryptionSettings";
    public string EncryptionKey { get; set; } = "";
    public string EncryptionKernel{ get; set; } = "";
}