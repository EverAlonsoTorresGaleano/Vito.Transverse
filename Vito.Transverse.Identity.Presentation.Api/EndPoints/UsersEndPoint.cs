using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.Application.TransverseServices.Users;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.EndPoints;

public static class UsersEndPoint
{
    public static void MapUsersEndPoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Users/v{apiVersion:apiVersion}").WithApiVersionSet(versionSet)
            .AddEndpointFilter<UsersFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("", GetUserListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get User  List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();



        endPointGroupVersioned.MapGet("dropdown", GetUserListItemAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get User  List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("{userId}", GetUserByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get User By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Menu", GetUserMenuByRoleIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get User By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();




        endPointGroupVersioned.MapPost("", CreateNewUserAsync)
             .MapToApiVersion(1.0)
             .WithSummary("Create New User Async")
            .WithDescription("[Author] [Authen] [Trace]")
             .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("", UpdateUserByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update User By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Delete/{userId}", DeleteUserByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete User By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPatch("password", ChangeUserPasswordAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Change User Password Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("activate/{activationToken}", ActivateAccountAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Activate Account Async")
            .WithDescription("[Anonymmous] [Trace]")
           .AllowAnonymous()
           .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("SendActivationEmail/{userId}", SendActivationEmailAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Send Activation Email Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("UserRoles", GetUserRolesListAsync)
               .MapToApiVersion(1.0)
               .WithSummary("Get User Roles List Async")
                 .WithDescription("[Author] [Authen] [Trace]")
               .RequireAuthorization()
               .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("UserRoles/{userId}/{roleId}", GetUserRoleByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get User Role By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("UserRoles", CreateNewUserRoleAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Create New User Role Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("UserRoles", UpdateUserRoleByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Update User Role By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("UserRoles/Delete/{userId}/{roleId}", DeleteUserRoleByIdAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Delete User Role By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Permissions/{userId}", GetUserPermissionListAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get User Permission List Async")
            .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Roles", GetRoleListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Role List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Roles/dropdown", GetRoleListItemAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Role List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();


        endPointGroupVersioned.MapGet("Roles/{roleId}", GetRoleByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Role By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Roles", UpdateRoleByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Role By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Roles/Delete/{roleId}", DeleteRoleByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Role By Id Async")
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
        var returnObject = await usersService.CreateNewUserAsync(companyId ?? deviceInformation!.ApplicationId!, userInfo, deviceInformation!);
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


    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetRoleListItemAsync(
HttpRequest request,
[FromServices] IUsersService usersService)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.GetRoleListItemAsync(deviceInformation.CompanyId);
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
    [FromQuery] long? userId)

    {
        var returnObject = await usersService.GetUserRolesListAsync(userId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserRoleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserRoleByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromRoute] long userId,
        [FromRoute] long roleId,
        [FromQuery] long? companyFk,
        [FromQuery] long? applicationFk)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.GetUserRoleByIdAsync(userId, roleId, companyFk ?? deviceInformation?.CompanyId, applicationFk ?? deviceInformation?.ApplicationId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserRoleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewUserRoleAsync(
       HttpRequest request,
       [FromServices] IUsersService usersService,
       [FromServices] IValidator<UserRoleDTO> validator,
       [FromBody] UserRoleDTO userRoleDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(userRoleDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await usersService.CreateNewUserRoleAsync(userRoleDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserRoleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateUserRoleByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromServices] IValidator<UserRoleDTO> validator,
        [FromBody] UserRoleDTO userRoleInfo)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(userRoleInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await usersService.UpdateUserRoleByIdAsync(userRoleInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteUserRoleByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromRoute] long userId,
        [FromRoute] long roleId,
        [FromQuery] long? companyFk,
        [FromQuery] long? applicationFk)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await usersService.DeleteUserRoleByIdAsync(userId, roleId, companyFk ?? deviceInformation?.CompanyId, applicationFk ?? deviceInformation?.ApplicationId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserPermissionListAsync(
           HttpRequest request,
           [FromServices] IUsersService usersService,
           [FromRoute] long userId)
    {
        var returnObject = await usersService.GetUserPermissionListAsync(userId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<UserDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserListAsync(
          HttpRequest request,
          [FromServices] IUsersService usersService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.GetUserListAsync(deviceInformation.CompanyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserListItemAsync(
      HttpRequest request,
      [FromServices] IUsersService usersService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.GetUserListItenAsync(deviceInformation.CompanyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromRoute] long userId)
    {
        var returnObject = await usersService.GetUserByIdAsync(userId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<MenuGroupDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUserMenuByRoleIdAsync(
    HttpRequest request,
    [FromServices] IUsersService usersService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.GetUserMenuByUserIdAsync(deviceInformation.UserId, deviceInformation.CompanyId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateUserByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromServices] IValidator<UserDTO> validator,
        [FromBody] UserDTO userInfo)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(userInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await usersService.UpdateUserByIdAsync(userInfo.Id, userInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteUserByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromRoute] long userId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await usersService.DeleteUserByIdAsync(userId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<UserDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> SendActivationEmailAsync(
     HttpRequest request,
     [FromServices] IUsersService usersService,
     [FromQuery] long? companyId,
     [FromRoute] long userId)

    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.SendActivationEmailAsync(companyId ?? deviceInformation!.ApplicationId!, userId, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<RoleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetRoleByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromRoute] long roleId)
    {
        var returnObject = await usersService.GetRoleByIdAsync(roleId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<RoleDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateRoleByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromServices] IValidator<RoleDTO> validator,
        [FromBody] RoleDTO roleInfo)
    {
        var validationResult = await validator.ValidateAsync(roleInfo);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }

        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await usersService.UpdateRoleByIdAsync(roleInfo.Id, roleInfo, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteRoleByIdAsync(
        HttpRequest request,
        [FromServices] IUsersService usersService,
        [FromRoute] long roleId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await usersService.DeleteRoleByIdAsync(roleId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }
}
