using Silmoon.Windows.Forms.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class ListViewForm : Form
    {
        public ListViewForm()
        {
            InitializeComponent();
            doubleBufferedListView1.OnVirtualModeColumnSort += ListView1_OnVirtualModeColumnSort;
            doubleBufferedListView1.VirtualListSize = 100;
            doubleBufferedListView1.VirtualMode = true;

            ListView listView = new ListView();
            using (var x = listView.UpdateScope())
            {

            }
        }

        private bool ListView1_OnVirtualModeColumnSort(SortOrder arg1, int arg2)
        {
            Debug.WriteLine($"Index: {arg2}, Sorting: {arg1}.");
            return true;
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem([e.ItemIndex.ToString(), e.ItemIndex.ToString(), e.ItemIndex.ToString()]);
        }

        private void ListView1_OnColumnSort(object sender, EventArgs e)
        {

        }

        private void doubleBufferedListView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem([e.ItemIndex.ToString(), e.ItemIndex.ToString(), e.ItemIndex.ToString()]);
        }
    }
}
