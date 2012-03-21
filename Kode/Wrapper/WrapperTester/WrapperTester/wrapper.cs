/** \file wrapper.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Runtime.InteropServices;

namespace WrapperTester // Has to be changed
{
    /// <summary>
    /// Contains a wrapper for the C++ functions in the dll file(USBC.dll).
    /// 
    /// Good idea to check USBC-documentation.pdf.
    /// 
    /// Notes:  Uses IntPtr arg for different types of C++ pointers.
    ///         Same function names as C++ but has "Wrapped" at the end.
    /// 
    /// \todo Add behind factory class.
    /// </summary>
    class Wrapper
    {
        // Members
        // -Singleton related
        private static Wrapper wrapOnlyInstance;

        // -Events not used outside class
        private DgateCallBack callbackfuncSuccessful;
        private DgateCallBack callbackfuncError;

        /// \todo Change default event handling. Example output more useful info.
        #region Events
        // Some callback events placed here if not found useful in outside context.
        private static void eventSuccess(IntPtr intptrConfigData)
        {
            System.Console.WriteLine("Success.");
        } 
        private static void eventError(IntPtr intptrConfigData)
        {
            System.Console.WriteLine("Error.");
        }
        #endregion

        // Settings constants
        // -Initialization mode
        /// <summary>
        /// For mode in initialization. 
        ///
        /// (MODE_ONLINE is normally used)
        /// </summary>
        public enum enumSystemModes
        {
            MODE_DEFAULT = 0, // Last used mode
            MODE_ONLINE = 1, // Force online mode(Normally used)
            MODE_SIMULAT = 2 // Simulator mode
        }
        // -Initialization system type
        /// <summary>
        /// For type in initialization.
        /// 
        /// (SYSTEM_TYPE_DEFAULT normally used)
        /// </summary>
        public enum enumSystemTypes
        {
            SYSTEM_TYPE_DEFAULT = 0, // Detect it(Normally used)
            SYSTEM_TYPE_ER4USB = 41 // ER-4
        }
        // -Axis control settings
        /// <summary>
        /// For chosing axis group in certain functions.
        /// </summary>
        public enum enumAxisSettings
        {
            AXIS_ROBOT,
            AXIS_PERIPHERALS,
            AXIS_GRIPPER,
            AXIS_0,
            AXIS_1,
            AXIS_2,
            AXIS_3,
            AXIS_4,
            AXIS_5,
            AXIS_6,
            AXIS_7,
            AXIS_ALL
        }
        // -Manual movement
        /// <summary>
        /// For chosing type of movement when enabling manual movement.
        /// </summary>
        public enum enumManualType
        {
            MANUAL_TYPE_AXES,
            MANUAL_TYPE_COORD
        }
        /// <summary>
        /// For chosing what part to move when movin manually.
        /// 
        /// Note: Some used for moving by axes and some used for moving by coordinates.
        /// </summary>
        public enum enumManualModeWhat
        {
            MANUAL_MOVE_BASE, // Axes
            MANUAL_MOVE_SHOULDER,
            MANUAL_MOVE_ELBOW,
            MANUAL_MOVE_WRISTPITCH,
            MANUAL_MOVE_WRISTROLL,
            MANUAL_MOVE_GRIPPER,
            MANUAL_MOVE_CONVEYERBELT,
            MANUAL_MOVE_X, // Coordinates
            MANUAL_MOVE_Y,
            MANUAL_MOVE_Z,
            MANUAL_MOVE_PITCH,
            MANUAL_MOVE_ROLL
        }

        // Functions
        // -Constructors and destructors
        private Wrapper()
        {
            // Initializes callback functions
            callbackfuncSuccessful = new DgateCallBack(eventSuccess);
            callbackfuncError = new DgateCallBack(eventError);
        }


        // -Singleton related
        public static Wrapper getInstance()
        {
            if(wrapOnlyInstance == null)
            {
                wrapOnlyInstance = new Wrapper();
            }
            return (wrapOnlyInstance);
        }

        // -Wrapped functions
        #region Initialization and settings
        /// <summary>
        /// Initializes the robot.
        /// </summary>
        /// <param name="_shrtMode">Mode. For example simulator.(Use one of constants)</param>
        /// <param name="_shrtType">Type of connection.(Use one of constants)</param>
        /// <returns>Returns true on successful call.(But errors can still happen)</returns>
        public bool initializationWrapped(enumSystemModes _sysmodeMode, enumSystemTypes _systypeType)
        {
            int iReturnValue = initialization((short)_sysmodeMode, (short)_systypeType, callbackfuncSuccessful, callbackfuncError);
            return ((iReturnValue == 1)? true : false);
        }
        /// <summary>
        /// Turns control on and off for certain axis group.
        /// </summary>
        /// <param name="bAxis">Axis group to affect.(Use enum)</param>
        /// <param name="_bControlOnOrOff">To have it turned off or on.</param>
        /// <returns>Returns true on successful call.</returns>
        public bool controlWrapped(enumAxisSettings _axisSettingsGroup, bool _bControlOnOrOff) 
        { 
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = Control(bArg, _bControlOnOrOff); /// \warning Bool arg in unwrapped version.
            return ((iReturnValue == 1) ? true : false);
        }

        /// <summary>
        /// Tells about the robot being online.
        /// </summary>
        /// <returns>Returns true if it is, false otherwise.</returns>
        public bool isOnlineOkWrapped()
        {
            int iReturnValue;
            iReturnValue = IsOnLineOk();
            return ((iReturnValue == 1) ? true : false);
        }
        #endregion

        #region Movement
        /// <summary>
        /// Homes a axis group.
        /// Should be called before calling most movement functions.
        /// </summary>
        /// <param name="_axisSettingsGroup">The axis group.(Use enum)</param>
        /// <param name="_funcptrHomingEventHandler">Function to be called for homing events.
        /// 
        /// Values being passed in event:
        ///     0xff: Homing started
        ///     1 - 8: Axis n being homed.
        ///     0x40: Homing ended.</param>
        /// <returns>Returns true on successful call.</returns>
        public bool homeWrapped(enumAxisSettings _axisSettingsGroup, DgateCallBackCharArg _funcptrHomingEventHandler) 
        {
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = Home((byte) _axisSettingsGroup, _funcptrHomingEventHandler);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Must be called to use manual movement.  
        /// </summary>
        /// <param name="_enummanMoveType">What to move by.(Axis(0), Coordinates(1))</param>
        /// <returns>Returns true on successful call.</returns>
        public bool enterManualWrapped(enumManualType _enummanMoveType)
        {
            short shrtTmp;
            int iReturnValue;
            switch(_enummanMoveType)
            {
                case enumManualType.MANUAL_TYPE_AXES:
                    shrtTmp = 0;
                    break;
                case enumManualType.MANUAL_TYPE_COORD:
                    shrtTmp = 1;
                    break;
                default:
                    return (false);
            }
            iReturnValue = EnterManual(shrtTmp);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Stops manual mode.
        /// </summary>
        /// <returns>Returns true on successful call.</returns>
        public bool closeManualWrapped()
        {
            int iReturnValue;
            iReturnValue = CloseManual();
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Moves the robot.
        /// homeWrapped must have been called if moving by coordinates. 
        /// </summary>
        public bool moveManualWrapped(enumManualModeWhat _enumWhatToMove, int _lSpeed)
        {
            int iReturnValue;
            iReturnValue = MoveManual(manualMovementToByte(_enumWhatToMove), _lSpeed);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Stops movement of axis.
        /// </summary>
        /// <param name="_bWhatToStop">Axis to stop.</param>
        /// <returns>Returns true on successful call.</returns>
        public bool stopWrapped(enumAxisSettings _bWhatToStop) /// \todo Refactor.
        {
            int iReturnValue;
            iReturnValue = Stop(axisSettingsToByte(_bWhatToStop));
            return ((iReturnValue == 1) ? true : false);
        }


        #endregion

        #region Gripper
        /// <summary>
        /// Opens the gripper.
        /// </summary>
        /// <returns>Returns true on successful call.</returns>
        public bool openGripperWrapped()
        {
            int iReturnValue;
            iReturnValue = OpenGripper();
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Closes the gripper.
        /// </summary>
        /// <returns>Returns true on successful call.</returns>
        public bool closeGripperWrapped()
        {
            int iReturnValue;
            iReturnValue = CloseGripper();
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Gives information about how much open the gripper is.(Between the 'fingers')
        /// 
        /// Note: Probably most useful to use the _shrtWidth arg.
        /// </summary>
        /// <param name="_shrtPerc">Data in percentage.</param>
        /// <param name="_shrtWidth">Data in width.(mm)</param>
        /// <returns>Returns true on successful call.</returns>
        public bool getJawWrapped(ref short _shrtPerc, ref short _shrtWidth)
        {
            int iReturnValue;
            iReturnValue = GetJaw(ref _shrtPerc, ref _shrtWidth);
            return ((iReturnValue == 1) ? true : false);
        }
        #endregion

        #region Event handling
        /// <summary>
        /// Adds functions to be called when motion starts and motion ends. 
        /// 
        /// Note: Ignoring return value.
        /// </summary>
        /// <param name="_funcptrCallbackEnd">Function to be called when motion has ended.</param>
        /// <param name="_funcptrCallbackStart">Function to be called when motion has started.</param>
        public void watchMotionWrapped(DgateCallBackCharArg _funcptrCallbackEnd, DgateCallBackCharArg _funcptrCallbackStart)
        {
            WatchMotion(_funcptrCallbackEnd, _funcptrCallbackStart);
        }

        /// <summary>
        /// Adds a function to be called when digital input changes.
        /// </summary>
        /// <param name="_funcptrCallbackEvent">The function to be called.</param>
        /// <returns>Returns true if successful call.</returns>
        public bool watchDigitalInputWrapped(DgateCallBackLongArg _funcptrCallbackEvent)
        {
            int iReturnValue;
            iReturnValue = WatchDigitalInput(_funcptrCallbackEvent);
            return ((iReturnValue == 1) ? true : false);
        }

        /// <summary>
        /// Stops watching of digital inputs.
        /// 
        /// Note: Probably means no more events.
        /// </summary>
        /// <returns>Returns true if successful call.</returns>
        public bool closeWatchDigitalInputWrapped()
        {
            int iReturnValue;
            iReturnValue = CloseWatchDigitalInput();
            return ((iReturnValue == 1) ? true : false);
        }
        #endregion


        #region Imported references(Should use wrapped versions)
        // -Function pointers
        [UnmanagedFunctionPointer( CallingConvention.Cdecl, CharSet = CharSet.Ansi )] /** \todo Wrap timer around */
        private delegate void DgateCallBack(IntPtr voidptrConfigData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBackCharArg(Byte bArg);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBackLongArg(long lArg); /// \warning Using long.

        // -Robot functions
        [DllImport("USBC.dll", EntryPoint = "?Initialization@@YAHFFP6AXPAX@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int initialization(short shrtMode, short shrtType, DgateCallBack funcprtCallBack, DgateCallBack funcptrCallBackError);

        [DllImport("USBC.dll", EntryPoint = "?Control@@YAHEH@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Control(byte bAxis, bool bIsOn);

        [DllImport("USBC.dll", EntryPoint = "?Home@@YAHEP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Home(byte axis, DgateCallBackCharArg funcptrCallBack);

        [DllImport("USBC.dll", EntryPoint = "?OpenGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        private static extern int OpenGripper();

        [DllImport("USBC.dll", EntryPoint = "?CloseGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CloseGripper();

        [DllImport("USBC.dll", EntryPoint = "?GetJaw@@YAHPAF0@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetJaw(ref short perc, ref short metric);

        [DllImport("USBC.dll", EntryPoint = "?EnterManual@@YAHF@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int EnterManual(short shrtArg);

        [DllImport("USBC.dll", EntryPoint = "?CloseManual@@YAHXZ", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int CloseManual();

        [DllImport("USBC.dll", EntryPoint = "?MoveManual@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)] // In C++ speed is long
        private static extern int MoveManual(byte bAxis, int lSpeed);

        [DllImport("USBC.dll", EntryPoint = "?Stop@@YAHE@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int Stop(byte axis);

        [DllImport("USBC.dll", EntryPoint = "?WatchMotion@@YAP6AXPAX@ZP6AX0@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern DgateCallBackCharArg WatchMotion(DgateCallBackCharArg funcptrCallbackEnd, DgateCallBackCharArg funcptrCallbackStart);

        [DllImport("USBC.dll", EntryPoint = "?WatchDigitalInp@@YAHP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WatchDigitalInput(DgateCallBackLongArg funcptrCallbackEvent);

        [DllImport("USBC.dll", EntryPoint = "?CloseWatchDigitalInp@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CloseWatchDigitalInput();

        [DllImport("USBC.dll", EntryPoint = "?IsOnLineOk@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        private static extern int IsOnLineOk();

        [DllImport("USBC.dll", EntryPoint = "?MoveLinear@@YAHPADF0F@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sSecondaryPos, short shrtPointToMoveTo);

        [DllImport("USBC.dll", EntryPoint = "?DefineVector@@YAHEPADF@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector);

        [DllImport("USBC.dll", EntryPoint = "?Teach@@YAHPADFPAJFJ@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType); // long types used in C++ functions.
        #endregion


        #region Helper functions
        private byte axisSettingsToByte(enumAxisSettings axisSettingsArg)
        {
            byte bArg;
            switch (axisSettingsArg)
            {
                case enumAxisSettings.AXIS_ROBOT:
                    bArg = (byte)'A';
                    break;
                case enumAxisSettings.AXIS_PERIPHERALS:
                    bArg = (byte)'B';
                    break;
                case enumAxisSettings.AXIS_GRIPPER:
                    bArg = (byte) 'G';
                    break;
                case enumAxisSettings.AXIS_0:
                    bArg = (byte) '0';
                    break;
                case enumAxisSettings.AXIS_1:
                    bArg = (byte) '1';
                    break;
                case enumAxisSettings.AXIS_2:
                    bArg = (byte) '2';
                    break;
                case enumAxisSettings.AXIS_3:
                    bArg = (byte) '3';
                    break;
                case enumAxisSettings.AXIS_4:
                    bArg = (byte) '4';
                    break;
                case enumAxisSettings.AXIS_5:
                    bArg = (byte) '5';
                    break;
                case enumAxisSettings.AXIS_6:
                    bArg = (byte) '6';
                    break;
                case enumAxisSettings.AXIS_7:
                    bArg = (byte) '7';
                    break;
                case enumAxisSettings.AXIS_ALL:
                    bArg = (byte)'&';
                    break;
                default:
                    throw new ArgumentOutOfRangeException("axisSettingsArg group invalid.");
            }
            return(bArg);
        }
        private byte manualMovementToByte(enumManualModeWhat enumArg)
        {
            byte bArg;
            switch (enumArg)
            {
                case enumManualModeWhat.MANUAL_MOVE_BASE:
                    bArg = (byte)'0';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_SHOULDER:
                    bArg = (byte)'1';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_ELBOW:
                    bArg = (byte)'2';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_WRISTPITCH:
                    bArg = (byte)'3';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_WRISTROLL:
                    bArg = (byte)'4';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_GRIPPER:
                    bArg = (byte)'5';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT:
                    bArg = (byte)'7';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_X:
                    bArg = (byte)'0';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_Y:
                    bArg = (byte)'1';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_Z:
                    bArg = (byte)'2';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_PITCH:
                    bArg = (byte)'3';
                    break;
                case enumManualModeWhat.MANUAL_MOVE_ROLL:
                    bArg = (byte)'4';
                    break;
                default:
                    throw new ArgumentOutOfRangeException("enumArg group invalid.");
            }
            return(bArg);
        }
        #endregion
    }
}
