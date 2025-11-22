namespace Vito.Transverse.Identity.Entities.ModelsDTO;

//developers
public class ListItemDTO
{
    public string ListItemGroupFk { get; set; } = null!;

    public string  Id { get; set; } = null!;

    public string NameTranslationKey { get; set; } = null!;

    public string DescriptionTranslationKey { get; set; } = null!;

    public int? OrderIndex { get; set; }

    public bool IsEnabled { get; set; }
}
