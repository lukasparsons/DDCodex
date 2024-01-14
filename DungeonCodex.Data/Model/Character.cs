using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCodex.Data.Model.Interface;

namespace DungeonCodex.Data.Model
{
    public class Character : IPersisted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
        public string DDBLink { get; set; }
        public int Level { get; set; }

    }
}
