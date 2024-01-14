using DungeonCodex.Data.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DungeonCodex.Web.Lib.Dependencies
{
    public class PageDependencies : IBasePageDependencies
    {
        public UserManager<ApplicationUser> UserManager { get; init; }
    }
}
