using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Application.TransverseServices.Companies;

public interface ICompaniesService
{


    Task<CompanyDTO?> CreateNewCompanyAsync(CompanyDTO newRecord, DeviceInformationDTO deviceInformation);

    Task<CompanyDTO?> UpdateCompanyApplicationsAsync(CompanyDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation);


    Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipAsync(long? companyId);

    Task<CompanyMembershipsDTO?> GetCompanyMembershipByIdAsync(long membershipId);

    Task<CompanyMembershipsDTO?> UpdateCompanyMembershipByIdAsync(long membershipId, CompanyMembershipsDTO membershipInfo, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteCompanyMembershipByIdAsync(long membershipId, DeviceInformationDTO deviceInformation);

    Task<List<CompanyDTO>> GetAllCompanyListAsync();

    Task<CompanyDTO?> GetCompanyByIdAsync(long companyId);

    Task<CompanyDTO?> UpdateCompanyByIdAsync(long companyId, CompanyDTO companyInfo, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteCompanyByIdAsync(long companyId, DeviceInformationDTO deviceInformation);

    Task<List<MembershipTypeDTO>> GetAllMembershipTypeListAsync();

    Task<MembershipTypeDTO?> GetMembershipTypeByIdAsync(long membershipTypeId);

    Task<MembershipTypeDTO?> CreateNewMembershipTypeAsync(MembershipTypeDTO membershipTypeDTO, DeviceInformationDTO deviceInformation);

    Task<MembershipTypeDTO?> UpdateMembershipTypeByIdAsync(long membershipTypeId, MembershipTypeDTO membershipTypeDTO, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteMembershipTypeByIdAsync(long membershipTypeId, DeviceInformationDTO deviceInformation);
    Task<List<ListItemDTO>> GetAllCompanyListItemAsync();
    Task<List<ListItemDTO>> GetCompanyMemberhipListItemAsync(long value);
    Task<List<ListItemDTO>> GetAllMembershipTypeListItemAsync();
}
