using FolderWatcherWorker.Watchers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FolderWatcherWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly FolderWatcher _fileWatcher;

        public Worker(ILogger<Worker> logger, FolderWatcher folderWatcher)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileWatcher = folderWatcher ?? throw new ArgumentNullException(nameof(folderWatcher));
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service started", DateTimeOffset.Now);
            _fileWatcher.Ini();

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running");
                await Task.Delay(1000, stoppingToken);
            }*/

            _logger.LogInformation("Worker running");

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service stopped");
            _fileWatcher.Dispose();

            return base.StopAsync(cancellationToken);
        }
    }
}
