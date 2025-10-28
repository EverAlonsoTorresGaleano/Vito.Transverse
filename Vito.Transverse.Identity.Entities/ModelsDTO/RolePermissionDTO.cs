namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record RolePermissionDTO
{
    public long RoleFk { get; set; }

    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long Id { get; set; }

    public long ModuleFk { get; set; }

    public long? EndpointFk { get; set; }

    public long? ComponentFk { get; set; }

    public string? PropertyValue { get; set; }
}
