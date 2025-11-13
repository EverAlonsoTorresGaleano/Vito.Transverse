namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record ModuleDTO
{
    public long ApplicationFk { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public long? PositionIndex { get; set; }

    public bool IsActive { get; set; }

    public bool IsVisible { get; set; }

    public bool IsApi { get; set; }

    public string IconName { get; set; } = null!;

    public List<EndpointDTO> Endpoints { get; set; } = new();

    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string DescriptionTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}
