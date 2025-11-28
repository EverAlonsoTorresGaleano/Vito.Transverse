using Vito.Framework.Common.DTO;
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

}
