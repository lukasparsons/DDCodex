using DungeonCodex.Web.Lib.Dependencies;
using DungeonCodex.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DungeonCodex.Web.Pages.Admin
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IBasePageDependencies dependencies) : base(dependencies)
        {
        }

        public void OnGet()
        {
        }
    }
}
