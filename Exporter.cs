namespace Export_Android_Project
{
    using System.Text;
    using System.Threading;

    public sealed class ExportOptions
    {
        public string RootPath { get; set; } = "";
        public string OutputPath { get; set; } = "";
        public bool Markdown { get; set; } = true;
        public long MaxFileBytes { get; set; } = 1024 * 1024;
    }

    public sealed class ExportProgress
    {
        public int Processed { get; set; }
        public string CurrentFile { get; set; } = "";
    }

    public sealed class ExportResult
    {
        public int Written { get; set; }
        public long TotalBytes { get; set; }
        public List<string> Skipped { get; } = new();
    }

    public static class Exporter
    {
        private static readonly Dictionary<string, string> LangByExt = new(StringComparer.OrdinalIgnoreCase)
        {
            [".kt"] = "kotlin",
            [".kts"] = "kotlin",
            [".java"] = "java",
            [".xml"] = "xml",
            [".ts"] = "typescript",
            [".tsx"] = "tsx",
            [".js"] = "javascript",
            [".jsx"] = "jsx",
            [".json"] = "json",
            [".gradle"] = "groovy",
            [".toml"] = "toml",
            [".pro"] = "text",
            [".properties"] = "properties",
            [".md"] = "markdown",
            [".txt"] = "text",
        };

        public static ExportResult Export(
            IReadOnlyList<string> files,
            ExportOptions options,
            IProgress<ExportProgress> progress,
            CancellationToken token)
        {
            var result = new ExportResult();
            var utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

            using var writer = new StreamWriter(options.OutputPath, append: false, utf8);

            if (options.Markdown)
            {
                writer.WriteLine($"# Project export: {Path.GetFileName(options.RootPath.TrimEnd(Path.DirectorySeparatorChar))}");
                writer.WriteLine();
                writer.WriteLine($"- Source: `{options.RootPath}`");
                writer.WriteLine($"- Generated: {DateTime.Now:yyyy-MM-dd HH:mm}");
                writer.WriteLine($"- Files: {files.Count}");
                writer.WriteLine();
                writer.WriteLine("---");
                writer.WriteLine();
            }

            int processed = 0;
            foreach (var file in files)
            {
                token.ThrowIfCancellationRequested();
                processed++;
                progress.Report(new ExportProgress { Processed = processed, CurrentFile = file });

                var rel = Relative(options.RootPath, file);
                try
                {
                    var info = new FileInfo(file);
                    if (info.Length > options.MaxFileBytes)
                    {
                        result.Skipped.Add($"{rel} (too large: {info.Length / 1024} KB)");
                        continue;
                    }

                    string text;
                    try
                    {
                        text = File.ReadAllText(file);
                    }
                    catch (Exception ex)
                    {
                        result.Skipped.Add($"{rel} ({ex.GetType().Name}: {ex.Message})");
                        continue;
                    }

                    if (options.Markdown)
                    {
                        var ext = Path.GetExtension(file);
                        var lang = LangByExt.TryGetValue(ext, out var l) ? l : "";
                        writer.WriteLine($"## `{rel}`");
                        writer.WriteLine();
                        writer.WriteLine("```" + lang);
                        writer.WriteLine(text);
                        writer.WriteLine("```");
                        writer.WriteLine();
                    }
                    else
                    {
                        writer.WriteLine($"File Name: {Path.GetFileName(file)}");
                        writer.WriteLine($"File Path: {rel}");
                        writer.WriteLine("Content:");
                        writer.WriteLine(text);
                        writer.WriteLine(new string('*', 60));
                    }

                    result.Written++;
                    result.TotalBytes += info.Length;
                }
                catch (Exception ex)
                {
                    result.Skipped.Add($"{rel} ({ex.GetType().Name}: {ex.Message})");
                }
            }

            return result;
        }

        private static string Relative(string root, string path)
        {
            try { return Path.GetRelativePath(root, path); }
            catch { return path; }
        }
    }
}
