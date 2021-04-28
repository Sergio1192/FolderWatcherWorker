using FolderWatcherWorker.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Management.Automation;

namespace FolderWatcherWorker.Watchers
{
    public sealed class FolderWatcher : IDisposable
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly IOptionsMonitor<WorkOptions> _options;
        private readonly ILogger<FolderWatcher> _logger;

        public FolderWatcher(ILogger<FolderWatcher> logger, FileSystemWatcher fileSystemWatcher, IOptionsMonitor<WorkOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileSystemWatcher = fileSystemWatcher ?? throw new ArgumentNullException(nameof(fileSystemWatcher));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void Dispose()
        {
            _fileSystemWatcher.Dispose();
        }

        public void Ini()
        {
            // Configuration
            _fileSystemWatcher.Path = _options.CurrentValue.Path;
            _fileSystemWatcher.IncludeSubdirectories = true;
            _fileSystemWatcher.EnableRaisingEvents = true;

            // Events
            _fileSystemWatcher.Changed += OnChanged;
            _fileSystemWatcher.Created += OnCreated;
        }

        // Event functions
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            _logger.LogInformation($"Changed: {e.FullPath}");
            RunPowershellScript();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"Created: {e.FullPath}");
            RunPowershellScript();
        }

        private void RunPowershellScript()
        {
            using var ps = PowerShell.Create();

            //var result = ps.AddCommand(_options.CurrentValue.ScriptPath).Invoke();
            var result = ps.AddScript(File.ReadAllText(_options.CurrentValue.ScriptPath)).Invoke();
        }
    }
}
