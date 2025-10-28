using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using  Vito.Transverse.Identity.Application.TransverseServices.Audit;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.Endpoints;

public static class AuditEndPoint
{
    public static void MapAuditEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Auditories/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<AuditFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();


        endPointGroupVersioned.MapGet("/", GetAuditRecordsListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Audit Records List Async")
           .WithDescription("[Require Authorization]")
          .RequireAuthorization()
         .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("CompanyEntityAudits", GetCompanyEntityAuditsListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Company Entity Audits List Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("ActivityLogs", GetActivityLogListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Activity Log List Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Notifications", GetNotificationsListAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Get Notifications List Async")
            .WithDescription("[Require Authorization]")
             .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Entities", GetEntityListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Entity List Async")
         .WithDescription("[Require Authorization]")
         .RequireAuthorization()
       .AddEndpointFilter<RoleAuthorizationFilter>();
    }

    public static async Task<Results<Ok<List<CompanyEntityAuditDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCompanyEntityAuditsListAsync(
       HttpRequest request,
       [FromServices] IAuditService auditService,
       [FromQuery] long? companyId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await auditService.GetCompanyEntityAuditsListAsync(companyId ?? deviceInformation!.CompanyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<AuditRecordDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAuditRecordsListAsync(
       HttpRequest request,
       [FromServices] IAuditService auditService,
       [FromQuery] long? companyId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await auditService.GetAuditRecordsListAsync(companyId ?? deviceInformation!.CompanyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ActivityLogDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActivityLogListAsync(
     HttpRequest request,
     [FromServices] IAuditService auditService,
     [FromQuery] long? companyId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await auditService.GetActivityLogListAsync(companyId ?? deviceInformation!.CompanyId);
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

    public static async Task<Results<Ok<List<EntityDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetEntityListAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService)
    {
        var returnObject = await auditService.GetEntityListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }
}