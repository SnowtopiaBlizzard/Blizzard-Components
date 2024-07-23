#pragma warning disable CS0162
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace Blizzard.Helpers
{/*
    public static class BlizzardLogger
    {
        private static List<object> logger = new List<object>();

        private static GameObject loggingPanel = BlizzardData.BETA_ASSET_BUNDLE.LoadAsset<GameObject>("BlizzardLoggerLoggingPanel");
        private static GameObject canvasObject = UnityEngine.Object.Instantiate(BlizzardData.BETA_ASSET_BUNDLE.LoadAsset<GameObject>("BlankCanvas"));

        static BlizzardLogger()
        {
            Application.quitting += OnApplicationQuit;
            UnityEngine.Object.DontDestroyOnLoad(canvasObject);
        }

        public static void Log(object message, bool popup=false)
        {
            if (!BlizzardData.DEBUG_MODE) return;

            logger.Add(message);
            
            if (popup)
            {
                CreatePopup(message);
            }
        }

        public static void CreatePopup(object message)
        {
            UnityEngine.Object.Instantiate(loggingPanel, canvasObject.transform).transform.Find("LoggingMessage").GetComponent<TextMeshProUGUI>().text = message.ToString();
        }

        private static void OnApplicationQuit()
        {
            SaveLogToFile();
        }

        private static string GetCurrentTimeAsLoggingName()
        {
            DateTime now = DateTime.Now;

            string formattedDate = string.Format("{0}-{1}-{2}-{3}-{4}-{5}",
                                                 now.Year,
                                                 now.Month,
                                                 now.Day,
                                                 now.Hour,
                                                 now.Minute,
                                                 now.Second);

            return formattedDate;
        }

        private static void SaveLogToFile()
        {
            string filePath = Path.Combine(Path.Combine(Application.persistentDataPath, "../Blizzard/BetaLogs"), GetCurrentTimeAsLoggingName()+".beta.blizzard-log");
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var logEntry in logger)
                    {
                        writer.WriteLine(logEntry);
                    }
                }
            }
            catch (IOException ex)
            {
                Debug.LogError($"Failed to save log to file: {ex.Message}");
            }
        }
    }*/
}
#pragma warning restore CS0162