using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using VscpHelperLibWrapper.DataTypes;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.Interfaces;

namespace VscpSimulator
{
	public class ReceiverSim : IVscpEventReceiver
	{
		public event EventHandler<VscpMsgReceivedEventArgs> VscpMsgReceivedEvent;

		private Timer _receiveTimer = new Timer(1000);

		public ReceiverSim()
		{
			_receiveTimer.Elapsed += ReceiveTimerOnElapsed;
		}

		public bool UseBlockingReceiver { get; set; }

		private void ReceiveTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			VscpEventStruct vscpEvent = GetNextEvent(true, false);
			OnMessageReceived(vscpEvent);
		}

		public void StartReceiving()
		{
			_receiveTimer.Start();
		}

		public void StopReceiving()
		{
			_receiveTimer.Stop();
		}

		public VscpEventStruct GetNextEvent(bool suppressError, bool useBlocking)
		{
			VscpEventStruct data = new VscpEventStruct();
			data.crc = 0x23;
			data.guid = new byte[] { 0x01, 0x02 };
			data.head = 0x45;
			data.vscp_class = 0x01;
			data.vscp_type = 0x02;

			return data;
		}

		private void OnMessageReceived(VscpEventStruct data)
		{
			EventHandler<VscpMsgReceivedEventArgs> handler = VscpMsgReceivedEvent;
			if (handler != null)
			{
				handler.Invoke(this, new VscpMsgReceivedEventArgs(data));
			}
		}

		public bool IsReceiveLoopStarted
        {
            get { throw new NotImplementedException(); }
        }

		public VscpEventExStruct GetNextEventEx(bool suppressError)
		{
			throw new NotImplementedException();
		}

		#region Dispose
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_receiveTimer != null)
				{
					_receiveTimer.Stop();
					_receiveTimer.Dispose();
					_receiveTimer = null;
				}
			}
		}
		#endregion
	}
}
