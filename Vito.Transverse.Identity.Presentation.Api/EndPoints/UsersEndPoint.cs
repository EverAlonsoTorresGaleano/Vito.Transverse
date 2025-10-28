using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using  Vito.Transverse.Identity.Application.TransverseServices.Users;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.EndPoints;

public static class UsersEndPoint
{
    public static void MapUsersEndPoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Users/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<UsersFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("Users", GetUserListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get User  List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();


        endPointGroupVersioned.MapGet("Users/Roles", GetUserRolesListAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get User Roles List Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Users/Permissions", GetUserPermissionListAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get User Permission List Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();


        endPointGroupVersioned.MapPost("Users/Add", CreateNewUserAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Create New User Async")
            .WithDescription("[Author] [Authen] [Trace]")
             .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPatch("Users/password", ChangeUserPasswordAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Change User Password Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("users/activate/{activationToken}", ActivateAccountAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Activate Account Async")
            .WithDescription("[Anonymmous] [Trace]")
           .AllowAnonymous()
           .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("users/activationEmail", SendActivationEmailAsync)
   .MapToApiVersion(1.0)
   .WithSummary("Send Activation Email Async")
    .WithDescription("[Author] [Authen] [Trace]")
   .RequireAuthorization()
  .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Roles", GetRoleListAsync)
    .MapToApiVersion(1.0)
    .WithSummary("Get Role List Async")
    .WithDescription("[Author] [Authen] [Trace]")
    .RequireAuthorization()
    .AddEndpointFilter<RoleAuthorizationFilter>();
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewUserAsync(
       HttpRequest request,
       [FromServices] IUsersService usersService,
       [FromServices] IValidator<UserDTO> validator,
       [FromBody] UserDTO userInfo,
       [FromQuery] long? companyId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(userInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await usersService.CreateNewUserAsync(companyId ?? deviceInformation!.ApplicationId!.Value, userInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<RoleDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetRoleListAsync(
  HttpRequest request,
  [FromServices] IUsersService usersService,
  [FromQuery] long? companyId)

    {
        var returnObject = await usersService.GetRoleListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> ChangeUserPasswordAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromServices] IValidator<UserDTO> validator,
        [FromBody] UserDTO userInfo,
        [FromQuery] long? companyId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(userInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await usersService.ChangeUserPasswordAsync(userInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<bool?>, NotFound, ValidationProblem>> ActivateAccountAsync(
       HttpRequest request,
       [FromServices] IUsersService usersService,
       [FromServices] IValidator<string> validator,
       [FromRoute] string activationToken)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(activationToken);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await usersService.ActivateAccountAsync(activationToken, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<UserRoleDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserRolesListAsync(
    HttpRequest request,
    [FromServices] IUsersService usersService,
    [FromQuery] long userId)

    {
        var returnObject = await usersService.GetUserRolesListAsync(userId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserPermissionListAsync(
           HttpRequest request,
           [FromServices] IUsersService usersService,
           [FromQuery] long userId)
    {
        var returnObject = await usersService.GetUserPermissionListAsync(userId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<UserDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserListAsync(
          HttpRequest request,
          [FromServices] IUsersService usersService,
          [FromQuery] long companyId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.GetUserListAsync(companyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> SendActivationEmailAsync(
     HttpRequest request,
     [FromServices] IUsersService usersService,
     [FromQuery] long? companyId,
     [FromQuery] long? userId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.SendActivationEmailAsync(companyId ?? deviceInformation!.ApplicationId!.Value, userId ?? deviceInformation!.UserId!.Value, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }
}
