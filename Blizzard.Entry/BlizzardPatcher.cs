using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blizzard.Standalone
{
    public static class BlizzardPatcher
    {
        public static void PatchGame()
        {
            // Path to the JSON file
            string jsonUpdateFile = Path.Combine(BlizzardStandaloneData.zipExtractionPath, "installation/update.json");

            // Read the JSON file content
            string jsonContent = File.ReadAllText(jsonUpdateFile);

            // Parse the JSON content into a JObject
            JObject jsonData = JObject.Parse(jsonContent);

            // Access the installation object
            JObject installation = (JObject)jsonData["installation"];

            // Access and process the folders array
            JArray folders = (JArray)installation["folders"];
            foreach (var folder in folders)
            {
                string folderPath = folder.ToString();  // Use the folder path directly without modification

                // Ensure the destination directory exists
                string fullFolderPath = Path.Combine(Path.Combine(Application.dataPath, "../"), folderPath);
                Directory.CreateDirectory(fullFolderPath);
            }

            // Copy the files from the update
            JObject files = (JObject)installation["files"];
            foreach (var file in files)
            { 
                string filePath = file.Key;  // Source file path relative to the extraction path
                string folderPath = file.Value.ToString();  // Destination folder path

                // Construct full paths
                string sourceFilePath = Path.Combine(BlizzardStandaloneData.zipExtractionPath, "installation", filePath);
                string destinationFolderPath = Path.Combine(Path.Combine(Application.dataPath, "../"), folderPath);
                string destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(filePath));

                // Ensure the destination directory exists
                Directory.CreateDirectory(destinationFolderPath);

                // Copy the file
                File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
            }

            // Delete the extraction directory because no longer needed
            Directory.Delete(BlizzardStandaloneData.zipExtractionPath, true);
            BlizzardStandalone.Instance.RebuildGame();
        }
    }
}
