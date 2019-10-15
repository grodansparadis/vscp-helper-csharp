using System;
using System.Runtime.InteropServices;
using System.Text;
using VscpHelperLibWrapper.DataTypes;

namespace VscpHelperLibWrapper
{
    class NativeMethods
    {
        #region Session and Connection
        /// <summary>
        /// Opens a new VSCP session
        /// </summary>
        /// <returns></returns>
        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern long vscphlp_newSession();

        /// <summary>
        /// Closes a VSCP session
        /// </summary>
        /// <param name="handle"></param>
        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern void vscphlp_closeSession(long handle);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int vscphlp_open(long handle, 
            [MarshalAs(UnmanagedType.LPStr)] string pHostname, 
            [MarshalAs(UnmanagedType.LPStr)] string pUsername, 
            [MarshalAs(UnmanagedType.LPStr)] string pPassword);

		/// <summary>
		/// Closes the interface
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_close(long handle);

        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_setResponseTimeout(long handle, ulong timeout);

        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_isConnected(long handle);
        #endregion

        #region Version Info
        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_getDLLVersion(long handle, out long pVersion);


        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_getVendorString(long handle, byte[] pVendorStr, int size);


        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_getDriverInfo(long handle, byte[] pVendorStr, int size);
        #endregion

        #region Daemon events
        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_clearDaemonEventQueue(long handle);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_sendEvent(long handle, ref VscpEventStruct pEvent);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_sendEventEx(long handle, ref VscpEventExStruct pEvent);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_receiveEvent(long handle, out VscpEventStruct pEvent);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_receiveEventEx(long handle, out VscpEventExStruct pEvent);

        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_blockingReceiveEvent(long handle, out VscpEventStruct pEvent );

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_blockingReceiveEventEx(long handle, out VscpEventExStruct pEvent );

        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_enterReceiveLoop(long handle);

        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_quitReceiveLoop(long handle);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int vscphlp_isDataAvailable(long handle, out int pCount);

        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern void vscphlp_deleteVSCPevent(ref VscpEventStruct pEvent);

        #endregion

        #region Helper methods
        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode, BestFitMapping = false, ThrowOnUnmappableChar = true) ]
        public static extern long vscphlp_readStringValue([MarshalAs(UnmanagedType.LPStr)] string pStrValue);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_getDateStringFromEvent(byte[] buf, int size, ref VscpEventStruct pEvent); // previous: IntPtr buf_len

		[DllImport("vscphelper.dll", SetLastError = true)]
        public static extern ulong vscphlp_makeTimeStamp();

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_convertVSCPtoEx(out VscpEventExStruct pEventEx, ref VscpEventStruct pEvent);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_convertVSCPfromEx(out VscpEventStruct pEvent, ref VscpEventExStruct pEventEx);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_writeVscpDataToString(ref VscpEventStruct pEvent, byte[] pstr, int size, int bUseHtmlBreak);

        [DllImport("vscphelper.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int vscphlp_writeVscpEventToString(ref VscpEventStruct pEvent, byte[] pstr, int size);
        #endregion

        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_noop(long handle);

        [DllImport("vscphelper.dll", SetLastError = true)]
        public static extern int vscphlp_setAfterCommandSleep(long handle, ushort sleeptime);
    }
}
