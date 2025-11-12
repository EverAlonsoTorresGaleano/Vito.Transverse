

using Newtonsoft.Json; // You might need to install the Newtonsoft.Json NuGet package
using Vito.Framework.Common.Models.Security;

namespace Vito.Transverse.Identity.Presentation.Api.Helpers;

public class HttpHelper
{

    public async Task<string> GetBearerTokenAsync(string tokenEndpoint, string clientId, string clientSecret)
    {
        using (var client = new HttpClient())
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials"), // Or other grant types
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret)
        });

            HttpResponseMessage response = await client.PostAsync(tokenEndpoint, formContent);
            response.EnsureSuccessStatusCode(); // Throws an exception if not a success status code

            string jsonContent = await response.Content.ReadAsStringAsync();
            TokenResponseDTO tokenData = JsonConvert.DeserializeObject<TokenResponseDTO>(jsonContent);

            return tokenData.access_token;
        }
    }
}