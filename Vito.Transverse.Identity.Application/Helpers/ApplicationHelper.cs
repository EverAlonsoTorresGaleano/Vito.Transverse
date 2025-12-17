using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Application.TransverseServices.Localization;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Application.Helpers;

public static class ApplicationHelper
{

    public static CultureTranslationDTO ToCultureTranslationDTO(this DeviceInformationDTO deviceInfo, string key, string value)
    {
        CultureTranslationDTO cultureTranslation = new()
        {
            ApplicationFk = deviceInfo.ApplicationId,
            CultureFk = Thread.CurrentThread.CurrentCulture.Name,
            TranslationKey = key,
            TranslationValue = value

        };
        return cultureTranslation;
    }

    public static async Task UpsetTranslations(this ILocalizationService localizationService,DeviceInformationDTO deviceInformation, string nameTranslationKey, string nameTranslationValue, string descriptionTranslationKey, string descriptionTranslationValue)
    {
        var nameTranslation = deviceInformation.ToCultureTranslationDTO(nameTranslationKey, nameTranslationValue);
        var descriptionTranslation = deviceInformation.ToCultureTranslationDTO(descriptionTranslationKey, descriptionTranslationValue);

        await localizationService.UpsertCultureTranslationMasiveAsync([nameTranslation, descriptionTranslation]);
    }

}
