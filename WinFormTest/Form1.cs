using Silmoon.Windows.Forms;
using Silmoon.Windows.Forms.Extensions;
using Silmoon.Windows.Win32Api;

namespace WinFormTest
{
    public partial class Form1 : ScrollForm
    {
        public Form1()
        {
            InitializeComponent();
            CloseStyle = WindowCloseStyle.MixStyleExt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SetHeightEx(Height + 100, true);
            //Genie.ShowControl(progressBar1, true);
            var result = Win32ApiHelper.EnumWindows();
            ///filter WindowName is empty
            
            var list = result.Where(x => !string.IsNullOrEmpty(x.szWindowName) && x.szWindowName != "Default IME").ToList();
        }
    }
}
