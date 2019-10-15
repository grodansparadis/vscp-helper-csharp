using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VscpHelperLibWrapper.EventArguments;

namespace VscpHelperLibWrapper.Interfaces
{
	public interface IVscpLogging
	{
		event EventHandler<LogMessageEventArgs> LogMessageEvent;

		bool LogError(int errorCode, string message);

		bool LogVscpResult(int errorCode, string failMessage, string succesMessage);

		void LogMessage(string message, LogLevelEnum logLevel);

		void LogMessage(string message);

	}
}
