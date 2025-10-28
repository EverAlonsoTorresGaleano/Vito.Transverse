namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record EntityDTO
{
    public long Id { get; set; }

    public string SchemaName { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsSystemEntity { get; set; }
}
