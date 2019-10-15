using System;

namespace VscpHelperLibWrapper.EventArguments
{
    public enum LogLevelEnum { Error, Warning, Info };

    public class LogMessageEventArgs : EventArgs
    {

        public string Message { get; set; }

        public LogLevelEnum LogLevel { get; set; }

        public LogMessageEventArgs(string message, LogLevelEnum logLevel)
        {
            Message = message;
            LogLevel = logLevel;
        }
    }
}
