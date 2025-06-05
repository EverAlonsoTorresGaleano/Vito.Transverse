using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.Security;
using Vito.Transverse.Identity.Api.Filters;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Endpoints;

public static class OAuth2Endpoint
{
    public static void MapOAuth2Endpoint(this WebApplication app, ApiVersionSet versionSet)
    {

        var endPointGroupVersioned = app.MapGroup("api/Oauth2/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<InfrastructureFilter>()
            .AddEndpointFilter<OAuth2FeatureFlagFilter>();

        endPointGroupVersioned.MapPost("TokenAsync", TokenAync)
             .MapToApiVersion(1.0)
            .WithSummary("Get Authentication Token")
            .WithDescription("[Anonymous] [Trace]")
            .AllowAnonymous()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("CompanyAsync", CreateNewCompanyAsync)
            .MapToApiVersion(1.0)
           .WithSummary("Create New Company Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("ApplicationAsync", CreateNewApplicationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Application Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("UserAsync", CreateNewUserAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Create New User Async")
            .WithDescription("[Author] [Authen] [Trace]")
             .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("UserPasswordAsync", ChangeUserPasswordAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Change User Password Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("CompanyApplicationsAsync", UpdateCompanyApplicationsAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Update Company Applications Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("SendActivationEmailAsync", SendActivationEmailAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Send Activation Email Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("ActivateAccountAsync", ActivateAccountAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Activate Account Async")
            .WithDescription("[Anonymmous] [Trace]")
           .AllowAnonymous()
           .AddEndpointFilter<RoleAuthorizationFilter>();



        endPointGroupVersioned.MapGet("AllApplicationListAsync", GetAllApplicationListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get All Application List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("ApplicationListAsync", GetApplicationListAsync)
         .MapToApiVersion(1.0)
         .WithSummary("Get Application List Async")
          .WithDescription("[Author] [Authen] [Trace]")
         .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("CompanyMemberhipAsync", GetCompanyMemberhipAsync)
         .MapToApiVersion(1.0)
         .WithSummary("Get Company Memberhip By Company Async")
         .WithDescription("[Author] [Authen] [Trace]")
         .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("AllCompanyListAsync", GetAllCompanyListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get All Company List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("RoleListAsync", GetRoleListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Role List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("RolePermissionListAsync", GetRolePermissionListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Role Permission List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("ModuleListAsync", GetModuleListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Module List Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("EndpointsListAsync", GetEndpointsListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Endpoints List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("ComponentListAsync", GetComponentListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get Component List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("UserRolesListAsync", GetUserRolesListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get User Roles List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("UserPermissionListAsync", GetUserPermissionListAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get User Permission List Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("UserListAsync", GetUserListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get User  List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        
    }

    public static async Task<Results<Ok<TokenResponseDTO>, NotFound, ValidationProblem>> TokenAync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IValidator<TokenRequestDTO> validator,
        [FromBody] TokenRequestDTO requestBody)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(requestBody);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.NewJwtTokenAsync(requestBody, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewUserAsync(
       HttpRequest request,
       [FromServices] ISecurityService securityService,
       [FromServices] IValidator<UserDTO> validator,
       [FromBody] UserDTO userInfo,
       [FromQuery] long companyId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(userInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.CreateNewUserAsync(companyId, userInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> ChangeUserPasswordAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IValidator<UserDTO> validator,
        [FromBody] UserDTO userInfo,
        [FromQuery] long companyId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(userInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.ChangeUserPasswordAsync(userInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> SendActivationEmailAsync(
     HttpRequest request,
     [FromServices] ISecurityService securityService,
     [FromQuery] long companyId,
     [FromQuery] long userId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await securityService.SendActivationEmailAsync(companyId, userId, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<bool?>, NotFound, ValidationProblem>> ActivateAccountAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IValidator<string> validator,
        [FromQuery] string activationToken)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(activationToken);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.ActivateAccountAsync(activationToken, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }



    public static async Task<Results<Ok<ApplicationDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewApplicationAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
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
        var returnObject = await securityService.CreateNewApplicationAsync(applicationDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CompanyDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewCompanyAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IValidator<CompanyApplicationsDTO> validator,
        [FromBody] CompanyApplicationsDTO companyApplications,
        [FromQuery] long companyId,
        [FromQuery] long userId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(companyApplications);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.CreateNewCompanyAsync(companyApplications, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<CompanyDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateCompanyApplicationsAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IValidator<CompanyApplicationsDTO> validator,
        [FromBody] CompanyApplicationsDTO companyApplicationInfo,
        [FromQuery] long userId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(companyApplicationInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.UpdateCompanyApplicationsAsync(companyApplicationInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }





    public static async Task<Results<Ok<List<ApplicationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllApplicationListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService)

    {
        var returnObject = await securityService.GetAllApplicationListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ApplicationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetApplicationListAsync(
         HttpRequest request,
         [FromServices] ISecurityService securityService,
         [FromQuery] long? companyId)

    {
        var returnObject = await securityService.GetApplicationListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<CompanyMembershipsDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCompanyMemberhipAsync(
       HttpRequest request,
       [FromServices] ISecurityService securityService,
       [FromQuery] long? companyId)

    {
        var returnObject = await securityService.GetCompanyMemberhipAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<CompanyDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllCompanyListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService)

    {
        var returnObject = await securityService.GetAllCompanyListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<RoleDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetRoleListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long? companyId)

    {
        var returnObject = await securityService.GetRoleListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<RoleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetRolePermissionListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long roleId)

    {
        var returnObject = await securityService.GetRolePermissionListAsync(roleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<ModuleDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetModuleListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long? applicationId)

    {
        var returnObject = await securityService.GetModuleListAsync(applicationId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<EndpointDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetEndpointsListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long moduleId)

    {
        var returnObject = await securityService.GetEndpointsListAsync(moduleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ComponentDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetComponentListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long endpointId)

    {
        var returnObject = await securityService.GetComponentListAsync(endpointId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<UserRoleDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserRolesListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long userId)

    {
        var returnObject = await securityService.GetUserRolesListAsync(userId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserPermissionListAsync(
           HttpRequest request,
           [FromServices] ISecurityService securityService,
           [FromQuery] long userId)
    {
        var returnObject = await securityService.GetUserPermissionListAsync(userId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<UserDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserListAsync(
          HttpRequest request,
          [FromServices] ISecurityService securityService,
          [FromQuery] long companyId)
    {
        var returnObject = await securityService.GetUserListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }
}