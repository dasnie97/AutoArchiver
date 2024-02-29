namespace AutoArchiver
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IArchiver _archiver;

        public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostApplicationLifetime, IArchiver archiver)
        {
            _logger = logger;
           _hostApplicationLifetime = hostApplicationLifetime;
            _archiver = archiver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(await _archiver.Archive());
            _logger.LogInformation("Archive job done");
            _hostApplicationLifetime.StopApplication();
        }
    }
}
