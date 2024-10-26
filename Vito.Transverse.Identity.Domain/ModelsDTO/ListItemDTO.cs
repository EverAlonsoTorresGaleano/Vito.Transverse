namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public class ListItemDTO
{
    public string ListItemGroupFk { get; set; } = null!;

    public string  Id { get; set; } = null!;

    public string NameTranslationKey { get; set; } = null!;

    public int? OrderIndex { get; set; }

    public bool IsEnabled { get; set; }
}
