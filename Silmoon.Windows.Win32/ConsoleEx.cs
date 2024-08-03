using Silmoon.Windows.Win32.Apis;
using Silmoon.Windows.Win32.EnumDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Win32
{
    public sealed class ConsoleEx
    {
        public enum InputMode
        {
            LineInput = 0,
            EchoInput = 1,
        }
        public enum ConsoleColor
        {
            Black = 0,
            Blue = Const.FOREGROUND_BLUE,
            Green = Const.FOREGROUND_GREEN,

            SkyBlue = Const.FOREGROUND_BLUE + Const.FOREGROUND_GREEN,

            Red = Const.FOREGROUND_RED,
            Purple = Const.FOREGROUND_BLUE + Const.FOREGROUND_RED,
            Brown = Const.FOREGROUND_GREEN + Const.FOREGROUND_RED,
            White = Const.FOREGROUND_BLUE + Const.FOREGROUND_GREEN +
            Const.FOREGROUND_RED,
            Gray = Const.FOREGROUND_INTENSIFY,
            BlueForte = Const.FOREGROUND_BLUE + Const.FOREGROUND_INTENSIFY,
            GreenForte = Const.FOREGROUND_GREEN + Const.FOREGROUND_INTENSIFY,
            SkyBlueForte = Const.FOREGROUND_BLUE + Const.FOREGROUND_GREEN +
            Const.FOREGROUND_INTENSIFY,
            RedForte = Const.FOREGROUND_RED + Const.FOREGROUND_INTENSIFY,
            PurpleForte = Const.FOREGROUND_BLUE + Const.FOREGROUND_RED +
            Const.FOREGROUND_INTENSIFY,
            Yellow = Const.FOREGROUND_GREEN + Const.FOREGROUND_RED +
            Const.FOREGROUND_INTENSIFY,
            WhiteForte = Const.FOREGROUND_BLUE + Const.FOREGROUND_GREEN +
            Const.FOREGROUND_RED + Const.FOREGROUND_INTENSIFY

        }
        public enum CursorType
        {
            Off = 0,
            SingleLine = 1,
            Block = 2,
        }

        private IntPtr hConsoleIn;
        private IntPtr hConsoleOut;
        private CONSOLE_INFO conInfo;
        private CURSOR_INFO cursorInfo;
        private int backColor;
        private short backgroundAttrib;

        public string Title
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder(128);
                Kernel32.GetConsoleTitle(stringBuilder, 128);
                return stringBuilder.ToString();
            }
            set
            {
                Kernel32.SetConsoleTitle(value);
            }
        }
        public int Columns
        {
            get
            {
                return conInfo.MaxSize.x;
            }
        }
        public int Rows
        {
            get
            {
                return conInfo.MaxSize.y;
            }
        }
        public int CursorX
        {
            get
            {
                updateConsoleInfo();
                return conInfo.CursorPosition.x;
            }
        }
        public int CursorY
        {
            get
            {
                updateConsoleInfo();
                return conInfo.CursorPosition.y;
            }
        }
        public ConsoleEx()
        {
            Kernel32.AllocConsole();
            hConsoleIn = Kernel32.GetStdHandle(-10);
            hConsoleOut = Kernel32.GetStdHandle(-11);
            conInfo = new CONSOLE_INFO();
            updateConsoleInfo();
            cursorInfo = new CURSOR_INFO();
            SetCursorType(CursorType.SingleLine);
            backgroundAttrib = 7;
        }

        ~ConsoleEx()
        {
            //base.Finalize();
            //FreeConsole();
        }

        public static string PasswordInput()
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
            List<char> passArray = [];
            ConsoleKeyInfo cki = Console.ReadKey(true);
            string look = "~!@#$%^&*()/\\[]{}<>`";
            //int lookc = 0;
            while (cki.KeyChar != 13 && cki.KeyChar != 10)
            {
                if (cki.KeyChar == 8 || cki.KeyChar == 0)
                {
                    if (passArray.Count != 0)
                    {
                        passArray.RemoveAt(passArray.Count - 1);
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write("\0");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                }
                else
                {
                    passArray.Add(cki.KeyChar);
                    //if(lookc == look.Length)lookc = 0;
                    Console.Write(look[new Random(DateTime.Now.Millisecond).Next(0, look.Length - 1)]);
                    //Console.WriteLine ((int)cki.KeyChar);
                    //lookc++;
                }
                cki = Console.ReadKey(true);
            }
            Console.ResetColor();
            Console.WriteLine();
            return new string((char[])passArray.ToArray(), 0, passArray.Count);
        }
        public void SetMode(InputMode mode)
        {
            int i = 0;

            Kernel32.GetConsoleMode(hConsoleIn, ref i);
            if (mode == InputMode.EchoInput)
            {
                i &= -7;
            }
            else
            {
                i |= 6;
            }
            Kernel32.SetConsoleMode(hConsoleIn, i);
        }
        public void Clear()
        {
            int i = 0;
            COORD cOORD = new COORD();
            cOORD.x = 0;
            cOORD.y = 0;
            Kernel32.FillConsoleOutputCharacter(hConsoleOut, ' ', (short)(conInfo.MaxSize.x * conInfo.MaxSize.y), cOORD, ref i);
            Kernel32.FillConsoleOutputAttribute(hConsoleOut, backgroundAttrib, (short)(conInfo.MaxSize.x * conInfo.MaxSize.y), cOORD, ref i);
            MoveCursor(1, 1);
        }
        public void EchoInput(bool value)
        {
            int i = 0;

            Kernel32.GetConsoleMode(hConsoleIn, ref i);
            if (value)
            {
                i |= 4;
            }
            else
            {
                i &= -5;
            }
            Kernel32.SetConsoleMode(hConsoleIn, i);
        }
        public void SetColor(ConsoleColor foreColor, ConsoleColor backColor)
        {
            this.backColor = (int)backColor;
            SetColor(foreColor);
        }
        public void SetColor(ConsoleColor foreColor)
        {
            Kernel32.SetConsoleTextAttribute(hConsoleOut, (int)foreColor + 16 * backColor);
        }
        public void SetClsColor(ConsoleColor backColor)
        {
            backgroundAttrib = (short)((short)backColor * 16);
        }
        public void MoveCursor(int x, int y)
        {
            conInfo.CursorPosition.x = (short)(x - 1);
            conInfo.CursorPosition.y = (short)(y - 1);
            if (cursorInfo.Visible)
            {
                int i = conInfo.CursorPosition.x + conInfo.CursorPosition.y * 65536;
                Kernel32.SetConsoleCursorPosition(hConsoleOut, i);
            }
        }
        public void SetCursorType(CursorType newType)
        {
            switch (newType)
            {
                case CursorType.Block:
                    cursorInfo.Size = 100;
                    cursorInfo.Visible = true;
                    break;

                case CursorType.SingleLine:
                    cursorInfo.Size = 10;
                    cursorInfo.Visible = true;
                    break;

                case CursorType.Off:
                    cursorInfo.Size = 100;
                    cursorInfo.Visible = false;
                    break;
            }
            Kernel32.SetConsoleCursorInfo(hConsoleOut, ref cursorInfo);
            MoveCursor(conInfo.CursorPosition.x, conInfo.CursorPosition.y);
        }
        public void FreeConsole()
        {
            try
            {
                Kernel32.FreeConsole();
                Kernel32.CloseHandle(hConsoleIn);
                Kernel32.CloseHandle(hConsoleOut);
                Kernel32.FreeConsole();
            }
            catch
            {
            }
        }
        private void updateConsoleInfo()
        {
            Kernel32.GetConsoleScreenBufferInfo(hConsoleOut, ref conInfo);
        }
    }
}
