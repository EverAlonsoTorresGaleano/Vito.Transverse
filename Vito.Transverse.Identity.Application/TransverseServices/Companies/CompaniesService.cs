using Microsoft.Extensions.Logging;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Application.TransverseServices.Caching;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Companies;
using Vito.Transverse.Identity.Entities.DTO;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.Extensions;

namespace Vito.Transverse.Identity.Application.TransverseServices.Companies;

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

    public async Task<List<ListItemDTO>> GetAllCompanyListItemAsync()
    {
        var listItem = await GetAllCompanyListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
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

    public async Task<CompanyMembershipsDTO?> GetCompanyMembershipByIdAsync(long membershipId)
    {
        try
        {
            return await companiesRepository.GetCompanyMembershipByIdAsync(membershipId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyMembershipByIdAsync));
            throw;
        }
    }

    public async Task<CompanyMembershipsDTO?> UpdateCompanyMembershipByIdAsync(long membershipId, CompanyMembershipsDTO membershipInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            membershipInfo.Id = membershipId;
            return await companiesRepository.UpdateCompanyMembershipByIdAsync(membershipInfo, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyMembershipByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteCompanyMembershipByIdAsync(long membershipId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            return await companiesRepository.DeleteCompanyMembershipByIdAsync(membershipId, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCompanyMembershipByIdAsync));
            throw;
        }
    }


    public async Task<CompanyDTO?> GetCompanyByIdAsync(long companyId)
    {
        try
        {
            return await companiesRepository.GetCompanyByIdAsync(companyId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyByIdAsync));
            throw;
        }
    }

    public async Task<CompanyDTO?> UpdateCompanyByIdAsync(long companyId, CompanyDTO companyInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            companyInfo.Id = companyId;
            return await companiesRepository.UpdateCompanyByIdAsync(companyInfo, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteCompanyByIdAsync(long companyId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            return await companiesRepository.DeleteCompanyByIdAsync(companyId, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCompanyByIdAsync));
            throw;
        }
    }

    public async Task<List<MembershipTypeDTO>> GetAllMembershipTypeListAsync()
    {
        try
        {
            return await companiesRepository.GetAllMembershipTypeListAsync(x => x.Id > 0);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllMembershipTypeListAsync));
            throw;
        }
    }

    public async Task<MembershipTypeDTO?> GetMembershipTypeByIdAsync(long membershipTypeId)
    {
        try
        {
            return await companiesRepository.GetMembershipTypeByIdAsync(membershipTypeId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetMembershipTypeByIdAsync));
            throw;
        }
    }

    public async Task<MembershipTypeDTO?> CreateNewMembershipTypeAsync(MembershipTypeDTO membershipTypeDTO, DeviceInformationDTO deviceInformation)
    {
        try
        {
            return await companiesRepository.CreateNewMembershipTypeAsync(membershipTypeDTO, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewMembershipTypeAsync));
            throw;
        }
    }

    public async Task<MembershipTypeDTO?> UpdateMembershipTypeByIdAsync(long membershipTypeId, MembershipTypeDTO membershipTypeDTO, DeviceInformationDTO deviceInformation)
    {
        try
        {
            membershipTypeDTO.Id = membershipTypeId;
            return await companiesRepository.UpdateMembershipTypeByIdAsync(membershipTypeDTO, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateMembershipTypeByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteMembershipTypeByIdAsync(long membershipTypeId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            return await companiesRepository.DeleteMembershipTypeByIdAsync(membershipTypeId, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteMembershipTypeByIdAsync));
            throw;
        }
    }

    public async Task<List<ListItemDTO>> GetCompanyMemberhipListItemAsync(long companyId)
    {
        var listItem = await GetCompanyMemberhipAsync(companyId);
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetAllMembershipTypeListItemAsync()
    {
        var listItem = await GetAllMembershipTypeListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }
}
