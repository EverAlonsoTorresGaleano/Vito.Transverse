using Microsoft.Extensions.Logging;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Companies;
using Vito.Transverse.Identity.Entities.DTO;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Companies;

public class CompaniesService(ILogger<CompaniesService> logger, ICompaniesRepository companiesRepository, ICachingServiceMemoryCache cachingService) : ICompaniesService
{

    public async Task<List<CompanyDTO>> GetAllCompanyListAsync()
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<CompanyDTO>>(CacheItemKeysEnum.AllCompanyList.ToString());
            if (returnList == null)
            {
                returnList = await companiesRepository.GetAllCompanyListAsync(x => x.Id > 0);
                cachingService.SetCacheData(CacheItemKeysEnum.AllCompanyList.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllCompanyListAsync));
            throw;
        }

    }

    public async Task<CompanyDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await companiesRepository.CreateNewCompanyAsync(newRecord, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCompanyAsync));
            throw;
        }
    }

    public async Task<CompanyDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO companyApplicationInfo, DeviceInformationDTO deviceInformation)
    {
        CompanyDTO? returnValue = null;
        try
        {
            returnValue = await companiesRepository.UpdateCompanyApplicationsAsync(companyApplicationInfo, deviceInformation);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyApplicationsAsync));
            throw;
        }
        return returnValue;
    }



    public async Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipAsync(long? companyId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<CompanyMembershipsDTO>>(CacheItemKeysEnum.CompanyMemberhipListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await companiesRepository.GetCompanyMemberhipListAsync(x => companyId == null || x.CompanyFk == companyId);
                cachingService.SetCacheData(CacheItemKeysEnum.CompanyMemberhipListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyMemberhipAsync));
            throw;
        }
    }


}
