using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using  Vito.Transverse.Identity.Application.TransverseServices.Culture;
using  Vito.Transverse.Identity.Application.TransverseServices.Security;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.Endpoints;

/// <summary>
/// Home Endpoint
/// </summary>
/// <example>https://developer.usps.com/api/81</example>
public static class CultureEndpoint
{
    public static void MapCultureEndpoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Cultures/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CultureFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("UtcDate", GetUtcNow)
             .MapToApiVersion(1.0)
            .WithSummary("Get Utc Now")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("/", GetActiveCultureListAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Get Active Culture List Async")
             .WithDescription("[Require Authorization]");

        endPointGroupVersioned.MapGet("DropDrown", GetActiveCultureListItemDTOListAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Get Active Culture ListItemDTO List Async")
             .WithDescription("[Require Authorization]");
    }

    public static async Task<Results<Ok<DateTimeOffset>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUtcNow(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var returnObject = await Task.FromResult(cultureService.UtcNow());
        return TypedResults.Ok(returnObject);

    }

    public static async Task<Results<Ok<List<CultureDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActiveCultureListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await cultureService.GetActiveCultureListAsync(deviceInformation!.ApplicationId!.Value);
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActiveCultureListItemDTOListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await cultureService.GetActiveCultureListItemDTOListAsync(deviceInformation!.ApplicationId!.Value);
        return TypedResults.Ok(returnObject);
    }
}