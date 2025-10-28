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

    Task<List<CompanyDTO>> GetAllCompanyListAsync(Expression<Func<Company, bool>> filters, DataBaseServiceContext? context = null);

}
