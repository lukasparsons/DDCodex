using DungeonCodex.Data;
using DungeonCodex.Data.Model;
using DungeonCodex.Data.Repositories;

namespace DungeonCodex.Web.Init
{
    public static class DataRepositoryInitializer
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            DCContext getContext(IServiceProvider sp) => sp.GetRequiredService<DCContext>();

            // Simle Data Repositories. Can be replaced with more specific ones if necessary.
            services.AddScoped<ISimpleDataRepository<Campaign>>(sp => 
                new SimpleDataRepository<Campaign>(getContext(sp)));
            services.AddScoped<ISimpleDataRepository<Session>>(sp =>
                new SimpleDataRepository<Session>(getContext(sp)));
            services.AddScoped<ISimpleDataRepository<BlackoutDate>>(sp =>
                new SimpleDataRepository<BlackoutDate>(getContext(sp)));
            services.AddScoped<ISimpleDataRepository<Character>>(sp =>
               new SimpleDataRepository<Character>(getContext(sp)));
        }
    }
}
