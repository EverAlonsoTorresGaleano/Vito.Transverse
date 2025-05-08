using Microsoft.Extensions.Options;
using System.Globalization;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.BAL.TransverseServices.Localization;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.ModelsDTO;
namespace Vito.Transverse.Identity.BAL.TransverseServices.Culture;


/// <see cref="ICultureService"/>
public class CultureService(ICultureRepository _cultureRepository, ILocalizationService localizationService, ICachingServiceMemoryCache _cachingService, IOptions<CultureSettingsOptions> _cultureSettingsOptions) : ICultureService
{

    CultureSettingsOptions _cultureSettingsOptionsValues = _cultureSettingsOptions.Value;

    #region Date/Time
    public CultureInfo GetCurrectCulture()
    {
        return _cultureRepository.GetCurrentCulture();
    }

    public string SetCurrectCulture(string cultureId)
    {
        return _cultureRepository.SetCurrentCulture(cultureId);
    }

    public DateTimeOffset UtcNow()
    {
        return _cultureRepository.UtcNow();
    }
    #endregion 

    #region Localization

    public async Task<List<ListItemDTO>> GetActiveCultureListItemDTOListAsync(long applicationId)
    {
        var returnList = await GetActiveCultureListAsync(applicationId);
        var returnListDTO = returnList.ToListItemDTOList();
        returnListDTO.ForEach(c => c.NameTranslationKey = localizationService.GetLocalizedMessageAsync(c.NameTranslationKey, applicationId).TranslationValue);
        return returnListDTO;
    }

    public async Task<List<CultureDTO>> GetActiveCultureListAsync(long applicationId)
    {
        var cacheList = _cachingService.GetCacheDataByKey<List<CultureDTO>>(CacheItemKeysEnum.CultureList.ToString() + applicationId);
        List<CultureDTO> returnList = new();
        if (cacheList == null)
        {
            cacheList = await _cultureRepository.GetActiveCultureListAsync();
            //Localize
            cacheList.ForEach(c => c.Name = localizationService.GetLocalizedMessageAsync(c.NameTranslationKey, applicationId).TranslationValue);
            _cachingService.SetCacheData(CacheItemKeysEnum.CultureList.ToString(), cacheList);
        }
        return cacheList;
    }




    #endregion
}
