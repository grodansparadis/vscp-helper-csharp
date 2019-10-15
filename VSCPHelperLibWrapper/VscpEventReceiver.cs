using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VscpHelperLibWrapper.Const;
using VscpHelperLibWrapper.DataTypes;
using VscpHelperLibWrapper.Enums;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.Interfaces;

namespace VscpHelperLibWrapper
{
	public class VscpEventReceiver : IVscpEventReceiver
	{
		public event EventHandler<VscpMsgReceivedEventArgs> VscpMsgReceivedEvent;

		#region Fields
		private IVscpConnection _connection = null;
		private long _handleBlockingReceiver = -1;
		private Thread _workerThread = null;
		private AutoResetEvent _stopEvent = new AutoResetEvent(false);
		private bool _stopReceiveLoop = true;
		private IVscpLogging _logger = null;
		#endregion

		public VscpEventReceiver(IVscpConnection connection, IVscpLogging logger)
		{
            if (connection == null) { throw new ArgumentNullException("connection"); }
            if (logger == null) { throw new ArgumentNullException("logger"); }
            _connection = connection;
            _logger = logger;
			_connection.ConnectionStateChangedEvent += ConnectionStateChangedEvent;
		}

		private void ConnectionStateChangedEvent(object sender, ConnectionStateEventArgs e)
		{
			if (e.State == ConnectionStateEnum.Closing) // Closing chanel, stop everything...
			{
				StopReceiving();
			}
		}

		/// <summary>
		/// Indicates if the receiverloop (= thread) is started.
		/// </summary>
		public bool IsReceiveLoopStarted { get { return !_stopReceiveLoop; } }

		/// <summary>
		/// When a blocking receiver is used, a second chanel to the daemon will be opened for the blocking receiver.
		/// Otherwise the main channel will be polled for events.
		/// </summary>
		public bool UseBlockingReceiver { get; set; }

		public void StartReceiving()
		{
			if (_workerThread != null) { StopReceiving(); }
			_workerThread = new Thread(DoWorkReceiver);
			_stopReceiveLoop = false;
			_workerThread.Start();
		}

		public void StopReceiving()
		{
			if (_workerThread == null) { return; }
			_stopReceiveLoop = true;
			_stopEvent.Set();
			if (!_workerThread.Join(100)) // Carefull with the join! The GUI thread can be processing a message. This will result in a deadlock when joining the worker thread. 
			{
				_workerThread.Abort();
			}
		}

		private void DoWorkReceiver()
		{
			while (!_stopReceiveLoop)
			{
				if (!UseBlockingReceiver) // Are we blocking or polling?
				{
					if (_stopEvent.WaitOne(10)) { continue; }
				}
				else
				{
					NativeMethods.vscphlp_enterReceiveLoop(_connection.Handle);
				}
				VscpEventStruct eventData = GetNextEvent(true, UseBlockingReceiver);
				if (eventData.IsValid() && !_stopReceiveLoop) // Check if event holds real data.
				{
					OnMessageReceived(eventData);
				}
			}
		}

		public VscpEventStruct GetNextEvent(bool suppressError, bool useBlocking)
		{
			int result = VscpConst.VSCP_ERROR_ERROR;
			VscpEventStruct vscpEvent = new VscpEventStruct();
			vscpEvent.guid = new byte[16];
			vscpEvent.pdata = IntPtr.Zero;
			if (_connection.ConnectionState != ConnectionStateEnum.Started)
			{
				_logger.LogMessage("We are not connected.", LogLevelEnum.Error);
				return vscpEvent;
			}

			if (useBlocking)
			{
				result = NativeMethods.vscphlp_blockingReceiveEvent(_connection.Handle, out vscpEvent);
			}
			else
			{
				result = NativeMethods.vscphlp_receiveEvent(_connection.Handle, out vscpEvent);
			}
			if (!suppressError)
			{
				_logger.LogError(result, "ReceiveEvent error");
			}
			return vscpEvent;
		}

		public VscpEventStruct GetNextEvent(bool suppressError)
		{
			return GetNextEvent(suppressError, false);
		}

		private void OnMessageReceived(VscpEventStruct data)
		{
			EventHandler<VscpMsgReceivedEventArgs> handler = VscpMsgReceivedEvent;
			if ((handler != null) && (!_stopReceiveLoop))
			{
				handler.Invoke(this, new VscpMsgReceivedEventArgs(data));
			}
		}

		public VscpEventExStruct GetNextEventEx(bool suppressError)
		{
			VscpEventExStruct eventData = new VscpEventExStruct();
			if (_connection.ConnectionState != ConnectionStateEnum.Started)
			{
				_logger.LogMessage("We are not connected.", LogLevelEnum.Error);
				return eventData;
			}
			eventData.guid = new byte[16];
			var result = NativeMethods.vscphlp_receiveEventEx(_connection.Handle, out eventData);

			if (!suppressError)
			{
				_logger.LogError(result, "ReceiveEvent error");
			}
			return eventData;
		}

		#region IDisposable
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_stopEvent != null)
				{
					_stopEvent.Dispose();
					_stopEvent = null;
				}
			}
		}
		#endregion


	}
}
