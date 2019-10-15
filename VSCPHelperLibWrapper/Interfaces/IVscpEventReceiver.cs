using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VscpHelperLibWrapper.DataTypes;
using VscpHelperLibWrapper.EventArguments;

namespace VscpHelperLibWrapper.Interfaces
{
	public interface IVscpEventReceiver : IDisposable
	{
		event EventHandler<VscpMsgReceivedEventArgs> VscpMsgReceivedEvent;

		bool IsReceiveLoopStarted { get; }

		bool UseBlockingReceiver { get; set; }

		void StartReceiving();

		void StopReceiving();

		VscpEventStruct GetNextEvent(bool suppressError, bool useBlocking);

		VscpEventExStruct GetNextEventEx(bool suppressError);

	}
}
