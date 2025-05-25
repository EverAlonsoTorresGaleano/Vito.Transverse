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
public class CultureService(ICultureRepository cultureRepository, ILocalizationService localizationService, ICachingServiceMemoryCache cachingService, IOptions<CultureSettingsOptions> cultureSettingsOptions) : ICultureService
{

    CultureSettingsOptions _cultureSettingsOptionsValues = cultureSettingsOptions.Value;

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

    public async Task<List<ListItemDTO>> GetActiveCultureListItemDTOListAsync(long applicationId)
    {
        string cultureId = cultureRepository.GetCurrentCultureId();
        var returnList = await GetActiveCultureListAsync(applicationId);
        var returnListDTO = returnList.ToListItemDTOList();
        returnListDTO.ForEach(c => c.NameTranslationKey = localizationService.GetLocalizedMessageByKeyAndParamsSync(applicationId, cultureId, c.NameTranslationKey).TranslationValue);
        return returnListDTO;
    }

    public async Task<List<CultureDTO>> GetActiveCultureListAsync(long applicationId)
    {
        string cultureId = cultureRepository.GetCurrentCultureId();
        var cacheList = cachingService.GetCacheDataByKey<List<CultureDTO>>(CacheItemKeysEnum.CultureList.ToString() + applicationId);
        List<CultureDTO> returnList = new();
        if (cacheList == null)
        {
            cacheList = await cultureRepository.GetActiveCultureListAsync();
            //Localize
            cacheList.ForEach(c => c.Name = localizationService.GetLocalizedMessageByKeyAndParamsSync(applicationId, cultureId, c.NameTranslationKey).TranslationValue);
            cachingService.SetCacheData(CacheItemKeysEnum.CultureList.ToString() + applicationId, cacheList);
        }
        return cacheList;
    }




    #endregion
}
