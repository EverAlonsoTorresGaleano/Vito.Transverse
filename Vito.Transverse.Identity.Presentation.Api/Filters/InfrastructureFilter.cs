using Microsoft.Extensions.Options;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.Presentation.Api.Helpers;
using Vito.Transverse.Identity.Application.TransverseServices.Culture;
using Vito.Transverse.Identity.Application.TransverseServices.Security;
using Wangkanai.Detection.Services;

namespace Vito.Framework.Api.Filters;
public class InfrastructureFilter : IEndpointFilter
{
    private const string CONSTANT_APIVERSION_TEXT = "apiVersion";
    private const string CONSTANT_APIVERSION_TEXTFULL = "{apiVersion:apiVersion}";
    private const string CONSTANT_ENDPOINT_URL_PREFIX = "/";

    public readonly IDetectionService _userDeviceDetectionService;

    public InfrastructureFilter(ICultureService cultureService, IDetectionService userDeviceDetectionService, ISecurityService securityService, IOptions<CultureSettingsOptions> cultureOptions)
    {
        ApilHelper.InitializeApiHelper(cultureService, securityService, cultureOptions);
        _userDeviceDetectionService = userDeviceDetectionService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.HttpContext.Request;
        var response = context.HttpContext.Response;
        var arguments = context.Arguments;

        request.SetCurrectCultureFromHeader();
        var detectRequestInfo = DetectRequestInfo(context.HttpContext);

        context.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] = detectRequestInfo;
        return await next(context);
    }


    public DeviceInformationDTO DetectRequestInfo(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;

        var jwtTokenBearer = request.Headers.Authorization.ToString();
        var jwtToken = jwtTokenBearer.DecodeJwtTokenSync();
        DeviceInformationDTO deviceInfo = new()
        {
            HostName = request.Host.ToString(),
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            DeviceType = _userDeviceDetectionService.Device.Type.ToString(),
            Browser = $"{_userDeviceDetectionService.Browser.Name.ToString()} v{_userDeviceDetectionService.Browser.Version.ToString()}",
            Platform = $"{_userDeviceDetectionService.Platform.Name.ToString()} v{_userDeviceDetectionService.Platform.Version.ToString()} [{_userDeviceDetectionService.Platform.Processor.ToString()}]",
            Engine = $"{_userDeviceDetectionService.Engine.Name.ToString()}",


            EndPointPattern = GetEndPointPattern(request.HttpContext),
            EndPointUrl = request.HttpContext.Request.Path!,
            Method = request.Method,
            QueryString = request.QueryString.ToString(),
            UserAgent = request.Headers.UserAgent.ToString(),
            Referer = request.Headers.Referer.ToString(),
            ApplicationId = jwtToken!.GetJwtTokenClaimLong(CustomClaimTypes.ApplicationId.ToString()) ?? FrameworkConstants.Application_DefaultId,
            CompanyId = jwtToken!.GetJwtTokenClaimLong(CustomClaimTypes.CompanyId.ToString()) ?? FrameworkConstants.Company_DefaultId,
            UserId = jwtToken!.GetJwtTokenClaimLong(CustomClaimTypes.UserId.ToString()) ?? FrameworkConstants.UserId_UserUnknown,
            RoleId = jwtToken!.GetJwtTokenClaimLong(CustomClaimTypes.RoleId.ToString()) ?? FrameworkConstants.RoleId_UserUnknown,
            CultureId = request.GetCurrectCulture().Name,

        };
        return deviceInfo;
    }

    private string GetEndPointPattern(HttpContext httpContext)
    {
        var endPoint = httpContext.GetEndpoint() as RouteEndpoint;
        var endPointPattern = endPoint!.RoutePattern.RawText;
        var apiVersion = httpContext.GetRouteValue(CONSTANT_APIVERSION_TEXT);
        var enpointUrl = endPointPattern!.Replace($"{CONSTANT_APIVERSION_TEXTFULL}", apiVersion.ToString());
        enpointUrl = CONSTANT_ENDPOINT_URL_PREFIX + enpointUrl;
        return enpointUrl;
    }
}