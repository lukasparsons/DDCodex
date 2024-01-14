using Dawn;
using DungeonCodex.Data.Model;
using DungeonCodex.Data.Repositories;
using DungeonCodex.Web.Extensions;
using DungeonCodex.Web.Lib.Dependencies;
using DungeonCodex.Web.Pages.Shared;

namespace DungeonCodex.Web.Pages.Campaigns
{
    public class IndexModel : BasePageModel
    {
        public IEnumerable<Campaign> Campaigns { get; set; } = new List<Campaign>();
        public IEnumerable<Campaign> CurrentUserDmCampaigns { get; set; } = new List<Campaign>();
        public IEnumerable<Campaign> ParticipatingCampaigns { get; set; } = new List<Campaign>();
        public IEnumerable<Character> PlayerCharacters { get; set; } = new List<Character>();

        private readonly ISimpleDataRepository<Campaign> _campaignRepository;
        private readonly ISimpleDataRepository<Character> _characterRepository;

        public IndexModel(ISimpleDataRepository<Campaign> campaignRepository, ISimpleDataRepository<Character> characterRepository, IBasePageDependencies dependencies)
            : base(dependencies)
        {
            _campaignRepository = Guard.Argument(campaignRepository).NotNull().Value;
            _characterRepository = Guard.Argument(characterRepository).NotNull().Value;
        }

        public async Task OnGetAsync()
        {
            Campaigns = await _campaignRepository.GetAllAsync();
            SetUserCampaigns();
        }

        private void SetUserCampaigns()
        {
            SetCampaignsRanByCurrentUser();
            SetCampaignsPlayedByCurrentUser();
            SetPlayerCharacters();
        }

        private void SetCampaignsRanByCurrentUser()
        {
            CurrentUserDmCampaigns = Campaigns.Where(c => c.DungeonMasterUserId == CurrentUser.GetIdOrEmpty());
        }

        private void SetCampaignsPlayedByCurrentUser()
        {
            ParticipatingCampaigns = Campaigns.Where(c => c.Characters.Any(cp => cp.UserId == CurrentUser.GetIdOrEmpty()));
        }

        private void SetPlayerCharacters()
        {
            List<string> campaignIds = Campaigns.Select(_ => _.Id).ToList();
            PlayerCharacters = _characterRepository.GetWhereAsync(c => 
            c.UserId == CurrentUser.GetIdOrEmpty() && 
            campaignIds.Contains(c.CampaignId)).Result;
        }
    }
}
