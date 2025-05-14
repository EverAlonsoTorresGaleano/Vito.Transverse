using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Localization;

public interface ILocalizationService
{
    Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesByApplicationAsync(long applicationId);

    Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesAsync(long applicationId, string cultureId);

    Task<List<CultureTranslationDTO>> GetLocalizedMessagesByKeyAsync(long applicationId, string localizationMessageKey);

    CultureTranslationDTO GetLocalizedMessageByKeyAndParamsSync(long applicationId, string cultureId, string localizationMessageKey, params object?[] parameters);
}
