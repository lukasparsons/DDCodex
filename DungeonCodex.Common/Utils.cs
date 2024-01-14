using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCodex.Common
{
    public static class Utils
    {
        public static string NewId() => Guid.NewGuid().ToString();
    }
}
