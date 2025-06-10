namespace Export_Android_Project
{
	using System.Text;
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
			folderBrowserDialog1.Description = "Select the Android project folder";
			folderBrowserDialog1.ShowDialog();
			if (folderBrowserDialog1.SelectedPath != "")
			{
				string projectPath = folderBrowserDialog1.SelectedPath;

				try
				{
					textBox1.Text = projectPath;

					button2.Enabled = true;

				}
				catch (Exception ex)
				{
					MessageBox.Show("Error exporting project: " + ex.Message);
				}
			}
			else
			{
				MessageBox.Show("No folder selected.");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();

			saveFileDialog1.Filter = "Tet Files (*.txt)|*.txt|All Files (*.*)|*.*";
			saveFileDialog1.Title = "Save Exported Project";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					textBox2.Text = saveFileDialog1.FileName;
					button3.Enabled = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error saving file: " + ex.Message);
				}
			}
			else
			{
				MessageBox.Show("Save operation cancelled.");
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{

			try
			{
				string[] files = Directory.GetFiles(textBox1.Text, "*.*", SearchOption.AllDirectories);
				StringBuilder sb = new StringBuilder();
				foreach (string file in files)
				{
					if (!file.EndsWith(".kt") && !file.EndsWith(".kts") && !file.EndsWith(".xml"))
						continue;
					sb.AppendLine($"File Name: {Path.GetFileName(file)}");
					sb.AppendLine($"File Path: {file}");
					sb.AppendLine($"Content:\n{File.ReadAllText(file)}");
					sb.AppendLine(new string('*', 10));
				}

				File.WriteAllText(textBox2.Text, sb.ToString());
				button2.Enabled = false;
				button3.Enabled = false;
				MessageBox.Show("Files read successfully");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error reading files: " + ex.Message);
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			MessageBox.Show("This application exports Android project files to a text file.\n\n" +
				"1. Click 'Select Project Folder' to choose the Android project directory.\n" +
				"2. Click 'Save Exported Project' to specify where to save the output file.\n" +
				"3. Click 'Export Files' to read and save the contents of .kt, .kts, and .xml files in the project.\n\n" +
				"Note: Only Kotlin and XML files are processed.\n\nCreated by Kosay Hatem", "Help");
		}
	}
}
