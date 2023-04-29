namespace Demo
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
            UpdateButton = new Button();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(12, 4);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(95, 34);
            UpdateButton.TabIndex = 0;
            UpdateButton.Text = "更新";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Bottom;
            richTextBox1.Location = new Point(0, 44);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(800, 406);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(113, 4);
            button1.Name = "button1";
            button1.Size = new Size(95, 34);
            button1.TabIndex = 0;
            button1.Text = "暂停拉取";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(214, 4);
            button2.Name = "button2";
            button2.Size = new Size(95, 34);
            button2.TabIndex = 0;
            button2.Text = "清除";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(richTextBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(UpdateButton);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button UpdateButton;
        private RichTextBox richTextBox1;
        private Button button1;
        private Button button2;
    }
}