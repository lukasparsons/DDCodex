using DungeonCodex.Common;
using DungeonCodex.Data.Model;

namespace DungeonCodex.Data.SeedData
{
    public static class SeedDefinitions
    {
        public static List<Campaign> Campaigns = new()
        {
            new Campaign()
            {
                Id = Utils.NewId(),
                CampaignName = "Storm King's Thunder",
                Code = "SKT",
                PreferredCadence = Common.Enums.CadenceType.Weekly,
            },
            new Campaign()
            {
                Id = Utils.NewId(),
                CampaignName = "Isle of Kamesh",
                Code = "IoK",
                PreferredCadence = Common.Enums.CadenceType.Weekly,
            },
            new Campaign()
            {
                Id = Utils.NewId(),
                CampaignName = "Breaking of the Brow",
                Code = "BotB",
                PreferredCadence = Common.Enums.CadenceType.Monthly,
            }
        };
    }
}
