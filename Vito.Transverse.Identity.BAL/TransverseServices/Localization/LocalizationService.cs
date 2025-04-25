using Microsoft.Extensions.Options;
using System.Globalization;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Localization;
using Vito.Transverse.Identity.Domain.Constants;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Localization;

public class LocalizationService(ILocalizationRepository _localizationRepository, ICachingServiceMemoryCache _cachingService, IOptions<CultureSettingsOptions> _cultureSettingsOptions) : ILocalizationService
{
    CultureSettingsOptions _cultureSettingsOptionsValues = _cultureSettingsOptions.Value;

    private static object[]? ValidateParamArray(object[]? parameters)
    {
        if (parameters?.Length == 1 && parameters.First().ToString()!.Contains(IdentityConstants.Separator_Comma))
        {
            parameters = parameters.First().ToString()!.Split(IdentityConstants.Separator_Comma);
        }
        return parameters;
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
        parameters = ValidateParamArray(parameters);

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
}
