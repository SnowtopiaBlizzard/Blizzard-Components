using Blizzard.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Blizzard.Loading
{
    public class BlizzardLoader
    {
        private readonly List<string> loadingStrings = new List<string> {
            "Watching the Avalanche",
            "Enjoying Tea",
            "Looking at the Blizzard",
            "Hugging Bamsen",
            "Mapping Mappers",
            "Eating Cheese",
            "Polishing Snowtopia"
        };

        public BlizzardLoader()
        {
            SceneManager.LoadScene("LoadingScreen");
            GameEvents.onLoadingLoaded += OnLoadScene;
        }

        private void OnLoadScene()
        {
            GameEvents.onLoadingLoaded -= OnLoadScene;
            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            foreach (var loadingString in loadingStrings)
            {
                Blizzard.MainUIHandler.ChangeLoadingScreenText(loadingString);
                await Task.Delay(1000); 
            }

            UnloadLoadingScreen();
        }

        private void UnloadLoadingScreen()
        {
            SceneManager.LoadScene("Launcher");
        }
    }

    
}
