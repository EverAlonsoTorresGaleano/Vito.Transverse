using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Localization;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Endpoints;

public static class LocalizationEndpoint
{

    public static void MapLocalizationEndpoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Localization/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<LocalizationFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();


        endPointGroupVersioned.MapGet("AllLocalizedMessagesByApplicationAsync", GetAllLocalizedMessagesByApplicationAsync)
             .MapToApiVersion(1.0)
            .WithSummary("Get All Localized Messages By Application Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();


        endPointGroupVersioned.MapGet("AllLocalizedMessagesAsync", GetAllLocalizedMessagesAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Localized Messages Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("LocalizedMessagesByKeyAsync", GetLocalizedMessagesByKeyAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get Localized Messages By Key Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();


        endPointGroupVersioned.MapGet("LocalizedMessageByKeyAndParamsSync", GetLocalizedMessageByKeyAndParamsSync)
             .MapToApiVersion(1.0)
            .WithSummary("Get Localized Message By Key Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();
    }


    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllLocalizedMessagesByApplicationAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long applicationId)
    {

        var returnObject = await localizationService.GetAllLocalizedMessagesByApplicationAsync(applicationId);
        return TypedResults.Ok(returnObject);

    }


    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllLocalizedMessagesAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long applicationId,
        [FromQuery] string? cultureId)
    {

        var returnObject = await localizationService.GetAllLocalizedMessagesAsync(applicationId, cultureId!);
        return TypedResults.Ok(returnObject);

    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLocalizedMessagesByKeyAsync(
    HttpRequest request,
    [FromServices] ISecurityService securityService,
    [FromServices] ILocalizationService localizationService,
    [FromQuery] long applicationId,
    [FromQuery] string localizationMessageKey)
    {

        var returnObject = await localizationService.GetLocalizedMessagesByKeyAsync(applicationId, localizationMessageKey);
        return TypedResults.Ok(returnObject);

    }

    public static Results<Ok<CultureTranslationDTO>, NotFound, UnauthorizedHttpResult, ValidationProblem> GetLocalizedMessageByKeyAndParamsSync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long applicationId,
        [FromQuery] string? cultureId,
        [FromQuery] string localizationMessageKey,
        [FromQuery] params string[]? parameters)
    {

        var returnObject = localizationService.GetLocalizedMessageByKeyAndParamsSync(applicationId, cultureId!, localizationMessageKey, parameters ?? []);
        return TypedResults.Ok(returnObject);

    }


}