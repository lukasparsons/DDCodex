using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCodex.Data.Model
{
    public class BlackoutDate
    {
        public int Id { get; set; }
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
