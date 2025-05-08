using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Localization;

public interface ILocalizationService
{
    Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesAsync(long applicationId);

    CultureTranslationDTO GetLocalizedMessageAsync(string localizationMessageKey, long applicationId, params object?[] parameters);
}
