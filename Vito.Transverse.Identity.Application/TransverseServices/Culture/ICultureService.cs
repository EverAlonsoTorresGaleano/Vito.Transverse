using System.Globalization;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Culture;

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
    string GetCurrentCultureId();
}
