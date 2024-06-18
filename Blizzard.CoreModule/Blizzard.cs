using Blizzard.Loading;
using Blizzard.UI;

namespace Blizzard
{
    public static class Blizzard
    {
        internal static MainUIHandler MainUIHandler;

        public static void Init()
        {
            MainUIHandler = new MainUIHandler();
            new BlizzardLoader();
        }
    }
}
