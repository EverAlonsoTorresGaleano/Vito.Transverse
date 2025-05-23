using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
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

        endPointGroupVersioned.MapGet("LocalizationMessagesListByApplicationAsync", GetLocalizationMessagesListByApplicationAsync)
             .MapToApiVersion(1.0)
            .WithSummary("Get All Localization Messages By Application Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("LocalizationMessagesListByApplicationAndCultureAsync", GetLocalizationMessagesListByApplicationAndCultureAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Localization Messages By Application And Culture Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("LocalizationMessagesListByKeyAsync", GetLocalizationMessagesListByKeyAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get Localization Messages By Key Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("LocalizationMessageByKeySync", GetLocalizationMessageByKeySync)
             .MapToApiVersion(1.0)
            .WithSummary("Get Localization Message By Key With Param Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();
    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLocalizationMessagesListByApplicationAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long applicationId)
    {

        var returnObject = await localizationService.GetLocalizedMessagesListByApplicationAsync(applicationId);
        return TypedResults.Ok(returnObject);

    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLocalizationMessagesListByApplicationAndCultureAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long applicationId,
        [FromQuery] string? cultureId)
    {

        var returnObject = await localizationService.GetLocalizedMessagesListByApplicationAndCultureAsync(applicationId, cultureId!);
        return TypedResults.Ok(returnObject);

    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLocalizationMessagesListByKeyAsync(
    HttpRequest request,
    [FromServices] ISecurityService securityService,
    [FromServices] ILocalizationService localizationService,
    [FromQuery] long applicationId,
    [FromQuery] string localizationMessageKey)
    {

        var returnObject = await localizationService.GetLocalizedMessagesListByKeyAsync(applicationId, localizationMessageKey);
        return TypedResults.Ok(returnObject);

    }

    public static Results<Ok<CultureTranslationDTO>, NotFound, UnauthorizedHttpResult, ValidationProblem> GetLocalizationMessageByKeySync(
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