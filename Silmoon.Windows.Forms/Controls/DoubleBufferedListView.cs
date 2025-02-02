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
    }
}
