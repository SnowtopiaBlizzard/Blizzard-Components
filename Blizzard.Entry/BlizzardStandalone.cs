using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Blizzard.Standalone
{
    public class BlizzardStandalone
    {
        public static BlizzardStandalone Instance;

        public BlizzardStandalone()
        {
            Instance = this;
            string coreModulePath = Path.Combine(BlizzardStandaloneData.basePath, "Assemblies/Blizzard.CoreModule.dll");
            if (File.Exists(coreModulePath))
            {
                Assembly.LoadFrom(coreModulePath).GetType("Blizzard.Blizzard").GetMethod("Init").Invoke(this, null);
                return;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void RebuildGame()
        {
            string coreModulePath = Path.Combine(BlizzardStandaloneData.basePath, "Assemblies/Blizzard.CoreModule.dll");
            Assembly.LoadFrom(coreModulePath).GetType("Blizzard.Blizzard").GetMethod("Init").Invoke(this, null);
            //SceneManager.LoadScene("Launcher"); 
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;  
            if (scene.name == "MainMenu")
            {
                CreateInstallButton();
            }
        }

        private void CreateInstallButton()
        {
            GameObject settingsButton = GameObject.Find("MainMenuCanvas/Main menu/Left/Menu/Button - Settings");
            GameObject blizzardButton = UnityEngine.Object.Instantiate(settingsButton, settingsButton.transform.parent);
            blizzardButton.name = "Button - Install Blizzard";
            blizzardButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Install Blizzard";
            blizzardButton.transform.SetSiblingIndex(3);

            Button button = blizzardButton.GetComponent<Button>();
            button.onClick.AddListener(() => {
                SceneManager.sceneLoaded += BeginInstall;
                SceneManager.LoadScene("LoadingScreen");
            });
        }

        private void BeginInstall(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= BeginInstall;
            BlizzardInstaller.Install();
        }
    }
}
