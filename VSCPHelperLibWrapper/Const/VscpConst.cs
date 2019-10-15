namespace VscpHelperLibWrapper.Const
{
    public class VscpConst
    {
        // Error Codes
        public const int VSCP_ERROR_SUCCESS = 0; // All is OK
        public const int VSCP_ERROR_ERROR = -1; // Error
        public const int VSCP_ERROR_CHANNEL = 7; // Invalid channel
        public const int VSCP_ERROR_FIFO_EMPTY = 8; // FIFO is empty
        public const int VSCP_ERROR_FIFO_FULL = 9; // FIFI is full
        public const int VSCP_ERROR_FIFO_SIZE = 10; // FIFO size error
        public const int VSCP_ERROR_FIFO_WAIT = 11;
        public const int VSCP_ERROR_GENERIC = 12; // Generic error
        public const int VSCP_ERROR_HARDWARE = 13; // Hardware error
        public const int VSCP_ERROR_INIT_FAIL = 14; // Initialization failed
        public const int VSCP_ERROR_INIT_MISSING = 15;
        public const int VSCP_ERROR_INIT_READY = 16;
        public const int VSCP_ERROR_NOT_SUPPORTED = 17; // Not supported
        public const int VSCP_ERROR_OVERRUN = 18; // Overrun
        public const int VSCP_ERROR_RCV_EMPTY = 19; // Receive buffer empty
        public const int VSCP_ERROR_REGISTER = 20; // Register value error
        public const int VSCP_ERROR_TRM_FULL = 21; // Transmit buffer full
        public const int VSCP_ERROR_LIBRARY = 28; // Unable to load library
        public const int VSCP_ERROR_PROCADDRESS = 29; // Unable get library proc. address
        public const int VSCP_ERROR_ONLY_ONE_INSTANCE = 30; // Only one instance allowed
        public const int VSCP_ERROR_SUB_DRIVER = 31; // Problem with sub driver call
        public const int VSCP_ERROR_TIMEOUT = 32; // Time-out
        public const int VSCP_ERROR_NOT_OPEN = 33; // The device is not open.
        public const int VSCP_ERROR_PARAMETER = 34; // A parameter is invalid.
        public const int VSCP_ERROR_MEMORY = 35; // Memory exhausted.
        public const int VSCP_ERROR_INTERNAL = 36; // Some kind of internal program error
        public const int VSCP_ERROR_COMMUNICATION = 37; // Some kind of communication error
        public const int VSCP_ERROR_USER = 38; // Login error user name
        public const int VSCP_ERROR_PASSWORD = 39; // Login error password
        public const int VSCP_ERROR_CONNECTION = 40; // Could not connect   
        public const int VSCP_ERROR_INVALID_HANDLE = 41; // The handle is not valid
        public const int VSCP_ERROR_OPERATION_FAILED = 42; // Operation failed for some reason


        public const int VSCP_MAX_DATA = 487;
    }
}
