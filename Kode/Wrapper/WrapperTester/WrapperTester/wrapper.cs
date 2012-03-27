/** \file wrapper.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WrapperTester // Has to be changed
{
    // Extra classes
    /// <summary>
    /// Class to contain point for use in one of the vector classes.
    /// </summary>
    class VecPoint
    {
        public int iX;
        public int iY;
        public int iZ;
        public int iPitch;
        public int iRoll;
        public VecPoint(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll)
        {
            iX = _iX;
            iY = _iY;
            iZ = _iZ;
            iPitch = _iPitch;
            iRoll = _iRoll;
        }
    }
    /// <summary>
    /// Base class for vector used in wrapper.
    /// 
    /// Should use the derived classes.
    /// </summary>
    class SIRVector
    {
        // Members
        protected string sName;
        protected List<VecPoint> lstPoints;
        protected int iType; // What kind of pointer

        // Functions
        public int getSize()
        {
            return (lstPoints.Count);
        }
        public void addPoint(VecPoint pNewVecPoint)
        {
            lstPoints.Add(pNewVecPoint);     
        }
        public VecPoint getPoint(int iIndex)
        {
            return (lstPoints[iIndex]);
        }
        public string Name
        {
            get { return sName; }
        }
        public int Type
        {
            get { return iType; }
        }

    }

    /// <summary>
    /// SIRVector class for absolute positions.
    /// </summary>
    class AbsCoordSirVector : SIRVector
    {
        // Functions 
        public AbsCoordSirVector(string _sName)
        {
            sName = _sName;
            lstPoints = new List<VecPoint>();
            iType = -32766;
        }
    }
    
    /// <summary>
    /// SIRVector class for relative positions.
    /// </summary>
    class RelCoordSirVector : SIRVector
    {
        // Functions 
        public RelCoordSirVector(string _sName)
        {
            sName = _sName;
            lstPoints = new List<VecPoint>();
            iType = -32767;
        }
    }

    /// <summary>
    /// Contains a wrapper for the C++ functions in the dll file(USBC.dll).
    /// 
    /// Good idea to check USBC-documentation.pdf.
    /// 
    /// Notes:  Uses IntPtr arg for different types of C++ pointers.
    ///         Same function names as C++ but has "Wrapped" at the end.
    ///         Try to have handlers in delegates which in entire use of wrapper, so the memory for the handler doesn´t get removed by GC.
    /// 
    /// \todo Add behind factory class.
    /// </summary>
    class Wrapper
    {
        // Members
        // -Singleton related
        private static Wrapper wrapOnlyInstance;

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
        /// For chosing what part to move when moving manually.
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
            // Nothing
        }


        // -Singleton related
        /// <summary>
        /// Gets the wrapper.
        /// </summary>
        /// <returns>The wrapper.</returns>
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
        /// 
        /// Note: Should wait for it to be done before calling other functions.
        /// \todo Refactor delegate to contain ConfigData and ErrorInfo if found necessary.
        /// </summary>
        /// <param name="_shrtMode">Mode.(Use one of constants[Normally use online mode])</param>
        /// <param name="_shrtType">Type of connection.(Use one of constants[Normally use default])</param>
        /// <param name="_funcptrSuccess">Function to be called on success.</param>
        /// <param name="_funcptrError">Function to be called on error.</param>
        /// <returns>Returns true on successful call.(But errors can still happen)</returns>
        public bool initializationWrapped(enumSystemModes _sysmodeMode, enumSystemTypes _systypeType, DgateCallBack _funcptrSuccess, DgateCallBack _funcptrError)
        {
            int iReturnValue = initialization((short)_sysmodeMode, (short)_systypeType, _funcptrSuccess, _funcptrError);
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
        public bool homeWrapped(enumAxisSettings _axisSettingsGroup, DgateCallBackByteRefArg _funcptrHomingEventHandler)
        {
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = Home((byte) bArg, _funcptrHomingEventHandler);
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
        /// \warning Seems to be unfunctional. 
        /// </summary>
        public bool moveManualWrapped(enumManualModeWhat _enumWhatToMove, int _lSpeed)
        {
            int iReturnValue;
            iReturnValue = MoveManual(manualMovementToByte(_enumWhatToMove), _lSpeed); ///\warning WARN
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

        /// <summary>
        /// Flytter robotten til et punkt.
        /// 
        /// \warning not using pos 2 in wrapped dll function.
        /// \warning Seems to be unfunctional.
        /// </summary>
        /// <param name="_sNameOfVector">Navnet på vektoren i robotten.</param>
        /// <param name="_iIndex">Index for punkt.</param>
        /// <returns>Returns true on successfull call.</returns>
        public bool moveLinearWrapped(string _sNameOfVector, int _iIndex)
        {
            int iReturn;
            iReturn = MoveLinear(_sNameOfVector, (short)_iIndex, null, 0); // Ignoring last value.
            return (iReturn == 1);
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

        #region Vectors
        /// <summary>
        /// Defines a new vector in robot memory.
        /// 
        /// Note: Good idea to have in program one of the SIRVector classes to contains vector information.
        /// </summary>
        /// <param name="_enumGroup">Group can use:
        ///     Robot(Normally used)
        ///     Peripherals
        ///     All</param>
        /// <param name="_sVectorName">Name of vector.</param>
        /// <param name="_shrtLength">Length of vector.(Number of points.)</param>
        /// <returns>Returns true on successfull call.</returns>
        public bool defineVectorWrapped(enumAxisSettings _enumGroup, string _sVectorName, short _shrtLength)
        {
            int iReturn;
            iReturn = DefineVector(axisSettingsToByte(_enumGroup), _sVectorName, _shrtLength);
            return (iReturn == 1);
        }
        /// <summary>
        /// Add the vector points to the vector with the same name. 
        /// 
        /// Note: Should call 'defineVectorWrapped' first.
        /// </summary>
        /// <param name="vecTheSirVector">The vector with the points.</param>
        /// <returns>Returns true on succeessfull call.</returns>
        public bool teachWrapped(SIRVector vecTheSirVector)
        {
            int iReturn;
            for (int i = 0; i < vecTheSirVector.getSize(); i++)
            {
                VecPoint pTmp = vecTheSirVector.getPoint(i);
                int x, y, z, pitch, roll;
                x = pTmp.iX;
                y = pTmp.iY;
                z = pTmp.iZ;
                pitch = pTmp.iPitch;
                roll = pTmp.iRoll;
                int[] iArray = new int[]{x, y, z, pitch, roll};

                iReturn = Teach(vecTheSirVector.Name, (short) i, iArray, 5, vecTheSirVector.Type); // 5 because of 5 ints
                if (iReturn == 0)
                    return (false);
            }
            return (true);
        }
        /// <summary>
        /// Returns the position of the robot.
        /// 
        /// \warning Ignoring wrapper int return value.
        /// \todo Refactor buffer type and wrapped dll signature.
        /// </summary>
        /// <returns>Returns current position.</returns>
        public VecPoint getCurrentPosition() /// \warning Not sure about buffer type to use.
        {
            // Used from site 
            int[] pEnc = new int[8]; // Ignored
            int[] pJoint = new int[8]; // Ignored
            int[] pXYZ = new int[8];
            GetCurrentPosition(ref pEnc, ref pJoint, ref pXYZ);
            return (new VecPoint(pXYZ[0], pXYZ[1], pXYZ[2], pXYZ[3], pXYZ[4]));
        }
        #endregion

        #region Imported references(Should use wrapped versions)
        // -Function pointers
        [UnmanagedFunctionPointer( CallingConvention.Cdecl, CharSet = CharSet.Ansi )]
        public delegate void DgateCallBack(IntPtr voidptrConfigData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBackCharArg(Byte bArg);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBackLongArg(long lArg); /// \warning Using long.

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBackByteRefArg(ref byte bArg);

        // -Robot functions
        [DllImport("USBC.dll", EntryPoint = "?Initialization@@YAHFFP6AXPAX@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int initialization(short shrtMode, short shrtType, DgateCallBack funcprtCallBack, DgateCallBack funcptrCallBackError);

        [DllImport("USBC.dll", EntryPoint = "?Control@@YAHEH@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Control(byte bAxis, bool bIsOn);

        [DllImport("USBC.dll", EntryPoint = "?Home@@YAHEP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Home(byte axis, DgateCallBackByteRefArg funcptrCallBack);

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

        [DllImport("USBC.dll", EntryPoint = "?GetCurrentPosition@@YAHPAY07J00@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetCurrentPosition(ref int[] ibufEnc, ref int[] ibufJoint, ref int[] ibufXYZ);
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
