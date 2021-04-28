using FolderWatcherWorker;
using FolderWatcherWorker.Models.Options;
using FolderWatcherWorker.Watchers;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<Worker>()
                .AddSingleton<FolderWatcher>()
                .AddSingleton<FileSystemWatcher>()
                .Configure<WorkOptions>(configuration.GetSection(WorkOptions.NAME));

            return services;
        }
    }
}
