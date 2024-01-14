using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCodex.Common
{
    public static class PrimitiveExtensions
    {
        public static string NewIdIfNull(this string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                id = Utils.NewId();

            return id;
        }

        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
