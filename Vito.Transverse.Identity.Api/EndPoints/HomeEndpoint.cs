using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;

namespace Vito.Transverse.Identity.Api.Endpoints;

public static class HomeEndpoint
{
    public static void MapHomeEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Home/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
             .RequireAuthorization()
             .AddEndpointFilter<HomeFeatureFlagFilter>()
             .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("DetectAsync", DetectAync)
             .MapToApiVersion(1.0)
             .WithSummary("Detect version 1.0")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("PingAsync", HomePingAync)
            .MapToApiVersion(1.0)
            .WithSummary("Ping version 1.0")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("DetectAsync", DetectV0_9Aync)
            .MapToApiVersion(0.9)
             .WithSummary("Ping version 0.9")
            .WithDescription("[Anonymous]")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("PingAsync", HomePingV0_9Aync)
            .MapToApiVersion(0.9)
             .WithSummary("Ping version 0.9")
            .WithDescription("[Anonymous]")
            .AllowAnonymous();


    }


    public static async Task<Ok<PingResponseDTO>> DetectV0_9Aync(
    HttpRequest request,
    [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        PingResponseDTO returnObject = new()
        {
            PingMessage = "Detect v0.9",
            PingDate = cultureService.UtcNow().DateTime,
            DeviceInformation = deviceInformation!
        };
        var returnObjectAsync = await Task.FromResult(returnObject);
        return TypedResults.Ok(returnObjectAsync);
    }


    public static async Task<Results<Ok<PingResponseDTO>, UnauthorizedHttpResult>> DetectAync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
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

    public static async Task<Ok<PingResponseDTO>> HomePingV0_9Aync(
     HttpRequest request,
     [FromServices] ICultureService cultureService)
    {
        PingResponseDTO returnObject = new()
        {
            PingMessage = "Pong v0.9",
            PingDate = cultureService.UtcNow().DateTime,
        };
        var returnObjectAsync = await Task.FromResult(returnObject);
        return TypedResults.Ok(returnObjectAsync);
    }

    public static async Task<Results<Ok<PingResponseDTO>, UnauthorizedHttpResult>> HomePingAync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {

        PingResponseDTO returnObject = new()
        {
            PingMessage = "Pong v1.0",
            PingDate = cultureService.UtcNow().DateTime,
        };
        var returnObjectAsync = await Task.FromResult(returnObject);
        return TypedResults.Ok(returnObjectAsync);

    }



}