using DungeonCodex.Common.Enums;
using DungeonCodex.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DungeonCodex.Data
{
    public class DDSContext : IdentityDbContext<ApplicationUser>
    {
        public DDSContext(DbContextOptions<DDSContext> options) : base(options)
        {
        }

        public DbSet<BlackoutDate> BlackoutDates { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignParticipant> CampaignParticipants { get; set; }
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

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Campaigns)
                .WithOne(cp => cp.User)
                .HasForeignKey(cp => cp.UserId);

            modelBuilder.Entity<Campaign>()
                .HasMany(c => c.CampaignParticipants)
                .WithOne(cp => cp.Campaign)
                .HasForeignKey(cp => cp.CampaignId);

            modelBuilder.Entity<Campaign>()
                .HasOne(c => c.DungeonMasterUser)
                .WithMany()
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
    }
}
