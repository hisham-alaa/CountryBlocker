using CountryBlocker.Application.Services;
using CountryBlocker.Domain.Interfaces;

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
            builder.Services.AddScoped<LogService>();

            // Register Repository Implementations (In-Memory)
            builder.Services.AddSingleton<IBlockedCountryRepository, InMemoryCountryBlockRepository>();
            builder.Services.AddSingleton<ILogRepository, InMemoryLogRepository>();
            builder.Services.AddSingleton<ITemporalBlockedCountryRepository, InMemoryTemporalBlockRepository>();

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