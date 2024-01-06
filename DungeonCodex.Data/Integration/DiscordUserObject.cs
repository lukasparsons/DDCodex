using System.Text.Json;

namespace DungeonCodex.Data.Integration
{
    public class DiscordUserObject
    {
        public string DiscordUserId { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Discriminator { get; set; }
        public int PublicFlags { get; set; }
        public int PremiumType { get; set; }
        public int Flags { get; set; }
        public object Banner { get; set; }
        public string AccentColor { get; set; }
        public string GlobalName { get; set; }
        public string BannerColor { get; set; }
        public bool MFAEnabled { get; set; }
        public string Locale { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }

        public static DiscordUserObject ParseFromEndpointResponse(JsonElement response)
        {
            return new DiscordUserObject()
            {
                DiscordUserId = response.GetProperty("id").GetString()!,
                Username = response.GetProperty("username").GetString()!,
                Avatar = response.GetProperty("avatar").GetString()!,
                Discriminator = response.GetProperty("discriminator").GetString()!,
                PublicFlags = response.GetProperty("public_flags").GetInt32(),
                PremiumType = response.GetProperty("premium_type").GetInt32(),
                Flags = response.GetProperty("flags").GetInt32(),
                Banner = response.GetProperty("banner").GetString()!,
                AccentColor = response.GetProperty("accent_color").GetString()!,
                GlobalName = response.GetProperty("global_name").GetString()!,
                BannerColor = response.GetProperty("banner_color").GetString()!,
                MFAEnabled = response.GetProperty("mfa_enabled").GetBoolean(),
                Locale = response.GetProperty("locale").GetString()!,
                Email = response.GetProperty("email").GetString()!,
                Verified = response.GetProperty("verified").GetBoolean(),
            };
        }
    }
}
