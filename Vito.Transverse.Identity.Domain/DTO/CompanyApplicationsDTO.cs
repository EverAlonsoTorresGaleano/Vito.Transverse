using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Domain.DTO;

public record CompanyApplicationsDTO 
{
    public long UserId { get; set; }

    public CompanyDTO Company { get; set; } = null!;    
    public List<ApplicationDTO> Applications { get; set; } = null!;
}
