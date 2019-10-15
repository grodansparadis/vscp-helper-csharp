using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VscpHelperLibWrapper.Enums;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.Interfaces;

namespace VscpHelperLibWrapper
{
	public class VscpConnection : IVscpConnection
	{
		public event EventHandler<ConnectionStateEventArgs> ConnectionStateChangedEvent;

		#region Fields
		private IVscpLogging _logger = null;
		private Thread _openConnectionThread = null;
		#endregion

		public long Handle { get; private set; }
		public VscpConnection(IVscpLogging logger)
		{
            if (logger == null) { throw new ArgumentNullException("logger"); }
            _logger = logger;
			Handle = -1;
			ConnectionState = ConnectionStateEnum.Idle;
		}

		public ConnectionStateEnum ConnectionState { get; private set; }

		private void OnConnectionStateChange(ConnectionStateEnum state)
		{
			if (ConnectionState == state) { return; } // Prevent false events.

			ConnectionState = state;
			EventHandler<ConnectionStateEventArgs> handle = ConnectionStateChangedEvent;
			if (handle != null)
			{
				handle.Invoke(this, new ConnectionStateEventArgs(state));
			}
		}

		public void AbortOpenConnection()
		{
			if (_openConnectionThread == null) { return; }

			_openConnectionThread.Abort();
			_openConnectionThread = null;
			OnConnectionStateChange(ConnectionStateEnum.Idle);
		}

		public void OpenConnection(string hostname, string user, string password)
		{
			if (_openConnectionThread != null)
			{
				AbortOpenConnection();
			}
			_openConnectionThread = new Thread(() => Open(hostname, user, password));
			_openConnectionThread.Name = "OpenSessionThread";
			_openConnectionThread.Start();
			OnConnectionStateChange(ConnectionStateEnum.Connecting);
		}

		private void Open(string hostname, string user, string password)
		{
			long handle = -1;

			if (Handle != -1)
			{
				_logger.LogMessage("Handle was not -1. Connection should allready be open.", LogLevelEnum.Warning);
				return;
			}

			try
			{
				handle = NativeMethods.vscphlp_newSession();
			}
			catch (DllNotFoundException ex)
			{
				_logger.LogMessage("Error dll not found: " + ex.Message, LogLevelEnum.Error);
				return;
			}

			_logger.LogError(NativeMethods.vscphlp_setResponseTimeout(handle, 1), "Set responsetime error.");
			_logger.LogError(NativeMethods.vscphlp_open(handle, hostname, user, password), "Open channel error."); // When there is no daemon running, this will be stuck for 30 seconds.
			_logger.LogError(NativeMethods.vscphlp_noop(handle), "Noop error.");

			if (_logger.LogError(NativeMethods.vscphlp_isConnected(handle), "No connection."))
			{
				Handle = handle;
				_logger.LogMessage(string.Format("Connection succesfull, session handle: {0}", handle), LogLevelEnum.Info);
			}
			else
			{
				_logger.LogMessage("Connection unsuccesfull", LogLevelEnum.Info);
				return;
			}
			OnConnectionStateChange(ConnectionStateEnum.Started);
		}

		public void CloseConnection()
		{
			if (Handle == -1) { return; }
			OnConnectionStateChange(ConnectionStateEnum.Closing);

			_logger.LogError(NativeMethods.vscphlp_close(Handle), "Error while closing.");
			NativeMethods.vscphlp_closeSession(Handle);
			OnConnectionStateChange(ConnectionStateEnum.Stopped);
			_logger.LogMessage("Connection closed", LogLevelEnum.Info);
			Handle = -1;
		}
	}
}
