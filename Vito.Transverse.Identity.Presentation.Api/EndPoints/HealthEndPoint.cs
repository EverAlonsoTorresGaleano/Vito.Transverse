using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using  Vito.Transverse.Identity.Application.TransverseServices.Audit;
using  Vito.Transverse.Identity.Application.TransverseServices.Caching;
using  Vito.Transverse.Identity.Application.TransverseServices.Culture;
using  Vito.Transverse.Identity.Application.TransverseServices.Security;
using Vito.Transverse.Identity.Entities.Enums;

namespace Vito.Transverse.Identity.Presentation.Api.Endpoints;

public static class HealthEndPoint
{
    public static void MapHealthEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Health/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
        .AddEndpointFilter<AuditFeatureFlagFilter>()
        .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("/", HealthCheckAllAsync)
        .MapToApiVersion(1.0)
        .WithSummary("HealthCheck Async")
        .WithDescription("[Require Authorization]")
        .RequireAuthorization()
        .AddEndpointFilter<RoleAuthorizationFilter>();


        endPointGroupVersioned.MapGet("cache", HealthCheckCacheAsync)
        .MapToApiVersion(1.0)
        .WithSummary("CacheAsync Async")
        .WithDescription("[Require Authorization]")
        .RequireAuthorization()
        .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("database", HealthCheckDatabaseAsync)
        .MapToApiVersion(1.0)
        .WithSummary("DatabaseAsync Async")
        .WithDescription("[Require Authorization]")
        .RequireAuthorization()
        .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Detect", DetectAync)
             .MapToApiVersion(1.0)
             .WithSummary("Detect version 1.0")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("Ping", HomePingAync)
            .MapToApiVersion(1.0)
            .WithSummary("Ping version 1.0")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("Detect", DetectV0_9Aync)
            .MapToApiVersion(0.9)
             .WithSummary("Ping version 0.9")
            .WithDescription("[Anonymous]")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("Ping", HomePingV0_9Aync)
            .MapToApiVersion(0.9)
             .WithSummary("Ping version 0.9")
            .WithDescription("[Anonymous]")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("Ping2", HomePingV0_9Aync)
            .MapToApiVersion(0.9)
            .WithSummary("Ping version 0.9")
            .WithDescription("[Anonymous]")
            .AllowAnonymous();

    }

    public static async Task<Results<Ok<List<HealthCheckResult>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> HealthCheckAllAsync(
       HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromServices] ICachingServiceMemoryCache cacheService,
        [FromServices] ICultureService cultureService)
    {
        var cacheCheck = HealthCheckCacheAsync(request, cacheService, cultureService);
        var cacheCheckResult = cacheCheck.Result as Ok<HealthCheckResult>;

        var databaseCheck = await HealthCheckDatabaseAsync(request, auditService, cultureService);
        var databaseCheckResult = databaseCheck.Result as Ok<HealthCheckResult>;

        List<HealthCheckResult> healthCheckList = [cacheCheckResult!.Value, databaseCheckResult!.Value];

        return TypedResults.Ok(healthCheckList);
    }

    public static Results<Ok<HealthCheckResult>, UnauthorizedHttpResult, NotFound, ValidationProblem> HealthCheckCacheAsync(
   HttpRequest request,
       [FromServices] ICachingServiceMemoryCache cacheService,
       [FromServices] ICultureService cultureService)
    {
        HealthStatus healthCheckStatus = HealthStatus.Healthy;
        Dictionary<string, object> healthData = [];
        var startDate = cultureService.UtcNow().ToLocalTime();
        healthData.Add(nameof(startDate), startDate);
        try
        {
            var returnValue = cacheService.SetCacheData(CacheItemKeysEnum.HealthCheck.ToString(), CacheItemKeysEnum.HealthCheck.ToString());
            healthData.Add(nameof(cacheService.SetCacheData), returnValue);
            var cacheData = cacheService.GetCacheDataByKey<object>(CacheItemKeysEnum.HealthCheck.ToString());
            if (!cacheData!.ToString()!.Equals(CacheItemKeysEnum.HealthCheck.ToString()))
            {
                healthCheckStatus = HealthStatus.Degraded;
                healthData.Add(nameof(cacheService.GetCacheDataByKey), false);
            }
            else
            {
                healthData.Add(nameof(cacheService.GetCacheDataByKey), true);
            }
            returnValue = cacheService.DeleteCacheDataByKey(CacheItemKeysEnum.HealthCheck.ToString());
            healthData.Add(nameof(cacheService.DeleteCacheDataByKey), true);
        }
        catch (Exception ex)
        {
            healthCheckStatus = HealthStatus.Unhealthy;
            healthData.Add(nameof(Exception), ex.Message);
        }
        var endDate = cultureService.UtcNow().ToLocalTime();
        healthData.Add(nameof(endDate), endDate);
        var totalTime = endDate - startDate;
        healthData.Add(nameof(totalTime), totalTime);
        HealthCheckResult healthCheck = new(healthCheckStatus, nameof(HealthCheckCacheAsync), null, healthData);
        return TypedResults.Ok(healthCheck);
    }

    public static async Task<Results<Ok<HealthCheckResult>, UnauthorizedHttpResult, NotFound, ValidationProblem>> HealthCheckDatabaseAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromServices] ICultureService cultureService)
    {
        HealthStatus healthCheckStatus = HealthStatus.Healthy;
        Dictionary<string, object> healthData = [];
        var startDate = cultureService.UtcNow().ToLocalTime();
        healthData.Add(nameof(startDate), startDate);
        try
        {
            var healthDataParams = await auditService.GetDatabaseHealth();
            healthDataParams.ToList().ForEach(x => healthData.Add(x.Key, x.Value));
        }
        catch (Exception ex)
        {
            healthCheckStatus = HealthStatus.Unhealthy;
            healthData.Add(nameof(Exception), ex.Message);
        }
        var endDate = cultureService.UtcNow().ToLocalTime();
        healthData.Add(nameof(endDate), endDate);
        var totalTime = endDate - startDate;
        healthData.Add(nameof(totalTime), totalTime);
        HealthCheckResult healthCheck = new(healthCheckStatus, nameof(HealthCheckDatabaseAsync), null, healthData);
        return TypedResults.Ok(healthCheck);
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