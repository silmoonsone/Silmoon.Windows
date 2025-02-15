using Silmoon.Windows.Forms;
using Silmoon.Windows.Forms.Extensions;
using Silmoon.Windows.Win32;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //CloseStyle = WindowCloseStyle.MinZoomFadeOut;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ctlEnumAllWindowButton_Click(object sender, EventArgs e)
        {
            // SetHeightEx(Height + 100, true);
            // Genie.ShowControl(progressBar1, true);
            var result = Win32ApiHelper.EnumWindows();
            // filter WindowName is empty
            var list = result.Where(x => !string.IsNullOrEmpty(x.szWindowName) && x.szWindowName != "Default IME").ToList();
        }

        private void ctlListViewTestButton_Click(object sender, EventArgs e)
        {
            var listviewForm = new ListViewForm();
            listviewForm.FormClosed += (s, e) => Close();
            listviewForm.Show();
            Hide();
        }
    }
}
