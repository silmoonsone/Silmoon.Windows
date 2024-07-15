namespace Silmoon.Windows.Forms.Extensions
{
    public static class FormExtension
    {
        public static void SetCenterControl(this Form form, Control control, bool setXCenter = true, bool setYCenter = true)
        {
            // 计算窗体的中心点
            int centerX = form.ClientSize.Width / 2;
            int centerY = form.ClientSize.Height / 2;

            // 根据Panel的尺寸更新它的位置，以使其居中
            if (setXCenter)
                control.Left = centerX - control.Width / 2;
            if (setYCenter)
                control.Top = centerY - control.Height / 2;
        }
    }
}
