using DungeonCodex.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DungeonCodex.Data.Model.Interface;

namespace DungeonCodex.Data.Model
{
    public class Campaign : IPersisted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string CampaignName { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string? DungeonMasterUserId { get; set; }
        public ApplicationUser DungeonMasterUser { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<Character> Characters { get; set; } = new List<Character>();
        public CadenceType PreferredCadence { get; set; }
    }
}
