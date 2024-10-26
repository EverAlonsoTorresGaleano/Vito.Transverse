namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public record CompanyDTO
(
    Guid Id,
    string Name,
    string? Subdomain,
    string Email,
    Guid? Secret,
    bool IsActive,
    byte[]? Avatar,
    string? CultureFk,
    string? CountryFk
);