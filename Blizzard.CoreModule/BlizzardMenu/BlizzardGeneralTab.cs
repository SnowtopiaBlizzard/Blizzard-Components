using Blizzard.Api;
using Blizzard.User;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Blizzard.BlizzardMenu
{
    public static class BlizzardGeneralTab
    {
        // Main Paths
        private const string GENERAL_TAB_PATH = "Content/Content/GeneralTab";
        private const string ACCOUNT_CONTENT_PATH = GENERAL_TAB_PATH + "/AccountContent";

        // AccountContent Paths
        private const string DISCORD_SETTINGS_PATH = ACCOUNT_CONTENT_PATH + "/DiscordIcon";
        private const string DISCORD_ICON_PATH = DISCORD_SETTINGS_PATH + "/Icon";
        private const string USERNAME_PATH = DISCORD_SETTINGS_PATH + "/Username";
        private const string USERNAME_TEXT_PATH = USERNAME_PATH + "/Text (TMP)";

        // UI Objects
        private static Button discordButton;
        private static Button nameButton;
        private static TextMeshProUGUI nameComponent;

        // Properties
        private static GameObject ScreenUI;
        private static DiscordUser User => UserHandler.user;

        internal static async void Init(GameObject screenUIObject)
        {
            ScreenUI = screenUIObject;

            discordButton = ScreenUI.transform.Find(DISCORD_ICON_PATH).GetComponent<Button>();
            nameButton = ScreenUI.transform.Find(USERNAME_PATH).GetComponent<Button>();
            nameComponent = ScreenUI.transform.Find(USERNAME_TEXT_PATH).GetComponent<TextMeshProUGUI>();

            discordButton.onClick.AddListener(onDiscordButtonClick);
            await HandleDiscordConnection();
        }

        // When the discord icon is clicked, open register url. If token file exists, update user.
        private static async void onDiscordButtonClick()
        {
            // If exists then check if token is okay.
            if (File.Exists(UserHandler.TokenFilePath))
            {
                // Update the UserHandler.user with the new token.
                await UserHandler.UpdateUserAsync();

                // Set the name if token is vaild.
                bool success = await HandleDiscordConnection();

                // If token is vaild, remove this lisenter.
                if (success)
                {
                    discordButton.onClick.RemoveAllListeners();
                    return;
                }
            }

            // Open the register url
            UserApiController.OpenRegisterUrl();
        }

        // If it has a valid token, set the name.
        private static async Task<bool> HandleDiscordConnection()
        {
            // Read token
            string token = await GetDiscordToken();

            // If there is a token, set the Text to the username.
            if (token != null)
            {
                AddDiscordName(token);
                return true;
            }
            
            // Token invaild.
            return false;
        }

        private static async void AddDiscordName(string token)
        {
            nameComponent.text = User.Name;

            // Add EventTrigger for hover effect
            EventTrigger eventTrigger = nameButton.gameObject.AddComponent<EventTrigger>();
            eventTrigger.triggers = new List<EventTrigger.Entry>();

            // OnPointerEnter (hover start)
            var pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            pointerEnter.callback.AddListener((eventData) => { nameComponent.fontStyle |= FontStyles.Strikethrough; });
            eventTrigger.triggers.Add(pointerEnter);

            // OnPointerExit (hover end)
            var pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            pointerExit.callback.AddListener((eventData) => { nameComponent.fontStyle &= ~FontStyles.Strikethrough; });
            eventTrigger.triggers.Add(pointerExit);

            // When the username clicks, you remove the token.
            nameButton.onClick.AddListener(async () => {
                // Remove the current listener
                nameButton.onClick.RemoveAllListeners();
                
                // Delete the token file
                File.Delete(UserHandler.TokenFilePath);

                // Update the user struct
                await UserHandler.UpdateUserAsync();

                // Add the connection Lisenter
                discordButton.onClick.AddListener(onDiscordButtonClick);

                // Reset the text
                nameComponent.text = "Please connect your account";
                nameComponent.fontStyle &= ~FontStyles.Strikethrough;

                // Destory the Hover Component
                UnityEngine.Object.Destroy(ScreenUI.transform.Find(USERNAME_PATH).GetComponent<EventTrigger>());
            });
        }

        // Get the User Token
        private static async Task<string> GetDiscordToken()
        {
            // If it exist, read the file.
            if (File.Exists(UserHandler.TokenFilePath))
            {
                // Content of token file "blizzardtoken://<token>/"
                string token = File.ReadAllText(UserHandler.TokenFilePath).Replace("blizzardtoken://", "").Replace("/", "");

                // Validate the token
                bool result = await UserApiController.ValidateTokenAsync(token);

                // If its not valid, delete the token file.
                if (!result)
                {
                    File.Delete(UserHandler.TokenFilePath);
                    return null;
                }

                // Return the token.
                return token;
            }
            return null;
        }
    }
}
