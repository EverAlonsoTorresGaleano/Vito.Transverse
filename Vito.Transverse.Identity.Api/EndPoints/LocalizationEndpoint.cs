using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Localization;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Endpoints;

/// <summary>
/// Home Endpoint
/// </summary>
/// <example>https://developer.usps.com/api/81</example>
public static class LocalizationEndpoint
{

    public static void MapLocalizationEndpoint(this WebApplication app, ApiVersionSet versionSet)
    {
        //var endPointGroupNoVersioned = app.MapGroup("api/Oauth2/");

        //endPointGroupNoVersioned.MapPost("Token", TokenAync)
        //    .WithSummary("Get Authentication Token")
        //    .AddEndpointFilter<SecurityFeatureFlagFilter>()
        //    .AddEndpointFilter<InfrastructureFilter>();

        var endPointGroupVersioned = app.MapGroup("api/Localization/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<LocalizationFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();


        endPointGroupVersioned.MapGet("AllLocalizedMessagesByApplicationAsync", GetAllLocalizedMessagesByApplicationAsync)
             .MapToApiVersion(1.0)
            .WithSummary("GetAllLocalizedMessagesByApplicationAsync")
            .AddEndpointFilter<LocalizationFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();


        endPointGroupVersioned.MapGet("AllLocalizedMessagesAsync", GetAllLocalizedMessagesAsync)
            .MapToApiVersion(1.0)
            .WithSummary("GetAllLocalizedMessagesAsync")
            .AddEndpointFilter<LocalizationFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("LocalizedMessagesByKeyAsync", GetLocalizedMessagesByKeyAsync)
           .MapToApiVersion(1.0)
           .WithSummary("GetLocalizedMessagesByKeyAsync")
           .AddEndpointFilter<LocalizationFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();


        endPointGroupVersioned.MapGet("LocalizedMessageByKeyAndParamsSync", GetLocalizedMessageByKeyAndParamsSync)
             .MapToApiVersion(1.0)
            .WithSummary("GetLocalizedMessageByKeyAsync")
             .AddEndpointFilter<LocalizationFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>()
            .RequireAuthorization();
    }


    public static async Task<Results<Ok<List<CultureTranslationDTO>>, NotFound, ValidationProblem>> GetAllLocalizedMessagesByApplicationAsync(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long applicationId)
    {
        var returnObject = await localizationService.GetAllLocalizedMessagesByApplicationAsync(applicationId);
        return TypedResults.Ok(returnObject);
    }


    public static async Task<Results<Ok<List<CultureTranslationDTO>>, NotFound, ValidationProblem>> GetAllLocalizedMessagesAsync(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long applicationId,
        [FromQuery] string? cultureId)
    {
        var returnObject = await localizationService.GetAllLocalizedMessagesAsync(applicationId, cultureId!);
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, NotFound, ValidationProblem>> GetLocalizedMessagesByKeyAsync(
    HttpRequest request,
    [FromServices] ILocalizationService localizationService,
    [FromQuery] long applicationId,
    [FromQuery] string localizationMessageKey)
    {
        var returnObject = await localizationService.GetLocalizedMessagesByKeyAsync(applicationId, localizationMessageKey);
        return TypedResults.Ok(returnObject);
    }

    public static Results<Ok<CultureTranslationDTO>, NotFound, ValidationProblem> GetLocalizedMessageByKeyAndParamsSync(
        HttpRequest request,
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