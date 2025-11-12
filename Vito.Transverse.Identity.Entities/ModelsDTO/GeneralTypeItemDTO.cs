namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public partial class GeneralTypeItemDTO
{
    public long ListItemGroupFk { get; set; }

    public int? OrderIndex { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public string ItemGroupNameTranslationKey { get; set; } = null!;

}