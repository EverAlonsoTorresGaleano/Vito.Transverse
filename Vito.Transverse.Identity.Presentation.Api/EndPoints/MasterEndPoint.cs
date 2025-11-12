using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Application.TransverseServices.Master;
using Vito.Transverse.Identity.Application.TransverseServices.Culture;
using Vito.Transverse.Identity.Application.TransverseServices.Security;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Presentation.Api.Filters;
using Vito.Transverse.Identity.Presentation.Api.Filters.FeatureFlag;

namespace Vito.Transverse.Identity.Presentation.Api.EndPoints;

public static class MasterEndPoint
{
    public static void MapMasterEndPoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Master/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
            .AddEndpointFilter<MasterFeatureFlagFilter>()
            .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("Secuences", GetAllSecuencesListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Secuences List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Secuences/{secuenceId}", GetSecuenceByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Secuence By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("Secuences", CreateNewSecuenceAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Secuence Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Secuences", UpdateSecuenceByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Secuence By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Secuences/Delete/{secuenceId}", DeleteSecuenceByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Secuence By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Cultures", GetAllCultureListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Culture List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Cultures/dropdown", GetAllCultureListItemAsync)
         .MapToApiVersion(1.0)
         .WithSummary("Get All Culture List Async")
         .WithDescription("[Author] [Authen] [Trace]")
         .RequireAuthorization()
         .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Cultures/UtcDate", GetUtcNow)
            .MapToApiVersion(1.0)
            .WithSummary("Get Utc Now")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Cultures/Active", GetActiveCultureListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Active Culture List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Cultures/Active/DropDown", GetActiveCultureListItemDTOListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Active Culture ListItemDTO List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Cultures/{cultureId}", GetCultureByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Culture By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("Cultures", CreateNewCultureAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Culture Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Cultures", UpdateCultureByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Culture By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Cultures/Delete/{cultureId}", DeleteCultureByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Culture By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Languages", GetAllLanguageListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Language List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Languages/dropdown", GetAllLanguageListItemAsync)
           .MapToApiVersion(1.0)
           .WithSummary("Get All Language List Async")
           .WithDescription("[Author] [Authen] [Trace]")
           .RequireAuthorization()
           .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Languages/{languageId}", GetLanguageByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Language By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("Languages", CreateNewLanguageAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Language Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Languages", UpdateLanguageByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Language By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Languages/Delete/{languageId}", DeleteLanguageByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Language By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Countries", GetAllCountryListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Country List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Countries/dropdown", GetAllCountryListItemAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Country List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("Countries/{countryId}", GetCountryByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Country By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("Countries", CreateNewCountryAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Country Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("Countries", UpdateCountryByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Country By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("Countries/Delete/{countryId}", DeleteCountryByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Country By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("GeneralTypeGroups", GetAllGeneralTypeGroupListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All General Type Group List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("GeneralTypeGroups/dropdown", GetAllGeneralTypeGroupListItemAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All General Type Group List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("GeneralTypeGroups/{generalTypeGroupId}", GetGeneralTypeGroupByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get General Type Group By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("GeneralTypeGroups", CreateNewGeneralTypeGroupAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New General Type Group Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("GeneralTypeGroups", UpdateGeneralTypeGroupByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update General Type Group By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("GeneralTypeGroups/Delete/{generalTypeGroupId}", DeleteGeneralTypeGroupByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete General Type Group By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("GeneralTypeItems", GetAllGeneralTypeItemListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All General Type Item List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("GeneralTypeItems/dropdown/{groupId}", GetGeneralTypeItemByGroupIdListItemAsync)
          .MapToApiVersion(1.0)
          .WithSummary("Get All General Type Item List Async")
          .WithDescription("[Author] [Authen] [Trace]")
          .RequireAuthorization()
          .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("GeneralTypeItems/ByGroupId/{groupId}", GetGeneralTypeItemByGroupIdListAsync)
         .MapToApiVersion(1.0)
         .WithSummary("Get All General Type Item List Async")
         .WithDescription("[Author] [Authen] [Trace]")
         .RequireAuthorization()
         .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("GeneralTypeItems/{generalTypeItemId}", GetGeneralTypeItemByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get General Type Item By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("GeneralTypeItems", CreateNewGeneralTypeItemAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New General Type Item Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("GeneralTypeItems", UpdateGeneralTypeItemByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update General Type Item By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("GeneralTypeItems/Delete/{generalTypeItemId}", DeleteGeneralTypeItemByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete General Type Item By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("NotificationTemplates", GetAllNotificationTemplateListAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Notification Template List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("NotificationTemplates/dropdown", GetAllNotificationTemplateListItemAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get All Notification Template List Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapGet("NotificationTemplates/{notificationTemplateId}", GetNotificationTemplateByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Get Notification Template By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPost("NotificationTemplates", CreateNewNotificationTemplateAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Create New Notification Template Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapPut("NotificationTemplates", UpdateNotificationTemplateByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Update Notification Template By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();

        endPointGroupVersioned.MapDelete("NotificationTemplates/Delete/{notificationTemplateId}", DeleteNotificationTemplateByIdAsync)
            .MapToApiVersion(1.0)
            .WithSummary("Delete Notification Template By Id Async")
            .WithDescription("[Author] [Authen] [Trace]")
            .RequireAuthorization()
            .AddEndpointFilter<RoleAuthorizationFilter>();
    }

    public static async Task<Results<Ok<List<SecuencesDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllSecuencesListAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllSecuencesListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<SecuencesDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetSecuenceByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] long secuenceId)
    {
        var returnObject = await masterService.GetSecuenceByIdAsync(secuenceId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<SecuencesDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewSecuenceAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<SecuencesDTO> validator,
        [FromBody] SecuencesDTO secuenceDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(secuenceDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.CreateNewSecuenceAsync(secuenceDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<SecuencesDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateSecuenceByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<SecuencesDTO> validator,
        [FromBody] SecuencesDTO secuenceDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(secuenceDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.UpdateSecuenceByIdAsync(secuenceDTO.Id, secuenceDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteSecuenceByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] long secuenceId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await masterService.DeleteSecuenceByIdAsync(secuenceId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    // Culture CRUD Endpoints
    public static async Task<Results<Ok<DateTimeOffset>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetUtcNow(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var returnObject = await Task.FromResult(cultureService.UtcNow());
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<CultureDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActiveCultureListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await cultureService.GetActiveCultureListAsync(deviceInformation!.ApplicationId!.Value);
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetActiveCultureListItemDTOListAsync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] ICultureService cultureService)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var returnObject = await cultureService.GetActiveCultureListItemDTOListAsync(deviceInformation!.ApplicationId!.Value);
        return TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<CultureDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllCultureListAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllCultureListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllCultureListItemAsync(
     HttpRequest request,
     [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllCultureListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CultureDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCultureByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] string cultureId)
    {
        var returnObject = await masterService.GetCultureByIdAsync(cultureId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CultureDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewCultureAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<CultureDTO> validator,
        [FromBody] CultureDTO cultureDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(cultureDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.CreateNewCultureAsync(cultureDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CultureDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateCultureByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<CultureDTO> validator,
        [FromBody] CultureDTO cultureDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(cultureDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.UpdateCultureByIdAsync(cultureDTO.Id, cultureDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteCultureByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] string cultureId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await masterService.DeleteCultureByIdAsync(cultureId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    // Language CRUD Endpoints
    public static async Task<Results<Ok<List<LanguageDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllLanguageListAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllLanguageListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllLanguageListItemAsync(
    HttpRequest request,
    [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllLanguageListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<LanguageDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetLanguageByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] string languageId)
    {
        var returnObject = await masterService.GetLanguageByIdAsync(languageId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<LanguageDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewLanguageAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<LanguageDTO> validator,
        [FromBody] LanguageDTO languageDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(languageDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.CreateNewLanguageAsync(languageDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<LanguageDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateLanguageByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<LanguageDTO> validator,
        [FromBody] LanguageDTO languageDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(languageDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.UpdateLanguageByIdAsync(languageDTO.Id, languageDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteLanguageByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] string languageId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await masterService.DeleteLanguageByIdAsync(languageId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    // Country CRUD Endpoints
    public static async Task<Results<Ok<List<CountryDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllCountryListAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllCountryListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllCountryListItemAsync(
    HttpRequest request,
    [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllCountryListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CountryDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetCountryByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] string countryId)
    {
        var returnObject = await masterService.GetCountryByIdAsync(countryId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CountryDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewCountryAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<CountryDTO> validator,
        [FromBody] CountryDTO countryDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(countryDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.CreateNewCountryAsync(countryDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<CountryDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateCountryByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<CountryDTO> validator,
        [FromBody] CountryDTO countryDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(countryDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.UpdateCountryByIdAsync(countryDTO.Id, countryDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteCountryByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] string countryId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await masterService.DeleteCountryByIdAsync(countryId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    // GeneralTypeGroup CRUD Endpoints
    public static async Task<Results<Ok<List<GeneralTypeGroupDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllGeneralTypeGroupListAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllGeneralTypeGroupListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllGeneralTypeGroupListItemAsync(
    HttpRequest request,
    [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllGeneralTypeGroupListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<GeneralTypeGroupDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetGeneralTypeGroupByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] long generalTypeGroupId)
    {
        var returnObject = await masterService.GetGeneralTypeGroupByIdAsync(generalTypeGroupId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<GeneralTypeGroupDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewGeneralTypeGroupAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<GeneralTypeGroupDTO> validator,
        [FromBody] GeneralTypeGroupDTO generalTypeGroupDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(generalTypeGroupDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.CreateNewGeneralTypeGroupAsync(generalTypeGroupDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<GeneralTypeGroupDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateGeneralTypeGroupByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<GeneralTypeGroupDTO> validator,
        [FromBody] GeneralTypeGroupDTO generalTypeGroupDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(generalTypeGroupDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.UpdateGeneralTypeGroupByIdAsync(generalTypeGroupDTO.Id, generalTypeGroupDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteGeneralTypeGroupByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] long generalTypeGroupId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await masterService.DeleteGeneralTypeGroupByIdAsync(generalTypeGroupId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    // GeneralTypeItem CRUD Endpoints
    public static async Task<Results<Ok<List<GeneralTypeItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllGeneralTypeItemListAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllGeneralTypeItemListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllGeneralTypeItemListItemAsync(
     HttpRequest request,
     [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllGeneralTypeItemListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<GeneralTypeItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetGeneralTypeItemByGroupIdListAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] GeneralTypesGroupEnum groupId)
    {
        var returnObject = await masterService.GetGeneralTypeItemByGroupIdListAsync((int)groupId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetGeneralTypeItemByGroupIdListItemAsync(
    HttpRequest request,
    [FromServices] IMasterService masterService,
    [FromRoute] GeneralTypesGroupEnum groupId)
    {
        var returnObject = await masterService.GetGeneralTypeItemByGroupIdListItemAsync((int)groupId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<GeneralTypeItemDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetGeneralTypeItemByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] long generalTypeItemId)
    {
        var returnObject = await masterService.GetGeneralTypeItemByIdAsync(generalTypeItemId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<GeneralTypeItemDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewGeneralTypeItemAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<GeneralTypeItemDTO> validator,
        [FromBody] GeneralTypeItemDTO generalTypeItemDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(generalTypeItemDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.CreateNewGeneralTypeItemAsync(generalTypeItemDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<GeneralTypeItemDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateGeneralTypeItemByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<GeneralTypeItemDTO> validator,
        [FromBody] GeneralTypeItemDTO generalTypeItemDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(generalTypeItemDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.UpdateGeneralTypeItemByIdAsync(generalTypeItemDTO.Id, generalTypeItemDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteGeneralTypeItemByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] long generalTypeItemId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await masterService.DeleteGeneralTypeItemByIdAsync(generalTypeItemId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }

    // NotificationTemplate CRUD Endpoints
    public static async Task<Results<Ok<List<NotificationTemplateDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllNotificationTemplateListAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllNotificationTemplateListAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<List<ListItemDTO>>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetAllNotificationTemplateListItemAsync(
      HttpRequest request,
      [FromServices] IMasterService masterService)
    {
        var returnObject = await masterService.GetAllNotificationTemplateListItemAsync();
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<NotificationTemplateDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> GetNotificationTemplateByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] long notificationTemplateId)
    {
        var returnObject = await masterService.GetNotificationTemplateByIdAsync(notificationTemplateId);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<NotificationTemplateDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> CreateNewNotificationTemplateAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<NotificationTemplateDTO> validator,
        [FromBody] NotificationTemplateDTO notificationTemplateDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(notificationTemplateDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.CreateNewNotificationTemplateAsync(notificationTemplateDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok<NotificationTemplateDTO>, UnauthorizedHttpResult, NotFound, ValidationProblem>> UpdateNotificationTemplateByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromServices] IValidator<NotificationTemplateDTO> validator,
        [FromBody] NotificationTemplateDTO notificationTemplateDTO)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var validationResult = await validator.ValidateAsync(notificationTemplateDTO);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await masterService.UpdateNotificationTemplateByIdAsync(notificationTemplateDTO.Id, notificationTemplateDTO, deviceInformation!);
        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

    public static async Task<Results<Ok, UnauthorizedHttpResult, NotFound>> DeleteNotificationTemplateByIdAsync(
        HttpRequest request,
        [FromServices] IMasterService masterService,
        [FromRoute] long notificationTemplateId)
    {
        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;
        var deleted = await masterService.DeleteNotificationTemplateByIdAsync(notificationTemplateId, deviceInformation!);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }
}

