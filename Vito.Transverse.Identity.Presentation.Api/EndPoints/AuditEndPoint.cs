using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.Presentation.Api.Validators;
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


        endPointGroupVersioned.MapGet("/AuditRecords", GetAuditRecordsListAsync)
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

        endPointGroupVersioned.MapGet("CompanyEntityAudits/{companyEntityAuditId}", GetCompanyEntityAuditByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Company Entity Audit By Id Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("CompanyEntityAudits", CreateNewCompanyEntityAuditAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Company Entity Audit Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("CompanyEntityAudits", UpdateCompanyEntityAuditByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Company Entity Audit By Id Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("CompanyEntityAudits/Delete/{companyEntityAuditId}", DeleteCompanyEntityAuditByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Company Entity Audit By Id Async")
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

        endPointGroupVersioned.MapGet("Entities/dropdown", GetEntityListItemAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get Entity List Async")
          .WithDescription("[Require Authorization]")
          .RequireAuthorization()
        .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Entities/{entityId}", GetEntityByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Entity By Id Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("Entities", CreateNewEntityAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Entity Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Entities", UpdateEntityByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Entity By Id Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Entities/Delete/{entityId}", DeleteEntityByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Entity By Id Async")
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

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetEntityListItemAsync(
    HttpRequest request,
    [FromServices] IAuditService auditService)
    {
        var returnObject = await auditService.GetEntityListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<EntityDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetEntityByIdAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromRoute] long entityId)
    {
        var returnObject = await auditService.GetEntityByIdAsync(entityId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<EntityDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewEntityAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromServices] IValidator<EntityDTO> validator,
        [FromBody] EntityDTO entityDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(entityDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await auditService.CreateNewEntityAsync(entityDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<EntityDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateEntityByIdAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromServices] IValidator<EntityDTO> validator,
        [FromBody] EntityDTO entityDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(entityDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await auditService.UpdateEntityByIdAsync(entityDTO.Id, entityDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteEntityByIdAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromRoute] long entityId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await auditService.DeleteEntityByIdAsync(entityId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<CompanyEntityAuditDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCompanyEntityAuditByIdAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromRoute] long companyEntityAuditId)
    {
        var returnObject = await auditService.GetCompanyEntityAuditByIdAsync(companyEntityAuditId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CompanyEntityAuditDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewCompanyEntityAuditAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromServices] IValidator<CompanyEntityAuditDTO> validator,
        [FromBody] CompanyEntityAuditDTO companyEntityAuditDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(companyEntityAuditDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await auditService.CreateNewCompanyEntityAuditAsync(companyEntityAuditDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CompanyEntityAuditDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateCompanyEntityAuditByIdAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromServices] IValidator<CompanyEntityAuditDTO> validator,
        [FromBody] CompanyEntityAuditDTO companyEntityAuditDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(companyEntityAuditDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await auditService.UpdateCompanyEntityAuditByIdAsync(companyEntityAuditDTO.Id, companyEntityAuditDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteCompanyEntityAuditByIdAsync(
        HttpRequest request,
        [FromServices] IAuditService auditService,
        [FromRoute] long companyEntityAuditId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await auditService.DeleteCompanyEntityAuditByIdAsync(companyEntityAuditId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }
}