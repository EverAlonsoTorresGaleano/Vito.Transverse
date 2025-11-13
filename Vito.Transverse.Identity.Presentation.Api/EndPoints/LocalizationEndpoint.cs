using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        var endPointGroupVersioned = app.MapGroup("api/Localizations/v{apiVersion:apiVersion}").WithApiVersionSet(versionSet)
            .AddEndpointFilter<LocalizationFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();


        endPointGroupVersioned.MapGet("", GetLocalizationMessagesListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Localization Current Application/ Current Culture Async")
            .WithDescription("[Anonymous]")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("{messageKey}", GetCultureTranslationByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Culture Translation Current Application/ Current Culture  ByKey Async")
            .WithDescription("[Anonymous]")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("{messageKey}/All", GetLocalizationMessagesListByKeyAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Localization Messages By Key Current Application All Cultures Async")
            .WithDescription("[Require Authorization]")
            .AllowAnonymous();

        endPointGroupVersioned.MapGet("{messageKey}/WithParams", GetLocalizationMessagesListByKeyAndParamsAsync)
             .MapToApiVersion(1.0)
            .WithSummary("Get Localization Current Application/ Current Culture By Key With Param Async")
            .WithDescription("[Require Authorization]")
            .AllowAnonymous();



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
        [FromServices] ILocalizationService localizationService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await localizationService.GetLocalizedMessagesListByApplicationAndCultureAsync(deviceInformation!.ApplicationId!, deviceInformation!.CultureId!);
        return TypedResults.Ok(returnObject);

    }

    public static async Task<Results<Ok<List<CultureTranslationDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLocalizationMessagesListByKeyAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromRoute] string messageKey)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await localizationService.GetLocalizedMessagesListByKeyAsync(deviceInformation!.ApplicationId!, messageKey);
        return TypedResults.Ok(returnObject);

    }

    public static Results<Ok<CultureTranslationDTO>, NotFound, UnauthorizedHttpResult, ValidationProblem> GetLocalizationMessagesListByKeyAndParamsAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ILocalizationService localizationService,
        [FromRoute] string messageKey,
        [FromQuery] params string[]? parameters)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var returnObject = localizationService.GetLocalizedMessageByKeyAndParamsSync(deviceInformation!.ApplicationId, deviceInformation.CultureId, messageKey, parameters ?? []);
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CultureTranslationDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCultureTranslationByIdAsync(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService,
        [FromRoute] string messageKey)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await localizationService.GetCultureTranslationByIdAsync(deviceInformation.ApplicationId, deviceInformation.CultureId, messageKey);
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
        [FromBody] CultureTranslationDTO cultureTranslationDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(cultureTranslationDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await localizationService.UpdateCultureTranslationAsync(deviceInformation.ApplicationId, deviceInformation.CultureId, cultureTranslationDTO.TranslationKey, cultureTranslationDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteCultureTranslationAsync(
        HttpRequest request,
        [FromServices] ILocalizationService localizationService,
        [FromQuery] string translationKey)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await localizationService.DeleteCultureTranslationAsync(deviceInformation.ApplicationId, deviceInformation.CultureId, translationKey, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }
}