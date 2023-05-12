using SyncDB_WithExcellData.Services;

namespace SyncDB_WithExcellData
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _hostLifetime;
        private readonly IServiceProvider _provider;

        public Worker(
            ILogger<Worker> logger,
            IHostApplicationLifetime hostLifetime,
            IServiceProvider provider)
        {
            _logger = logger;
            _hostLifetime = hostLifetime;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            using (IServiceScope scope = _provider.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;

                SyncDB syncDB = scope.ServiceProvider.GetRequiredService<SyncDB>();

                await syncDB.StartSync();
            }

            _logger.LogInformation("Worker complete at: {time}", DateTimeOffset.Now);

            _hostLifetime.StopApplication();
        }
    }
}