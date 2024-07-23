using Blizzard.Helpers;
using UnityEngine;

namespace Blizzard
{
    public static class BlizzardData
    {

        public const string VERSION = "pre-dev";
        public const string GAME_VERSION = "1.0.6";
        public const string VERSION_TEXT = "v"+GAME_VERSION + " Blizzard-" + VERSION;

        public const bool DEBUG_MODE = true;

        public static bool IS_BETA { get { return DEBUG_MODE; } }

        private static AssetBundle betaBundle;
        public static AssetBundle BETA_ASSET_BUNDLE
        {
            get
            {
                if (!IS_BETA) return null;
                if (betaBundle == null)
                {
                    betaBundle = BundleHelper.LoadIfNotLoaded(BundleHelper.AssetBetaBundlePath + "blizzard.beta");
                }
                return betaBundle;
            }
        }
    }
}
