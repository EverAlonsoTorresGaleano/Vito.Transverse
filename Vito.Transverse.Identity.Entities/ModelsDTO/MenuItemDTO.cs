namespace Vito.Transverse.Identity.Entities.ModelsDTO;


//developers
public record MenuItemDTO
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public string Description { get; set; }
    public string Path { get; set; }

    public long? PositionIndex { get; set; }

    public bool CanCreate { get; set; }
    public bool CanView { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }

    public List<MenuComponentDTO> Items { get; set; }
    public bool IsVisible { get; set; }
    public bool IsApi { get; set; }
}