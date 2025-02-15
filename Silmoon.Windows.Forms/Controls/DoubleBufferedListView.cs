using Silmoon.Windows.Forms.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Forms.Controls
{
    public class DoubleBufferedListView : ListView
    {
        public event Func<SortOrder, int?, bool> OnColumnSort;
        public SortOrder VirtualOrder { get; set; } = SortOrder.None;
        public int? VirtualSortColumnIndex { get; private set; } = null;
        public List<int> AccepteSortColumns { get; set; } = [];
        public string AscSortColumnSymbol { get; set; } = "▲";
        public string DescSortColumnSymbol { get; set; } = "▼";
        public DoubleBufferedListView()
        {
            // 启用双缓冲
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                ListViewExtension.SelectAllItems(this);
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            if (AccepteSortColumns.Contains(e.Column))
            {
                foreach (var item in AccepteSortColumns)
                {
                    Columns[item].Text = Columns[item].Text.Replace(" " + AscSortColumnSymbol, "").Replace(" " + DescSortColumnSymbol, "");
                }

                if (VirtualSortColumnIndex.HasValue && VirtualSortColumnIndex.Value != e.Column)
                {
                    VirtualSortColumnIndex = e.Column;
                    VirtualOrder = SortOrder.Ascending;
                    Columns[VirtualSortColumnIndex.Value].Text += " " + AscSortColumnSymbol;
                }
                else
                {
                    VirtualSortColumnIndex = e.Column;
                    if (VirtualOrder == SortOrder.None)
                    {
                        VirtualOrder = SortOrder.Ascending;
                        Columns[VirtualSortColumnIndex.Value].Text += " " + AscSortColumnSymbol;
                    }
                    else
                    {
                        if (VirtualOrder == SortOrder.Ascending)
                        {
                            VirtualOrder = SortOrder.Descending;
                            Columns[VirtualSortColumnIndex.Value].Text += " " + DescSortColumnSymbol;
                        }
                        else
                        {
                            //SortColumnIndex = e.Column;
                            //AscSort = true;
                            //Columns[SortColumnIndex.Value].Text += " " + AscSortColumnSymbol;

                            VirtualSortColumnIndex = null;
                            VirtualOrder = SortOrder.None;
                        }
                    }
                }
            }
            if (OnColumnSort?.Invoke(VirtualOrder, VirtualSortColumnIndex) ?? false) Invalidate();
            base.OnColumnClick(e);
        }
    }
}
