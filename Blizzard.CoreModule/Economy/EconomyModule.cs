using Blizzard.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Blizzard.Economy
{
    public class EconomyModule
    {
        private const string ECONOMY_ASSET_NAME = "Economy_Tooltip";

        /// <summary>
        /// Called when Game loaded
        /// </summary>
        public EconomyModule()
        {
            SceneManager.sceneLoaded += sceneLoaded;
        }

        private void sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "UI")
            {
                //BundleHelper.LoadIfNotLoaded(BundleHelper.AssetBundlePath + "economy").LoadAsset;

                Transform topMiddle = GameObject.Find("UI/NormalUpdateCanvas/Top middle").transform;

                Transform slopeObject = topMiddle.transform.Find("Slopes");
                GameObject moneyObject = Object.Instantiate(slopeObject.gameObject, topMiddle);

                moneyObject.transform.Find("UI Slot - T1 - Slopes/Widget/Text").GetComponent<TextMeshProUGUI>().text = "100$";
                moneyObject.transform.Find("UI Slot - T1 - Slopes/Widget/Icon/Filled").GetComponent<Image>().sprite = BundleHelper.LoadIfNotLoaded(BundleHelper.AssetBundlePath + "economy").LoadAsset<Sprite>(ECONOMY_ASSET_NAME);
            }
        }
    }
}
