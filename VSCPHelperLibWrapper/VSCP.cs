using System;
using System.Threading;
using VscpHelperLibWrapper.Const;
using VscpHelperLibWrapper.Enums;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.Interfaces;
using VscpHelperLibWrapper.DataTypes;
using System.Text;

namespace VscpHelperLibWrapper
{
    public class Vscp : IVscp
    {
		#region Events        
		public event EventHandler<LogMessageEventArgs> LogMessageEvent;
        #endregion

        #region Fields
		private IVscpLogging _logger = new VscpLogging();
		private IVscpEventReceiver _receiver = null;
		#endregion

		public Vscp()
        {
			_logger.LogMessageEvent += LoggerLogMessageEvent;
			Connection = new VscpConnection(_logger);
		}

		#region Daemon
		public string GetVendorString()
		{
			return VscpHelper.GetVendorString(Connection.Handle);
		}
		#endregion

		#region Connection
		public IVscpConnection Connection { get; set; }

		#endregion

		#region Logging
		private void LoggerLogMessageEvent(object sender, LogMessageEventArgs e)
		{
			EventHandler<LogMessageEventArgs> handler = LogMessageEvent;
			if (handler != null)
			{
				handler.Invoke(this, new LogMessageEventArgs(e.Message, e.LogLevel));
			}
		}
		#endregion

		#region ReceivingMessages
		public IVscpEventReceiver Receiver
		{
			get
			{
				if (_receiver == null)
				{
					_receiver = new VscpEventReceiver(Connection, _logger);
				}
				return _receiver;
			}
		}

		public void StartReceiving()
        {
			if (_receiver == null)
			{
				_logger.LogMessage("Can not start receiving, not connected");
				return;
			}
			_receiver.StartReceiving();
        }

		public void StopReceiving()
		{
			if (_receiver == null) { return; }
			_receiver.StopReceiving();
		}
		#endregion

		#region Management
		public void ClearDaemonEventQueue()
        {
			_logger.LogVscpResult(NativeMethods.vscphlp_clearDaemonEventQueue(Connection.Handle), "Error while clearing event daemon queue", "Daemon queue cleared");
        }

        public long GetVersionInfo()
        {
            long pVersion;
			_logger.LogVscpResult(NativeMethods.vscphlp_getDLLVersion(Connection.Handle, out pVersion), "Error while retrieving version info from daemon", "Daemon version: " + pVersion);
            return pVersion;
        }

        public int IsDataAvailable()
        {
            int count;
			_logger.LogVscpResult(NativeMethods.vscphlp_isDataAvailable(Connection.Handle, out count), "Error while retrieving data available count", "Data available count: " + count);
            return count;
        }
        
        #endregion

        #region SendEvent

        public void SendEvent(VscpEventStruct vscpEvent)
        {
			_logger.LogVscpResult(NativeMethods.vscphlp_sendEvent(Connection.Handle, ref vscpEvent), "Send event failed", "Message send");
        }

        public void SendEventEx(VscpEventExStruct vscpEventEx)
        {
			_logger.LogVscpResult(NativeMethods.vscphlp_sendEventEx(Connection.Handle, ref vscpEventEx), "Send event failed", "Message send");
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
				Connection.AbortOpenConnection();
				if (_receiver != null)
				{
					_receiver.StopReceiving();
					_receiver.Dispose();
					_receiver = null;
				}
            }
        }
        #endregion
    }
}
