using DungeonCodex.Common.Enums;

namespace DungeonCodex.Data.Model
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DungeonMasterUserId { get; set; }
        public ApplicationUser DungeonMasterUser { get; set; }
        public ICollection<CampaignParticipant> CampaignParticipants { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public CadenceType PreferredCadence { get; set; }
    }
}
