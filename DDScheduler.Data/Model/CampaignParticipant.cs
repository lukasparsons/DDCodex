using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDScheduler.Data.Model
{
    public class CampaignParticipant
    {
        public int CampaignParticipantId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}
