using DungeonCodex.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace DungeonCodex.Web.Lib.Dependencies
{
    public class PageDependenciesBuilder
    {
        UserManager<ApplicationUser> userManager;

        public PageDependenciesBuilder WithUserManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            return this;
        }

        public PageDependencies Build()
        {
            return new PageDependencies()
            {
                UserManager = userManager
            };
        }
    }
}
