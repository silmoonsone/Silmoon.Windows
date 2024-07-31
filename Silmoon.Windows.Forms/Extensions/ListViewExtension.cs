using Silmoon.Windows.Forms.ControllerHelpers;
using Silmoon.Windows.Win32.Structs;
using Silmoon.Windows.Win32Api.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Forms.Extensions
{
    public static class ListViewExtension
    {
        public static void AutoResizeAllColumnsWidth(this ListView listView, bool lastColumnWidthFill = true, int adjust = 0)
        {
            if (listView.View == View.Details)
            {
                for (int i = 0; i < listView.Columns.Count - 1; i++)
                {
                    listView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                if (lastColumnWidthFill) listView.AutoFullLastColumnWidth(adjust);
            }
        }
        public static void AutoFullLastColumnWidth(this ListView listView, int adjust)
        {
            if (listView.View == View.Details)
            {
                // 确保至少有一个列
                if (listView.Columns.Count != 0)
                {
                    // 计算剩余宽度
                    int totalWidth = listView.ClientSize.Width;
                    for (int i = 0; i < listView.Columns.Count - 1; i++)
                    {
                        totalWidth -= listView.Columns[i].Width;
                    }

                    // 将最后一个列的宽度设置为剩余宽度
                    listView.Columns[listView.Columns.Count - 1].Width = totalWidth + adjust;
                }
            }
        }
        public static void ColumnSort(this ListView listView, ColumnClickEventArgs e)
        {
            if (listView.View == View.Details)
            {
                if (listView.ListViewItemSorter is ListViewItemComparer comparer)
                {
                    listView.ListViewItemSorter = new ListViewItemComparer(e.Column, !comparer.Ascending);
                }
                else
                {
                    listView.ListViewItemSorter = new ListViewItemComparer(e.Column, true);
                }
            }
        }
        /// <summary>
        /// Select all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be selected</param>
        public static void SelectAllItems(this ListView list)
        {
            SetItemState(list, -1, 2, 2);
        }

        /// <summary>
        /// Deselect all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be deselected</param>
        public static void DeselectAllItems(this ListView list)
        {
            SetItemState(list, -1, 2, 0);
        }

        /// <summary>
        /// Set the item state on the given item
        /// </summary>
        /// <param name="list">The listview whose item's state is to be changed</param>
        /// <param name="itemIndex">The index of the item to be changed</param>
        /// <param name="mask">Which bits of the value are to be set?</param>
        /// <param name="value">The value to be set</param>
        public static void SetItemState(this ListView list, int itemIndex, int mask, int value)
        {
            LVITEM lvItem = new LVITEM();
            lvItem.stateMask = mask;
            lvItem.state = value;
            User32.SendMessage(list.Handle, User32.LVM_SETITEMSTATE, itemIndex, ref lvItem);
        }
    }
}
