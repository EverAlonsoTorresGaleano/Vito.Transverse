using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Localization;

public interface ILocalizationService
{
    Task<List<CultureTranslationDTO>> GetLocalizedMessagesListByApplicationAsync(long applicationId);

    Task<List<CultureTranslationDTO>> GetLocalizedMessagesListByApplicationAndCultureAsync(long applicationId, string cultureId);

    Task<List<CultureTranslationDTO>> GetLocalizedMessagesListByKeyAsync(long applicationId, string localizationMessageKey);

    CultureTranslationDTO GetLocalizedMessageByKeyAndParamsSync(long applicationId, string cultureId, string localizationMessageKey, params object?[] parameters);
}
