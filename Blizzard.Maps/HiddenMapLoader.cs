using System.Linq;

namespace Blizzard.Maps
{
    public static class HiddenMapLoader
    {
        public static void Init()
        {
            foreach (var level in Core.Levels)
            {
                level.Options = ELevelOption.Release;
            }
            Core.Levels.Add(createLevelData(
                "MAP_SkullMountains_01",
                "Skull Mountains",
                2,
                2f,
                2f,
                2f,
                2f,
                2f,
                1f
            ));
            Core.Levels.Add(createLevelData(
                "MAP_ThePeak_01",
                "The Peak",
                2,
                2f,
                2f,
                2f,
                2f,
                2f,
                1f
            ));
            Core.Levels.Add(createLevelData(
                "MAP_TheWall_01",
                "The Wall",
                2,
                2f,
                2f,
                2f,
                2f,
                2f,
                1f
            ));
        }

        private static LevelData createLevelData(string sceneName, string publicName, int snowfrontCount, float difficulty,
            float ParameterAccessibility, float ParameterDiversity, float ParameterHomogeneity, float ParameterBuildable, float ParameterObstacles)
        {
            return new LevelData
            {
                Options = ELevelOption.Release,
                sceneName = sceneName,
                publicName = publicName,
                thumbnail = Core.Levels.First().thumbnail,
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
