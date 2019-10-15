using System;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.DataTypes;

namespace VscpHelperLibWrapper.Interfaces
{
    public interface IVscp : IDisposable
    {
        /// <summary>
        /// Subscribe when you want to receive log messages. 
        /// </summary>
        event EventHandler<LogMessageEventArgs> LogMessageEvent;

		IVscpEventReceiver Receiver { get;}

		IVscpConnection Connection { get; }
	}
}
