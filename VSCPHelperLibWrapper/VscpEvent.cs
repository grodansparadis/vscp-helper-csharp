using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VSCPHelperLibWrapper
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VscpEvent
    {
        public UInt16 crc;             // crc checksum - currently only used for UDP and RF

        //[MarshalAs(UnmanagedType.LPArray)]
        public IntPtr pdata;            // Pointer to data. Max 487 (512- 25) bytes
        
        // Following two are for daemon internal use
        public UInt32 obid;            // Used by driver for channel info etc.
        public UInt32 timestamp;       // Relative time stamp for package in microseconds
        
        // CRC should be calculated from here to end + datablock
        public UInt16 head;            // Bit 16   GUID is IP v.6 address.
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
        
        public UInt16 sizeData;        // Number of valid data bytes	

        public bool IsValid()
        {
            if (vscp_type == 0x00)
            {
                return false;
            }
            return true;
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
