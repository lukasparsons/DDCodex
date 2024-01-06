using DDScheduler.Data.Integration;
using DDScheduler.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DDScheduler.Data.Repositories
{
    public class UserDataRepository
    {
        public static ApplicationUser UpsertUser(DDSContext context, DiscordUserObject identity)
        {
            if (identity is null) throw new ArgumentNullException(nameof(identity));
            var user = context.Users.FirstOrDefault(u => u.DiscordUserId == identity.DiscordUserId);
            if (user is null)
            {
                user = new ApplicationUser()
                {
                    DiscordUserId = identity.DiscordUserId,
                    Email = identity.Email,
                    UserName = identity.GlobalName,
                    Nickname = identity.GlobalName,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            return user;
        }
    }
}
