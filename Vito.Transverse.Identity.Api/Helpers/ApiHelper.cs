using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using Twilio.Rest.Api.V2010.Account.Usage.Record;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Vito.Transverse.Identity.Domain.Models;

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


    public static string GetValueFromHeader(this HttpRequest request, string headerKey, string defaultValue = "")
    {
        var hasHeaderKey = request.Headers.ContainsKey(headerKey);
        var returnValue = hasHeaderKey ? request.Headers[headerKey].ToString() : defaultValue;
        return returnValue;
    }


    public static bool SetValueOnHeader(this HttpRequest request, string headerKey, string headerValue)
    {
        var returnValue = false;
        try
        {
            request.HttpContext.Response.Headers.Remove(headerKey);
            request.HttpContext.Response.Headers.Append(headerKey, headerValue);
            returnValue = true;
        }
        catch (Exception)
        {

        }

        return returnValue;
    }

    public static string GetApplicationIdFromHeader(this HttpRequest request)
    {
        var applicationId = request.GetValueFromHeader(FrameworkConstants.Header_ApplicationId);
        long companyIdNumber = 0;
        if (long.TryParse(applicationId, out long companyIdNumberTmp))
        {
            companyIdNumber = companyIdNumberTmp;
        }
        request.SetValueOnHeader(FrameworkConstants.Header_ApplicationId, applicationId);
        return applicationId; ;
    }

    public static long GetCompanyIdFromHeader(this HttpRequest request)
    {
        var companyId = request.GetValueFromHeader(FrameworkConstants.Header_CompanyId);
        long companyIdNumber = 0;
        if (long.TryParse(companyId, out long companyIdNumberTmp))
        {
            companyIdNumber=companyIdNumberTmp;
        }
        request.SetValueOnHeader(FrameworkConstants.Header_CompanyId, companyId);
        return companyIdNumber;
    }

    public static void SetCurrectCultureFromHeader(this HttpRequest request)
    {
        var cultureId = request.GetValueFromHeader(FrameworkConstants.Header_CultureId, _cultureOptions.DefaultCulture);
        //Set Culture on Main Thread
        _cultureService.SetCurrectCulture(cultureId!);

        request.SetValueOnHeader(FrameworkConstants.Header_CultureId, cultureId);

        // SetValidatorCulture(cultureId!);  
    }


    //private static void SetValidatorCulture(string cultureId)
    //{
    //    ValidatorOptions.Global.LanguageManager.Enabled=true;
    //    ValidatorOptions.Global.LanguageManager.Culture = _cultureService.GetCurrectCulture();
    //    ValidatorOptions.Global.LanguageManager = new ValidatorLanguageManager(_cultureService, cultureId!);
    //}

    public static CultureInfo GetCurrectCulture(this HttpRequest request)
    {
        var cultureInfo = _cultureService.GetCurrectCulture();
        return cultureInfo;
    }

}
