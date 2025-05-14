namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public class ModuleDTO
{
    public long ApplicationFk { get; set; }

    public long Id { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public long? PositionIndex { get; set; }

    public bool IsActive { get; set; }

    public bool IsVisible { get; set; }

    public bool IsApi { get; set; }

    public List<PageDTO> Pages { get; set; } = new();       

    public string ApplicationNameTranslationKey { get; set; } = null!;
}
