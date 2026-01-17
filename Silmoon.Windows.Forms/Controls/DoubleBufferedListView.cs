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
        public event Func<SortOrder, int, bool> OnVirtualModeColumnSort;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SortOrder VirtualSortOrder { get; set; } = SortOrder.None;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int? VirtualSortColumnIndex { get; private set; } = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string AscSortColumnSymbol { get; set; } = "▲";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DescSortColumnSymbol { get; set; } = "▼";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
            if (VirtualMode)
            {
                SortOrder tmpOrder;

                if (VirtualSortColumnIndex != e.Column)
                    tmpOrder = SortOrder.Ascending;
                else
                {
                    tmpOrder = VirtualSortOrder switch
                    {
                        SortOrder.None => SortOrder.Ascending,
                        SortOrder.Ascending => SortOrder.Descending,
                        _ => SortOrder.None
                    };
                }
                var result = OnVirtualModeColumnSort?.Invoke(tmpOrder, e.Column) ?? false;

                if (result)
                {
                    foreach (ColumnHeader item in Columns)
                    {
                        if (SortSymbolColumnTextPrefix)
                            item.Text = item.Text.Replace(" " + AscSortColumnSymbol, string.Empty).Replace(" " + DescSortColumnSymbol, string.Empty);
                        else item.Text = item.Text.Replace(AscSortColumnSymbol + " ", string.Empty).Replace(DescSortColumnSymbol + " ", string.Empty);
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
                    Invalidate();
                }
            }
            base.OnColumnClick(e);
        }
    }
    public readonly ref struct ListViewUpdateScope
    {
        private readonly ListView _listView;

        public ListViewUpdateScope(ListView listView)
        {
            _listView = listView;
            _listView?.BeginUpdate();
        }

        public void Dispose()
        {
            var lv = _listView;
            if (lv == null || lv.IsDisposed) return;
            lv.EndUpdate();
        }
    }
}