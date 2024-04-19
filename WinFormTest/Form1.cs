using Silmoon.Windows.Forms;
using Silmoon.Windows.Forms.Extensions;

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

        }
    }
}
