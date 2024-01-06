using System.ComponentModel.DataAnnotations.Schema;

namespace DungeonCodex.Data.Model
{
    public class Session
    {
        public int SessionId { get; set; }
        public int CampaignId { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public Campaign Campaign { get; set; }
        [Column(TypeName = "date")]
        public DateOnly SessionDate { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly SessionTime { get; set; }
    }
}
