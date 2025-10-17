using CountryBlocker.Application.Interfaces.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CountryBlocker.Infrastructure.Jobs
{
    public class TemporalBlockRemoverService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TemporalBlockRemoverService> _logger;

        public TemporalBlockRemoverService(IServiceProvider serviceProvider, ILogger<TemporalBlockRemoverService> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Temporal Block Remover Service started at {DateTime.UtcNow}");

            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));

            try
            {
                do
                {
                    await CleanExpiredBlocksAsync(stoppingToken);
                }
                while (await timer.WaitForNextTickAsync(stoppingToken));
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("🛑 Temporal Block Remover Service stopping gracefully...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Unexpected error in Temporal Block Remover Service");
            }
        }

        private async Task CleanExpiredBlocksAsync(CancellationToken token)
        {
            using var scope = _serviceProvider.CreateScope();
            var countryService = scope.ServiceProvider.GetRequiredService<IBlockedCountryService>();

            try
            {
                _logger.LogInformation("Cleaning expired temporary blocks...");
                countryService.RemoveExpiredTemporalBlocks();
                _logger.LogInformation("Cleanup done at {Time}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during cleanup run.");
            }

            await Task.CompletedTask;
        }
    }
}