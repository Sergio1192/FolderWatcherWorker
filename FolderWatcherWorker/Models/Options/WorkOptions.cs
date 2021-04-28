namespace FolderWatcherWorker.Models.Options
{
    public class WorkOptions
    {
        public static readonly string NAME = nameof(WorkOptions).Replace(nameof(Options), string.Empty);

        public string Path { get; set; }
        public string ScriptPath { get; set; }
    }
}
