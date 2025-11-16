using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Framework.Common.Models.SocialNetworks;

namespace Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Master;

public interface IMasterRepository
{
    Task<List<SequencesDTO>> GetAllSequencesListAsync(Expression<Func<Sequence, bool>> filters, DataBaseServiceContext? context = null);

    Task<SequencesDTO?> GetSequenceByIdAsync(long secuenceId, DataBaseServiceContext? context = null);

    Task<SequencesDTO?> CreateNewSequenceAsync(SequencesDTO secuenceDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<SequencesDTO?> UpdateSequenceByIdAsync(SequencesDTO secuenceDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteSequenceByIdAsync(long secuenceId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    // Culture CRUD
    Task<List<CultureDTO>> GetAllCultureListAsync(DataBaseServiceContext? context = null);

    Task<CultureDTO?> GetCultureByIdAsync(string cultureId, DataBaseServiceContext? context = null);

    Task<CultureDTO?> CreateNewCultureAsync(CultureDTO cultureInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<CultureDTO?> UpdateCultureByIdAsync(string cultureId, CultureDTO cultureInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteCultureByIdAsync(string cultureId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    // Language CRUD
    Task<List<LanguageDTO>> GetAllLanguageListAsync(DataBaseServiceContext? context = null);

    Task<LanguageDTO?> GetLanguageByIdAsync(string languageId, DataBaseServiceContext? context = null);

    Task<LanguageDTO?> CreateNewLanguageAsync(LanguageDTO languageInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<LanguageDTO?> UpdateLanguageByIdAsync(string languageId, LanguageDTO languageInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteLanguageByIdAsync(string languageId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    // Country CRUD
    Task<List<CountryDTO>> GetAllCountryListAsync(DataBaseServiceContext? context = null);

    Task<CountryDTO?> GetCountryByIdAsync(string countryId, DataBaseServiceContext? context = null);

    Task<CountryDTO?> CreateNewCountryAsync(CountryDTO countryInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<CountryDTO?> UpdateCountryByIdAsync(string countryId, CountryDTO countryInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteCountryByIdAsync(string countryId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    // GeneralTypeItem CRUD
    Task<List<GeneralTypeItemDTO>> GetAllGeneralTypeItemListAsync(Expression<Func<GeneralTypeItem, bool>> filters, DataBaseServiceContext? context = null);

    Task<GeneralTypeItemDTO?> GetGeneralTypeItemByIdAsync(long generalTypeItemId, DataBaseServiceContext? context = null);

    Task<GeneralTypeItemDTO?> CreateNewGeneralTypeItemAsync(GeneralTypeItemDTO generalTypeItemInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<GeneralTypeItemDTO?> UpdateGeneralTypeItemByIdAsync(GeneralTypeItemDTO generalTypeItemInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteGeneralTypeItemByIdAsync(long generalTypeItemId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    // GeneralTypeGroup CRUD
    Task<List<GeneralTypeGroupDTO>> GetAllGeneralTypeGroupListAsync(Expression<Func<GeneralTypeGroup, bool>> filters, DataBaseServiceContext? context = null);

    Task<GeneralTypeGroupDTO?> GetGeneralTypeGroupByIdAsync(long generalTypeGroupId, DataBaseServiceContext? context = null);

    Task<GeneralTypeGroupDTO?> CreateNewGeneralTypeGroupAsync(GeneralTypeGroupDTO generalTypeGroupInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<GeneralTypeGroupDTO?> UpdateGeneralTypeGroupByIdAsync(GeneralTypeGroupDTO generalTypeGroupInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteGeneralTypeGroupByIdAsync(long generalTypeGroupId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    // NotificationTemplate CRUD
    Task<List<NotificationTemplateDTO>> GetAllNotificationTemplateListAsync(Expression<Func<NotificationTemplate, bool>> filters, DataBaseServiceContext? context = null);

    Task<NotificationTemplateDTO?> GetNotificationTemplateByIdAsync(long notificationTemplateId, DataBaseServiceContext? context = null);

    Task<NotificationTemplateDTO?> CreateNewNotificationTemplateAsync(NotificationTemplateDTO notificationTemplateInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<NotificationTemplateDTO?> UpdateNotificationTemplateByIdAsync(NotificationTemplateDTO notificationTemplateInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteNotificationTemplateByIdAsync(long notificationTemplateId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);
}

