using Dawn;
using DungeonCodex.Data.Model;
using DungeonCodex.Data.Repositories;
using DungeonCodex.Web.Lib.Dependencies;
using DungeonCodex.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DungeonCodex.Web.Pages.Campaigns
{
    public class DetailsModel : BasePageModel
    {
        public Campaign Campaign { get; set; }
        public IEnumerable<Character> Characters { get; set; } = new List<Character>();

        private readonly ISimpleDataRepository<Campaign> _campaignRepository;
        private readonly ISimpleDataRepository<Character> _characterRepository;

        public DetailsModel(
            ISimpleDataRepository<Campaign> campaignRepository,
            ISimpleDataRepository<Character> characterRepository,
            IBasePageDependencies dependencies) : base(dependencies)
        {
            _campaignRepository = Guard.Argument(campaignRepository).NotNull().Value;
            _characterRepository = Guard.Argument(characterRepository).NotNull().Value;
        }

        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToPage("/Campaigns/Index");
            }

            Campaign = _campaignRepository.GetAsync(id).Result!;
            Characters = _characterRepository.GetWhereAsync(c => c.CampaignId == id).Result;
            return Page();
        }
    }
}
