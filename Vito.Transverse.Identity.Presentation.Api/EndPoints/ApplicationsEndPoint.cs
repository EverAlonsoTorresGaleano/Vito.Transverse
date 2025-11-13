using System.Collections.Generic;
using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.Application.TransverseServices.Applications;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.EndPoints;

public static class ApplicationsEndPoint
{
    public static void MapApplicationsEndPoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/applications/v{apiVersion:apiVersion}").WithApiVersionSet(versionSet)
            .AddEndpointFilter<ApplicationsFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();


        endPointGroupVersioned.MapGet("", GetAllApplicationListAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get All Application List Async")
             .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
           .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("dropdown", GetAllApplicationListItemAsync)
         .MapToApiVersion(1.0)
         .WithSummary("Get All Application List Async")
           .WithDescription("[Author] [Authen] [Trace]")
         .RequireAuthorization()
         .AddEndpointFilter<RoleAuthorizationFilter>();

         

        endPointGroupVersioned.MapGet("{applicationId}", GetApplicationByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Application List Async")
             .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();


        endPointGroupVersioned.MapPost("", CreateNewApplicationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Application Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("", UpdateApplicationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Application Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Delete/{applicationId}", DeleteApplicationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Application Async")
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

        endPointGroupVersioned.MapGet("Roles/{applicationId}", GetRoleListByApplicationIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Role List By Application Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Modules", GetModuleListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Module List Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Modules/dropdown", GetModuleListItemAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Get Module List Async")
             .WithDescription("[Author] [Authen] [Trace]")
             .RequireAuthorization()
             .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Modules/{moduleId}", GetModuleByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Module By Id Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("Modules", CreateNewModuleAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Create New Module Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Modules", UpdateModuleByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Update Module By Id Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Modules/Delete/{moduleId}", DeleteModuleByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Delete Module By Id Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Modules/{moduleId}/endpoints", GetEndpointsListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Endpoints List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Modules/{moduleId}/endpoints/dropdown", GetEndpointsListItemAsync)
              .MapToApiVersion(1.0)
              .WithSummary("Get Endpoints List Async")
                .WithDescription("[Author] [Authen] [Trace]")
              .RequireAuthorization()
              .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("endpoints/{endpointId}/Components", GetComponentListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Component List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("endpoints/{endpointId}/Components/dropdown", GetComponentListItemAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Component List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Components/{componentId}", GetComponentByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Component By Id Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("Components", CreateNewComponentAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Create New Component Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Components", UpdateComponentByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Update Component By Id Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Components/Delete/{componentId}", DeleteComponentByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Delete Component By Id Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("endpoints/{endpointId}", GetEndpointByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Endpoint By Id Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("endpoints", CreateNewEndpointAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Create New Endpoint Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("endpoints", UpdateEndpointByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Update Endpoint By Id Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("endpoints/Delete/{endpointId}", DeleteEndpointByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Delete Endpoint By Id Async")
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

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllApplicationListItemAsync(
      HttpRequest request,
      [FromServices] IApplicationsService applicationService)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await applicationService.GetAllApplicationListItemAsync(deviceInformation.CompanyId);
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



    public static async Task<Results<Ok<ApplicationDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateApplicationAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromServices] IValidator<ApplicationDTO> validator,
        [FromBody] ApplicationDTO applicationDTO)
    {
        var validationResult = await validator.ValidateAsync(applicationDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }

        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await applicationService.UpdateApplicationAsync(applicationDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteApplicationAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long applicationId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await applicationService.DeleteApplicationAsync(applicationId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }


    public static async Task<Results<Ok<RoleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetRolePermissionListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromQuery] long roleId)

    {
        var returnObject = await applicationService.GetRolePermissionListAsync(roleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<RoleDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetRoleListByApplicationIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long applicationId)

    {
        var returnObject = await applicationService.GetRoleListByApplicationIdAsync(applicationId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<ModuleDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetModuleListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromQuery] long? applicationId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await applicationService.GetModuleListAsync(applicationId ?? deviceInformation!.ApplicationId!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetModuleListItemAsync(
    HttpRequest request,
    [FromServices] IApplicationsService applicationService)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await applicationService.GetModuleListItemAsync(deviceInformation!.ApplicationId!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<ModuleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetModuleByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long moduleId)

    {
        var returnObject = await applicationService.GetModuleByIdAsync(moduleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<ModuleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewModuleAsync(
       HttpRequest request,
       [FromServices] IApplicationsService applicationService,
       [FromServices] IValidator<ModuleDTO> validator,
       [FromBody] ModuleDTO moduleDTO,
       [FromQuery] long applicationId,
       [FromQuery] long userId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(moduleDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await applicationService.CreateNewModuleAsync(moduleDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<ModuleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateModuleByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromServices] IValidator<ModuleDTO> validator,
        [FromBody] ModuleDTO moduleInfo)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(moduleInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await applicationService.UpdateModuleByIdAsync(moduleInfo.Id, moduleInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteModuleByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long moduleId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await applicationService.DeleteModuleByIdAsync(moduleId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<List<EndpointDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetEndpointsListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long moduleId)

    {
        var returnObject = await applicationService.GetEndpointsListAsync(moduleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetEndpointsListItemAsync(
    HttpRequest request,
    [FromServices] IApplicationsService applicationService,
    [FromRoute] long moduleId)

    {
        var returnObject = await applicationService.GetEndpointsListItemAsync(moduleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ComponentDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetComponentListAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long endpointId)

    {
        var returnObject = await applicationService.GetComponentListAsync(endpointId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetComponentListItemAsync(
    HttpRequest request,
    [FromServices] IApplicationsService applicationService,
    [FromRoute] long endpointId)

    {
        var returnObject = await applicationService.GetComponentListItemAsync(endpointId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<EndpointDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetEndpointByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long endpointId)

    {
        var returnObject = await applicationService.GetEndpointByIdAsync(endpointId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<EndpointDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewEndpointAsync(
       HttpRequest request,
       [FromServices] IApplicationsService applicationService,
       [FromServices] IValidator<EndpointDTO> validator,
       [FromBody] EndpointDTO endpointDTO,
       [FromQuery] long moduleId,
       [FromQuery] long userId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(endpointDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await applicationService.CreateNewEndpointAsync(endpointDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<EndpointDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateEndpointByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromServices] IValidator<EndpointDTO> validator,
        [FromBody] EndpointDTO endpointInfo)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(endpointInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await applicationService.UpdateEndpointByIdAsync(endpointInfo.Id, endpointInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteEndpointByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long endpointId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await applicationService.DeleteEndpointByIdAsync(endpointId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<ComponentDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetComponentByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long componentId)
    {
        var returnObject = await applicationService.GetComponentByIdAsync(componentId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<ComponentDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewComponentAsync(
       HttpRequest request,
       [FromServices] IApplicationsService applicationService,
       [FromServices] IValidator<ComponentDTO> validator,
       [FromBody] ComponentDTO componentDTO,
       [FromQuery] long endpointId,
       [FromQuery] long userId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(componentDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await applicationService.CreateNewComponentAsync(componentDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<ComponentDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateComponentByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromServices] IValidator<ComponentDTO> validator,
        [FromBody] ComponentDTO componentInfo)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(componentInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await applicationService.UpdateComponentByIdAsync(componentInfo.Id, componentInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteComponentByIdAsync(
        HttpRequest request,
        [FromServices] IApplicationsService applicationService,
        [FromRoute] long componentId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await applicationService.DeleteComponentByIdAsync(componentId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

}
