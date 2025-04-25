using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
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

    public static async Task<Results<Ok<DateTimeOffset>, NotFound, ValidationProblem>> GetUtcNow(
        HttpRequest request,
        [FromServices] ICultureService cultureService)
    {
        var returnObject = await Task.FromResult(cultureService.UtcNow());
        return TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<CultureDTO>>, NotFound, ValidationProblem>> GetActiveCultureListAsync(
        HttpRequest request,
        [FromServices] ICultureService cultureService)
    {
        var returnObject = await cultureService.GetActiveCultureListAsync();
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, NotFound, ValidationProblem>> GetActiveCultureListItemDTOListAsync(
        HttpRequest request,
        [FromServices] ICultureService cultureService)
    {
        var returnObject = await cultureService.GetActiveCultureListItemDTOListAsync();
        return TypedResults.Ok(returnObject);
    }
}