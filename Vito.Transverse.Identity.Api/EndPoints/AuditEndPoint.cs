using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Api.Filters;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Audit;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Endpoints;

public static class AuditEndPoint
{
    public static void MapAuditEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Audit/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<AuditFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("CompanyEntityAuditsListAsync", GetCompanyEntityAuditsListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Company Entity Audits List Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<AuthorizationFilter>();

        endPointGroupVersioned.MapGet("AuditRecordsListAsync", GetAuditRecordsListAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get Audit Records List Async")
            .WithDescription("[Require Authorization]")
           .RequireAuthorization()
          .AddEndpointFilter<AuthorizationFilter>();

        endPointGroupVersioned.MapGet("ActivityLogListAsync", GetActivityLogListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Activity Log List Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<AuthorizationFilter>();

        endPointGroupVersioned.MapGet("NotificationsListAsync", GetNotificationsListAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Get Notifications List Async")
            .WithDescription("[Require Authorization]")
             .RequireAuthorization()
          .AddEndpointFilter<AuthorizationFilter>();
    }

    public static async Task<Results<Ok<List<CompanyEntityAuditDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCompanyEntityAuditsListAsync(
       HttpRequest request,
       [FromServices] IAuditService auditService,
       [FromQuery] long? companyId)
    {
        var returnObject = await auditService.GetCompanyEntityAuditsListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<AuditRecordDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAuditRecordsListAsync(
       HttpRequest request,
       [FromServices] IAuditService auditService,
       [FromQuery] long? companyId)
    {
        var returnObject = await auditService.GetAuditRecordsListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ActivityLogDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActivityLogListAsync(
     HttpRequest request,
     [FromServices] IAuditService auditService,
     [FromQuery] long? companyId)
    {
        var returnObject = await auditService.GetActivityLogListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<NotificationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetNotificationsListAsync(
    HttpRequest request,
    [FromServices] IAuditService auditService,
    [FromQuery] long? companyId)
    {
        var returnObject = await auditService.GetNotificationsListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }
}