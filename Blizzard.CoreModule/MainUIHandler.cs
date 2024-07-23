﻿using Blizzard.Events;
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
            LoadVersionText("Version");
            LoadVersionText("Version (1)");

            LoadBackgroundImage(GameObject.Find("MainMenuCanvas/Main menu - Background/Background"));
            LoadIconImage();
        }

        private void LoadVersionText(string versionName)
        {
            GameObject versionObject = GameObject.Find($"MainMenuCanvas/Main menu/{versionName}");
            versionObject.GetComponent<TextMeshProUGUI>().text = BlizzardData.VERSION_TEXT;
            UnityEngine.Object.Destroy(versionObject.GetComponent<VersionWidget>());
            versionObject.GetComponent<RectTransform>().sizeDelta += new Vector2(400, 0);
        }

        private void LoadIconImage()
        {
            GameObject iconImage = GameObject.Find("MainMenuCanvas/Main menu/Left/Logo/Icon");
            Image iconImageComponent = iconImage.GetComponent<Image>();

            string assetBundle = BlizzardData.IS_BETA ? BundleHelper.AssetBetaBundlePath + "blizzard.beta" : BundleHelper.AssetBundlePath + "blizzard_content";
            string imageAssetName = BlizzardData.IS_BETA ? "blizzard_beta_logo" : "blizzard_logo"; 

            iconImageComponent.sprite = BundleHelper.LoadIfNotLoaded(assetBundle).LoadAsset<Sprite>(imageAssetName);
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
