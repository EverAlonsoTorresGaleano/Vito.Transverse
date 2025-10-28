using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Entities.DTO;

public record CompanyApplicationsDTO 
{
    public long UserId { get; set; }

    public CompanyDTO Company { get; set; } = null!;    
    public List<ApplicationDTO> Applications { get; set; } = null!;
}
