namespace Export_Android_Project
{
    using System.Threading;

    public partial class Form1 : Form
    {
        private static readonly (string Ext, bool Default)[] Extensions = new[]
        {
            (".kt",         true),
            (".kts",        true),
            (".xml",        true),
            (".ts",         true),
            (".tsx",        true),
            (".java",       false),
            (".js",         false),
            (".jsx",        false),
            (".json",       false),
            (".gradle",     false),
            (".toml",       false),
            (".pro",        false),
            (".properties", false),
            (".md",         false),
            (".txt",        false),
        };

        private static readonly HashSet<string> NoiseFolders = new(StringComparer.OrdinalIgnoreCase)
        {
            "build", ".gradle", ".idea", ".git", "node_modules",
            "bin", "obj", ".vs", ".dart_tool", "generated",
            ".next", ".nuxt", ".cache", "dist", "out", ".venv", "__pycache__"
        };

        private CancellationTokenSource? cts;
        private bool suppressCheckEvent;

        public Form1()
        {
            InitializeComponent();
            foreach (var (ext, def) in Extensions)
                clbExtensions.Items.Add(ext, def);
        }

        private void btnBrowseSource_Click(object? sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog { Description = "Select the project folder" };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            txtSource.Text = dlg.SelectedPath;

            if (string.IsNullOrWhiteSpace(txtOutput.Text))
            {
                var parent = Path.GetDirectoryName(dlg.SelectedPath) ?? dlg.SelectedPath;
                txtOutput.Text = Path.Combine(parent, Path.GetFileName(dlg.SelectedPath) + "-export.md");
            }

            LoadTree(dlg.SelectedPath);
        }

        private void btnBrowseOutput_Click(object? sender, EventArgs e)
        {
            using var dlg = new SaveFileDialog
            {
                Filter = "Markdown files (*.md)|*.md|Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Save exported project",
                FileName = string.IsNullOrWhiteSpace(txtOutput.Text)
                    ? "project-export.md"
                    : Path.GetFileName(txtOutput.Text)
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
                txtOutput.Text = dlg.FileName;
        }

        private void chkSkipNoise_CheckedChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSource.Text) && Directory.Exists(txtSource.Text))
                LoadTree(txtSource.Text);
        }

        private void LoadTree(string rootPath)
        {
            Cursor = Cursors.WaitCursor;
            tvProject.BeginUpdate();
            try
            {
                tvProject.Nodes.Clear();
                var rootNode = CreateNode(rootPath, true);
                tvProject.Nodes.Add(rootNode);
                PopulateNode(rootNode);
                rootNode.Expand();
                suppressCheckEvent = true;
                try { SetCheckRecursive(rootNode, true); }
                finally { suppressCheckEvent = false; }
            }
            finally
            {
                tvProject.EndUpdate();
                Cursor = Cursors.Default;
            }
        }

        private static TreeNode CreateNode(string path, bool isDir)
        {
            var name = Path.GetFileName(path);
            if (string.IsNullOrEmpty(name)) name = path;
            return new TreeNode(name) { Tag = new NodeInfo(path, isDir) };
        }

        private void PopulateNode(TreeNode node)
        {
            if (node.Tag is not NodeInfo info || !info.IsDirectory || info.Populated) return;
            node.Nodes.Clear();
            try
            {
                foreach (var d in Directory.EnumerateDirectories(info.Path).OrderBy(p => p, StringComparer.OrdinalIgnoreCase))
                {
                    var name = Path.GetFileName(d);
                    if (chkSkipNoise.Checked && NoiseFolders.Contains(name)) continue;
                    var child = CreateNode(d, true);
                    if (HasChildren(d)) child.Nodes.Add(new TreeNode("...") { Tag = null });
                    node.Nodes.Add(child);
                }
                foreach (var f in Directory.EnumerateFiles(info.Path).OrderBy(p => p, StringComparer.OrdinalIgnoreCase))
                {
                    node.Nodes.Add(CreateNode(f, false));
                }
            }
            catch { }
            info.Populated = true;
        }

        private static bool HasChildren(string dir)
        {
            try { return Directory.EnumerateFileSystemEntries(dir).Any(); }
            catch { return false; }
        }

        private void tvProject_BeforeExpand(object? sender, TreeViewCancelEventArgs e)
        {
            if (e.Node?.Tag is NodeInfo info && info.IsDirectory && !info.Populated)
            {
                foreach (TreeNode c in e.Node.Nodes.Cast<TreeNode>().ToList())
                    if (c.Tag == null) c.Remove();
                PopulateNode(e.Node);
            }
        }

        private void tvProject_AfterCheck(object? sender, TreeViewEventArgs e)
        {
            if (suppressCheckEvent || e.Node == null) return;
            if (e.Action == TreeViewAction.Unknown) return;

            suppressCheckEvent = true;
            Cursor = Cursors.WaitCursor;
            tvProject.BeginUpdate();
            try
            {
                if (e.Node.Tag is NodeInfo info && info.IsDirectory)
                    EnsurePopulatedRecursive(e.Node);
                SetCheckRecursive(e.Node, e.Node.Checked);
            }
            finally
            {
                tvProject.EndUpdate();
                Cursor = Cursors.Default;
                suppressCheckEvent = false;
            }
        }

        private void EnsurePopulatedRecursive(TreeNode node)
        {
            if (node.Tag is not NodeInfo info || !info.IsDirectory) return;
            if (!info.Populated)
            {
                foreach (TreeNode c in node.Nodes.Cast<TreeNode>().ToList())
                    if (c.Tag == null) c.Remove();
                PopulateNode(node);
            }
            foreach (TreeNode child in node.Nodes)
                EnsurePopulatedRecursive(child);
        }

        private static void SetCheckRecursive(TreeNode node, bool value)
        {
            node.Checked = value;
            foreach (TreeNode child in node.Nodes)
                SetCheckRecursive(child, value);
        }

        private static IEnumerable<string> GatherCheckedFiles(TreeNode node)
        {
            if (node.Tag is NodeInfo info)
            {
                if (!info.IsDirectory && node.Checked)
                    yield return info.Path;
            }
            foreach (TreeNode child in node.Nodes)
                foreach (var f in GatherCheckedFiles(child))
                    yield return f;
        }

        private async void btnExport_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSource.Text) || !Directory.Exists(txtSource.Text))
            {
                MessageBox.Show(this, "Choose a valid project folder first.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOutput.Text))
            {
                MessageBox.Show(this, "Choose an output file first.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tvProject.Nodes.Count == 0)
            {
                MessageBox.Show(this, "The project tree is empty.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var extensions = clbExtensions.CheckedItems.Cast<string>()
                .Select(s => s.ToLowerInvariant())
                .ToHashSet();
            if (extensions.Count == 0)
            {
                MessageBox.Show(this, "Select at least one extension.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var files = GatherCheckedFiles(tvProject.Nodes[0])
                .Where(f => extensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                .ToList();
            if (files.Count == 0)
            {
                MessageBox.Show(this, "No files match the checked extensions in your selection.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var options = new ExportOptions
            {
                RootPath = txtSource.Text,
                OutputPath = txtOutput.Text,
                Markdown = chkMarkdown.Checked,
                MaxFileBytes = (long)numMaxSizeKb.Value * 1024L,
            };

            cts = new CancellationTokenSource();
            SetBusy(true);
            progress.Value = 0;
            progress.Maximum = files.Count;
            lblStatus.Text = $"Exporting 0 / {files.Count}...";

            var progressReport = new Progress<ExportProgress>(p =>
            {
                progress.Value = Math.Min(p.Processed, progress.Maximum);
                lblStatus.Text = $"Exporting {p.Processed} / {files.Count} — {Path.GetFileName(p.CurrentFile)}";
            });

            try
            {
                var result = await Task.Run(() => Exporter.Export(files, options, progressReport, cts.Token));
                lblStatus.Text = $"Done. Wrote {result.Written}, skipped {result.Skipped.Count}, {result.TotalBytes / 1024} KB.";

                var msg = $"Export complete.\n\nWrote: {result.Written}\nSkipped: {result.Skipped.Count}\nTotal: {result.TotalBytes / 1024} KB";
                if (result.Skipped.Count > 0)
                {
                    var preview = string.Join(Environment.NewLine, result.Skipped.Take(10));
                    var more = result.Skipped.Count > 10 ? $"{Environment.NewLine}... and {result.Skipped.Count - 10} more." : "";
                    msg += $"\n\nSkipped files:\n{preview}{more}";
                }
                MessageBox.Show(this, msg, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Cancelled.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Failed.";
                MessageBox.Show(this, "Export failed: " + ex.Message, "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
                cts?.Dispose();
                cts = null;
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e) => cts?.Cancel();

        private void btnAbout_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(this,
                "Export Project to Text\n\n" +
                "Select a project folder, choose which files/folders to include in the tree, " +
                "pick the extensions to export, and save the result to a single text or Markdown file.\n\n" +
                "Checking a folder in the tree automatically checks every file and subfolder inside it.\n\n" +
                "Supports Kotlin, Java, XML, TypeScript (.ts/.tsx), JavaScript, JSON, Gradle, TOML, and more.\n\n" +
                "Created by Kosay Hatem",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetBusy(bool busy)
        {
            btnExport.Enabled = !busy;
            btnBrowseSource.Enabled = !busy;
            btnBrowseOutput.Enabled = !busy;
            clbExtensions.Enabled = !busy;
            tvProject.Enabled = !busy;
            chkSkipNoise.Enabled = !busy;
            chkMarkdown.Enabled = !busy;
            numMaxSizeKb.Enabled = !busy;
            btnCancel.Enabled = busy;
        }

        private sealed class NodeInfo
        {
            public string Path { get; }
            public bool IsDirectory { get; }
            public bool Populated { get; set; }
            public NodeInfo(string path, bool isDir) { Path = path; IsDirectory = isDir; }
        }
    }
}
