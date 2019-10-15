using System;
using VscpHelperLibWrapper.Enums;

namespace VscpHelperLibWrapper.EventArguments
{
    public class ConnectionStateEventArgs : EventArgs
    {
        public ConnectionStateEnum State { get; set; }

        public ConnectionStateEventArgs(ConnectionStateEnum state)
        {
            State = state;
        }
    }
}
