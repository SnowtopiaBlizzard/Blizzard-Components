using Blizzard.Events;
using Blizzard.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Blizzard.UI
{
    public class MainUIHandler
    {
        public MainUIHandler()
        {
            GameEvents.onMainMenuLoaded += OnMainMenuLoaded;
            GameEvents.onLoadingLoaded += OnLoadingScreenLoaded;
        }

        // Loading UI
        #region
        private void OnLoadingScreenLoaded()
        {
            ChangeLoadingScreenText("Blizzard...");
        }

        public void ChangeLoadingScreenText(string text)
        {
            GameObject loadingTextObject = GameObject.Find("SceneLoader/LoadingCanvas/Loading/Bottom/Text");
            loadingTextObject.GetComponent<TextMeshProUGUI>().text = text;
        } 
        #endregion


        // Main Menu UI
        #region
        private void OnMainMenuLoaded()
        {
            GameObject version1GameObject = GameObject.Find("MainMenuCanvas/Main menu/Version");
            version1GameObject.GetComponent<TMPro.TextMeshProUGUI>().text = BlizzardData.VERSION_TEXT;
            Object.Destroy(version1GameObject.GetComponent<VersionWidget>());
            version1GameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(400, 0);

            GameObject version2GameObject = GameObject.Find("MainMenuCanvas/Main menu/Version (1)");
            version2GameObject.GetComponent<TMPro.TextMeshProUGUI>().text = BlizzardData.VERSION_TEXT;
            Object.Destroy(version2GameObject.GetComponent<VersionWidget>());
            version2GameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(400, 0);

            LoadBackgroundImage(GameObject.Find("MainMenuCanvas/Main menu - Background/Background"));
        }

        private void LoadBackgroundImage(GameObject mainMenuCanvas)
        {
            AssetBundle blizzardBundle = BundleHelper.LoadIfNotLoaded(BundleHelper.AssetBundlePath + "blizzard_content");
            if (blizzardBundle == null)
            {
                Debug.LogError("Blizzard asset bundle not found");
                return;
            }

            Image imageComponent = mainMenuCanvas.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = blizzardBundle.LoadAsset<Sprite>("blizzard_background");
            }
            else
            {
                Debug.LogError("Image component not found on background image object");
            }
        }
        #endregion
    }
}
