using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Application.TransverseServices.Localization;

public interface ILocalizationService
{
    Task<List<CultureTranslationDTO>> GetLocalizedMessagesListByApplicationAsync(long applicationId);

    Task<List<CultureTranslationDTO>> GetLocalizedMessagesListByApplicationAndCultureAsync(long applicationId, string cultureId);

    Task<List<CultureTranslationDTO>> GetLocalizedMessagesListByKeyAsync(long applicationId, string localizationMessageKey);

    CultureTranslationDTO GetLocalizedMessageByKeyAndParamsSync(long applicationId, string cultureId, string localizationMessageKey, params object?[] parameters);

    Task<CultureTranslationDTO?> GetCultureTranslationByIdAsync(long applicationId, string cultureId, string translationKey);

    Task<CultureTranslationDTO> CreateNewCultureTranslationAsync(CultureTranslationDTO cultureTranslationDTO, DeviceInformationDTO deviceInformation);

    Task<bool> UpdateCultureTranslationAsync(long applicationId, string cultureId, string translationKey, CultureTranslationDTO cultureTranslationDTO, DeviceInformationDTO deviceInformation);

    Task<bool> DeleteCultureTranslationAsync(long applicationId, string cultureId, string translationKey, DeviceInformationDTO deviceInformation);
    Task<bool> UpsertCultureTranslationMasiveAsync(List<CultureTranslationDTO> cultureTranslationDTOs);
}
