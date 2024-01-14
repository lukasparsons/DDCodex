using DungeonCodex.Data.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCodex.Data.Model
{
    public class BlackoutDate : IPersisted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public DateOnly Date { get; set; }
        public BlackoutDateType Type { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

    public enum BlackoutDateType
    {
        Unavailable,
        CannotRunGame,
        TentativelyUnavailable
    }
}
