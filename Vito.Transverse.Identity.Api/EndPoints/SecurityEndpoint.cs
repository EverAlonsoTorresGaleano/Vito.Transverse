using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Models.Security;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;

namespace Vito.Transverse.Identity.Api.Endpoints;

/// <summary>
/// Home Endpoint
/// </summary>
/// <example>https://developer.usps.com/api/81</example>
public static class SecurityEndpoint
{
    public static void MapSecurityEndpoint(this WebApplication app, ApiVersionSet versionSet)
    {
        //var endPointGroupNoVersioned = app.MapGroup("api/Oauth2/");

        //endPointGroupNoVersioned.MapPost("Token", TokenAync)
        //    .WithSummary("Get Authentication Token")
        //    .AddEndpointFilter<SecurityFeatureFlagFilter>()
        //    .AddEndpointFilter<InfrastructureFilter>();


        var endPointGroupVersioned = app.MapGroup("api/Oauth2/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet);

        endPointGroupVersioned.MapPost("Token", TokenAync)
             .MapToApiVersion(1.0)
            .WithSummary("Get Authentication Token")
            .AddEndpointFilter<InfrastructureFilter>()
            .AddEndpointFilter<SecurityFeatureFlagFilter>();

    }

    public static async Task<Results<Ok<TokenResponseDTO>, NotFound, ValidationProblem>> TokenAync(
        HttpRequest request,
        [FromServices] ISecurityService securityService,
        [FromServices] IValidator<TokenRequestDTO> validator,
        [FromBody] TokenRequestDTO requestBody)
    {

        var deviceInformation = request.HttpContext.Items[FrameworkConstants.HttpContext_DeviceInformationList] as DeviceInformationDTO;

        var validationResult = await validator.ValidateAsync(requestBody);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(errors: validationResult.ToDictionary());
        }
        var returnObject = await securityService.CreateAuthenticationTokenAsync(requestBody, deviceInformation!);

        return returnObject == null ? TypedResults.NotFound() : TypedResults.Ok(returnObject);
    }

}