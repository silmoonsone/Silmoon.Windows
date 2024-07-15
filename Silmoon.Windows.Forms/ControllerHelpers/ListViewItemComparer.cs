using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Forms.ControllerHelpers
{
    public class ListViewItemComparer : IComparer
    {
        private int column;
        public bool Ascending { get; private set; }

        public ListViewItemComparer(int column, bool ascending)
        {
            this.column = column;
            this.Ascending = ascending;
        }

        public int Compare(object x, object y)
        {
            ListViewItem itemX = x as ListViewItem;
            ListViewItem itemY = y as ListViewItem;

            string textX = itemX.SubItems[column].Text;
            string textY = itemY.SubItems[column].Text;

            int result = string.Compare(textX, textY);

            return Ascending ? result : -result;
        }
    }
}
