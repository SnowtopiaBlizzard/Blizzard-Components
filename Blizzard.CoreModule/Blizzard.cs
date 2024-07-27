using Blizzard.BlizzardMenu;
using Blizzard.Economy;
using Blizzard.Events;
using Blizzard.Loading;
using Blizzard.Maps;
using Blizzard.UI;
using Blizzard.User;

namespace Blizzard
{
    public static class Blizzard
    {
        internal static MainUIHandler MainUIHandler;

        public static EconomyModule EconomyModule;

        public static async void Init()
        {
            GameEvents.onGameLoaded += onGameLoaded;
            GameEvents.onMainMenuLoaded += onMainMenuLoaded;
            
            HiddenMapLoader.Init();
            await UserHandler.UpdateUserAsync();

            MainUIHandler = new MainUIHandler();
            new BlizzardLoader();
        }

        private static void onMainMenuLoaded()
        {
            BlizzardMenuController.Init();
        }

        private static void onGameLoaded()
        {
            EconomyModule = new EconomyModule();
        }
    }
}
