using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.Application.TransverseServices.Audit;
using Vito.Transverse.Identity.Application.TransverseServices.Caching;
using Vito.Transverse.Identity.Application.TransverseServices.Security;
using Vito.Transverse.Identity.Entities.Enums;

namespace Vito.Transverse.Identity.Presentation.Api.Endpoints;

public static class CacheEndpoint
{
    public static void MapCacheEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Cache/v{apiVersion:apiVersion}").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CacheFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("", GetCacheList)
            .MapToApiVersion(1.0)
            .WithSummary("Get Cache Collentions List")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapDelete("clear", ClearCache)
            .MapToApiVersion(1.0)
            .WithSummary("Delete All Cache Collections")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapDelete("delete/{cacheKey}", DeleteCacheDataByKey)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Cache by Key")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();


    }

    public static Results<Ok<bool>, UnauthorizedHttpResult> DeleteCacheDataByKey(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICachingServiceMemoryCache cacheService,
        [FromRoute] string cacheKey) // CacheItemKeysEnum
    {
        var returnValue = cacheService.DeleteCacheDataByKey(cacheKey);
        return TypedResults.Ok(returnValue);
    }

    public static async Task<Results<Ok<bool>, UnauthorizedHttpResult>> ClearCache(
      HttpRequest request,
      [FromServices] ISecurityService securityService,
           [FromServices] IAuditService auditService,
      [FromServices] ICachingServiceMemoryCache cacheService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var returnValue = cacheService.ClearCacheData();
        await auditService.AddNewActivityLogAsync(deviceInformation!, OAuthActionTypeEnum.OAuthActionType_ClearCache);
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