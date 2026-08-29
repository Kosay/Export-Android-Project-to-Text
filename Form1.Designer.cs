namespace Export_Android_Project
{
    partial class Form1
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblSource = new Label();
            txtSource = new TextBox();
            btnBrowseSource = new Button();
            lblOutput = new Label();
            txtOutput = new TextBox();
            btnBrowseOutput = new Button();
            lblExtensions = new Label();
            clbExtensions = new CheckedListBox();
            txtNewExt = new TextBox();
            btnAddExt = new Button();
            grpOptions = new GroupBox();
            chkSkipNoise = new CheckBox();
            chkMarkdown = new CheckBox();
            lblMaxSize = new Label();
            numMaxSizeKb = new NumericUpDown();
            lblTree = new Label();
            tvProject = new TreeView();
            progress = new ProgressBar();
            lblStatus = new Label();
            btnExport = new Button();
            btnCancel = new Button();
            btnAbout = new Button();
            grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxSizeKb).BeginInit();
            SuspendLayout();

            // lblSource
            lblSource.AutoSize = true;
            lblSource.Location = new Point(12, 15);
            lblSource.Text = "Project folder:";

            // txtSource
            txtSource.Location = new Point(110, 12);
            txtSource.Size = new Size(500, 23);
            txtSource.ReadOnly = true;

            // btnBrowseSource
            btnBrowseSource.Location = new Point(616, 11);
            btnBrowseSource.Size = new Size(80, 25);
            btnBrowseSource.Text = "Browse...";
            btnBrowseSource.UseVisualStyleBackColor = true;
            btnBrowseSource.Click += btnBrowseSource_Click;

            // lblOutput
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(12, 44);
            lblOutput.Text = "Output file:";

            // txtOutput
            txtOutput.Location = new Point(110, 41);
            txtOutput.Size = new Size(500, 23);

            // btnBrowseOutput
            btnBrowseOutput.Location = new Point(616, 40);
            btnBrowseOutput.Size = new Size(80, 25);
            btnBrowseOutput.Text = "Browse...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;

            // lblExtensions
            lblExtensions.AutoSize = true;
            lblExtensions.Location = new Point(12, 78);
            lblExtensions.Text = "Extensions:";

            // clbExtensions
            clbExtensions.Location = new Point(12, 96);
            clbExtensions.Size = new Size(200, 270);
            clbExtensions.CheckOnClick = true;

            // txtNewExt
            txtNewExt.Location = new Point(12, 371);
            txtNewExt.Size = new Size(120, 23);
            txtNewExt.PlaceholderText = ".ext";
            txtNewExt.KeyDown += txtNewExt_KeyDown;

            // btnAddExt
            btnAddExt.Location = new Point(137, 370);
            btnAddExt.Size = new Size(75, 25);
            btnAddExt.Text = "Add";
            btnAddExt.UseVisualStyleBackColor = true;
            btnAddExt.Click += btnAddExt_Click;

            // grpOptions
            grpOptions.Location = new Point(12, 402);
            grpOptions.Size = new Size(200, 130);
            grpOptions.Text = "Options";
            grpOptions.Controls.Add(chkSkipNoise);
            grpOptions.Controls.Add(chkMarkdown);
            grpOptions.Controls.Add(lblMaxSize);
            grpOptions.Controls.Add(numMaxSizeKb);

            // chkSkipNoise
            chkSkipNoise.Location = new Point(10, 22);
            chkSkipNoise.Size = new Size(180, 20);
            chkSkipNoise.Text = "Skip noise folders";
            chkSkipNoise.Checked = true;
            chkSkipNoise.CheckedChanged += chkSkipNoise_CheckedChanged;

            // chkMarkdown
            chkMarkdown.Location = new Point(10, 46);
            chkMarkdown.Size = new Size(180, 20);
            chkMarkdown.Text = "Markdown output";
            chkMarkdown.Checked = true;

            // lblMaxSize
            lblMaxSize.AutoSize = true;
            lblMaxSize.Location = new Point(10, 76);
            lblMaxSize.Text = "Max file size (KB):";

            // numMaxSizeKb
            numMaxSizeKb.Location = new Point(10, 96);
            numMaxSizeKb.Size = new Size(100, 23);
            numMaxSizeKb.Minimum = 1;
            numMaxSizeKb.Maximum = 1000000;
            numMaxSizeKb.Value = 1024;

            // lblTree
            lblTree.AutoSize = true;
            lblTree.Location = new Point(224, 78);
            lblTree.Text = "Project tree (checking a folder checks everything inside):";

            // tvProject
            tvProject.Location = new Point(224, 96);
            tvProject.Size = new Size(472, 436);
            tvProject.CheckBoxes = true;
            tvProject.HideSelection = false;
            tvProject.AfterCheck += tvProject_AfterCheck;
            tvProject.BeforeExpand += tvProject_BeforeExpand;

            // progress
            progress.Location = new Point(12, 545);
            progress.Size = new Size(684, 18);

            // lblStatus
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 570);
            lblStatus.Text = "Ready";

            // btnExport
            btnExport.Location = new Point(431, 595);
            btnExport.Size = new Size(85, 28);
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;

            // btnCancel
            btnCancel.Location = new Point(522, 595);
            btnCancel.Size = new Size(85, 28);
            btnCancel.Text = "Cancel";
            btnCancel.Enabled = false;
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;

            // btnAbout
            btnAbout.Location = new Point(613, 595);
            btnAbout.Size = new Size(85, 28);
            btnAbout.Text = "About";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;

            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(710, 635);
            Controls.Add(lblSource);
            Controls.Add(txtSource);
            Controls.Add(btnBrowseSource);
            Controls.Add(lblOutput);
            Controls.Add(txtOutput);
            Controls.Add(btnBrowseOutput);
            Controls.Add(lblExtensions);
            Controls.Add(clbExtensions);
            Controls.Add(txtNewExt);
            Controls.Add(btnAddExt);
            Controls.Add(grpOptions);
            Controls.Add(lblTree);
            Controls.Add(tvProject);
            Controls.Add(progress);
            Controls.Add(lblStatus);
            Controls.Add(btnExport);
            Controls.Add(btnCancel);
            Controls.Add(btnAbout);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Export Project to Text";
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxSizeKb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSource = null!;
        private TextBox txtSource = null!;
        private Button btnBrowseSource = null!;
        private Label lblOutput = null!;
        private TextBox txtOutput = null!;
        private Button btnBrowseOutput = null!;
        private Label lblExtensions = null!;
        private CheckedListBox clbExtensions = null!;
        private TextBox txtNewExt = null!;
        private Button btnAddExt = null!;
        private GroupBox grpOptions = null!;
        private CheckBox chkSkipNoise = null!;
        private CheckBox chkMarkdown = null!;
        private Label lblMaxSize = null!;
        private NumericUpDown numMaxSizeKb = null!;
        private Label lblTree = null!;
        private TreeView tvProject = null!;
        private ProgressBar progress = null!;
        private Label lblStatus = null!;
        private Button btnExport = null!;
        private Button btnCancel = null!;
        private Button btnAbout = null!;
    }
}
