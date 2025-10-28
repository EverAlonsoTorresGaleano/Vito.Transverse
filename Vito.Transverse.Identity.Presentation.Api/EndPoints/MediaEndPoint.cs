using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using  Vito.Transverse.Identity.Application.TransverseServices.Media;
using  Vito.Transverse.Identity.Application.TransverseServices.Security;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.Endpoints;

/// <summary>
/// Home Endpoint
/// </summary>
public static class MediaEndPoint
{
    public static void MapMediaEndPoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Media/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<CacheFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("pictures", GetPictureList)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture List")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("pictures/{pictureName}", GetPictureByName)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture By Name")
            .WithDescription("[AllowAnonymous]")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("pictures/WithWildCard/{pictureNameWildCard}", GetPictureByNameWildCard)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture By Name Wild Card")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();
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

        var returnValue = await mediaService.GetPictureByName(companyId ?? deviceInformation!.ApplicationId!.Value, pictureName);
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

        var returnValue = await mediaService.GetPictureByNameWildCard(companyId ?? deviceInformation!.ApplicationId!.Value, pictureNameWildCard);
        return TypedResults.Ok(returnValue);
    }
}