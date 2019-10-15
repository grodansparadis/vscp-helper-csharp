using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using VscpHelperLibWrapper.Enums;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.Interfaces;
using VscpHelperLibWrapper.DataTypes;
using VscpHelperLibWrapper;

namespace VscpSimulator
{
    public class Simulator : IVscp
    {
        #region Events
        public event EventHandler<LogMessageEventArgs> LogMessageEvent;
        #endregion

        #region Fields
		private IVscpEventReceiver _receiver = null;
		private IVscpConnection _connection = null;
		private IVscpLogging _logger = new VscpLogging();
		#endregion

		public Simulator()
        {
			_logger.LogMessageEvent += LoggerLogMessageEvent;

			_receiver = new ReceiverSim();
			_connection = new ConnectionSim(_logger);
		}

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

		public IVscpEventReceiver Receiver
		{
			get
			{
				return _receiver;
			}
		}

		public IVscpConnection Connection
		{
			get
			{
				return _connection;
			}
		}

        #region Dispose
        public void Dispose()
        {

        }
        #endregion
    }
}
