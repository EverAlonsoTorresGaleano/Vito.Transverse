using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.Application.TransverseServices.Localization;
using Vito.Transverse.Identity.Application.TransverseServices.Security;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.Endpoints;

public static class LocalizationEndpoint
{
    public static void MapLocalizationEndpoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Localizations/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<LocalizationFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();


        endPointGroupVersioned.MapGet("/", GetLocalizationMessagesListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Localization Messages By Application Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("/{messageKey}/All", GetLocalizationMessagesListByKeyAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Localization Messages By Key Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();



        endPointGroupVersioned.MapGet("/ByCulture/{cultureId}", GetLocalizationMessagesListByCultureAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Localization Messages By Application And Culture Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();



        endPointGroupVersioned.MapGet("{messageKey}/WithParams", GetLocalizationMessagesListByKeyAndParamsAsync)
             .MapToApiVersion(1.0)
            .WithSummary("Get Localization Message By Key With Param Async")
            .WithDescription("[Require Authorization]")
            .RequireAuthorization();

        endPointGroupVersioned.MapGet("{messageKey}", GetCultureTranslationByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Culture Translation By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("", CreateNewCultureTranslationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Culture Translation Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("", UpdateCultureTranslationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Culture Translation Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Delete", DeleteCultureTranslationAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Culture Translation Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();
    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLocalizationMessagesListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long? applicationId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await localizationService.GetLocalizedMessagesListByApplicationAsync(applicationId ?? deviceInformation!.ApplicationId!.Value);
        return TypedResults.Ok(returnObject);

    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLocalizationMessagesListByKeyAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long? applicationId,
        [FromRoute] string messageKey)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await localizationService.GetLocalizedMessagesListByKeyAsync(applicationId ?? deviceInformation!.ApplicationId!.Value, messageKey);
        return TypedResults.Ok(returnObject);

    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLocalizationMessagesListByCultureAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long? applicationId,
        [FromRoute] string? cultureId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await localizationService.GetLocalizedMessagesListByApplicationAndCultureAsync(applicationId ?? deviceInformation!.ApplicationId!.Value, cultureId!);
        return TypedResults.Ok(returnObject);

    }



    public static Results<Ok<CultureTranslationDTO>, NotFound, UnauthorizedHttpResult, ValidationProblem> GetLocalizationMessagesListByKeyAndParamsAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long? applicationId,
        [FromQuery] string? cultureId,
        [FromRoute] string messageKey,
        [FromQuery] params string[]? parameters)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var returnObject = localizationService.GetLocalizedMessageByKeyAndParamsSync(applicationId ?? deviceInformation!.ApplicationId!.Value, cultureId! ?? deviceInformation!.CultureId!, messageKey, parameters ?? []);
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CultureTranslationDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCultureTranslationByIdAsync(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService,
        [FromRoute] string messageKey)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await localizationService.GetCultureTranslationByIdAsync(deviceInformation.ApplicationId ?? 0, deviceInformation.CultureId, messageKey);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CultureTranslationDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewCultureTranslationAsync(
       HttpRequest request,
       [FromServices] ILocalizationService localizationService,
       [FromServices] IValidator<CultureTranslationDTO> validator,
       [FromBody] CultureTranslationDTO cultureTranslationDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(cultureTranslationDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await localizationService.CreateNewCultureTranslationAsync(cultureTranslationDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CultureTranslationDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateCultureTranslationAsync(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService,
        [FromServices] IValidator<CultureTranslationDTO> validator,
        [FromBody] CultureTranslationDTO cultureTranslationDTO,
        [FromQuery] long applicationId,
        [FromQuery] string cultureId,
        [FromQuery] string translationKey)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(cultureTranslationDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await localizationService.UpdateCultureTranslationAsync(applicationId, cultureId, translationKey, cultureTranslationDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteCultureTranslationAsync(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] long applicationId,
        [FromQuery] string cultureId,
        [FromQuery] string translationKey)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await localizationService.DeleteCultureTranslationAsync(applicationId, cultureId, translationKey, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }
}