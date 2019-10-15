using System;
using System.Windows.Forms;
using VscpHelperLibWrapper;
using VscpHelperLibWrapper.Enums;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.DataTypes;
using VscpHelperLibWrapper.Interfaces;

namespace TestGUI
{
    public partial class Form1 : Form
    {
        Vscp _vscp = null;

        public Form1()
        {
            InitializeComponent();

            _vscp = new Vscp();
            _vscp.LogMessageEvent += VSCP_LogMessageEvent;
            _vscp.Receiver.VscpMsgReceivedEvent += VscpMsgReceivedEvent;
            _vscp.Connection.ConnectionStateChangedEvent += VscpConnectionStateChangedEvent;

            VscpConnectionStateChangedEvent(this, new ConnectionStateEventArgs(ConnectionStateEnum.Stopped));
            //OpenSession(txtBxAddress.Text, txtBxUserName.Text, txtBxPassword.Text);

            //GetVersionInfo();
            //SendEvent();
            //VSCP.vscphlp_clearDaemonEventQueue(_handle);
        }

        private void VscpConnectionStateChangedEvent(object sender, ConnectionStateEventArgs e)
        {
            if (IsDisposed) { return; }

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { VscpConnectionStateChangedEvent(sender, e); }));
                return;
            }
            txtBxAddress.Enabled = e.State != ConnectionStateEnum.Started;
            txtBxPassword.Enabled = e.State != ConnectionStateEnum.Started;
            txtBxUserName.Enabled = e.State != ConnectionStateEnum.Started;
            
            groupBoxReceive.Enabled = e.State == ConnectionStateEnum.Started;
            groupBoxSend.Enabled = e.State == ConnectionStateEnum.Started;
            bttnClearDaemonEventQueue.Enabled = e.State == ConnectionStateEnum.Started;
            bttnVersionInfo.Enabled = e.State == ConnectionStateEnum.Started;

            lblConnectionState.Text = e.State != ConnectionStateEnum.Started ? "State: Not connected" : "State: Connected";
            bttnStartStopConnection.Text = e.State != ConnectionStateEnum.Started ? "Connect" : "Disconnect";
            if (e.State == ConnectionStateEnum.Connecting)
            {
                bttnStartStopConnection.Text = "Abort";
                lblConnectionState.Text = "State: Connecting";
            }
        }

        private void VscpMsgReceivedEvent(object sender, VscpMsgReceivedEventArgs e)
        {
            LogMsg(e.Data.ToString());
        }

        void VSCP_LogMessageEvent(object sender, LogMessageEventArgs e)
        {
            LogMsg(e.Message);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseSession();
        }

        private void OpenSession(string connection, string username, string password)
        {
            _vscp.Connection.OpenConnection(connection, username, password);
        }

        private void CloseSession()
        {
            _vscp.Receiver.StopReceiving();
            _vscp.Connection.CloseConnection();
        }

        #region Logging
        private void LogMsg(string format, params object[] list)
        {
            AppendTextBox(false, format, list);
        }

        private void AppendTextBox(bool includeTimestamp, string format, params object[] list)
        {
            if (IsDisposed) { return; }

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { LogMsg(format, includeTimestamp, list); }));
                return;
            }

            string msg = includeTimestamp ? DateTime.Now.ToString("HH:mm:ss ") : string.Empty;
            msg += String.Format(format, list);
            richTextBox1.AppendText(msg + Environment.NewLine);
        }

        #endregion

        private void bttnStartStopRec_Click(object sender, EventArgs e)
        {
            if (_vscp.Receiver.IsReceiveLoopStarted)
            {
                bttnStartStopRec.Text = "Start receiving";
                _vscp.Receiver.StopReceiving();

            }
            else
            {
                bttnStartStopRec.Text = "Stop receiving";
                _vscp.Receiver.StartReceiving();
            }
        }

        private void bttnReceiveEvent_Click(object sender, EventArgs e)
        {
            LogMsg(_vscp.Receiver.GetNextEvent(false, false).ToString());
        }

        private void bttnStartStopConnection_Click(object sender, EventArgs e)
        {
            if (_vscp.Connection.ConnectionState == ConnectionStateEnum.Connecting)
            {
                _vscp.Connection.AbortOpenConnection();
            }
            else if (_vscp.Connection.ConnectionState != ConnectionStateEnum.Started)
            {
                OpenSession(txtBxAddress.Text, txtBxUserName.Text, txtBxPassword.Text);
            }

            else
            {
                CloseSession();
            }
        }

        private void bttnSendEvent_Click(object sender, EventArgs e)
        {
            ushort vscpClass;
            ushort vscpType;
            bool succes = ushort.TryParse(txtBxClass.Text, out vscpClass);
            succes &= ushort.TryParse(txtBxType.Text, out vscpType);
            if (succes)
            {
                if (sender == bttnSendEventEx)
                {
                    SendEventEx(vscpClass, vscpType);
                }
                else
                {
                    SendEvent(vscpClass, vscpType);
                }
            }
            else
            {
                LogMsg("Class or Type can't be parsed to a ushort");
            }
        }

        private void SendEvent(ushort vscpClass, ushort vscpType)
        {
            VscpEventStruct vscpEvent = new VscpEventStruct();
            vscpEvent.crc = 0;
            vscpEvent.head = 0;
            vscpEvent.obid = 0;
            vscpEvent.vscp_class = vscpClass;
            vscpEvent.vscp_type = vscpType;

            byte[] data = { 0x01 };
            vscpEvent.SetData(data);
            _vscp.SendEvent(vscpEvent);
        }

        private void SendEventEx(ushort vscpClass, ushort vscpType)
        {
            VscpEventExStruct vscpEventEx = new VscpEventExStruct();
            vscpEventEx.crc = 0;
            vscpEventEx.head = 0;
            vscpEventEx.obid = 0;
            vscpEventEx.vscp_class = vscpClass;
            vscpEventEx.vscp_type = vscpType;

            byte[] data = { 0x01 };
            vscpEventEx.sizeData = (ushort)data.Length;
            vscpEventEx.data = data;
            vscpEventEx.guid = new byte[16];
            _vscp.SendEventEx(vscpEventEx);
        }

        private void bttnClearDaemonEventQueue_Click(object sender, EventArgs e)
        {
            _vscp.ClearDaemonEventQueue();
        }

        private void bttnVersionInfo_Click(object sender, EventArgs e)
        {
            _vscp.GetVersionInfo();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _vscp.Receiver.GetNextEventEx(false);
        }
    }
}
