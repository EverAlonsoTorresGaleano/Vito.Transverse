using Microsoft.Extensions.Logging;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Application.TransverseServices.Caching;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Master;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Infrastructure.Extensions;

namespace Vito.Transverse.Identity.Application.TransverseServices.Master;

public class MasterService(ILogger<MasterService> logger, IMasterRepository masterRepository, ICachingServiceMemoryCache cachingService) : IMasterService
{
    public async Task<List<SecuencesDTO>> GetAllSecuencesListAsync()
    {
        try
        {
            var returnList = await masterRepository.GetAllSecuencesListAsync(x => x.Id > 0);
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllSecuencesListAsync));
            throw;
        }
    }

    public async Task<SecuencesDTO?> GetSecuenceByIdAsync(long secuenceId)
    {
        try
        {
            return await masterRepository.GetSecuenceByIdAsync(secuenceId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetSecuenceByIdAsync));
            throw;
        }
    }

    public async Task<SecuencesDTO?> CreateNewSecuenceAsync(SecuencesDTO secuenceDTO, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.CreateNewSecuenceAsync(secuenceDTO, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewSecuenceAsync));
            throw;
        }
    }

    public async Task<SecuencesDTO?> UpdateSecuenceByIdAsync(long secuenceId, SecuencesDTO secuenceDTO, DeviceInformationDTO deviceInformation)
    {
        try
        {
            secuenceDTO.Id = secuenceId;
            return await masterRepository.UpdateSecuenceByIdAsync(secuenceDTO, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateSecuenceByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteSecuenceByIdAsync(long secuenceId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            return await masterRepository.DeleteSecuenceByIdAsync(secuenceId, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteSecuenceByIdAsync));
            throw;
        }
    }

    // Culture CRUD
    public async Task<List<CultureDTO>> GetAllCultureListAsync()
    {
        try
        {
            var returnList = await masterRepository.GetAllCultureListAsync();
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllCultureListAsync));
            throw;
        }
    }

    public async Task<CultureDTO?> GetCultureByIdAsync(string cultureId)
    {
        try
        {
            var returnValue = await masterRepository.GetCultureByIdAsync(cultureId);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCultureByIdAsync));
            throw;
        }
    }

    public async Task<CultureDTO?> CreateNewCultureAsync(CultureDTO cultureInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.CreateNewCultureAsync(cultureInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCultureAsync));
            throw;
        }
    }

    public async Task<CultureDTO?> UpdateCultureByIdAsync(string cultureId, CultureDTO cultureInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.UpdateCultureByIdAsync(cultureId, cultureInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCultureByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteCultureByIdAsync(string cultureId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var deleted = await masterRepository.DeleteCultureByIdAsync(cultureId, deviceInformation);
            return deleted;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCultureByIdAsync));
            throw;
        }
    }

    // Language CRUD
    public async Task<List<LanguageDTO>> GetAllLanguageListAsync()
    {
        try
        {
            var returnList = await masterRepository.GetAllLanguageListAsync();
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllLanguageListAsync));
            throw;
        }
    }

    public async Task<LanguageDTO?> GetLanguageByIdAsync(string languageId)
    {
        try
        {
            var returnValue = await masterRepository.GetLanguageByIdAsync(languageId);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetLanguageByIdAsync));
            throw;
        }
    }

    public async Task<LanguageDTO?> CreateNewLanguageAsync(LanguageDTO languageInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.CreateNewLanguageAsync(languageInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewLanguageAsync));
            throw;
        }
    }

    public async Task<LanguageDTO?> UpdateLanguageByIdAsync(string languageId, LanguageDTO languageInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.UpdateLanguageByIdAsync(languageId, languageInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateLanguageByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteLanguageByIdAsync(string languageId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var deleted = await masterRepository.DeleteLanguageByIdAsync(languageId, deviceInformation);
            return deleted;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteLanguageByIdAsync));
            throw;
        }
    }

    // Country CRUD
    public async Task<List<CountryDTO>> GetAllCountryListAsync()
    {
        try
        {
            var returnList = await masterRepository.GetAllCountryListAsync();
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllCountryListAsync));
            throw;
        }
    }

    public async Task<CountryDTO?> GetCountryByIdAsync(string countryId)
    {
        try
        {
            var returnValue = await masterRepository.GetCountryByIdAsync(countryId);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCountryByIdAsync));
            throw;
        }
    }

    public async Task<CountryDTO?> CreateNewCountryAsync(CountryDTO countryInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.CreateNewCountryAsync(countryInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCountryAsync));
            throw;
        }
    }

    public async Task<CountryDTO?> UpdateCountryByIdAsync(string countryId, CountryDTO countryInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.UpdateCountryByIdAsync(countryId, countryInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCountryByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteCountryByIdAsync(string countryId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var deleted = await masterRepository.DeleteCountryByIdAsync(countryId, deviceInformation);
            return deleted;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCountryByIdAsync));
            throw;
        }
    }

    // GeneralTypeItem CRUD
    public async Task<List<GeneralTypeItemDTO>> GetAllGeneralTypeItemListAsync()
    {
        try
        {
            var returnList = await masterRepository.GetAllGeneralTypeItemListAsync(x => x.Id > 0);
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllGeneralTypeItemListAsync));
            throw;
        }
    }

    public async Task<GeneralTypeItemDTO?> GetGeneralTypeItemByIdAsync(long generalTypeItemId)
    {
        try
        {
            var returnValue = await masterRepository.GetGeneralTypeItemByIdAsync(generalTypeItemId);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetGeneralTypeItemByIdAsync));
            throw;
        }
    }

    public async Task<GeneralTypeItemDTO?> CreateNewGeneralTypeItemAsync(GeneralTypeItemDTO generalTypeItemInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.CreateNewGeneralTypeItemAsync(generalTypeItemInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewGeneralTypeItemAsync));
            throw;
        }
    }

    public async Task<GeneralTypeItemDTO?> UpdateGeneralTypeItemByIdAsync(long generalTypeItemId, GeneralTypeItemDTO generalTypeItemInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            generalTypeItemInfo.Id = generalTypeItemId;
            return await masterRepository.UpdateGeneralTypeItemByIdAsync(generalTypeItemInfo, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateGeneralTypeItemByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteGeneralTypeItemByIdAsync(long generalTypeItemId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var deleted = await masterRepository.DeleteGeneralTypeItemByIdAsync(generalTypeItemId, deviceInformation);
            return deleted;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteGeneralTypeItemByIdAsync));
            throw;
        }
    }

    // GeneralTypeGroup CRUD
    public async Task<List<GeneralTypeGroupDTO>> GetAllGeneralTypeGroupListAsync()
    {
        try
        {
            var returnList = await masterRepository.GetAllGeneralTypeGroupListAsync(x => x.Id > 0);
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllGeneralTypeGroupListAsync));
            throw;
        }
    }

    public async Task<List<GeneralTypeItemDTO>> GetGeneralTypeItemByGroupIdListAsync(long groupId)
    {
        try
        {
            var returnList = await masterRepository.GetAllGeneralTypeItemListAsync(x => x.ListItemGroupFk == groupId);
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllGeneralTypeGroupListAsync));
            throw;
        }
    }

    public async Task<GeneralTypeGroupDTO?> GetGeneralTypeGroupByIdAsync(long generalTypeGroupId)
    {
        try
        {
            var returnValue = await masterRepository.GetGeneralTypeGroupByIdAsync(generalTypeGroupId);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetGeneralTypeGroupByIdAsync));
            throw;
        }
    }

    public async Task<GeneralTypeGroupDTO?> CreateNewGeneralTypeGroupAsync(GeneralTypeGroupDTO generalTypeGroupInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.CreateNewGeneralTypeGroupAsync(generalTypeGroupInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewGeneralTypeGroupAsync));
            throw;
        }
    }

    public async Task<GeneralTypeGroupDTO?> UpdateGeneralTypeGroupByIdAsync(long generalTypeGroupId, GeneralTypeGroupDTO generalTypeGroupInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            generalTypeGroupInfo.Id = generalTypeGroupId;
            return await masterRepository.UpdateGeneralTypeGroupByIdAsync(generalTypeGroupInfo, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateGeneralTypeGroupByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteGeneralTypeGroupByIdAsync(long generalTypeGroupId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var deleted = await masterRepository.DeleteGeneralTypeGroupByIdAsync(generalTypeGroupId, deviceInformation);
            return deleted;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteGeneralTypeGroupByIdAsync));
            throw;
        }
    }

    // NotificationTemplate CRUD
    public async Task<List<NotificationTemplateDTO>> GetAllNotificationTemplateListAsync()
    {
        try
        {
            var returnList = await masterRepository.GetAllNotificationTemplateListAsync(x => x.Id > 0);
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllNotificationTemplateListAsync));
            throw;
        }
    }

    public async Task<NotificationTemplateDTO?> GetNotificationTemplateByIdAsync(long notificationTemplateId)
    {
        try
        {
            var returnValue = await masterRepository.GetNotificationTemplateByIdAsync(notificationTemplateId);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetNotificationTemplateByIdAsync));
            throw;
        }
    }

    public async Task<NotificationTemplateDTO?> CreateNewNotificationTemplateAsync(NotificationTemplateDTO notificationTemplateInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await masterRepository.CreateNewNotificationTemplateAsync(notificationTemplateInfo, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewNotificationTemplateAsync));
            throw;
        }
    }

    public async Task<NotificationTemplateDTO?> UpdateNotificationTemplateByIdAsync(long notificationTemplateId, NotificationTemplateDTO notificationTemplateInfo, DeviceInformationDTO deviceInformation)
    {
        try
        {
            notificationTemplateInfo.Id = notificationTemplateId;
            return await masterRepository.UpdateNotificationTemplateByIdAsync(notificationTemplateInfo, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateNotificationTemplateByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteNotificationTemplateByIdAsync(long notificationTemplateId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var deleted = await masterRepository.DeleteNotificationTemplateByIdAsync(notificationTemplateId, deviceInformation);
            return deleted;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteNotificationTemplateByIdAsync));
            throw;
        }
    }

    public async Task<List<ListItemDTO>> GetAllNotificationTemplateListItemAsync()
    {
        var listItem = await GetAllNotificationTemplateListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetAllCultureListItemAsync()
    {
        var listItem = await GetAllCultureListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetAllLanguageListItemAsync()
    {
        var listItem = await GetAllLanguageListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetAllCountryListItemAsync()
    {
        var listItem = await GetAllCountryListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetAllGeneralTypeGroupListItemAsync()
    {
        var listItem = await GetAllGeneralTypeGroupListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetAllGeneralTypeItemListItemAsync()
    {
        var listItem = await GetAllGeneralTypeItemListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<List<ListItemDTO>> GetGeneralTypeItemByGroupIdListItemAsync(int groupId)
    {
        var listItem = await GetGeneralTypeItemByGroupIdListAsync(groupId);
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }
}

