﻿using Silmoon.Threading;
using Silmoon.Windows.Forms.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silmoon.Windows.Forms
{
    public partial class ScrollForm : Form
    {
        int fromW = 0;
        int fromH = 0;
        Point location = new Point(0, 0);
        bool realClose = false;
        bool extCenter = false;
        int extToW = 0;
        int extToH = 0;

        ThreadStart closeProc = null;
        ThreadStart hideProc = null;
        public GenieExtension Genie = null;

        public ScrollForm()
        {
            InitializeComponent();
            closeProc = _close_mix_thread_proc;
            hideProc = _hide_mix_thread_proc;
            Genie = new GenieExtension(this);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WindowCloseStyle CloseStyle
        {
            get
            {
                if (closeProc == _close_max_thread_proc)
                    return WindowCloseStyle.MaxZoomFadeOut;
                else if (closeProc == _close_mix_thread_proc)
                    return WindowCloseStyle.MinZoomFadeOut;
                else
                    return WindowCloseStyle.None;
            }
            set
            {
                switch (value)
                {
                    case WindowCloseStyle.MaxZoomFadeOut:
                        closeProc = _close_max_thread_proc;
                        break;
                    case WindowCloseStyle.MinZoomFadeOut:
                        closeProc = _close_mix_thread_proc;
                        break;
                    default:
                        break;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public WindowCloseStyle HideStyle
        {
            get
            {
                if (hideProc == _close_max_thread_proc)
                    return WindowCloseStyle.MaxZoomFadeOut;
                else if (hideProc == _close_mix_thread_proc)
                    return WindowCloseStyle.MinZoomFadeOut;
                else
                    return WindowCloseStyle.None;
            }
            set
            {
                switch (value)
                {
                    case WindowCloseStyle.MaxZoomFadeOut:
                        hideProc = _close_max_thread_proc;
                        break;
                    case WindowCloseStyle.MinZoomFadeOut:
                        hideProc = _close_mix_thread_proc;
                        break;
                    default:
                        break;
                }
            }
        }

        void _close_mix_thread_proc()
        {
            bool complete = false;
            while (!complete)
            {
                this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
                {
                    if (this.Height > 50)
                    {
                        this.Size = new Size(this.Width, this.Height - 18);
                        this.Location = new Point(this.Location.X, this.Location.Y + 9);
                        Opacity = Opacity - 0.03;
                    }
                    else if (this.Width > 150)
                    {
                        this.Size = new Size(this.Width - 18, this.Height);
                        this.Location = new Point(this.Location.X + 9, this.Location.Y);
                        Opacity = Opacity - 0.05;
                    }
                    else
                    {
                        this.Width = fromW;
                        this.Height = fromH;
                        this.Location = new Point(location.X, location.Y);
                        complete = true;
                    }
                }));
                Thread.Sleep(3);
            }
            this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
            {
                Close();
            }));
        }
        void _close_max_thread_proc()
        {
            for (int i = 0; i < 50; i++)
            {
                if (this.IsDisposed) break;
                this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
                {
                    this.Size = new Size(this.Width + 18, this.Height + 18);
                    this.Location = new Point(this.Location.X - 9, this.Location.Y - 9);
                    Opacity = Opacity - 0.05;
                }));
            }
            this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
            {
                this.Width = fromW;
                this.Height = fromH;
                this.Location = new Point(location.X, location.Y);

                Close();
            }));
        }

        void _start_scroll_thread_proc()
        {
            Thread.Sleep(10);
            bool complete1 = false;
            bool complete2 = false;
            while (!(complete1 && complete2) && !this.IsDisposed)
            {
                try
                {
                    this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
                    {
                        if (this.Width < fromW)
                            this.Width += 40;
                        else
                        {
                            this.Width = fromW;
                            complete1 = true;
                        }
                        if (this.Opacity != 1)
                            this.Opacity += 0.02;
                        else complete2 = true;
                    }));
                }
                catch { return; }
                Thread.Sleep(10);
            }
        }

        void _hide_mix_thread_proc()
        {
            if (this.IsDisposed) return;

            bool complete = false;
            while (!complete)
            {
                this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
                {
                    if (this.Height > 50)
                    {
                        this.Size = new Size(this.Width, this.Height - 18);
                        this.Location = new Point(this.Location.X, this.Location.Y + 9);
                        Opacity = Opacity - 0.03;
                    }
                    else if (this.Width > 150)
                    {
                        this.Size = new Size(this.Width - 18, this.Height);
                        this.Location = new Point(this.Location.X + 9, this.Location.Y);
                        Opacity = Opacity - 0.05;
                    }
                    else
                    {
                        this.Width = fromW;
                        this.Height = fromH;
                        this.Location = new Point(location.X, location.Y);
                        complete = true;
                    }
                }));
                Thread.Sleep(3);
            }
            this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
            {
                this.Opacity = 0;
                Hide();
            }));
        }
        void _hide_max_thread_proc()
        {
            if (this.IsDisposed) return;

            for (int i = 0; i < 50; i++)
            {
                this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
                {
                    this.Size = new Size(this.Width + 18, this.Height + 18);
                    this.Location = new Point(this.Location.X - 9, this.Location.Y - 9);
                    Opacity = Opacity - 0.05;
                }));
            }
            this.Width = fromW;
            this.Height = fromH;
            this.Location = new Point(location.X, location.Y);

            this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
            {
                this.Opacity = 0;
                Hide();
            }));
        }

        void _resizeW_scroll_thread_proc()
        {
            bool complete = false;
            while (!complete)
            {
                if (this.Height == extToW) return;
                this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
                {
                    if (this.Height - extToW < 0)
                    {
                        this.Height = this.Height + 10;
                        if (this.Height > extToW)
                        {
                            this.Height = extToW;
                            complete = true;
                        }
                        if (extCenter)
                            this.Location = new Point(this.Location.X, this.Location.Y - 5);
                    }
                    else
                    {
                        int abs = Math.Abs(extToW);
                        this.Height = this.Height - 10;
                        if (this.Height < abs)
                        {
                            this.Height = abs;
                            complete = true;
                        }
                        if (extCenter)
                            this.Location = new Point(this.Location.X, this.Location.Y + 5);
                    }
                }));
                Thread.Sleep(5);
            }
        }
        void _resizeH_scroll_thread_proc()
        {
            bool complete = false;
            while (!complete)
            {
                if (this.Width == extToH) return;
                this.Invoke(new EventHandler(delegate (object sender1, EventArgs e1)
                {
                    if (this.Width - extToH < 0)
                    {
                        this.Width = this.Width + 10;
                        if (this.Width > extToH)
                        {
                            this.Width = extToH;
                            complete = true;
                        }
                        if (extCenter)
                            this.Location = new Point(this.Location.X - 5, this.Location.Y);
                    }
                    else
                    {
                        int abs = Math.Abs(extToH);
                        this.Width = this.Width - 10;
                        if (this.Width < abs)
                        {
                            this.Width = abs;
                            complete = true;
                        }
                        if (extCenter)
                            this.Location = new Point(this.Location.X + 5, this.Location.Y);
                    }
                }));
                Thread.Sleep(5);
            }
        }

        FormClosingEventArgs closeArgs;

        protected override void OnLoad(EventArgs e)
        {
            refreshStateParam();
            base.OnLoad(e);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CloseStyle != WindowCloseStyle.None)
            {
                if (!realClose)
                {
                    realClose = !realClose;
                    if (this.Height > 50)
                    {
                        closeArgs = e;
                        e.Cancel = true;
                        ThreadHelper.ExecAsync(closeProc);
                        this.Text = "";
                        if (this.WindowState == FormWindowState.Maximized)
                            this.WindowState = FormWindowState.Normal;
                    }
                    else
                        e.Cancel = false;
                }
            }
            base.OnFormClosing(e);
        }

        public virtual void ShowEx()
        {
            this.Show();
            this.Opacity = 0;
            this.Width = 1;
            this.Visible = true;
            ThreadHelper.ExecAsync(_start_scroll_thread_proc);
        }
        public virtual void HideEx()
        {
            refreshStateParam();
            ThreadHelper.ExecAsync(hideProc);
        }
        public void SetHeightEx(int newHeight)
        {
            if (Height == newHeight) return;
            SetHeightEx(newHeight, false);
        }
        public void SetHeightEx(int newHeight, bool center)
        {
            if (Height == newHeight) return;
            extToW = newHeight;
            extCenter = center;
            ThreadHelper.ExecAsync(_resizeW_scroll_thread_proc);
        }
        public void SetWidthEx(int newWidth)
        {
            if (Width == newWidth) return;
            SetWidthEx(newWidth, false);
        }
        public void SetWidthEx(int newWidth, bool center)
        {
            if (Width == newWidth) return;
            extToH = newWidth;
            extCenter = center;
            ThreadHelper.ExecAsync(_resizeH_scroll_thread_proc);
        }

        void refreshStateParam()
        {
            fromW = this.Width;
            fromH = this.Height;
            location = new Point(this.Location.X, this.Location.Y);
        }

        public enum WindowCloseStyle
        {
            MaxZoomFadeOut, MinZoomFadeOut, None
        }
    }
}
