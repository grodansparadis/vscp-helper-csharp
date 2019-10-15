using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VSCPHelperLibWrapper
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VscpEventEx
    {
        public UInt16 crc;             // crc checksum - currently only used for UDP and RF
        
        // Following two are for daemon internal use        
        public UInt32 obid;            // Used by driver for channel info etc.

        public UInt32  timestamp;             // Relative time stamp for package in microseconds.

        // CRC should be calculated from
        // here to end + datablock
        public byte head;         // bit 7,6,5 prioriy => Priority 0-7 where 0 is highest.
                                  // bit 4 = hardcoded, true for a hardcoded device.
                                  // bit 3 = Dont calculate CRC, Set to zero to use CRC.
                                  // bit 2 = Set means this is CAN message.
                                  // bit 1 = If bit 2 is set; Extended CAN message if set
                                  // bit 0 = If bit 2 is set: Remote frame if set

        public UInt16  vscp_class;            // VSCP class

        public UInt16  vscp_type;             // VSCP type

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] guid;            // Node globally unique id LSB(0) -> MSB(15

        public UInt16  sizeData;              // Number of valid data bytes		
        
        public byte[]  data;   // Pointer to data. Max 487 (512- 25) bytes

        // http://www.vscp.org/docs/vscphelper/doku.php?id=helper_lib_api_communication
    }
}
