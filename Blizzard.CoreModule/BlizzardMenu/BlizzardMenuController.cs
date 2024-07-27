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
            objectUI = UnityEngine.Object.Instantiate(instanceUI, GameObject.Find("MainMenuCanvas").transform);
            objectUI.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            objectUI.SetActive(state);
            BundleHelper.PatchUIObject(objectUI);

            BlizzardGeneralTab.Init(objectUI);
        }

        public static void Toggle()
        {
            objectUI.transform.SetSiblingIndex(2);
            state = !state;
            objectUI.SetActive(state);
        }
    }
}
