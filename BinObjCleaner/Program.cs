using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;

class Program
{
    static void Main(string[] args)
    {
        // 1. Load configuration
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var settings = config.GetSection("CleanerSettings").Get<CleanerSettings>();

        if (settings == null || string.IsNullOrWhiteSpace(settings.SourcePath))
        {
            Console.Error.WriteLine("CleanerSettings.SourcePath is missing in appsettings.json");
            return;
        }

        // 2. Use settings in your logic
        Console.WriteLine($"Source Path: {settings.SourcePath}");
        Console.WriteLine($"Targets: {string.Join(", ", settings.Names)}");
        Console.WriteLine(settings.DryRun ? "Mode: DRY RUN" : "Mode: DELETE");
        Console.WriteLine($"Parallelism: {settings.Parallelism}");

        var errors = new ConcurrentBag<string>();
        var targets = Directory.EnumerateDirectories(settings.SourcePath, "*", new EnumerationOptions
        {
            RecurseSubdirectories = true,
            IgnoreInaccessible = true
        })
        .Where(d => settings.Names.Contains(Path.GetFileName(d), StringComparer.OrdinalIgnoreCase))
        .ToList();

        Parallel.ForEach(targets,
            new ParallelOptions { MaxDegreeOfParallelism = settings.Parallelism },
            dir =>
            {
                try
                {
                    if (settings.DryRun)
                    {
                        Console.WriteLine($"[DRY RUN] {dir}");
                    }
                    else
                    {
                        Directory.Delete(dir, recursive: true);
                        Console.WriteLine($"Deleted: {dir}");
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"{dir} :: {ex.Message}");
                }
            });

        if (!errors.IsEmpty)
        {
            Console.WriteLine("\nErrors:");
            foreach (var e in errors) Console.WriteLine(e);
        }
    }
}

public class CleanerSettings
{
    public string SourcePath { get; set; } = string.Empty;
    public string[] Names { get; set; } = Array.Empty<string>();
    public bool DryRun { get; set; }
    public int Parallelism { get; set; } = Environment.ProcessorCount;
}
