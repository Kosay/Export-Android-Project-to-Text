namespace Export_Android_Project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			textBox1 = new TextBox();
			button1 = new Button();
			button2 = new Button();
			textBox2 = new TextBox();
			label2 = new Label();
			button3 = new Button();
			button4 = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 46);
			label1.Name = "label1";
			label1.Size = new Size(44, 15);
			label1.TabIndex = 0;
			label1.Text = "Project";
			// 
			// textBox1
			// 
			textBox1.Enabled = false;
			textBox1.Location = new Point(58, 43);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(457, 23);
			textBox1.TabIndex = 1;
			// 
			// button1
			// 
			button1.Location = new Point(521, 43);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 2;
			button1.Text = "Browse";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Enabled = false;
			button2.Location = new Point(520, 88);
			button2.Name = "button2";
			button2.Size = new Size(75, 23);
			button2.TabIndex = 5;
			button2.Text = "Browse";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// textBox2
			// 
			textBox2.Enabled = false;
			textBox2.Location = new Point(57, 88);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(457, 23);
			textBox2.TabIndex = 4;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(11, 91);
			label2.Name = "label2";
			label2.Size = new Size(31, 15);
			label2.TabIndex = 3;
			label2.Text = "Save";
			// 
			// button3
			// 
			button3.Enabled = false;
			button3.Location = new Point(177, 142);
			button3.Name = "button3";
			button3.Size = new Size(75, 23);
			button3.TabIndex = 6;
			button3.Text = "Save";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button4
			// 
			button4.Location = new Point(329, 142);
			button4.Name = "button4";
			button4.Size = new Size(75, 23);
			button4.TabIndex = 7;
			button4.Text = "About";
			button4.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(606, 199);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(textBox2);
			Controls.Add(label2);
			Controls.Add(button1);
			Controls.Add(textBox1);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Name = "Form1";
			Text = "Export Android Project";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox textBox1;
		private Button button1;
		private Button button2;
		private TextBox textBox2;
		private Label label2;
		private Button button3;
		private Button button4;
	}
}
