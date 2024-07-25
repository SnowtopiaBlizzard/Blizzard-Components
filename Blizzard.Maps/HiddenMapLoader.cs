using Blizzard.Helpers;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Blizzard.Maps
{
    public static class HiddenMapLoader
    {
        public static void Init()
        {
            Core.Levels.Add(createLevelData(
                "MAP_SkullMountains_01",
                "Skull Mountains",
                "Skull_Mountain_1",
                2,
                3.5f,
                2f,
                1.5f,
                2.4f,
                0.5f,
                2.7f
            ));
            Core.Levels.Add(createLevelData(
                "MAP_ThePeak_01",
                "The Peak",
                "The_Peak_1",
                3,
                3f,
                1.1f,
                1.3f,
                1.9f,
                0.9f,
                2.4f
            ));
            Core.Levels.Add(createLevelData(
                "MAP_TheWall_01",
                "The Wall",
                "The_Wall_1",
                2,
                3f,
                1.1f,
                1.2f,
                2.2f,
                0.3f,
                1.1f
            ));
        }

        private static LevelData createLevelData(string sceneName, string publicName, string thumnailName, int snowfrontCount, float difficulty,
            float ParameterAccessibility, float ParameterDiversity, float ParameterHomogeneity, float ParameterBuildable, float ParameterObstacles)
        {
            return new LevelData
            {
                Options = ELevelOption.Release,
                sceneName = sceneName,
                publicName = publicName,
                thumbnail = BlizzardData.CONTENT_ASSET_BUNDLE.LoadAsset<Sprite>(thumnailName),
                SnowfrontCount = snowfrontCount,
                Difficulty = difficulty,
                ParameterAccessibility = ParameterAccessibility,
                ParameterDiversity = ParameterDiversity,
                ParameterHomogeneity = ParameterHomogeneity,
                ParameterBuildable = ParameterBuildable,
                ParameterObstacles = ParameterObstacles
            };
        }
    }
}
