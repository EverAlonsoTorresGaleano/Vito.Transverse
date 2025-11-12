using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Application.TransverseServices.Master;

public interface IMasterService
{
    Task<List<SecuencesDTO>> GetAllSecuencesListAsync();

    Task<SecuencesDTO?> GetSecuenceByIdAsync(long secuenceId);

    Task<SecuencesDTO?> CreateNewSecuenceAsync(SecuencesDTO secuenceDTO, DeviceInformationDTO deviceInformation);

    Task<SecuencesDTO?> UpdateSecuenceByIdAsync(long secuenceId, SecuencesDTO secuenceDTO, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteSecuenceByIdAsync(long secuenceId, DeviceInformationDTO deviceInformation);

    // Culture CRUD
    Task<List<CultureDTO>> GetAllCultureListAsync();

    Task<CultureDTO?> GetCultureByIdAsync(string cultureId);

    Task<CultureDTO?> CreateNewCultureAsync(CultureDTO cultureInfo, DeviceInformationDTO deviceInformation);

    Task<CultureDTO?> UpdateCultureByIdAsync(string cultureId, CultureDTO cultureInfo, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteCultureByIdAsync(string cultureId, DeviceInformationDTO deviceInformation);

    // Language CRUD
    Task<List<LanguageDTO>> GetAllLanguageListAsync();

    Task<LanguageDTO?> GetLanguageByIdAsync(string languageId);

    Task<LanguageDTO?> CreateNewLanguageAsync(LanguageDTO languageInfo, DeviceInformationDTO deviceInformation);

    Task<LanguageDTO?> UpdateLanguageByIdAsync(string languageId, LanguageDTO languageInfo, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteLanguageByIdAsync(string languageId, DeviceInformationDTO deviceInformation);

    // Country CRUD
    Task<List<CountryDTO>> GetAllCountryListAsync();

    Task<CountryDTO?> GetCountryByIdAsync(string countryId);

    Task<CountryDTO?> CreateNewCountryAsync(CountryDTO countryInfo, DeviceInformationDTO deviceInformation);

    Task<CountryDTO?> UpdateCountryByIdAsync(string countryId, CountryDTO countryInfo, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteCountryByIdAsync(string countryId, DeviceInformationDTO deviceInformation);

    // GeneralTypeItem CRUD
    Task<List<GeneralTypeItemDTO>> GetAllGeneralTypeItemListAsync();

    Task<GeneralTypeItemDTO?> GetGeneralTypeItemByIdAsync(long generalTypeItemId);

    Task<GeneralTypeItemDTO?> CreateNewGeneralTypeItemAsync(GeneralTypeItemDTO generalTypeItemInfo, DeviceInformationDTO deviceInformation);

    Task<GeneralTypeItemDTO?> UpdateGeneralTypeItemByIdAsync(long generalTypeItemId, GeneralTypeItemDTO generalTypeItemInfo, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteGeneralTypeItemByIdAsync(long generalTypeItemId, DeviceInformationDTO deviceInformation);

    // GeneralTypeGroup CRUD
    Task<List<GeneralTypeGroupDTO>> GetAllGeneralTypeGroupListAsync();

    Task<GeneralTypeGroupDTO?> GetGeneralTypeGroupByIdAsync(long generalTypeGroupId);

    Task<GeneralTypeGroupDTO?> CreateNewGeneralTypeGroupAsync(GeneralTypeGroupDTO generalTypeGroupInfo, DeviceInformationDTO deviceInformation);

    Task<GeneralTypeGroupDTO?> UpdateGeneralTypeGroupByIdAsync(long generalTypeGroupId, GeneralTypeGroupDTO generalTypeGroupInfo, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteGeneralTypeGroupByIdAsync(long generalTypeGroupId, DeviceInformationDTO deviceInformation);

    // NotificationTemplate CRUD
    Task<List<NotificationTemplateDTO>> GetAllNotificationTemplateListAsync();

    Task<NotificationTemplateDTO?> GetNotificationTemplateByIdAsync(long notificationTemplateId);

    Task<NotificationTemplateDTO?> CreateNewNotificationTemplateAsync(NotificationTemplateDTO notificationTemplateInfo, DeviceInformationDTO deviceInformation);

    Task<NotificationTemplateDTO?> UpdateNotificationTemplateByIdAsync(long notificationTemplateId, NotificationTemplateDTO notificationTemplateInfo, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteNotificationTemplateByIdAsync(long notificationTemplateId, DeviceInformationDTO deviceInformation);
    Task<List<GeneralTypeItemDTO>> GetGeneralTypeItemByGroupIdListAsync(long groupId);
    Task<List<ListItemDTO>> GetAllNotificationTemplateListItemAsync();
    Task<List<ListItemDTO>> GetAllCultureListItemAsync();
    Task<List<ListItemDTO>> GetAllLanguageListItemAsync();
    Task<List<ListItemDTO>> GetAllCountryListItemAsync();
    Task<List<ListItemDTO>> GetAllGeneralTypeGroupListItemAsync();
    Task<List<ListItemDTO>> GetAllGeneralTypeItemListItemAsync();
    Task<List<ListItemDTO>> GetGeneralTypeItemByGroupIdListItemAsync(int groupId);
}

