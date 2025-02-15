using Silmoon.Windows.Forms.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Forms.Controls
{
    public class DoubleBufferedListView : ListView
    {
        public event Func<SortOrder, int?, bool> OnColumnSort;
        public SortOrder VirtualSortOrder { get; set; } = SortOrder.None;
        public int? VirtualSortColumnIndex { get; private set; } = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<int> AcceptSortColumns { get; set; } = new List<int>();
        public string AscSortColumnSymbol { get; set; } = "▲";
        public string DescSortColumnSymbol { get; set; } = "▼";
        public bool SortSymbolColumnTextPrefix { get; set; } = true;
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
            if (AcceptSortColumns?.Contains(e.Column) ?? false)
            {
                foreach (var item in AcceptSortColumns)
                {
                    if (SortSymbolColumnTextPrefix)
                        Columns[item].Text = Columns[item].Text.Replace(" " + AscSortColumnSymbol, string.Empty).Replace(" " + DescSortColumnSymbol, string.Empty);
                    else Columns[item].Text = Columns[item].Text.Replace(AscSortColumnSymbol + " ", string.Empty).Replace(DescSortColumnSymbol + " ", string.Empty);
                }

                if (VirtualSortColumnIndex.HasValue && VirtualSortColumnIndex.Value != e.Column)
                {
                    VirtualSortColumnIndex = e.Column;
                    VirtualSortOrder = SortOrder.Ascending;

                    if (SortSymbolColumnTextPrefix) Columns[VirtualSortColumnIndex.Value].Text += " " + AscSortColumnSymbol;
                    else Columns[VirtualSortColumnIndex.Value].Text = AscSortColumnSymbol + " " + Columns[VirtualSortColumnIndex.Value].Text;
                }
                else
                {
                    VirtualSortColumnIndex = e.Column;
                    if (VirtualSortOrder == SortOrder.None)
                    {
                        VirtualSortOrder = SortOrder.Ascending;

                        if (SortSymbolColumnTextPrefix) Columns[VirtualSortColumnIndex.Value].Text += " " + AscSortColumnSymbol;
                        else Columns[VirtualSortColumnIndex.Value].Text = AscSortColumnSymbol + " " + Columns[VirtualSortColumnIndex.Value].Text;
                    }
                    else
                    {
                        if (VirtualSortOrder == SortOrder.Ascending)
                        {
                            VirtualSortOrder = SortOrder.Descending;

                            if (SortSymbolColumnTextPrefix) Columns[VirtualSortColumnIndex.Value].Text += " " + DescSortColumnSymbol;
                            else Columns[VirtualSortColumnIndex.Value].Text = DescSortColumnSymbol + " " + Columns[VirtualSortColumnIndex.Value].Text;
                        }
                        else
                        {
                            //SortColumnIndex = e.Column;
                            //AscSort = true;
                            //Columns[SortColumnIndex.Value].Text += " " + AscSortColumnSymbol;

                            VirtualSortColumnIndex = null;
                            VirtualSortOrder = SortOrder.None;
                        }
                    }
                }
            }
            if (OnColumnSort?.Invoke(VirtualSortOrder, VirtualSortColumnIndex) ?? false) Invalidate();
            base.OnColumnClick(e);
        }
    }
}
