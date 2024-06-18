using System;
using UnityEngine.SceneManagement;

namespace Blizzard.Events
{
    public class GameEvents
    {
        // Main Menu
        private const string MAIN_MENU_NAME = "MainMenu";
        public static event Action onMainMenuLoaded;
        public static event Action onMainMenuUnloaded;

        // Game
        private const string GAME_NAME = "MAP_";
        public static event Action onGameLoaded;
        public static event Action onGameUnloaded;

        // Loading
        private const string LOADING_NAME = "LoadingScreen";
        public static event Action onLoadingLoaded;
        public static event Action onLoadingUnloaded;

        static GameEvents()
        {
            SceneManager.sceneLoaded += sceneLoaded;
            SceneManager.sceneUnloaded += sceneUnloaded;
        }

        private static void sceneUnloaded(Scene scene)
        {
            if (scene.name.StartsWith(GAME_NAME))
            {
                onGameUnloaded?.Invoke();
                return;
            }

            switch (scene.name)
            {
                case MAIN_MENU_NAME:
                    onMainMenuUnloaded?.Invoke();
                    break;
                case LOADING_NAME:
                    onLoadingUnloaded?.Invoke();
                    break;
                default:
                    break;
            }
        }

        private static void sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.StartsWith(GAME_NAME))
            {
                onGameLoaded?.Invoke();
                return;
            }

            switch (scene.name)
            {
                case MAIN_MENU_NAME:
                    onMainMenuLoaded?.Invoke();
                    break;
                case LOADING_NAME:
                    onLoadingLoaded?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}
