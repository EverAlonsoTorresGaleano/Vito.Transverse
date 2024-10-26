using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;

namespace Vito.Transverse.Identity.Api.Endpoints;

/// <summary>
/// Home Endpoint
/// </summary>
public static class HomeEndpoint
{
    public static void MapHomeEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        //var endPointGroupNoVersioned = app.MapGroup("api/Home/");

        //endPointGroupNoVersioned.MapGet("Detect", DetectAync)
        //     .WithSummary("Detect version 1.0")
        //     .AddEndpointFilter<HomeFeatureFlagFilter>()
        //     .AddEndpointFilter<InfrastructureFilter>();

        //endPointGroupNoVersioned.MapGet("Ping", HomePingAync)
        //    .WithSummary("Ping version 1.0")
        //    .AddEndpointFilter<HomeFeatureFlagFilter>()
        //    .AddEndpointFilter<InfrastructureFilter>();


        var endPointGroupVersioned = app.MapGroup("api/Home/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
             .RequireAuthorization()
             .AddEndpointFilter<HomeFeatureFlagFilter>()
             .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("Detect", DetectAync)
             .MapToApiVersion(1.0)
             .WithSummary("Detect version 1.0"); ;

        endPointGroupVersioned.MapGet("Ping", HomePingAync)
            .MapToApiVersion(1.0)
            .WithSummary("Ping version 1.0"); ;

        endPointGroupVersioned.MapGet("Ping", HomePingV0_9Aync)
            .MapToApiVersion(0.9)
             .WithSummary("Ping version 0.9")
            .AllowAnonymous();
    }


    public static async Task<Ok<PingResponseDTO>> DetectAync(HttpRequest request, [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        PingResponseDTO returnObject = new()
        {
            PingMessage = "Detect v1.0",
            PingDate = cultureService.UtcNow().DateTime,
            DeviceInformation = deviceInformation!
        };
        var returnObjectAsync = await Task.FromResult(returnObject);
        return TypedResults.Ok(returnObjectAsync);
    }

    public static async Task<Ok<PingResponseDTO>> HomePingAync(HttpRequest request, [FromServices] ICultureService cultureService)
    {
        PingResponseDTO returnObject = new()
        {
            PingMessage = "Pong v1.0",
            PingDate = cultureService.UtcNow().DateTime,
        };
        var returnObjectAsync = await Task.FromResult(returnObject);
        return TypedResults.Ok(returnObjectAsync);
    }

    public static async Task<Ok<PingResponseDTO>> HomePingV0_9Aync(HttpRequest request, [FromServices] ICultureService cultureService)
    {
        PingResponseDTO returnObject = new()
        {
            PingMessage = "Pong v0.9",
            PingDate = cultureService.UtcNow().DateTime,
        };
        var returnObjectAsync = await Task.FromResult(returnObject);
        return TypedResults.Ok(returnObjectAsync);
    }

}