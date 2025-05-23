using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Media;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Endpoints;

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

        endPointGroupVersioned.MapGet("PictureList", GetPictureList)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture List")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("PictureByName", GetPictureByName)
            .MapToApiVersion(1.0)
            .WithSummary("Get Picture By Name")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("PictureByNameWildCard", GetPictureByNameWildCard)
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
        var returnValue = await mediaService.GetPictureList(companyId);
        return TypedResults.Ok(returnValue);
    }

    public static async Task<Results<FileContentHttpResult, NotFound, UnauthorizedHttpResult>> GetPictureByName(
      HttpRequest request,
      [FromServices] ISecurityService securityService,
      [FromServices] IMediaService mediaService,
      [FromQuery] long companyId,
      [FromQuery] string name)
    {
        var returnValue = await mediaService.GetPictureByName(companyId, name);
        return returnValue == null ? TypedResults.NotFound() : TypedResults.File(returnValue.BinaryPicture, contentType: string.Empty, fileDownloadName: $"{returnValue.Name}.png");
    }

    public static async Task<Results<Ok<List<PictureDTO>>, UnauthorizedHttpResult>> GetPictureByNameWildCard(
          HttpRequest request,
          [FromServices] ISecurityService securityService,
          [FromServices] IMediaService mediaService,
          [FromQuery] long companyId,
          [FromQuery] string wildCard)
    {
        var returnValue = await mediaService.GetPictureByNameWildCard(companyId, wildCard);
        return TypedResults.Ok(returnValue);
    }
}