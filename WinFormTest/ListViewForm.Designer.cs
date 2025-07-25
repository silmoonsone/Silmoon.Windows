using Silmoon.Windows.Forms.Controls;

namespace WinFormTest
{
    partial class ListViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            doubleBufferedListView1 = new DoubleBufferedListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            SuspendLayout();
            // 
            // doubleBufferedListView1
            // 
            doubleBufferedListView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            doubleBufferedListView1.DescSortColumnSymbol = "▼";
            doubleBufferedListView1.Dock = DockStyle.Fill;
            doubleBufferedListView1.FullRowSelect = true;
            doubleBufferedListView1.Location = new Point(0, 0);
            doubleBufferedListView1.Name = "doubleBufferedListView1";
            doubleBufferedListView1.Size = new Size(800, 450);
            doubleBufferedListView1.SortSymbolColumnTextPrefix = true;
            doubleBufferedListView1.TabIndex = 0;
            doubleBufferedListView1.UseCompatibleStateImageBehavior = false;
            doubleBufferedListView1.View = View.Details;
            doubleBufferedListView1.VirtualSortOrder = SortOrder.None;
            doubleBufferedListView1.RightToLeftLayoutChanged += ListView1_OnColumnSort;
            doubleBufferedListView1.RetrieveVirtualItem += doubleBufferedListView1_RetrieveVirtualItem;
            // 
            // columnHeader1
            // 
            columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            columnHeader3.Width = 180;
            // 
            // ListViewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(doubleBufferedListView1);
            Name = "ListViewForm";
            Text = "ListViewForm";
            ResumeLayout(false);
        }

        #endregion

        private DoubleBufferedListView doubleBufferedListView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
    }
}