using DungeonCodex.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCodex.Web.Extensions
{
    public static class ApplicationUserExtensions
    {
        public static string GetIdOrEmpty(this ApplicationUser? user)
        {
            if (user is null)
                return string.Empty;
            return user.Id;
        }
    }
}
