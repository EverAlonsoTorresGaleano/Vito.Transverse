using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.Api.Helpers;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Endpoints;

/// <summary>
/// Home Endpoint
/// </summary>
/// <example>https://developer.usps.com/api/81</example>
public static class CultureEndpoint
{

    public static void MapCultureEndpoint(this WebApplication app, ApiVersionSet versionSet)
    {
        //var endPointGroupNoVersioned = app.MapGroup("api/Oauth2/");

        //endPointGroupNoVersioned.MapPost("Token", TokenAync)
        //    .WithSummary("Get Authentication Token")
        //    .AddEndpointFilter<SecurityFeatureFlagFilter>()
        //    .AddEndpointFilter<InfrastructureFilter>();

        var endPointGroupVersioned = app.MapGroup("api/Culture/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CultureFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("UtcNow", GetUtcNow)
             .MapToApiVersion(1.0)
            .WithSummary("GetUtcNow")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("ActiveCultureListAsync", GetActiveCultureListAsync)
             .MapToApiVersion(1.0)
            .WithSummary("GetActiveCultureListAsync");

        endPointGroupVersioned.MapGet("ActiveCultureListItemDTOListAsync", GetActiveCultureListItemDTOListAsync)
             .MapToApiVersion(1.0)
            .WithSummary("GetActiveCultureListItemDTOListAsync");
    }

    public static async Task<Results<Ok<DateTimeOffset>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUtcNow(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var hasPermissions = await securityService.ValidateEndpointAuthorizationAsync(deviceInformation!);
        if (hasPermissions is null)
        {
            return TypedResults.Unauthorized();
        }
        else
        {
            var returnObject = await Task.FromResult(cultureService.UtcNow());
            return TypedResults.Ok(returnObject);
        }
    }


    public static async Task<Results<Ok<List<CultureDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActiveCultureListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var hasPermissions = await securityService.ValidateEndpointAuthorizationAsync(deviceInformation!);
        if (hasPermissions is null)
        {
            return TypedResults.Unauthorized();
        }
        else
        {
            var applicationId = request.GetCompanyIdFromHeader();
            var returnObject = await cultureService.GetActiveCultureListAsync(applicationId);
            return TypedResults.Ok(returnObject);
        }
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActiveCultureListItemDTOListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var hasPermissions = await securityService.ValidateEndpointAuthorizationAsync(deviceInformation!);
        if (hasPermissions is null)
        {
            return TypedResults.Unauthorized();
        }
        else
        {
            var applicationId = request.GetCompanyIdFromHeader();
            var returnObject = await cultureService.GetActiveCultureListItemDTOListAsync(applicationId);
            return TypedResults.Ok(returnObject);
        }
    }
}