using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VscpHelperLibWrapper.Const;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.Interfaces;

namespace VscpHelperLibWrapper
{
	public class VscpLogging : IVscpLogging
	{
		public event EventHandler<LogMessageEventArgs> LogMessageEvent;

		/// <summary>
		/// Logs an error message if the result was not VSCP_ERROR_SUCCESS
		/// </summary>
		/// <param name="errorCode"></param>
		/// <param name="message"></param>
		/// <returns>True if no error. False if an error was logged.</returns>
		public bool LogError(int errorCode, string message)
		{
			return LogVscpResult(errorCode, message, string.Empty);
		}

		/// <summary>
		/// Logs an error message. If the result is VSCP_ERROR_SUCCESS: succesMessage is logged, otherwise failMessage is logged.
		/// </summary>
		/// <param name="errorCode">The error code. Used to determine which message is logged.</param>
		/// <param name="failMessage">Message in case of an error.</param>
		/// <param name="succesMessage">Message in case of succes</param>
		/// <returns>True iof no error. False if an error was logged.</returns>
		public bool LogVscpResult(int errorCode, string failMessage, string succesMessage)
		{
			if (errorCode != VscpConst.VSCP_ERROR_SUCCESS)
			{
				OnLogMessage(string.Format("{0} ErrCode: {1}", failMessage, errorCode), LogLevelEnum.Error);
				return false;
			}
			if (!string.IsNullOrEmpty(succesMessage))
			{
				OnLogMessage(succesMessage, LogLevelEnum.Info);
			}
			return true;
		}

		public void LogMessage(string message)
		{
			LogMessage(message, LogLevelEnum.Info);
		}

		public void LogMessage(string message, LogLevelEnum logLevel)
		{
			OnLogMessage(message, logLevel);
		}

		private void OnLogMessage(string message, LogLevelEnum logLevel)
		{
			EventHandler<LogMessageEventArgs> handler = LogMessageEvent;
			if (handler != null)
			{
				handler.Invoke(this, new LogMessageEventArgs(message, logLevel));
			}
		}
	}
}
