using DungeonCodex.Data.Model.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DungeonCodex.Data.Model
{
    public class Session : IPersisted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string CampaignId { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public Campaign Campaign { get; set; }
        [Column(TypeName = "date")]
        public DateOnly SessionDate { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly SessionTime { get; set; }
    }
}
