using Blizzard.BlizzardMenu;
using Blizzard.Economy;
using Blizzard.Events;
using Blizzard.Loading;
using Blizzard.Maps;
using Blizzard.UI;

namespace Blizzard
{
    public static class Blizzard
    {
        internal static MainUIHandler MainUIHandler;

        public static EconomyModule EconomyModule;

        public static void Init()
        {
            GameEvents.onGameLoaded += onGameLoaded;

            HiddenMapLoader.Init();
            BlizzardMenuController.Init();

            MainUIHandler = new MainUIHandler();
            new BlizzardLoader();
        }

        private static void onGameLoaded()
        {
            EconomyModule = new EconomyModule();
        }
    }
}
