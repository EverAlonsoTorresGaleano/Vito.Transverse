using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using  Vito.Transverse.Identity.Application.TransverseServices.Applications;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.EndPoints;

public static class ApplicationsEndPoint
{
    public static void MapApplicationsEndPoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/applications/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<ApplicationsFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();


        endPointGroupVersioned.MapGet("", GetAllApplicationListAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get All Application List Async")
             .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
           .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("/{applicationId}", GetApplicationByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Application List Async")
             .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();


        endPointGroupVersioned.MapPost("Add", CreateNewApplicationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Application Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();


        endPointGroupVersioned.MapGet("ByCompany/{companyId}", GetApplicationListByCompanyAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Application List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();



        endPointGroupVersioned.MapGet("Roles/permissions", GetRolePermissionListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Role Permission List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Applications/Modules", GetModuleListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Module List Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Modules/{moduleId}/endpoints", GetEndpointsListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Endpoints List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Components", GetComponentListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Component List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

    }

    public static async Task<Results<Ok<List<ApplicationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllApplicationListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService)

    {
        var returnObject = await applicationService.GetAllApplicationListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<ApplicationDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetApplicationByIdAsync(
        HttpRequest request,
        [FromRoute] long applicationId,
        [FromServices] IApplicationsService applicationService)

    {
        ApplicationDTO returnObject = await applicationService.GetApplicationByIdAsync(applicationId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<ApplicationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetApplicationListByCompanyAsync(
         HttpRequest request,
         [FromServices] IApplicationsService applicationService,
         [FromRoute] long? companyId)

    {
        var returnObject = await applicationService.GetApplicationListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }
    public static async Task<Results<Ok<ApplicationDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewApplicationAsync(
       HttpRequest request,
       [FromServices] IApplicationsService applicationService,
       [FromServices] IValidator<ApplicationDTO> validator,
       [FromBody] ApplicationDTO applicationDTO,
       [FromQuery] long companyId,
       [FromQuery] long userId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(applicationDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await applicationService.CreateNewApplicationAsync(applicationDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }



    public static async Task<Results<Ok<RoleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetRolePermissionListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromQuery] long roleId)

    {
        var returnObject = await applicationService.GetRolePermissionListAsync(roleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<ModuleDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetModuleListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromQuery] long? applicationId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await applicationService.GetModuleListAsync(applicationId ?? deviceInformation!.ApplicationId!.Value);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<EndpointDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetEndpointsListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromQuery] long moduleId)

    {
        var returnObject = await applicationService.GetEndpointsListAsync(moduleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ComponentDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetComponentListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromQuery] long endpointId)

    {
        var returnObject = await applicationService.GetComponentListAsync(endpointId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


}
