using System;
using VscpHelperLibWrapper.Enums;
using VscpHelperLibWrapper.EventArguments;

namespace VscpHelperLibWrapper.Interfaces
{
    public interface IVscpConnection
    {

        /// <summary>
        /// Subscribe when you want to be notified when the connection (state) to the daemon is changed.
        /// </summary>
        event EventHandler<ConnectionStateEventArgs> ConnectionStateChangedEvent;

        void OpenConnection(string hostname, string user, string password);
        
        void CloseConnection();

        ConnectionStateEnum ConnectionState { get; }

        void AbortOpenConnection();
		long Handle { get; }
    }
}
