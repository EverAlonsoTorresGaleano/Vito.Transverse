using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.Application.TransverseServices.Media;
using Vito.Transverse.Identity.Application.TransverseServices.Security;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.EndPoints;

/// <summary>
/// Home Endpoint
/// </summary>
public static class MediaEndPoint
{
    public static void MapMediaEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Media/v{apiVersion:apiVersion}").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CacheFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("pictures", GetPictureList)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture List")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("pictures/ByName/{pictureName}", GetPictureByName)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture By Name")
            .WithDescription("[AllowAnonymous]")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("pictures/ByNameWildCard/{pictureNameWildCard}", GetPictureByNameWildCard)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture By Name Wild Card")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("pictures/{pictureId}", GetPictureByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("pictures", CreateNewPictureAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Picture Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("pictures", UpdatePictureByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Picture By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("pictures/{pictureId}", DeletePictureByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Picture By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();
    }

    public static async Task<Results<Ok<List<PictureDTO>>, UnauthorizedHttpResult>> GetPictureList(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IMediaService mediaService,
        [FromQuery] long companyId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var returnValue = await mediaService.GetPictureList(companyId);
        return TypedResults.Ok(returnValue);
    }

    public static async Task<Results<FileContentHttpResult, NotFound, UnauthorizedHttpResult>> GetPictureByName(
      HttpRequest request,
      [FromServices] ISecurityService securityService,
      [FromServices] IMediaService mediaService,
      [FromQuery] long? companyId,
      [FromRoute] string pictureName)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var returnValue = await mediaService.GetPictureByName(companyId ?? deviceInformation!.ApplicationId!, pictureName);
        return returnValue == null ? TypedResults.NotFound() : TypedResults.File(returnValue.BinaryPicture, contentType: string.Empty, fileDownloadName: $"{returnValue.Name}.png");
    }

    public static async Task<Results<Ok<List<PictureDTO>>, UnauthorizedHttpResult>> GetPictureByNameWildCard(
          HttpRequest request,
          [FromServices] ISecurityService securityService,
          [FromServices] IMediaService mediaService,
          [FromQuery] long? companyId,
          [FromQuery] string pictureNameWildCard)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var returnValue = await mediaService.GetPictureByNameWildCard(companyId ?? deviceInformation!.ApplicationId!, pictureNameWildCard);
        return TypedResults.Ok(returnValue);
    }

    public static async Task<Results<Ok<PictureDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetPictureByIdAsync(
        HttpRequest request,
        [FromServices] IMediaService mediaService,
        [FromRoute] long pictureId)
    {
        var returnObject = await mediaService.GetPictureByIdAsync(pictureId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<PictureDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewPictureAsync(
       HttpRequest request,
       [FromServices] IMediaService mediaService,
       [FromServices] IValidator<PictureDTO> validator,
       [FromBody] PictureDTO pictureDTO,
       [FromQuery] long userId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(pictureDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await mediaService.CreateNewPictureAsync(pictureDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<PictureDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdatePictureByIdAsync(
        HttpRequest request,
        [FromServices] IMediaService mediaService,
        [FromServices] IValidator<PictureDTO> validator,
        [FromBody] PictureDTO pictureDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(pictureDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await mediaService.UpdatePictureByIdAsync(pictureDTO.Id, pictureDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeletePictureByIdAsync(
        HttpRequest request,
        [FromServices] IMediaService mediaService,
        [FromRoute] long pictureId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await mediaService.DeletePictureByIdAsync(pictureId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }
}