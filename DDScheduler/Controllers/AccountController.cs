using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace DDScheduler.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet("~/signin", Name = "Login")]
        public IActionResult DiscordLogin(string? returnUrl = null)
        {
            returnUrl ??= "/";
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Discord");
        }

        [HttpGet("~/signout")]
        [HttpPost("~/signout")]
        public async Task<IActionResult> SignOutCurrentUser()
        {
            await HttpContext.SignOutAsync("Identity.Application");
            return SignOut(new AuthenticationProperties { RedirectUri = "/" });
        }
    }
}
