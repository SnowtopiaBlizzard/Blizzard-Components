using Blizzard.User;
using Newtonsoft.Json;
using System;

namespace Blizzard.User
{
    internal class TempUser
    {
#pragma warning disable
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
#pragma warning restore
    }

    public static class DiscordUserExtensions
    {
        public static DiscordUser FromJson(this DiscordUser user, string json)
        {
            var tempUser = JsonConvert.DeserializeObject<TempUser>(json);
            return new DiscordUser
            {
                Name = tempUser.Username,
                Role = ConvertRole(tempUser.Role)
            };
        }

        private static DiscordRole ConvertRole(string role)
        {
            return role.ToLower() switch
            {
                "user" => DiscordRole.User,
                "beta-tester" => DiscordRole.Tester,
                "administrator" => DiscordRole.Admin,
                _ => throw new ArgumentOutOfRangeException(nameof(role), $"Unknown role: {role}")
            };
        }
    }
}
