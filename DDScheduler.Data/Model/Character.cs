using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDScheduler.Data.Model
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
        public string DDBLink { get; set; }
        public int Level { get; set; }
        public int CampaignParticipantId { get; set; }
        public CampaignParticipant CampaignParticipant { get; set; }

    }
}
