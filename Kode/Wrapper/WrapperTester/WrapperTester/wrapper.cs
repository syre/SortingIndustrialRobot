/** \file Wrapper.cs */
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
        private static Wrapper wrapOnlyInstance;
        private dgateCallBack callbackfuncSucessful;
        private dgateCallBack callbackfuncError;
        private dgateCallBackCharArg callbackfuncHoming;

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
        private void eventHoming(Byte homeNotif)
        {
            Console.WriteLine("Home Event: " + homeNotif);
        }
        #endregion

        // Settings constants
        // -Initialization mode
        public const short MODE_DEFAULT = 0; // Last used mode
        public const short MODE_ONLINE = 1; // Force online mode
        public const short MODE_SIMULAT = 2; // Simulator mode
        // -Initialization system type
        public const short SYSTEM_TYPE_DEFAULT = 0; // Detect it
        public const short SYSTEM_TYPE_ER4USB= 41; // ER-4
        // -Axis control settings
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
        public enum enumManualType
        {
            MANUAL_TYPE_AXES,
            MANUAL_TYPE_COORD
        }

        // Functions
        // -Constructors and destructors
        private Wrapper()
        {
            // Initializes callback functions
            callbackfuncSucessful = new dgateCallBack(eventSuccess);
            callbackfuncError = new dgateCallBack(eventError);
            callbackfuncHoming = new dgateCallBackCharArg(eventHoming);
        }

        // -Helper functions
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
        public bool initializationWrapped(short _shrtMode, short _shrtType)
        {
            int iReturnValue = initialization(_shrtMode, _shrtType, callbackfuncSucessful, callbackfuncError);
            return ((iReturnValue == 1)? true : false);
        }
        /// <summary>
        /// Sets the location of the "Par" folder.
        /// </summary>
        /// <param name="charptrFolder">String with location of file.</param>
        /// <returns>Returns true on success.</returns>
        public bool setParameterFolderWrapped(string _sFolderAddress)
        {
            IntPtr intptrTmp = Marshal.StringToHGlobalAnsi(_sFolderAddress);
            int iReturnValue = setParameterFolder(intptrTmp);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Returns address of "Par" folder.
        /// </summary>
        /// <param name="_sFolderAddress">Buffer for address.</param>
        /// <returns>Returns true on success.</returns>
        public bool getParameterFolderWrapped(out string _sFolderAddress)
        {
            IntPtr intptrTmp = Marshal.AllocHGlobal(200);
            int iReturnValue = GetParameterFolder(intptrTmp);
            _sFolderAddress = Marshal.PtrToStringAuto(intptrTmp);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Turns control on and off for certain axis group.
        /// </summary>
        /// <param name="bAxis">Axis group to affect.(Use enum)</param>
        /// <param name="_bControlOnOrOff">To have it turned off or on.</param>
        /// <returns>Returns true on success,</returns>
        public bool controlWrapped(enumAxisSettings _axisSettingsGroup, bool _bControlOnOrOff)
        {
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = Control(bArg, _bControlOnOrOff);
            return ((iReturnValue == 1) ? true : false);
        }
        #endregion

        #region Movement
        /// <summary>
        /// Homes a axis group.
        /// Should be called before calling most movement functions.
        /// </summary>
        /// <param name="_axisSettingsGroup">The axis group.(Use enum)</param>
        /// <returns>Returns true on success.</returns>
        public bool homeWrapped(enumAxisSettings _axisSettingsGroup)
        {
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = Home((byte) _axisSettingsGroup, callbackfuncHoming);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Must be called to use manual movement.  
        /// </summary>
        /// <param name="_enummanMoveType">What to move by.(Axis(0), Coordinates(1))</param>
        /// <returns>Returns true on success.</returns>
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
        /// <returns>Returns true on success.</returns>
        public bool closeManualWrapped()
        {
            int iReturnValue;
            iReturnValue = CloseManual();
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Moves to robot.
        /// homeWrapped must have been called if moving by coordinates. 
        /// </summary>
        /// <param name="_bWhatToMove">Depends on moving by coordinates or axis.
        /// Axis:
        ///     Base(0)
        ///     Shoulder(1)
        ///     Elbow(2)
        ///     Wrist-Pitch(3)
        ///     Wrist-Roll(4)
        ///     Gripper(5)
        ///     Conveyer belt(7)
        /// Coordinates:
        ///     X(0)
        ///     Y(1)
        ///     Z(2)
        ///     Pitch(3)
        ///     Roll(4)
        /// </param>
        /// <param name="_lSpeed"></param>
        /// <returns>Returns true on success.</returns>
        public bool moveManualWrapped(byte _bWhatToMove, long _lSpeed) /// \todo Refactor.
        {
            int iReturnValue;
            iReturnValue = MoveManual(_bWhatToMove, _lSpeed);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Stops movement of axis.
        /// </summary>
        /// <param name="_bWhatToStop">Axis to stop.</param>
        /// <returns>Returns true on success.</returns>
        public bool stopWrapped(enumAxisSettings _bWhatToStop) /// \todo Refactor.
        {
            int iReturnValue;
            iReturnValue = Stop(axisSettingsToByte(_bWhatToStop));
            return ((iReturnValue == 1) ? true : false);
        }
        #endregion

        #region Gripper
        public bool openGripperWrapped()
        {
            int iReturnValue;
            iReturnValue = OpenGripper();
            return ((iReturnValue == 1) ? true : false);
        }
        public bool closeGripperWrapped()
        {
            int iReturnValue;
            iReturnValue = CloseGripper();
            return ((iReturnValue == 1) ? true : false);
        }
        public bool getJawWrapped(ref short _shrtPerc, ref short _shrtWidth)
        {
            int iReturnValue;
            iReturnValue = GetJaw(ref _shrtPerc, ref _shrtWidth);
            return ((iReturnValue == 1) ? true : false);
        }
        #endregion

        #region Imported references(Should use wrapped versions)
        // --Function pointer
        [UnmanagedFunctionPointer( CallingConvention.Cdecl, CharSet = CharSet.Ansi )] /** \todo Wrap timer around */
        private delegate void dgateCallBack(IntPtr voidptrConfigData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void dgateCallBackCharArg(Byte bArg);

        // --Robot functions
        [DllImport("USBC.dll", EntryPoint = "?Initialization@@YAHFFP6AXPAX@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int initialization(short shrtMode, short shrtType, dgateCallBack funcprtCallBack, dgateCallBack funcptrCallBackError);

        [DllImport("USBC.dll", EntryPoint = "?SetParameterFolder@@YAHPAD@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int setParameterFolder(IntPtr charptrFolder);

        [DllImport("USBC.dll", EntryPoint = "?GetParameterFolder@@YAHPAD@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetParameterFolder(IntPtr charptrFolderBuffer);

        [DllImport("USBC.dll", EntryPoint = "?Control@@YAHEH@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Control(byte bAxis, bool bIsOn);

        [DllImport("USBC.dll", EntryPoint = "?Home@@YAHEP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Home(byte axis, dgateCallBackCharArg funcptrCallBack);

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

        [DllImport("USBC.dll", EntryPoint = "?MoveManual@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MoveManual(byte bAxis, long lSpeed);

        [DllImport("USBC.dll", EntryPoint = "?Stop@@YAHE@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int Stop(byte axis);
        #endregion
    }
}
