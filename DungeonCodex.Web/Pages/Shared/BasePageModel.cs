using DungeonCodex.Data.Model;
using DungeonCodex.Web.Lib.Dependencies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DungeonCodex.Web.Pages.Shared
{
    public class BasePageModel : PageModel
    {
        public UserManager<ApplicationUser> UserManager;

        public ApplicationUser? CurrentUser => GetCurrentUser();

        public BasePageModel(IBasePageDependencies dependencies)
        {
            UserManager = dependencies.UserManager;
        }

        private ApplicationUser GetCurrentUser()
        {
            var user = HttpContext.User;
            return UserManager.GetUserAsync(user).Result!;
        }
    }
}
