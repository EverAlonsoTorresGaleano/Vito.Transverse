using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Entities.DTO;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Companies;

public interface ICompaniesRepository
{


    Task<CompanyDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<CompanyDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null);

    Task<CompanyMembershipsDTO?> GetCompanyMembershipByIdAsync(long membershipId, DataBaseServiceContext? context = null);

    Task<CompanyMembershipsDTO?> UpdateCompanyMembershipByIdAsync(CompanyMembershipsDTO membershipInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteCompanyMembershipByIdAsync(long membershipId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<List<CompanyDTO>> GetAllCompanyListAsync(Expression<Func<Company, bool>> filters, DataBaseServiceContext? context = null);

    Task<CompanyDTO?> GetCompanyByIdAsync(long companyId, DataBaseServiceContext? context = null);

    Task<CompanyDTO?> UpdateCompanyByIdAsync(CompanyDTO companyInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteCompanyByIdAsync(long companyId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<List<MembershipTypeDTO>> GetAllMembershipTypeListAsync(Expression<Func<MembershipType, bool>> filters, DataBaseServiceContext? context = null);

    Task<MembershipTypeDTO?> GetMembershipTypeByIdAsync(long membershipTypeId, DataBaseServiceContext? context = null);

    Task<MembershipTypeDTO?> CreateNewMembershipTypeAsync(MembershipTypeDTO membershipTypeDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<MembershipTypeDTO?> UpdateMembershipTypeByIdAsync(MembershipTypeDTO membershipTypeDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteMembershipTypeByIdAsync(long membershipTypeId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

}
