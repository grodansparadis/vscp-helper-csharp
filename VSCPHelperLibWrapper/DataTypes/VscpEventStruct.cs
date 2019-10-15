using System;
using System.Runtime.InteropServices;

namespace VscpHelperLibWrapper.DataTypes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VscpEventStruct
    {
        #region Fields
        public UInt16 crc;             // crc checksum - currently only used for UDP and RF

        //[MarshalAs(UnmanagedType.LPArray)]
        internal IntPtr pdata;            // Pointer to data. Max 487 (512- 25) bytes
        
        // Following two are for daemon internal use
        public UInt32 obid;            // Used by driver for channel info etc.
        public UInt32 timestamp;       // Relative time stamp for package in microseconds
        
        // CRC should be calculated from here to end + datablock
        public UInt16 head;     // Bit 16   GUID is IP v.6 address.
                                // bit 7, 6 & 5  priority, Priority 0-7 where 0 is highest.
                                // bit 4 = hard coded, true for a hard coded device.
                                // bit 3 = Don't calculate CRC, false for CRC usage.
                                // bit 2 = Reserved.
                                // bit 1 = Reserved.
                                // bit 0 = Reserved.

        public UInt16 vscp_class;      // VSCP class
        
        public UInt16 vscp_type;       // VSCP type

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] guid;            // Node globally unique id LSB(0) -> MSB(15
        
        internal UInt16 sizeData;        // Number of valid data bytes	

        #endregion

        public void SetData(byte[] data)
        {
            IntPtr pointer = IntPtr.Zero;
            sizeData = (ushort)data.Length;
            try
            {
                pointer = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, pointer, data.Length);
                pdata = pointer;
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }

		public string Class
		{
			get { return vscp_class.ToString(); }
		}

		public string Type
		{
			get { return vscp_type.ToString(); }
		}

		public UInt16 DataSize()
		{
			return sizeData;
		}

		public bool IsValid()
		{
			return vscp_type != 0x00;
		}

		public override string ToString()
        {
            if (guid == null)
            {
                return "Invalid message. GUID is null";
            }
            string msg = "VSCP Event -->  Class: " + vscp_class;
            msg += "  Type: " + vscp_type;
            msg += "  CRC: " + crc.ToString("X4");
            msg += "  GUID: ";
            for (int i = 0; i < guid.Length; i++)
            {
                byte b = guid[i];
                msg += b.ToString("X2") + ((i == guid.Length - 1) ? "" : ":");
            }

            msg += "  Data length: " + sizeData + "  Data: ";
            byte[] dataArray = new byte[sizeData];
            if ((pdata != IntPtr.Zero) && (sizeData < 500))
            {
                Marshal.Copy(pdata, dataArray, 0, sizeData);
                for (int i = 0; i < dataArray.Length; i++)
                {
                    byte b = dataArray[i];
                    msg += b.ToString("X2") + ((i == dataArray.Length - 1) ? "" : ":");
                }
            }
            return msg;
        }
    }
}
