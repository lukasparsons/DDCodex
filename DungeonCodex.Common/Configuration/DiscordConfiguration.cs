namespace DungeonCodex.Common.Configuration
{
    public class DiscordConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string CallbackPartialPath { get; set; }
        public string AuthorizationEndpoint { get; set; }
        public string TokenEndpoint { get; set; }
        public string UserInformationEndpoint { get; set; }
    }
}
