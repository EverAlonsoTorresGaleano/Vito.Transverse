using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.Security;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Endpoints;

/// <summary>
/// Home Endpoint
/// </summary>
/// <example>https://developer.usps.com/api/81</example>
public static class SecurityEndpoint
{
    public static void MapSecurityEndpoint(this WebApplication app, ApiVersionSet versionSet)
    {
        //var endPointGroupNoVersioned = app.MapGroup("api/Oauth2/");

        //endPointGroupNoVersioned.MapPost("Token", TokenAync)
        //    .WithSummary("Get Authentication Token")
        //    .AddEndpointFilter<SecurityFeatureFlagFilter>()
        //    .AddEndpointFilter<InfrastructureFilter>();


        var endPointGroupVersioned = app.MapGroup("api/Oauth2/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet);

        endPointGroupVersioned.MapPost("TokenAsync", TokenAync)
             .MapToApiVersion(1.0)
            .WithSummary("Get Authentication Token")
            .AddEndpointFilter<InfrastructureFilter>()
            .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapPost("CompanyAsync", CreateNewCompanyAsync)
            .MapToApiVersion(1.0)
           .WithSummary("CreateNewCompanyAsync")
           .RequireAuthorization()
           .AddEndpointFilter<InfrastructureFilter>()
           .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapPost("ApplicationAsync", CreateNewApplicationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("CreateNewApplicationAsync")
            .RequireAuthorization()
            .AddEndpointFilter<InfrastructureFilter>()
            .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapPost("UserAsync", CreateNewUserAsync)
             .MapToApiVersion(1.0)
             .WithSummary("CreateNewUserAsync")
             .RequireAuthorization()
             .AddEndpointFilter<InfrastructureFilter>()
             .AddEndpointFilter<SecurityFeatureFlagFilter>();



        endPointGroupVersioned.MapPut("UserPasswordAsync", ChangeUserPasswordAsync)
          .MapToApiVersion(1.0)
          .WithSummary("ChangeUserPasswordAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapPut("CompanyApplicationsAsync", UpdateCompanyApplicationsAsync)
           .MapToApiVersion(1.0)
           .WithSummary("UpdateCompanyApplicationsAsync")
           .RequireAuthorization()
           .AddEndpointFilter<InfrastructureFilter>()
           .AddEndpointFilter<SecurityFeatureFlagFilter>();



        endPointGroupVersioned.MapGet("SendActivationEmailAsync", SendActivationEmailAsync)
           .MapToApiVersion(1.0)
           .WithSummary("SendActivationEmailAsync")
           .RequireAuthorization()
           .AddEndpointFilter<InfrastructureFilter>()
           .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("ActivateAccountAsync", ActivateAccountAsync)
           .MapToApiVersion(1.0)
           .WithSummary("ActivateAccountAsync")
           .RequireAuthorization()
           .AddEndpointFilter<InfrastructureFilter>()
           .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("AllApplicationListAsync", GetAllApplicationListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("GetAllApplicationListAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("ApplicationListAsync", GetApplicationListAsync)
         .MapToApiVersion(1.0)
         .WithSummary("GetApplicationListAsync")
         .RequireAuthorization()
         .AddEndpointFilter<InfrastructureFilter>()
         .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("CompanyMemberhipAsync", GetCompanyMemberhipAsync)
         .MapToApiVersion(1.0)
         .WithSummary("GetCompanyMemberhipByCompanyAsync")
         .RequireAuthorization()
         .AddEndpointFilter<InfrastructureFilter>()
         .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("AllCompanyListAsync", GetAllCompanyListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("GetAllCompanyListAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();


        endPointGroupVersioned.MapGet("RoleListAsync", GetRoleListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("GetRoleListAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();


        endPointGroupVersioned.MapGet("RolePermissionListAsync", GetRolePermissionListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("GetRolePermissionListAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();


        endPointGroupVersioned.MapGet("ModuleListAsync", GetModuleListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("GetModuleListAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();


        endPointGroupVersioned.MapGet("PageListAsync", GetPageListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("GetPageListAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();


        endPointGroupVersioned.MapGet("ComponentListAsync", GetComponentListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("GetComponentListAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("UserRolesListAsync", GetUserRolesListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("GetUserRolesListAsync")
          .RequireAuthorization()
          .AddEndpointFilter<InfrastructureFilter>()
          .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("UserPermissionListAsync", GetUserPermissionListAsync)
           .MapToApiVersion(1.0)
           .WithSummary("GetUserPermissionListAsync")
           .RequireAuthorization()
           .AddEndpointFilter<InfrastructureFilter>()
           .AddEndpointFilter<SecurityFeatureFlagFilter>();


        endPointGroupVersioned.MapGet("CompanyEntityAuditsListAsync", GetCompanyEntityAuditsListAsync)
               .MapToApiVersion(1.0)
               .WithSummary("GetCompanyEntityAuditsListAsync")
               .RequireAuthorization()
               .AddEndpointFilter<InfrastructureFilter>()
               .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("AuditRecordsListAsync", GetAuditRecordsListAsync)
           .MapToApiVersion(1.0)
           .WithSummary("GetAuditRecordsListAsync")
           .RequireAuthorization()
           .AddEndpointFilter<InfrastructureFilter>()
           .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("ActivityLogListAsync", GetActivityLogListAsync)
         .MapToApiVersion(1.0)
         .WithSummary("GetActivityLogListAsync")
         .RequireAuthorization()
         .AddEndpointFilter<InfrastructureFilter>()
         .AddEndpointFilter<SecurityFeatureFlagFilter>();

        endPointGroupVersioned.MapGet("NotificationsListAsync", GetNotificationsListAsync)
         .MapToApiVersion(1.0)
         .WithSummary("GetNotificationsListAsync")
         .RequireAuthorization()
         .AddEndpointFilter<InfrastructureFilter>()
         .AddEndpointFilter<SecurityFeatureFlagFilter>();

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
        var returnObject = await securityService.CreateAuthenticationTokenAsync(requestBody, deviceInformation!);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<ApplicationDTO>, NotFound, ValidationProblem>> CreateNewApplicationAsync(
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
        var returnObject = await securityService.CreateNewApplicationAsync(applicationDTO, deviceInformation!, companyId, userId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CompanyDTO>, NotFound, ValidationProblem>> CreateNewCompanyAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IValidator<CompanyDTO> validator,
        [FromBody] CompanyDTO companyDTO,
        [FromQuery] long companyId,
        [FromQuery] long userId)
    {

        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(companyDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.CreateNewCompanyAsync(companyDTO, deviceInformation!, userId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, NotFound, ValidationProblem>> CreateNewUserAsync(
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
        var returnObject = await securityService.CreateNewUserAsync(userInfo, companyId, deviceInformation!);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<bool>, NotFound, ValidationProblem>> ChangeUserPasswordAsync(
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

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject.Value);
    }

    public static async Task<Results<Ok<bool>, NotFound, ValidationProblem>> UpdateCompanyApplicationsAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IValidator<CompanyApplicationDTO> validator,
        [FromBody] CompanyApplicationDTO companyApplicationInfo,
        [FromQuery] long userId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(companyApplicationInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.UpdateCompanyApplicationsAsync(companyApplicationInfo.companyInfo, companyApplicationInfo.applicationInfoList, userId, deviceInformation!);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject.Value);
    }


    public static async Task<Results<Ok<bool>, NotFound, ValidationProblem>> SendActivationEmailAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long companyId,
        [FromQuery] long userId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var returnObject = await securityService.SendActivationEmailAsync(companyId, userId, deviceInformation!);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject.Value);
    }

    public static async Task<Results<Ok<bool>, NotFound, ValidationProblem>> ActivateAccountAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long companyId,
        [FromQuery] long userId,
        [FromQuery] Guid activationId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.ActivateAccountAsync(companyId, userId, activationId, deviceInformation!);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject.Value);
    }

    public static async Task<Results<Ok<List<ApplicationDTO>>, NotFound, ValidationProblem>> GetAllApplicationListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetAllApplicationListAsync();

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ApplicationDTO>>, NotFound, ValidationProblem>> GetApplicationListAsync(
         HttpRequest request,
         [FromServices] ISecurityService securityService,
         [FromQuery] long? companyId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetApplicationListAsync(companyId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<CompanyMembershipsDTO>>, NotFound, ValidationProblem>> GetCompanyMemberhipAsync(
       HttpRequest request,
       [FromServices] ISecurityService securityService,
       [FromQuery] long? companyId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetCompanyMemberhipAsync(companyId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }



    public static async Task<Results<Ok<List<CompanyDTO>>, NotFound, ValidationProblem>> GetAllCompanyListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetAllCompanyListAsync();

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<RoleDTO>>, NotFound, ValidationProblem>> GetRoleListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long? companyId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetRoleListAsync(companyId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<RoleDTO>, NotFound, ValidationProblem>> GetRolePermissionListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long roleId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetRolePermissionListAsync(roleId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<ModuleDTO>>, NotFound, ValidationProblem>> GetModuleListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long? applicationId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetModuleListAsync(applicationId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<PageDTO>>, NotFound, ValidationProblem>> GetPageListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long moduleId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetPageListAsync(moduleId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ComponentDTO>>, NotFound, ValidationProblem>> GetComponentListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long pageId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetComponentListAsync(pageId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<UserRoleDTO>>, NotFound, ValidationProblem>> GetUserRolesListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromQuery] long userId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;


        var returnObject = await securityService.GetUserRolesListAsync(userId);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, NotFound, ValidationProblem>> GetUserPermissionListAsync(
           HttpRequest request,
           [FromServices] ISecurityService securityService,
           [FromQuery] long userId)
    {
        var returnObject = await securityService.GetUserPermissionListAsync(userId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<CompanyEntityAuditDTO>>, NotFound, ValidationProblem>> GetCompanyEntityAuditsListAsync(
       HttpRequest request,
       [FromServices] ISecurityService securityService,
       [FromQuery] long? companyId)
    {
        var returnObject = await securityService.GetCompanyEntityAuditsListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<AuditRecordDTO>>, NotFound, ValidationProblem>> GetAuditRecordsListAsync(
       HttpRequest request,
       [FromServices] ISecurityService securityService,
       [FromQuery] long? companyId)
    {
        var returnObject = await securityService.GetAuditRecordsListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }



    public static async Task<Results<Ok<List<ActivityLogDTO>>, NotFound, ValidationProblem>> GetActivityLogListAsync(
     HttpRequest request,
     [FromServices] ISecurityService securityService,
     [FromQuery] long? companyId)
    {
        var returnObject = await securityService.GetActivityLogListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<NotificationDTO1>>, NotFound, ValidationProblem>> GetNotificationsListAsync(
    HttpRequest request,
    [FromServices] ISecurityService securityService,
    [FromQuery] long? companyId)
    {
        var returnObject = await securityService.GetNotificationsListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

}