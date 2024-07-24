
using System.IO;
using UnityEngine;

namespace Blizzard.Standalone
{
    public static class BlizzardStandaloneData
    {
        public static readonly string downloadUrl = "https://bamsestudio.dk/api/snowtopia/install/latest";
        public static readonly string basePath = Path.Combine(Application.dataPath, "../Blizzard");
        public static readonly string zipFilePath = Path.Combine(basePath, "latest.zip");
        public static readonly string zipExtractionPath = Path.Combine(basePath, "temp");
    }
}
