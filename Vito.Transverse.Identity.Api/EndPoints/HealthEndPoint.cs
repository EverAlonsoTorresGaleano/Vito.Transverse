using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Extensions;
using Vito.Transverse.Identity.Api.Filters;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Audit;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.Domain.Enums;

namespace Vito.Transverse.Identity.Api.Endpoints;

public static class HealthEndPoint
{
    public static void MapHealthEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Health/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<AuditFeatureFlagFilter>()
        .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("AllAsync", HealthCheckAllAsync)
            .MapToApiVersion(1.0)
            .WithSummary("HealthCheck Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();


        endPointGroupVersioned.MapGet("CacheAsync", HealthCheckCacheAsync)
        .MapToApiVersion(1.0)
        .WithSummary("HealthCheck Async")
        .WithDescription("[Require Authorization]")
        .RequireAuthorization()
        .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("DatabaseAsync", HealthCheckDatabaseAsync)
        .MapToApiVersion(1.0)
        .WithSummary("HealthCheck Async")
        .WithDescription("[Require Authorization]")
        .RequireAuthorization()
        .AddEndpointFilter<RoleAuthorizationFilter>();

    }

    public static async Task<Results<Ok<List<HealthCheckResult>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> HealthCheckAllAsync(
       HttpRequest request,
       [FromServices] IAuditService auditService,
        [FromServices] ICachingServiceMemoryCache cacheService)
    {
        var cacheCheck = HealthCheckCacheAsync(request, cacheService);
        var cacheCheckResult = cacheCheck.Result as Ok<HealthCheckResult>;

        var databaseCheck = await HealthCheckDatabaseAsync(request, auditService);
        var databaseCheckResult = databaseCheck.Result as Ok<HealthCheckResult>;

        List<HealthCheckResult> healthCheckList = [cacheCheckResult!.Value, databaseCheckResult!.Value];

        return TypedResults.Ok(healthCheckList);
    }

    public static Results<Ok<HealthCheckResult>, UnauthorizedHttpResult, NotFound, ValidationProblem> HealthCheckCacheAsync(
   HttpRequest request,
   [FromServices] ICachingServiceMemoryCache cacheService)
    {
        HealthCheckResult healthCheck = new(HealthStatus.Healthy, "HealthCheckCacheAsync - Service Available"); ;
        try
        {
            cacheService.SetCacheData(CacheItemKeysEnum.HealthCheck.ToString(), CacheItemKeysEnum.HealthCheck.ToString());
            var cacheData = cacheService.GetCacheDataByKey<object>(CacheItemKeysEnum.HealthCheck.ToString());
            if (!cacheData!.ToString()!.Equals(CacheItemKeysEnum.HealthCheck.ToString()))
            {
                healthCheck = new(HealthStatus.Degraded, "HealthCheckCacheAsync - Service Unavailable");
            }
            cacheService.DeleteCacheDataByKey(CacheItemKeysEnum.HealthCheck.ToString());
        }
        catch (Exception ex)
        {
            healthCheck = new(HealthStatus.Unhealthy, $"HealthCheckCacheAsync {ex.Message}");
        }
        return TypedResults.Ok(healthCheck);
    }

    public static async Task<Results<Ok<HealthCheckResult>, UnauthorizedHttpResult, NotFound, ValidationProblem>> HealthCheckDatabaseAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService)
    {
        HealthCheckResult healthCheck = new(HealthStatus.Healthy, "HealthCheckDatabaseAsync - Service Available"); ;
        try
        {
            var returnList = await auditService.GetEntityListAsync();

            if (returnList.Count == 0)
            {
                healthCheck = new(HealthStatus.Degraded, "HealthCheckDatabaseAsync - Service Unavailable");
            }
        }
        catch (Exception ex)
        {
            healthCheck = new(HealthStatus.Unhealthy, $"HealthCheckDatabaseAsync {ex.GetErrorStakTrace()}");
        }
        return TypedResults.Ok(healthCheck);


    }
}