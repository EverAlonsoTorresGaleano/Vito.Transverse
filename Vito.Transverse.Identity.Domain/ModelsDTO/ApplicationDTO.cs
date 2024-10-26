namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public  class ApplicationDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Secret { get; set; }

    public bool IsActive { get; set; }

    public byte[]? Avatar { get; set; }

}
