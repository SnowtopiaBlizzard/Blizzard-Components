using Blizzard.Helpers;
using UnityEngine;

namespace Blizzard.BlizzardMenu
{
    public static class BlizzardMenuController
    {
        private static bool state = false;
        private static GameObject instanceUI = BlizzardData.CONTENT_ASSET_BUNDLE.LoadAsset<GameObject>("BlizzardSettingsUI");
        private static GameObject objectUI = null;

        public static void Init()
        {
            
        }

        public static void Toggle()
        {
            if (objectUI == null)
            {
                objectUI = Object.Instantiate(instanceUI, GameObject.Find("MainMenuCanvas").transform);
                BundleHelper.PatchUIObject(objectUI);
            }
            state = !state; 
            objectUI.SetActive(state);
        }
    }
}
