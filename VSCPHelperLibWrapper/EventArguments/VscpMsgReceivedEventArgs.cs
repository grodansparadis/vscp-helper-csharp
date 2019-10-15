using System;
using VscpHelperLibWrapper.DataTypes;

namespace VscpHelperLibWrapper.EventArguments
{
    public class VscpMsgReceivedEventArgs : EventArgs
    {
        public VscpEventStruct Data { get; set; }

        public VscpMsgReceivedEventArgs(VscpEventStruct data)
        {
            Data = data;
        }
    }
}
