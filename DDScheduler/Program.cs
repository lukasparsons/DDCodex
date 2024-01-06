using DDScheduler.Common.Configuration;
using DDScheduler.Data;
using DDScheduler.Init;
using Microsoft.EntityFrameworkCore;

namespace DDScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.Configure<DiscordConfiguration>(builder.Configuration.GetSection("DiscordConfiguration"));

            ServiceInitialization.InitializeServices(builder.Services, builder.Configuration);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            var dbFilePath = connectionString.Split('=')[1];

            var app = builder.Build();

            if (!File.Exists(dbFilePath))
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DDSContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapDefaultControllerRoute();

            app.UseHttpLogging();

            app.Run();
        }
    }
}