namespace Vito.Transverse.Identity.Entities.ModelsDTO;


//developers
public record MenuItemDTO
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public string Description { get; set; }
    public string Path { get; set; }
}