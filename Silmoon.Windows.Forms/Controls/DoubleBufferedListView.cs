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
        public event Action<SortOrder, int?> OnColumnSort;
        public int? SortColumnIndex { get; private set; } = null;
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

                if (SortColumnIndex.HasValue && SortColumnIndex.Value != e.Column)
                {
                    SortColumnIndex = e.Column;
                    Sorting = SortOrder.Ascending;
                    Columns[SortColumnIndex.Value].Text += " " + AscSortColumnSymbol;
                }
                else
                {
                    SortColumnIndex = e.Column;
                    if (Sorting == SortOrder.None)
                    {
                        Sorting = SortOrder.Ascending;
                        Columns[SortColumnIndex.Value].Text += " " + AscSortColumnSymbol;
                    }
                    else
                    {
                        if (Sorting == SortOrder.Ascending)
                        {
                            Sorting = SortOrder.Descending;
                            Columns[SortColumnIndex.Value].Text += " " + DescSortColumnSymbol;
                        }
                        else
                        {
                            //SortColumnIndex = e.Column;
                            //AscSort = true;
                            //Columns[SortColumnIndex.Value].Text += " " + AscSortColumnSymbol;

                            SortColumnIndex = null;
                            Sorting = SortOrder.None;
                        }
                    }
                }
                OnColumnSort?.Invoke(Sorting, SortColumnIndex);
                Invalidate();
            }
            base.OnColumnClick(e);
        }
    }
}
