using DungeonCodex.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DungeonCodex.Data.Extensions
{
    public static class DbSetExtensions
    {
        // TODO: This logic should be handled within the repository later
        public static IEnumerable<T> WithDefaultIncludes<T>(this DbSet<T> dbSet)
            where T : class, new()
        {
            var defaultImpl = new T();

            IEnumerable<T>? resolved = defaultImpl switch
            {
                ApplicationUser => DefaultIncludes<T>((dbSet as DbSet<ApplicationUser>)!),
                BlackoutDate => DefaultIncludes<T>((dbSet as DbSet<BlackoutDate>)!),
                Campaign => DefaultIncludes<T>((dbSet as DbSet<Campaign>)!),
                Character => DefaultIncludes<T>((dbSet as DbSet<Character>)!),
                Session => DefaultIncludes<T>((dbSet as DbSet<Session>)!),
                _ => dbSet
            };

            return resolved!;
        }

        private static IEnumerable<T>? DefaultIncludes<T>(DbSet<ApplicationUser> dbSet)
            where T : class => dbSet
            .Include(x => x.Characters)
            .Include(x => x.BlackoutDates) as IEnumerable<T>;

        private static IEnumerable<T>? DefaultIncludes<T>(DbSet<Campaign> dbSet) => dbSet
            .Include(x => x.Sessions)
            .Include(x => x.DungeonMasterUser)
            .Include(x => x.Characters) as IEnumerable<T>;

        private static IEnumerable<T>? DefaultIncludes<T>(DbSet<BlackoutDate> dbSet) => dbSet
            .Include(x => x.User) as IEnumerable<T>;

        private static IEnumerable<T>? DefaultIncludes<T>(DbSet<Character> dbSet) => dbSet
            .Include(x => x.Campaign)
            .Include(x => x.User) as IEnumerable<T>;

        private static IEnumerable<T>? DefaultIncludes<T>(DbSet<Session> dbSet) => dbSet
            .Include(x => x.Campaign) as IEnumerable<T>;
    }
}
