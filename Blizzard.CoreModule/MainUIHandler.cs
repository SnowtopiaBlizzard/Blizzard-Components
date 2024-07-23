using Blizzard.Events;
using Blizzard.Helpers;
using System;
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
            ChangeLoadingScreenText(BlizzardData.IS_BETA ? "Loading Blizzard Beta..." : "Loading Blizzard...");
        }

        public void ChangeLoadingScreenText(string text)
        {
            GameObject loadingTextObject = GameObject.Find("SceneLoader/LoadingCanvas/Loading/Bottom/Text");
            TextMeshProUGUI loadingTextComponent = loadingTextObject.GetComponent<TextMeshProUGUI>();
            loadingTextComponent.text = text;
        } 
        #endregion


        // Main Menu UI
        #region
        private void OnMainMenuLoaded()
        {
            CreateBlizzardMenu();
            
            SetVersionText("Version");
            SetVersionText("Version (1)");

            SetBackgroundImage(GameObject.Find("MainMenuCanvas/Main menu - Background/Background"));
            SetIconImage();
        }

        private void CreateBlizzardMenu()
        {
            GameObject settingsButton = GameObject.Find("MainMenuCanvas/Main menu/Left/Menu/Button - Settings");
            GameObject blizzardButton = UnityEngine.Object.Instantiate(settingsButton, settingsButton.transform.parent);
            blizzardButton.name = "Button - Blizzard";
            blizzardButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Blizzard";
            blizzardButton.transform.SetSiblingIndex(2);
        }

        private void SetVersionText(string versionName)
        {
            GameObject versionObject = GameObject.Find($"MainMenuCanvas/Main menu/{versionName}");
            versionObject.GetComponent<TextMeshProUGUI>().text = BlizzardData.VERSION_TEXT;
            UnityEngine.Object.Destroy(versionObject.GetComponent<VersionWidget>());
            versionObject.GetComponent<RectTransform>().sizeDelta += new Vector2(400, 0);
        }

        private void SetIconImage()
        {
            GameObject iconImage = GameObject.Find("MainMenuCanvas/Main menu/Left/Logo/Icon");
            Image iconImageComponent = iconImage.GetComponent<Image>();

            string assetBundle = BlizzardData.IS_BETA ? BundleHelper.AssetBetaBundlePath + "blizzard.beta" : BundleHelper.AssetBundlePath + "blizzard_content";
            string imageAssetName = BlizzardData.IS_BETA ? "blizzard_beta_logo" : "blizzard_logo"; 

            iconImageComponent.sprite = BundleHelper.LoadIfNotLoaded(assetBundle).LoadAsset<Sprite>(imageAssetName);
        }

        private void SetBackgroundImage(GameObject mainMenuCanvas)
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
