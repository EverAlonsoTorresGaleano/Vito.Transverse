namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record ComponentDTO
{
    public long ApplicationFk { get; set; }

    public long EndpointFk { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public string ObjectId { get; set; } = null!;

    public string ObjectName { get; set; } = null!;

    public string ObjectPropertyName { get; set; } = null!;

    public string DefaultPropertyValue { get; set; } = null!;

    public long? PositionIndex { get; set; }

    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string DescriptionTranslationKey { get; set; } = null!;
    public long ApplicationOwnerId { get; set; }
    public string ApplicationOwnerNameTranslationKey { get; set; } = null!;
}
