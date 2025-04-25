using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Localization;

public interface ILocalizationService
{
    Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesAsync();

    CultureTranslationDTO GetLocalizedMessage(string localizationMessageKey, params object?[] parameters);
}
