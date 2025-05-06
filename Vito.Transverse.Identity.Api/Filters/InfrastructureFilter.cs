using Microsoft.Extensions.Options;
using System.Net;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.Api.Helpers;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Wangkanai.Detection.Services;

namespace Vito.Framework.Api.Filters;
public class InfrastructureFilter : IEndpointFilter
{
    public readonly IDetectionService _userDeviceDetectionService;

    public InfrastructureFilter(ICultureService cultureService, IDetectionService userDeviceDetectionService, ISecurityService securityService, IOptions<CultureSettingsOptions> cultureOptions)
    {
        ApilHelper.InitializeApiHelper(cultureService, securityService, cultureOptions);
        _userDeviceDetectionService = userDeviceDetectionService;

    }

    public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.HttpContext.Request;
        var response = context.HttpContext.Response;
        var arguments = context.Arguments;

        request.SetCurrectCultureFromHeader();
        var detectRequestInfo = DetectRequestInfo(context.HttpContext);

        context.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] = detectRequestInfo;
        return next(context);
    }


    public DeviceInformationDTO DetectRequestInfo(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;

        DeviceInformationDTO deviceInfo = new()
        {
            Name = Dns.GetHostName(),
            IpAddress = string.Join(",", Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => !x.IsIPv6LinkLocal).ToList().Select(x => x)),
            DeviceType = _userDeviceDetectionService.Device.Type.ToString(),
            Browser = $"{_userDeviceDetectionService.Browser.Name.ToString()} v{_userDeviceDetectionService.Browser.Version.ToString()}",
            Platform = $"{_userDeviceDetectionService.Platform.Name.ToString()} v{_userDeviceDetectionService.Platform.Version.ToString()} [{_userDeviceDetectionService.Platform.Processor.ToString()}]",
            Engine = $"{_userDeviceDetectionService.Engine.Name.ToString()}",
            CultureId = request.GetCurrectCulture().Name,
            Scope= request.HttpContext.Request.Path,
        };

        var additionalInfo = deviceInfo.AddtionalInfo;
        additionalInfo = [];

        additionalInfo.Add(new("Endpoint", request.HttpContext.GetEndpoint()!.ToString()!));
        //returList.Add(new( "ApiId", request.GetRequestHeaderApiId() ));
        //returList.Add(new( "ApiScret", request.GetRequestHeaderApiScret() ));
        //returList.Add(new( "CompanyId", request.GetRequestHeaderCompanyId().ToString() ));
        additionalInfo.Add(new("Referer", request.Headers.Referer.ToString()));
        additionalInfo.Add(new("UserAgent", request.Headers.UserAgent.ToString()));
        additionalInfo.Add(new("Authorization", request.Headers.Authorization.ToString()));
        deviceInfo.AddtionalInfo = additionalInfo;
        return deviceInfo;
    }

}