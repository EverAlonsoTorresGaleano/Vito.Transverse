using Microsoft.Extensions.Options;
using System.Globalization;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Options;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Application.TransverseServices.Localization;
using  Vito.Transverse.Identity.Infrastructure.Extensions;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.Extensions;
using Vito.Transverse.Identity.Entities.ModelsDTO;
namespace  Vito.Transverse.Identity.Application.TransverseServices.Culture;


/// <see cref="ICultureService"/>
public class CultureService(ICultureRepository cultureRepository, ILocalizationService localizationService, ICachingServiceMemoryCache cachingService) : ICultureService
{
    #region Date/Time
    public CultureInfo GetCurrectCulture()
    {
        return cultureRepository.GetCurrentCulture();
    }

    public string SetCurrectCulture(string cultureId)
    {
        return cultureRepository.SetCurrentCulture(cultureId);
    }

    public DateTimeOffset UtcNow()
    {
        return cultureRepository.UtcNow();
    }
    #endregion 

    #region Localization

    public async Task<List<ListItemDTO>> GetActiveCultureListItemDTOListAsync()
    {
        var returnList = await GetActiveCultureListAsync();
        var returnListDTO = returnList.Select(x => x.ToListItemDTO()).ToList();
        return returnListDTO;
    }

    public async Task<List<CultureDTO>> GetActiveCultureListAsync()
    {
        var cacheList = cachingService.GetCacheDataByKey<List<CultureDTO>>(CacheItemKeysEnum.CultureList.ToString());
        List<CultureDTO> returnList = new();
        if (cacheList == null)
        {
             cacheList = await cultureRepository.GetActiveCultureListAsync();
            cachingService.SetCacheData(CacheItemKeysEnum.CultureList.ToString() , cacheList);
        }
        return cacheList;
    }

    public string GetCurrentCultureId()
    {
        string cultureId = cultureRepository.GetCurrentCultureId();
        return cultureId;
    }

    #endregion
}
