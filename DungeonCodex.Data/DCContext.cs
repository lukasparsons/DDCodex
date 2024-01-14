using DungeonCodex.Common.Enums;
using DungeonCodex.Data.Extensions;
using DungeonCodex.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DungeonCodex.Data
{
    public class DCContext : IdentityDbContext<ApplicationUser>
    {
        public DCContext(DbContextOptions<DCContext> options) : base(options)
        {
        }

        public DbSet<BlackoutDate> BlackoutDates { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlackoutDate>()
                .HasOne(e => e.User)
                .WithMany(e => e.BlackoutDates)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<BlackoutDate>()
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (BlackoutDateType)Enum.Parse(typeof(BlackoutDateType), v));
                

            modelBuilder.Entity<Campaign>()
                .HasOne(c => c.DungeonMasterUser)
                .WithMany(u => u.DMCampaigns)
                .HasForeignKey(c => c.DungeonMasterUserId)
                .IsRequired(false);

            modelBuilder.Entity<Campaign>()
                .HasMany(c => c.Sessions)
                .WithOne(s => s.Campaign)
                .HasForeignKey(s => s.CampaignId);

            modelBuilder.Entity<Campaign>()
                .Property(e => e.PreferredCadence)
                .HasConversion(
                    v => v.ToString(),
                    v => (CadenceType)Enum.Parse(typeof(CadenceType), v));

            modelBuilder.Entity<Session>()
                .Property(e => e.SessionDate)
                .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),
                    v => DateOnly.FromDateTime(v));

            modelBuilder.Entity<Session>()
                .Property(e => e.SessionTime)
                .HasConversion(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v));

        }

        public DbSet<T> ResolveDbSet<T>()
            where T : class, new()
        {
            Type type = typeof(T);
            var fooInstance = new T() ?? throw new InvalidOperationException("Cannot resolve DbSet for null type");
            return fooInstance switch
            {
                BlackoutDate => CastDbSet<T, BlackoutDate>(BlackoutDates),
                Campaign => CastDbSet<T, Campaign>(Campaigns),
                Character => CastDbSet<T, Character>(Characters),
                Session => CastDbSet<T, Session>(Sessions),
                _ => throw new InvalidOperationException($"Cannot resolve DbSet for type {type.Name}")
            };
        }

        private static DbSet<T> CastDbSet<T,T2>(DbSet<T2> dbSet)
            where T: class
            where T2: class
                => dbSet as DbSet<T>
            ?? throw new InvalidOperationException($"Cannot cast DbSet<{typeof(T2).Name}> to DbSet<{typeof(T).Name}>");

        
    }
}
