using DungeonCodex.Data.Model.Interface;
using Microsoft.AspNetCore.Identity;

namespace DungeonCodex.Data.Model
{
    public class ApplicationUser : IdentityUser, IPersisted
    {
        public string DiscordUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public ICollection<BlackoutDate> BlackoutDates { get; set; } = new List<BlackoutDate>();
        public ICollection<Character> Characters { get; set; } = new List<Character>();
        public ICollection<Campaign> DMCampaigns { get; set; } = new List<Campaign>();

        // TODO: Don't make this hardcoded.
        public bool IsAdmin => DiscordUserId.Equals("154036642372517888");
    }
}
