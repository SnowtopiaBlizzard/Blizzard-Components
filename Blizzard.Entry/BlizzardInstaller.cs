using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blizzard.Standalone
{
    public static class BlizzardInstaller
    {
        public static void Install()
        {
            ChangeLoadingScreenText("Downloading Latest Version.");
            InstallLatestVersion();
        }

        private static void ContinueInstallation()
        {
            ChangeLoadingScreenText("Extracting Files.");
            ExtractFile();
            ChangeLoadingScreenText("Patching Game.");
            BlizzardPatcher.PatchGame();
        }

        private static void ExtractFile()
        {
            Directory.CreateDirectory(BlizzardStandaloneData.zipExtractionPath);
            ZipFile.ExtractToDirectory(BlizzardStandaloneData.zipFilePath, BlizzardStandaloneData.zipExtractionPath);
            File.Delete(BlizzardStandaloneData.zipFilePath);
        }

        private static async void InstallLatestVersion()
        {
            try
            {
                await DownloadLatestVersion();
                ContinueInstallation();
            }
            catch (HttpRequestException httpEx)
            {
                ChangeLoadingScreenText($"HTTP Error: {httpEx.Message}");
            }
            catch (IOException ioEx)
            {
                ChangeLoadingScreenText($"File Error: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                ChangeLoadingScreenText($"Unexpected Error: {ex.Message}");
            }
        }

        private static async Task DownloadLatestVersion()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Add required headers, to prevent error 555
                    client.DefaultRequestHeaders.Add("Host", "bamsestudio.dk");
                    client.DefaultRequestHeaders.Add("User-Agent", "SnowtopiaModding/BlizzardInstaller");

                    using (HttpResponseMessage response = await client.GetAsync(BlizzardStandaloneData.downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                            using (FileStream fileStream = new FileStream(BlizzardStandaloneData.zipFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                await contentStream.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            throw new HttpRequestException($"Failed to download file. Status code: {response.StatusCode}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during download: {ex.Message}", ex);
            }
        }

        private static void ChangeLoadingScreenText(string text)
        {
            GameObject loadingTextObject = GameObject.Find("SceneLoader/LoadingCanvas/Loading/Bottom/Text");
            TextMeshProUGUI loadingTextComponent = loadingTextObject.GetComponent<TextMeshProUGUI>();
            loadingTextComponent.text = text;
        }
    }
}
