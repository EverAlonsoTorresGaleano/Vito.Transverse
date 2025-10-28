using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Companies;

public interface ICompaniesService
{


    Task<CompanyDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO newRecord, DeviceInformationDTO deviceInformation);

    Task<CompanyDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO companyApplicationsInfo, DeviceInformationDTO deviceInformation);


    Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipAsync(long? companyId);

    Task<List<CompanyDTO>> GetAllCompanyListAsync();

   
}
