using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VscpHelperLibWrapper.Const;
using VscpHelperLibWrapper.DataTypes;

namespace VscpHelperLibWrapper
{
    public class VscpHelper
    {

        /// <summary>
        /// This method isn't implemented yet in the VSCPHelper lib.
        /// </summary>
        /// <param name="vscpEvent"></param>
        /// <returns>DateTime from the VSCP event</returns>
        public static DateTime GetDateTimeFromEvent(VscpEventStruct vscpEvent)
        {
            byte[] byteStr = new byte[120];
            IntPtr buf_len = IntPtr.Zero;

            NativeMethods.vscphlp_getDateStringFromEvent(byteStr, byteStr.Length, ref vscpEvent);
			string dateTimeStr = Encoding.ASCII.GetString(byteStr).Trim('\0');
			DateTime retVal;
			if (DateTime.TryParse(dateTimeStr.ToString(), out retVal))
            {
                retVal = DateTime.MinValue;
            }

            return retVal;
        }

        public static long ReadStringValue(string str)
        {
            return NativeMethods.vscphlp_readStringValue(str);
        }

        public static ulong MakeTimeStamp()
        {
            return NativeMethods.vscphlp_makeTimeStamp();
        }

        public static VscpEventExStruct ConvertToVscpEx(VscpEventStruct vscpEvent)
        {
            VscpEventExStruct retVal;

            int result = NativeMethods.vscphlp_convertVSCPtoEx(out retVal, ref vscpEvent);
            if (result != VscpConst.VSCP_ERROR_SUCCESS)
            {
                throw new Exception("Error while converting vscpEvent to vscpEventEx. Errorcode: " + result);
            }
            return retVal;
        }

        public static VscpEventStruct ConvertToVscp(VscpEventExStruct vscpEventEx)
        {
            VscpEventStruct retVal;

            int result = NativeMethods.vscphlp_convertVSCPfromEx(out retVal, ref vscpEventEx);
            if (result != VscpConst.VSCP_ERROR_SUCCESS)
            {
                throw new Exception("Error while converting vscpEventEx to vscpEvent. Errorcode: " + result);
            }
            return retVal;
        }

        public static string VscpDataToString(VscpEventStruct vscpEvent, bool useHtmlBreaks)
        {
			byte[] byteStr = new byte[120];
			int result = NativeMethods.vscphlp_writeVscpDataToString(ref vscpEvent, byteStr, byteStr.Length, useHtmlBreaks ? 1 : 0);
            if (result != VscpConst.VSCP_ERROR_SUCCESS)
            {
                throw new Exception("Error while converting vscp data to string. Errorcode: " + result);
            }

            return Encoding.ASCII.GetString(byteStr).Trim('\0');
		}

		public static string VscpEventToString(VscpEventStruct vscpEvent, bool useHtmlBreaks)
		{
			byte[] byteStr = new byte[120];
			int result = NativeMethods.vscphlp_writeVscpEventToString(ref vscpEvent, byteStr, byteStr.Length);
			if (result != VscpConst.VSCP_ERROR_SUCCESS)
			{
				throw new Exception("Error while converting vscp data to string. Errorcode: " + result);
			}

			return Encoding.ASCII.GetString(byteStr).Trim('\0');
		}

		public static string GetVendorString(long handle) // TODO: When Handle is no longer needed, remove it...
		{
			byte[] byteStr = new byte[120];
			NativeMethods.vscphlp_getVendorString(handle, byteStr, byteStr.Length);
			return Encoding.ASCII.GetString(byteStr).Trim('\0');
		}

    }
}
