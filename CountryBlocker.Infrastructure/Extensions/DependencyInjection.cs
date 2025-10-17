using CountryBlocker.Application.Interfaces;
using CountryBlocker.Application.Interfaces.IRepository;
using CountryBlocker.Infrastructure.ExternalServices;
using CountryBlocker.Infrastructure.Jobs;
using CountryBlocker.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CountryBlocker.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            #region Repositories

            services.AddSingleton<IBlockedCountryRepository, InMemoryBlockedCountryRepository>();
            services.AddSingleton<IBlockedAttemptLogRepository, InMemoryLogRepository>();

            #endregion Repositories

            #region Http Clients

            services.AddHttpClient("IpApi-Client", client =>
            {
                client.BaseAddress = new Uri("https://ipapi.co/");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GeoChecker/1.0)");
            });

            #endregion Http Clients

            #region Third Party Services

            services.AddScoped<IGeoLocationProvider, GeoLocationService>();

            #endregion Third Party Services

            #region Background Jobs

            services.AddHostedService<TemporalBlockRemoverService>();

            #endregion Background Jobs

            return services;
        }
    }
}