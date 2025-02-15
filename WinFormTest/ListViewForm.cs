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
            doubleBufferedListView1.OnColumnSort += ListView1_OnColumnSort;
            doubleBufferedListView1.VirtualListSize = 100;
        }

        private bool ListView1_OnColumnSort(SortOrder arg1, int? arg2)
        {
            Debug.WriteLine($"Sorting: {arg1}, index: {arg2}");
            return true;
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem([e.ItemIndex.ToString(), e.ItemIndex.ToString(), e.ItemIndex.ToString()]);
        }

        private void ListView1_OnColumnSort(object sender, EventArgs e)
        {

        }
    }
}
