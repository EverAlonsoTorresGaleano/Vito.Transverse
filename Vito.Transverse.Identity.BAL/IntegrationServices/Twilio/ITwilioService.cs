namespace Vito.Transverse.Identity.BAL.IntegrationServices.Twilio;

/// <summary>
/// Handlig Social Network 
/// </summary>
public interface ITwilioService
{
    /// <summary>
    /// Send Message
    /// </summary>
    /// <param name="message"></param>
    /// <param name="targetPhoneNumber"></param>
    /// <returns></returns>
    public Task<string> SendSMSAsync(string message, string targetPhoneNumber);
}
