using AspNet.Security.OAuth.Discord;
using DDScheduler.Common.Configuration;
using DDScheduler.Data;
using DDScheduler.Data.Integration;
using DDScheduler.Data.Model;
using DDScheduler.Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace DDScheduler.Init
{
    public static class ServiceInitialization
    {
        public static void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Logs/ddschedule.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddDiscord(options =>
                {
                    var discordConfig = configuration.GetSection("DiscordConfiguration").Get<DiscordConfiguration>()!;
                    options.ClientId = discordConfig.ClientId;
                    options.ClientSecret = discordConfig.ClientSecret;
                    options.CallbackPath = DiscordAuthenticationDefaults.CallbackPath;
                    options.SaveTokens = true;
                    List<string> scopes = new()
                    {
                        "email",
                        "identify",
                        "guilds",
                        "guilds.members.read",
                    };

                    foreach (var scope in scopes)
                    {
                        options.Scope.Add(scope);
                    }

                    options.ClaimActions.MapCustomJson("urn:discord:avatar:url", user =>
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
                            user.GetString("id"),
                            user.GetString("avatar"),
                            user.GetString("avatar").StartsWith("a_") ? "gif" : "png"));

                    options.Events.OnCreatingTicket = async context =>
                    {
                        var discordIdentity = await SendRequestForIdentityData(context.AccessToken);
                        var dbContext = context.HttpContext.RequestServices.GetRequiredService<DDSContext>();
                        var user = UserDataRepository.UpsertUser(dbContext, discordIdentity);

                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();
                        var identity = await signInManager.CreateUserPrincipalAsync(user);
                        context.Principal = identity;

                        await signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme, context.Principal, new AuthenticationProperties());
                    };
                });

            services.AddDbContext<DDSContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ServiceInitialization).Assembly.ToString()));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DDSContext>()
                .AddDefaultTokenProviders();
        }

        private static async Task<DiscordUserObject> SendRequestForIdentityData(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://discordapp.com/api/users/@me");
            response.EnsureSuccessStatusCode();
            JsonElement responseJson = await response.Content.ReadFromJsonAsync<JsonElement>();
            return DiscordUserObject.ParseFromEndpointResponse(responseJson);
        }
    }
}
