using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blizzard.Api
{
    public class UserApiController : ApiHandler
    {
        // API URLs
        private const string ValidateTokenUrl = ApiEndpoint + "validate?token=";
        private const string GetInfoUrl = ApiEndpoint + "info?token=";
        private const string RegisterUrl = ApiEndpoint + "register";

        /// <summary>
        /// Checks if the token is valid.
        /// </summary>
        /// <param name="token">Token value to check.</param>
        /// <returns>True if the token is valid, otherwise false.</returns>
        /// <exception cref="Exception">Error with server.</exception>
        public static async Task<bool> ValidateTokenAsync(string token)
        {
            using (HttpClient client = CreateHttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ValidateTokenUrl + token);

                    return response.StatusCode == HttpStatusCode.OK;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("Error validating token", ex);
                }
            }
        }

        /// <summary>
        /// Gets the user info as JSON.
        /// </summary>
        /// <param name="token">Token for user.</param>
        /// <returns>JSON string with user info.</returns>
        /// <exception cref="Exception">Error with server.</exception>
        public static async Task<string> GetDiscordInfoAsync(string token)
        {
            using (HttpClient client = CreateHttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(GetInfoUrl + token);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    throw new Exception("Failed to retrieve user info");
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("Error retrieving user info", ex);
                }
            }
        }

        /// <summary>
        /// Opens the register URL in the web browser.
        /// </summary>
        public static void OpenRegisterUrl()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = RegisterUrl,
                UseShellExecute = true
            });
        }
    }
}
