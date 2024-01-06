using Microsoft.AspNetCore.Identity;

namespace DungeonCodex.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string DiscordUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public ICollection<BlackoutDate> BlackoutDates { get; set; }
        public ICollection<CampaignParticipant> Campaigns { get; set; }
    }
}
