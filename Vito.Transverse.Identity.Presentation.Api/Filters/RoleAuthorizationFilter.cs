
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Application.TransverseServices.Applications;

namespace Vito.Transverse.Identity.Presentation.Api.Filters;


/// <summary>
/// In Addition Authentication Bearrer Token Check Authorization PErmission for endpoints
/// </summary>
/// <param name="securityService"></param>
public class RoleAuthorizationFilter(IApplicationsService  applicationsService) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {

        var deviceInformation = context.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var hasPermissions = await applicationsService.ValidateEndpointAuthorizationAsync(deviceInformation!);
        if (hasPermissions is null)
        {
           // return TypedResults.Unauthorized();
        }
        return await next(context);
    }
}