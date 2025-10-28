using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Vito.Transverse.Identity.Entities.Options;

namespace  Vito.Transverse.Identity.Application.IntegrationServices.Twilio;


/// <see cref="ITwilioService"/>
public class TwilioService(IOptions<TwilioSettingsOptions> twilioIOption) : ITwilioService
{
    private TwilioSettingsOptions twilioOptions => twilioIOption.Value;

    /// <see cref="ITwilioService.SendSMSAsync(string, string)"/>
    public Task<string> SendSMSAsync(string message, string targetPhoneNumber)
    {
        var accountSid = twilioOptions.AccountSid;
        var authToken = twilioOptions.AuthToken;
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(new(targetPhoneNumber));
        messageOptions.From = new PhoneNumber(twilioOptions.FromPhoneNumber);
        messageOptions.Body = message;
        //messageOptions.MediaUrl = [new Uri("")];

        var messageResponse = MessageResource.Create(messageOptions);
        Console.WriteLine(messageResponse.Body);
        return Task.FromResult(messageResponse.Body);
    }
}
