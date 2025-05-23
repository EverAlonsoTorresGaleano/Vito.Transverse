using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vito.Framework.Api.Filters;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Api.Filters.FeatureFlag;
using Vito.Transverse.Identity.BAL.IntegrationServices.Twilio;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;

namespace Vito.Transverse.Identity.Api.EndPoints;

public static class TwilioEndPoint
{
    public static void MapTwilioEndPoint(this WebApplication app, ApiVersionSet versionSet)
    {
        var endPointGroupVersioned = app.MapGroup("api/Twilio/v{apiVersion:apiVersion}/").WithApiVersionSet(versionSet)
                .RequireAuthorization()
                .AddEndpointFilter<HomeFeatureFlagFilter>()
                .AddEndpointFilter<InfrastructureFilter>();

        endPointGroupVersioned.MapGet("SendSMS", SendSMSAync)
             .MapToApiVersion(1.0)
            .WithSummary("Twilio SendSMS version 1.0");

    }

    public static async Task<Ok<PingResponseDTO>> SendSMSAync(HttpRequest request, [FromServices] ICultureService cultureService, [FromServices] ITwilioService twilioService, [FromQuery] string message)
    {
        var returnMessage = await twilioService.SendSMSAsync(message + "Les comparto el nuevo emprendimiento visítenos en https://www.instagram.com/chicas.store.med fron DotNet", "+573146625684");
        PingResponseDTO returnObject = new()
        {
            PingMessage = returnMessage,
            PingDate = cultureService.UtcNow().DateTime,
        };
        return TypedResults.Ok(returnObject);
    }
}