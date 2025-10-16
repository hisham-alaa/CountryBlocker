using CountryBlocker.Application.Interfaces;
using CountryBlocker.Application.Interfaces.IRepository;
using CountryBlocker.Application.Services;
using CountryBlocker.Infrastructure.ExternalServices;
using CountryBlocker.Infrastructure.Repositories;

namespace CountryBlocker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Register Application Services
            builder.Services.AddScoped<BlockedCountryService>();
            builder.Services.AddScoped<IPCheckService>();
            builder.Services.AddScoped<BlockedAttemptLogService>();

            // Register Repository Implementations (In-Memory)
            builder.Services.AddSingleton<IBlockedCountryRepository, InMemoryBlockedCountryRepository>();
            builder.Services.AddSingleton<IBlockedAttemptLogRepository, InMemoryLogRepository>();
            builder.Services.AddSingleton<ITemporalBlockedCountryRepository, InMemoryTemporalBlockRepository>();
            builder.Services.AddScoped<IGeoLocationProvider, GeoLocationService>();


            //Swagger configuration for api documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}