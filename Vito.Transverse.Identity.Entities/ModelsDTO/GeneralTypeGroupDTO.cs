namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record GeneralTypeGroupDTO
{
    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public bool IsSystemType { get; set; }
}

