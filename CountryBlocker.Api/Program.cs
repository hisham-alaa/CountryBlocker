using CountryBlocker.Application.Interfaces.IService;
using CountryBlocker.Application.Services;
using CountryBlocker.Infrastructure.Extensions;

namespace CountryBlocker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            #region Application Services

            builder.Services.AddScoped<IBlockedCountryService, BlockedCountryService>();
            builder.Services.AddScoped<IIPCheckService, IPCheckService>();
            builder.Services.AddScoped<IBlockedAttemptLogService, BlockedAttemptLogService>();

            #endregion Application Services

            #region Swagger configuration for api documentation

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #endregion Swagger configuration for api documentation

            #region Infrastructure Services

            builder.Services.AddInfrastructure();
            builder.Services.AddHttpContextAccessor();
            #endregion Infrastructure Services

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}