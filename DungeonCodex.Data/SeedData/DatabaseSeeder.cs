using Dawn;
using DungeonCodex.Common;
using DungeonCodex.Common.Configuration;
using DungeonCodex.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DungeonCodex.Data.SeedData
{
    public class DatabaseSeeder : IDisposable
    {
        private readonly DCContext _context;
        private readonly DiscordConfiguration _configuration;
        public DatabaseSeeder(DCContext context, DiscordConfiguration configuration)
        {
            _context = Guard.Argument(context).NotNull().Value;
            _configuration = Guard.Argument(configuration).NotNull().Value;
        }

        public async Task Seed()
        {
            _context.Database.Migrate();


            foreach (var campaign in SeedDefinitions.Campaigns)
            {
                if (!_context.Campaigns.Any(c => c.Code == campaign.Code))
                {
                    await _context.Campaigns.AddAsync(campaign);
                }
            }

            await _context.SaveChangesAsync();

            var skt = await _context.Campaigns.FirstOrDefaultAsync(c => c.Code == "SKT");
            var lukeydukey = await _context.Users.FirstOrDefaultAsync(u => u.DiscordUserId == _configuration.AdminDiscordUserId);
            if (skt != null)
            {

                if (skt.DungeonMasterUserId is null && lukeydukey != null)
                {
                    skt.DungeonMasterUserId = lukeydukey.Id;
                    _context.Campaigns.Update(skt);
                }
            }

            var iok = await _context.Campaigns.Include(c => c.Characters).FirstOrDefaultAsync(c => c.Code == "IoK");
            if (iok != null)
            {
                if (lukeydukey != null && !iok.Characters.Any(cp => cp.UserId == lukeydukey.Id))
                {
                    _context.Characters.Add(new Character
                    {
                        Id = Utils.NewId(),
                        UserId = lukeydukey.Id,
                        CampaignId = iok.Id,
                        Class = "Warlock",
                        Name = "Nyx",
                        Level = 4,
                        Description = "Got some real personality issues.",
                        DDBLink = "https://ddb.ac/characters/1234567890"
                    });
                }
            }   


            await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();
    }
}
