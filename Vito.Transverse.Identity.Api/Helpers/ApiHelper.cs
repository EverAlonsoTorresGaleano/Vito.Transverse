using Microsoft.Extensions.Options;
using System.Globalization;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;

namespace Vito.Transverse.Identity.Api.Helpers;

public static class ApilHelper
{
    private static ICultureService _cultureService = default!;
    private static ISecurityService _securityService = default!;
    private static CultureSettingsOptions _cultureOptions = default!;
    //private static ILocalizationService localizationService;
    public static void InitializeApiHelper(ICultureService systemClock, ISecurityService securityService, IOptions<CultureSettingsOptions> culture)
    {
        //localizationService = _localizationService;
        _securityService = securityService;
        _cultureService = systemClock;
        _cultureOptions = culture.Value;
    }

    public static void SetCurrectCultureFromHeader(this HttpRequest request)
    {

        var hasCultureCookie = request.Headers.ContainsKey(FrameworkConstants.Header_CultureId);
        var cultureId = hasCultureCookie ? request.Headers[FrameworkConstants.Header_CultureId].ToString() : _cultureOptions.DefaultCulture;
        //Set Culture on Main Thread
        _cultureService.SetCurrectCulture(cultureId!);

        //TODO Set Time / fromats
        request.HttpContext.Response.Headers.Remove(FrameworkConstants.Header_CultureId);
        request.HttpContext.Response.Headers.Append(FrameworkConstants.Header_CultureId, cultureId);
    }


    public static CultureInfo GetCurrectCulture(this HttpRequest request)
    {
        var cultureInfo = _cultureService.GetCurrectCulture();
        return cultureInfo;
    }

}
