using Microsoft.Extensions.Options;
using System.Globalization;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Localization;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.BAL.Extensions;
using Vito.Transverse.Identity.Domain.ModelsDTO;
using Vito.Framework.Common.Constants;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Culture;


/// <see cref="ICultureService"/>
public class CultureService(ICultureRepository _cultureRepository, ILocalizationRepository _localizationRepository, ICachingServiceMemoryCache _cachingService, IOptions<CultureSettingsOptions> _cultureSettingsOptions) : ICultureService
{

    CultureSettingsOptions _cultureSettingsOptionsValues = _cultureSettingsOptions.Value;

    #region Date/Time
    public CultureInfo GetCurrectCulture()
    {
        return _cultureRepository.GetCurrectCulture();
    }

    public string SetCurrectCulture(string cultureId)
    {
        return _cultureRepository.SetCurrectCulture(cultureId);
    }

    public DateTimeOffset UtcNow()
    {
        return _cultureRepository.UtcNow();
    }
    #endregion 

    #region Localization

    public async Task<List<ListItemDTO>> GetActiveCultureListItemDTOListAsync()
    {
        var returnList = await GetActiveCultureListAsync();
        var returnListDTO = returnList.ToListItemDTOList();
        returnListDTO.ForEach(c => c.NameTranslationKey = GetLocalizedMessage(c.NameTranslationKey).TranslationValue);
        return returnListDTO;
    }

    public async Task<List<CultureDTO>> GetActiveCultureListAsync()
    {
        var cacheList = _cachingService.GetCacheDataByKey<List<CultureDTO>>(CacheItemKeysEnum.CultureList.ToString());
        List<CultureDTO> returnList = new();
        if (cacheList == null)
        {
            cacheList = await _localizationRepository.GetActiveCultureList();
            //Localize
            cacheList.ForEach(c => c.Name = GetLocalizedMessage(c.NameTranslationKey).TranslationValue);
            _cachingService.SetCacheData(CacheItemKeysEnum.CultureList.ToString(), cacheList);
        }
        return cacheList;
    }

    public async Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesAsync()
    {
        var cultureId = CultureInfo.CurrentCulture.Name;
        var cacheList = _cachingService.GetCacheDataByKey<List<CultureTranslationDTO>>(CacheItemKeysEnum.CultureTranslationsListByCultureId + cultureId);
        List<CultureTranslationDTO> returnList = new();
        if (cacheList == null)
        {
            cacheList = await _localizationRepository.GetAllLocalizedMessagesByCultureIdAsync(cultureId);
            _cachingService.SetCacheData(CacheItemKeysEnum.CultureTranslationsListByCultureId + cultureId, cacheList);
        }
        return cacheList;
    }

    public CultureTranslationDTO GetLocalizedMessage(string localizationMessageKey, params object?[] parameters)
    {
        CultureTranslationDTO? localizedMessage = default;
        var returnList = GetAllLocalizedMessagesAsync().GetAwaiter().GetResult();

        localizedMessage = returnList.FirstOrDefault(x => x.TranslationKey.Equals(localizationMessageKey));
        var cultureId = CultureInfo.CurrentCulture.Name;

        var messageNotFound = FrameworkConstants.Culture_TranslationMessage_MessageNotFound;
        if (localizedMessage is null)
        {
            var messageNotFoundInfo = returnList.FirstOrDefault(x => x.TranslationKey.Equals(FrameworkConstants.Culture_TranslationKey_MessageNotFound));
            if (messageNotFoundInfo is not null)
            {
                messageNotFound = messageNotFoundInfo.TranslationValue;
            }
        }

        var localizationMessageValue = localizedMessage != null ? localizedMessage.TranslationValue : string.Format(messageNotFound, cultureId, localizationMessageKey);
        if (localizedMessage is null)
        {
            var newRecord = new CultureTranslationDTO()
            {
                CultureFk = cultureId,
                TranslationKey = localizationMessageKey,
                TranslationValue = localizationMessageValue
            };
            if (!string.IsNullOrEmpty(localizationMessageKey) && _cultureSettingsOptionsValues.AutoAddMissingTranslations)
            {
                _localizationRepository.AddAsync(newRecord);
            }
            localizedMessage = newRecord;
        }

        if (parameters.Length > 0 && !string.IsNullOrEmpty(parameters?.GetValue(0)!.ToString()))
        {
            localizationMessageValue = string.Format(localizationMessageValue, parameters);
        }
        localizedMessage!.TranslationValue = localizationMessageValue;
        return localizedMessage;
    }


    #endregion
}
