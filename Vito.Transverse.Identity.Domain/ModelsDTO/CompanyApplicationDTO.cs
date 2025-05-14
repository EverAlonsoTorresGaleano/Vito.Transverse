namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public class CompanyApplicationDTO
{
    public CompanyDTO companyInfo { get; set; } = null!;

    public List<ApplicationDTO> applicationInfoList { get; set; } = null!;
}
