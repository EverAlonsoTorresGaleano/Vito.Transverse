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

    Task<List<CultureDTO>> GetActiveCultureListAsync(long applicationId);

    Task<List<ListItemDTO>> GetActiveCultureListItemDTOListAsync(long applicationId);

}
