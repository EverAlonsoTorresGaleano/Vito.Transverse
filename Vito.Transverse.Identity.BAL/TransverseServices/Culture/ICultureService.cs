using System.Globalization;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Culture;

/// <summary>
/// Culture Time Hours Hanglings
/// </summary>
public interface ICultureService
{
    CultureInfo GetCurrectCulture();

    string SetCurrectCulture(string cultureId);

     DateTimeOffset UtcNow();

    Task<List<CultureDTO>> GetActiveCultureListAsync();

    Task<List<ListItemDTO>> GetActiveCultureListItemDTOListAsync();

    Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesAsync();

    CultureTranslationDTO GetLocalizedMessage(string localizationMessageKey, params object?[] parameters);
}
