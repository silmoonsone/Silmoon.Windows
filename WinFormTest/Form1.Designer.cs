namespace WinFormTest
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
            ctlEnumAllWindowButton = new Button();
            ctlListViewTestButton = new Button();
            SuspendLayout();
            // 
            // ctlEnumAllWindowButton
            // 
            ctlEnumAllWindowButton.Location = new Point(12, 12);
            ctlEnumAllWindowButton.Name = "ctlEnumAllWindowButton";
            ctlEnumAllWindowButton.Size = new Size(75, 23);
            ctlEnumAllWindowButton.TabIndex = 0;
            ctlEnumAllWindowButton.Text = "EnumAllWindow";
            ctlEnumAllWindowButton.UseVisualStyleBackColor = true;
            ctlEnumAllWindowButton.Click += ctlEnumAllWindowButton_Click;
            // 
            // ctlListViewTestButton
            // 
            ctlListViewTestButton.Location = new Point(93, 12);
            ctlListViewTestButton.Name = "ctlListViewTestButton";
            ctlListViewTestButton.Size = new Size(75, 23);
            ctlListViewTestButton.TabIndex = 1;
            ctlListViewTestButton.Text = "ListViewTest";
            ctlListViewTestButton.UseVisualStyleBackColor = true;
            ctlListViewTestButton.Click += ctlListViewTestButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 575);
            Controls.Add(ctlListViewTestButton);
            Controls.Add(ctlEnumAllWindowButton);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button ctlEnumAllWindowButton;
        private Button ctlListViewTestButton;
    }
}
