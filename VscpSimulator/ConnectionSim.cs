using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VscpHelperLibWrapper.Enums;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.Interfaces;

namespace VscpSimulator
{
	public class ConnectionSim : IVscpConnection
	{
		public event EventHandler<ConnectionStateEventArgs> ConnectionStateChangedEvent;

		private ConnectionStateEnum _connectionState = ConnectionStateEnum.Idle;

		private IVscpLogging _logger;

		public ConnectionSim(IVscpLogging logger)
		{
			_logger = logger;
		}

		private void OnConnectionStateChange(ConnectionStateEnum state)
		{
			if (ConnectionState == state) { return; } // Prevent false events.

			_connectionState = state;
			EventHandler<ConnectionStateEventArgs> handle = ConnectionStateChangedEvent;
			if (handle != null)
			{
				handle.Invoke(this, new ConnectionStateEventArgs(state));
			}
		}

		public void OpenConnection(string hostname, string user, string password)
		{
			OnConnectionStateChange(ConnectionStateEnum.Started);
			_logger.LogMessage(string.Format("Connection succesfull, session handle: simulator"), LogLevelEnum.Info);
		}

		public void CloseConnection()
		{
			OnConnectionStateChange(ConnectionStateEnum.Stopped);
			_logger.LogMessage("Connection closed", LogLevelEnum.Info);
		}

		public ConnectionStateEnum ConnectionState
		{
			get { return _connectionState; }
		}

		public void AbortOpenConnection()
		{
			CloseConnection();
			_logger.LogMessage("Connection aborted", LogLevelEnum.Info);
		}

		public long Handle
		{
			get { return -1; }
		}
	}
}
