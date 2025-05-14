using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Localization;
using Vito.Transverse.Identity.Domain.Constants;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Localization;

public class LocalizationService(ILocalizationRepository _localizationRepository, ICachingServiceMemoryCache _cachingService, ILogger<LocalizationService> _logger, IOptions<CultureSettingsOptions> _cultureSettingsOptions) : ILocalizationService
{
    CultureSettingsOptions _cultureSettingsOptionsValues = _cultureSettingsOptions.Value;


    public async Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesByApplicationAsync(long applicationId)
    {
        var applicationMessageList = _cachingService.GetCacheDataByKey<List<CultureTranslationDTO>>(CacheItemKeysEnum.CultureTranslationsListByApplicationId + applicationId.ToString());
        List<CultureTranslationDTO> returnList = new();
        if (applicationMessageList == null)
        {
            applicationMessageList = await _localizationRepository.GetAllLocalizedMessagesByApplicationAsync(applicationId);
            _cachingService.SetCacheData(CacheItemKeysEnum.CultureTranslationsListByApplicationId + applicationId.ToString(), applicationMessageList);
        }
        return applicationMessageList;
    }


    //Fron end is suppoused to created json file on ther local files to render messages quickly
    public async Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesAsync(long applicationId, string cultureId)
    {
        List<CultureTranslationDTO> cultureMessageList = new();
        try
        {
            cultureId = !string.IsNullOrEmpty(cultureId) ? cultureId : CultureInfo.CurrentCulture.Name;
            cultureMessageList = _cachingService.GetCacheDataByKey<List<CultureTranslationDTO>>(CacheItemKeysEnum.CultureTranslationsListByApplicationIdCultureId + applicationId.ToString() + cultureId);
            if (cultureMessageList == null)
            {
                var applicationMessageList = await _localizationRepository.GetAllLocalizedMessagesByApplicationAsync(applicationId);
                cultureMessageList = applicationMessageList.Where(x => x.CultureFk.Equals(cultureId)).ToList();
#if DEBUG
                var fileName = string.Format(_cultureSettingsOptionsValues.LocalizationJsonFilePath!, cultureId);
                var localizationFile = new StringBuilder("{");
                cultureMessageList.ForEach(localizationRow =>
                {
                    localizationFile.Append($"{(char)34}{localizationRow.TranslationKey}{(char)34}:{(char)34}{localizationRow.TranslationValue}{(char)34},");
                });
                localizationFile.Remove(localizationFile.Length - 1, 1);
                localizationFile.Append("}");

                File.Delete(fileName);
                File.WriteAllText(fileName, localizationFile.ToString());
#endif
                _cachingService.SetCacheData(CacheItemKeysEnum.CultureTranslationsListByApplicationIdCultureId + applicationId.ToString() + cultureId, cultureMessageList);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllLocalizedMessagesAsync));
            throw;
        }
        return cultureMessageList;
    }


    public async Task<List<CultureTranslationDTO>> GetLocalizedMessagesByKeyAsync(long applicationId, string localizationMessageKey)
    {
        List<CultureTranslationDTO> returnList = new();
        try
        {
            var applicationMessageList = await _localizationRepository.GetAllLocalizedMessagesByApplicationAsync(applicationId);
            returnList = applicationMessageList.Where(x => x.TranslationKey.Equals(localizationMessageKey)).ToList();
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, message: nameof(GetLocalizedMessagesByKeyAsync));
            throw;
        }
        return returnList;
    }

    public CultureTranslationDTO GetLocalizedMessageByKeyAndParamsSync(long applicationId, string cultureId, string localizationMessageKey, params object?[] parameters)
    {
        CultureTranslationDTO? localizedMessage = default;
        try
        {
            cultureId = !string.IsNullOrEmpty(cultureId) ? cultureId : CultureInfo.CurrentCulture.Name;
            parameters = ValidateParamArray(parameters!)!;

            var returnList = GetAllLocalizedMessagesAsync(applicationId, cultureId).GetAwaiter().GetResult();

            localizedMessage = returnList.FirstOrDefault(x => x.TranslationKey.Equals(localizationMessageKey));


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
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetLocalizedMessageByKeyAndParamsSync));
            throw;
        }
        return localizedMessage;
    }


    private static object[]? ValidateParamArray(object[]? parameters)
    {
        if (parameters?.Length == 1 && parameters.First().ToString()!.Contains(IdentityConstants.Separator_Comma))
        {
            parameters = parameters.First().ToString()!.Split(IdentityConstants.Separator_Comma);
        }
        return parameters;
    }
}
