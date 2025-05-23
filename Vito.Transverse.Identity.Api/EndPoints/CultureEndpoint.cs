using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
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
        var endPointGroupVersioned = app.MapGroup("api/Culture/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CultureFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("UtcNow", GetUtcNow)
             .MapToApiVersion(1.0)
            .WithSummary("Get Utc Now")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("ActiveCultureListAsync", GetActiveCultureListAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Get Active Culture List Async")
             .WithDescription("[Require Authorization]");

        endPointGroupVersioned.MapGet("ActiveCultureListItemDTOListAsync", GetActiveCultureListItemDTOListAsync)
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
        var applicationId = request.GetCompanyIdFromHeader();
        var returnObject = await cultureService.GetActiveCultureListAsync(applicationId);
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActiveCultureListItemDTOListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var applicationId = request.GetCompanyIdFromHeader();
        var returnObject = await cultureService.GetActiveCultureListItemDTOListAsync(applicationId);
        return TypedResults.Ok(returnObject);
    }
}