using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.Api.Filters;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Vito.Transverse.Identity.Domain.Enums;

namespace Vito.Transverse.Identity.Api.Endpoints;

public static class CacheEndpoint
{
    public static void MapCacheEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Cache/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CacheFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Cache", ClearCache)
            .MapToApiVersion(1.0)
            .WithSummary("Delete All Cache Collections")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapDelete("CacheByKey", DeleteCacheDataByKey)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Cache by Key")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("CacheList", GetCacheList)
            .MapToApiVersion(1.0)
            .WithSummary("Get Cache Collentions List")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();
    }

    public static Results<Ok<bool>, UnauthorizedHttpResult> DeleteCacheDataByKey(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICachingServiceMemoryCache cacheService,
        [FromQuery] string cacheName) // CacheItemKeysEnum
    {
        var returnValue = cacheService.DeleteCacheDataByKey(cacheName);
        return TypedResults.Ok(returnValue);
    }

    public static async Task<Results<Ok<bool>, UnauthorizedHttpResult>> ClearCache(
      HttpRequest request,
      [FromServices] ISecurityService securityService,
      [FromServices] ICachingServiceMemoryCache cacheService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var returnValue = cacheService.ClearCacheData();
        await securityService.AddNewActivityLogAsync(deviceInformation!, OAuthActionTypeEnum.OAuthActionType_ClearCache);
        return TypedResults.Ok(returnValue);
    }

    public static Results<Ok<List<CacheSummaryDTO>>, UnauthorizedHttpResult> GetCacheList(
          HttpRequest request,
          [FromServices] ISecurityService securityService,
          [FromServices] ICachingServiceMemoryCache cacheService)
    {
        var returnValue = cacheService.GetCacheDataByKey<List<CacheSummaryDTO>>(CacheItemKeysEnum.All.ToString());
        return TypedResults.Ok(returnValue);
    }
}