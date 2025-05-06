namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public record CompanyDTO
(
    long Id,

    string Name,

    Guid CompanyClient,

    Guid CompanySecret,

    DateTime CreationDate,

    long CreatedByUserFk,

    string Subdomain,

    string Email,

    string DefaultCultureFk,

    string CountryFk,

    bool IsSystemCompany,

    byte[]? Avatar,

    DateTime? LastUpdateDate,

    long? LastUpdateByUserFk,

    bool IsActive
);