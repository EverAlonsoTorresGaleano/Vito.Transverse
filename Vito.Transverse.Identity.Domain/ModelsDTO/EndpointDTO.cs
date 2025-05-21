namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public record EndpointDTO
{
    public long ApplicationFk { get; set; }

    public long ModuleFk { get; set; }

    public long Id { get; set; }

    public long? PositionIndex { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public string EndpointUrl { get; set; } = null!;

    public string Method { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsVisible { get; set; }

    public bool IsApi { get; set; }

    public List<ComponentDTO> Components { get; set; } = new();

    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string DescriptionTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}
