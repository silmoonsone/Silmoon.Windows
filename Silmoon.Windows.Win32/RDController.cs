using Silmoon.Threading;
using Silmoon.Windows.Win32.Apis;
using Silmoon.Windows.Win32.EnumDefined;
using Silmoon.Windows.Win32.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Win32
{
    public class RDController
    {
        public event SessionChanged OnSessionChanged;
        private MessageForm messageForm = null;
        private Thread MessageThread = null;

        /// <summary>
        /// 获取TS用户回话列表
        /// </summary>
        /// <returns></returns>
        public static List<LogonUser> GetLogonUserList()
        {
            List<LogonUser> LogonUsers = null;
            #region 查询代码
            WTS_SESSION_INFO[] pSessionInfo = Wtsapi32.SessionEnumeration();
            LogonUser cum = null;
            LogonUsers = [];
            for (int i = 0; i < pSessionInfo.Length; i++)
            {
                if ("RDP-Tcp" != pSessionInfo[i].pWinStationName)
                {
                    try
                    {
                        int count = 0;
                        IntPtr buffer = IntPtr.Zero;
                        StringBuilder userName = new StringBuilder();
                        StringBuilder clientUser = new StringBuilder();
                        StringBuilder stateType = new StringBuilder();
                        byte[] protocalType = new byte[2];
                        byte[] connState = new byte[1];
                        StringBuilder clientAddress = new StringBuilder();

                        bool userNameBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, pSessionInfo[i].SessionID, WTSInfoClass.WTSUserName, out userName, out count);
                        bool clientUserBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, pSessionInfo[i].SessionID, WTSInfoClass.WTSClientName, out clientUser, out count);
                        bool stateTypeBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, pSessionInfo[i].SessionID, WTSInfoClass.WTSWinStationName, out stateType, out count);
                        bool protocalTypeBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, pSessionInfo[i].SessionID, WTSInfoClass.WTSClientProtocolType, out protocalType, out count);
                        bool connStateBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, pSessionInfo[i].SessionID, WTSInfoClass.WTSConnectState, out connState, out count);
                        bool clientAddressBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, pSessionInfo[i].SessionID, WTSInfoClass.WTSClientAddress, out clientAddress, out count);

                        if (userNameBool && clientUserBool && stateTypeBool & connStateBool)
                        {
                            cum = new LogonUser();
                            cum.SessionId = pSessionInfo[i].SessionID;
                            cum.UserName = userName.ToString();
                            cum.ClientUserName = clientUser.ToString();
                            cum.SessionType = stateType.ToString();
                            cum.ProtocalType = (LogonUser.ClientProtocalType)((int)protocalType[0]);
                            cum.ConnectState = (WTS_CONNECTSTATE_CLASS)connState[0];

                            //WTS_CLIENT_ADDRESS ad = new WTS_CLIENT_ADDRESS();


                            //var aa = clientAddress[1];
                        }
                        LogonUsers.Add(cum);
                    }
                    catch
                    {

                    }
                }
            }
            #endregion
            return LogonUsers;
        }

        public bool Disconnect(int sessionid, bool wait = false)
        {
            return Wtsapi32.WTSDisconnectSession(IntPtr.Zero, sessionid, wait);
        }
        public bool Logoff(int sessionid, bool wait = false)
        {
            return Wtsapi32.WTSLogoffSession(IntPtr.Zero, sessionid, wait);
        }
        public bool RegisterSessionChangedEvent()
        {
            if (messageForm != null) return false;
            MessageThread = ThreadHelper.ExecAsync(() =>
            {
                messageForm = new MessageForm(this);
                messageForm.Start();
                Application.Run();
            });
            return true;
        }
        public bool UnRegisterSessionChangedEvent()
        {
            try
            {
                if (messageForm != null)
                {
                    messageForm.Invoke(new ThreadStart(() =>
                    {
                        messageForm.Stop();
                    }));
                    messageForm.Close();
                    messageForm.Dispose();
                    messageForm = null;
                }
            }
            catch { }

            try
            {
                if (MessageThread != null && MessageThread.ThreadState == ThreadState.Running)
                {
                    MessageThread.Interrupt();
                    MessageThread = null;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public LogonUser GetUserInfo(int sessionid)
        {
            try
            {
                int count = 0;
                IntPtr buffer = IntPtr.Zero;
                StringBuilder userName = new StringBuilder();
                StringBuilder clientUser = new StringBuilder();
                StringBuilder stateType = new StringBuilder();
                byte[] protocalType = new byte[2];
                byte[] connState = new byte[1];
                StringBuilder clientAddress = new StringBuilder();

                bool userNameBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, sessionid, WTSInfoClass.WTSUserName, out userName, out count);
                bool clientUserBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, sessionid, WTSInfoClass.WTSClientName, out clientUser, out count);
                bool stateTypeBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, sessionid, WTSInfoClass.WTSWinStationName, out stateType, out count);
                bool protocalTypeBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, sessionid, WTSInfoClass.WTSClientProtocolType, out protocalType, out count);
                bool connStateBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, sessionid, WTSInfoClass.WTSConnectState, out connState, out count);
                bool clientAddressBool = Wtsapi32.WTSQuerySessionInformation(IntPtr.Zero, sessionid, WTSInfoClass.WTSClientAddress, out clientAddress, out count);

                if (userNameBool && clientUserBool && stateTypeBool & connStateBool)
                {
                    LogonUser user = new LogonUser();
                    user.SessionId = sessionid;
                    user.UserName = userName.ToString();
                    user.ClientUserName = clientUser.ToString();
                    user.SessionType = stateType.ToString();
                    user.ProtocalType = (LogonUser.ClientProtocalType)((int)protocalType[0]);
                    user.ConnectState = (WTS_CONNECTSTATE_CLASS)connState[0];
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public bool TSSendMessage(int sessionid, string title, string message, MessageBoxButtons buttons, int stimeout, bool wait)
        {
            int resp = 7;
            return Wtsapi32.WTSSendMessage(IntPtr.Zero, sessionid, title, title.Length, message, message.Length, buttons, stimeout, out resp, wait);
        }

        void onSessionChanged(int sessionid, WM_WTSSESSION_CHANGE change) => OnSessionChanged?.Invoke(sessionid, change);



        private class MessageForm : Form
        {
            RDController RDController { get; set; } = null;
            public MessageForm(RDController rDController)
            {
                RDController = rDController;
            }
            protected override void WndProc(ref Message m)
            {
                try
                {
                    if (m.Msg == 0x02B1)
                    {
                        int sessionid = (int)m.LParam;
                        WM_WTSSESSION_CHANGE para2 = (WM_WTSSESSION_CHANGE)m.WParam;
                        ThreadHelper.ExecAsync(() =>
                        {
                            RDController.onSessionChanged(sessionid, para2);
                        });
                    }
                }
                finally
                {
                    base.WndProc(ref m);
                }
            }

            public void Start() => Wtsapi32.WTSRegisterSessionNotification(Handle, 1);
            public void Stop() => Wtsapi32.WTSUnRegisterSessionNotification(Handle);
        }
    }
    public delegate void SessionChanged(int sessionid, WM_WTSSESSION_CHANGE change);
    public class LogonUser
    {
        #region 用户信息字段
        private int sessionId;

        private string userName;
        private string clientUserName;
        private string sessionType;
        private ClientProtocalType protocalType;
        private WTS_CONNECTSTATE_CLASS connectState;

        public int SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }
        /// <summary>
        /// 连接状态
        /// </summary>
        public WTS_CONNECTSTATE_CLASS ConnectState
        {
            get { return connectState; }
            set { connectState = value; }
        }
        /// <summary>
        /// 会话类型
        /// </summary>
        public string SessionType
        {
            get { return sessionType; }
            set { sessionType = value; }
        }
        /// <summary>
        /// 客户端名
        /// </summary>
        public string ClientUserName
        {
            get { return clientUserName; }
            set { clientUserName = value; }
        }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public ClientProtocalType ProtocalType
        {
            get { return protocalType; }
            set { protocalType = value; }
        }

        #endregion

        public enum ClientProtocalType
        {
            Console = 0,
            Other = 1,
            RDP = 2,
        }
    }

}
