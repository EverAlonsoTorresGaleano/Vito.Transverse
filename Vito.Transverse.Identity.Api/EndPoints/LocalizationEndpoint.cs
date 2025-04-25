using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
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
     

        endPointGroupVersioned.MapGet("AllLocalizedMessagesAsync", GetAllLocalizedMessagesAsync)
             .MapToApiVersion(1.0)
            .WithSummary("GetAllLocalizedMessagesAsync");

        endPointGroupVersioned.MapGet("LocalizedMessage", GetLocalizedMessage)
             .MapToApiVersion(1.0)
            .WithSummary("GetLocalizedMessage");
    }


    public static async Task<Results<Ok<List<CultureTranslationDTO>>, NotFound, ValidationProblem>> GetAllLocalizedMessagesAsync(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService)
    {
        var returnObject = await localizationService.GetAllLocalizedMessagesAsync();
        return TypedResults.Ok(returnObject);
    }

    public static Results<Ok<CultureTranslationDTO>, NotFound, ValidationProblem> GetLocalizedMessage(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] string localizationMessageKey,
        [FromQuery] params string[]? parameters)
    {
        var returnObject = localizationService.GetLocalizedMessage(localizationMessageKey, parameters ?? []);
        return TypedResults.Ok(returnObject);
    }


}