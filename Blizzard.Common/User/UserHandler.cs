using Blizzard.Api;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Blizzard.User
{
    public static class UserHandler
    {
        public static DiscordUser user;

        // Path to the token file stored in the user's Application Data folder
        public static readonly string TokenFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "blizzard.token");

        /// <summary>
        /// Updates the user information by retrieving the latest data from the API.
        /// </summary>
        public static async Task UpdateUserAsync()
        {
            // Retrieve the Discord token
            string token = await GetDiscordTokenAsync();
            if (token != null)
            {
                try
                {
                    // Get user info from the API and deserialize it
                    string json = await UserApiController.GetDiscordInfoAsync(token);
                    user = new DiscordUser().FromJson(json);
                }
                catch (Exception ex)
                {
                    // Handle or log the error accordingly
                    Console.WriteLine($"Error updating user: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Retrieves the Discord token from the file and validates it.
        /// </summary>
        /// <returns>The valid Discord token, or null if the token is invalid or does not exist.</returns>
        private static async Task<string> GetDiscordTokenAsync()
        {
            // Check if the token file exists
            if (File.Exists(TokenFilePath))
            {
                // Read and clean the token from the file
                string token = File.ReadAllText(TokenFilePath);
                token = token.Replace("blizzardtoken://", "").Replace("/", "");

                // Validate the token with the API
                bool isValid = await UserApiController.ValidateTokenAsync(token);

                if (!isValid)
                {
                    // Delete the token file if the token is invalid
                    File.Delete(TokenFilePath);
                    return null;    
                }

                // Return the valid token
                return token;
            }
            // Return null if the token file does not exist
            return null;
        }
    }
}
