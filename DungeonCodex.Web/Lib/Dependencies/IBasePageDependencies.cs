using DungeonCodex.Data.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DungeonCodex.Web.Lib.Dependencies
{
    public interface IBasePageDependencies
    {
        UserManager<ApplicationUser> UserManager { get; }
    }
}
