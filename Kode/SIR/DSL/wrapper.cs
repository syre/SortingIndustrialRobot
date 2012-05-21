/** \file wrapper.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;

namespace DSL
{
    // Extra classes
    /// <summary>
    /// Class to contain point for use in one of the vector classes.
    /// </summary>
    public class VecPoint
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
        public override string ToString()
        {
            return "("+iX + "," + iY + "," + iZ + "," + iPitch + "," + iRoll + ")";
        }
    }

    /// <summary>
    /// Base class for vector used in wrapper.
    /// 
    /// Should use the derived classes.
    /// </summary>
    public class SIRVector
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
    public class AbsCoordSirVector : SIRVector
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
    public class RelCoordSirVector : SIRVector
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
    public class Wrapper : IWrapper
    {
        // Members
        // -Normal
        private IDLL _dll;
        public IDLL DLL
        {
            get { return (_dll); }
            set { _dll = value; }
        }

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


        /// <summary>
        /// For chosing what part to move when setting Time or Speed
        /// </summary>
        public enum enumBGroup
        {
            GroupAnd,
            Group0,  //axis movements
            Group1,
            Group2,
            Group3,
            Group4,
            Group5,
            Group6,
            Group7,
            GroupA, //for robot movements
            GroupB, //for peripheral movements
            GroupG  //for gripper movements
        }

        // Functions
        // -Constructors and destructors
        private Wrapper()
        {
            _dll = new DLL();
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
        /// <param name="_sysmodeMode">Mode.[Normally use online mode]</param>
        /// <param name="_systypeType">Type of connection.[Normally use default]</param>
        /// <param name="_funcptrSuccess">Function to be called on success.</param>
        /// <param name="_funcptrError">Function to be called on error.</param>
        /// <returns>Returns true on successful call.(But errors can still happen)</returns>
        public bool initializationWrapped(enumSystemModes _sysmodeMode, enumSystemTypes _systypeType, DLL.DgateCallBack _funcptrSuccess, DLL.DgateCallBack _funcptrError)
        {
            int iReturnValue = _dll.Initialization((short)_sysmodeMode, (short)_systypeType, _funcptrSuccess, _funcptrError);
            return ((iReturnValue == 1)? true : false);
        }
        /// <summary>
        /// Turns control on and off for certain axis group.
        /// </summary>
        /// <param name="_axisSettingsGroup">Axis group to affect.</param>
        /// <param name="_bControlOnOrOff">To have it turned off or on.</param>
        /// <returns>Returns true on successful call.</returns>
        public bool controlWrapped(enumAxisSettings _axisSettingsGroup, bool _bControlOnOrOff) 
        { 
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = _dll.Control(bArg, _bControlOnOrOff); // Note: Bool arg in unwrapped version.
            return ((iReturnValue == 1) ? true : false);
        }

        /// <summary>
        /// Tells about the robot being online.
        /// </summary>
        /// <returns>Returns true if it is, false otherwise.</returns>
        public bool isOnlineOkWrapped()
        {
            int iReturnValue;
            iReturnValue = _dll.IsOnLineOk();
            return ((iReturnValue == 1) ? true : false);
        }

        public bool TimeWrapped(enumBGroup _bGroup, long _mTime)
        {
            int iReturnValue;
            iReturnValue = _dll.Time((byte)_bGroup, _mTime);
            return ((iReturnValue == 1) ? true : false);
        }

        public bool SpeedWrapped(enumBGroup _bGroup, long _mSpeed)
        {
            int iReturnValue;
            iReturnValue = _dll.Speed((byte)_bGroup, _mSpeed);
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
        public bool homeWrapped(enumAxisSettings _axisSettingsGroup, DLL.DgateCallBackByteRefArg _funcptrHomingEventHandler)
        {
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = _dll.Home((byte) bArg, _funcptrHomingEventHandler);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Must be called to use manual movement. 
		/// Seems to stop previous movement of any object(Axis) that was moving before.
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
            iReturnValue = _dll.EnterManual(shrtTmp);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Stops manual mode.
        /// </summary>
        /// <returns>Returns true on successful call.</returns>
        public bool closeManualWrapped()
        {
            int iReturnValue;
            iReturnValue = _dll.CloseManual();
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Moves the robot.
        /// homeWrapped must have been called if moving by coordinates.
		/// enterManual seems have to be called before each call to this function.
		/// Use stopWrapped to stop motion afterwards.(Moving some other part of the system also stops the previous movement, since the system can only handle one object(Axis) moving at a time.)
        /// </summary>
        public bool moveManualWrapped(enumManualModeWhat _enumWhatToMove, int _lSpeed)
        {
            int iReturnValue;
            iReturnValue = _dll.MoveManual(manualMovementToByte(_enumWhatToMove), _lSpeed);
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
            iReturnValue = _dll.Stop(axisSettingsToByte(_bWhatToStop));
            return ((iReturnValue == 1) ? true : false);
        }

        /// <summary>
        /// Moves the robot in a linear motion.
        /// 
        /// \warning Not using pos 2 in wrapped dll function.
        /// \warning Seems to be unfunctional.
        /// </summary>
        /// <param name="_sNameOfVector">Name of the vector in the Robot.</param>
        /// <param name="_iIndex">Index for point</param>
        /// <returns>Returns true on successfull call.</returns>
        public bool moveLinearWrapped(string _sNameOfVector, int _iIndex) // Can be changed to take Vector base class
        {
            int iReturn;
            iReturn = _dll.MoveLinear(_sNameOfVector, (short)_iIndex, null, 0); // Ignoring last value.
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
            iReturnValue = _dll.OpenGripper();
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Closes the gripper.
        /// </summary>
        /// <returns>Returns true on successful call.</returns>
        public bool closeGripperWrapped()
        {
            int iReturnValue;
            iReturnValue = _dll.CloseGripper();
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
            iReturnValue = _dll.GetJaw(ref _shrtPerc, ref _shrtWidth);
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
        public void watchMotionWrapped(DLL.DgateCallBackCharArg _funcptrCallbackEnd, DLL.DgateCallBackCharArg _funcptrCallbackStart)
        {
            _dll.WatchMotion(_funcptrCallbackEnd, _funcptrCallbackStart);
        }

        /// <summary>
        /// Adds a function to be called when digital input changes.
        /// </summary>
        /// <param name="_funcptrCallbackEvent">The function to be called.</param>
        /// <returns>Returns true if successful call.</returns>
        public bool watchDigitalInputWrapped(DLL.DgateCallBackLongArg _funcptrCallbackEvent)
        {
            int iReturnValue;
            iReturnValue = _dll.WatchDigitalInput(_funcptrCallbackEvent);
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
            iReturnValue = _dll.CloseWatchDigitalInput();
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
        ///     All
        /// </param>
        /// <param name="_sVectorName">Name of vector.</param>
        /// <param name="_shrtLength">Length of vector.(Number of points.)</param>
        /// <returns>Returns true on successfull call.</returns>
        public bool defineVectorWrapped(enumAxisSettings _enumGroup, string _sVectorName, short _shrtLength)
        {
            int iReturn;
            iReturn = _dll.DefineVector(axisSettingsToByte(_enumGroup), _sVectorName, _shrtLength);
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

                iReturn = _dll.Teach(vecTheSirVector.Name, (short) i, iArray, 5, vecTheSirVector.Type); // 5 because of 5 ints
                if (iReturn == 0)
                    return (false);
            }
            return (true);
        }
        /// <summary>
        /// Returns the position of the robot.
        /// 
        /// \warning Ignoring wrapper int return value.
		/// \warning Not sure about buffer type to use in impl.
        /// </summary>
        /// <returns>Returns current position.</returns>
        public VecPoint getCurrentPosition()
        {
            // Used from site 
            int[] pEnc = new int[8]; // Ignored
            int[] pJoint = new int[8]; // Ignored
            int[] pXYZ = new int[8];
            _dll.GetCurrentPosition(ref pEnc, ref pJoint, ref pXYZ);
            return (new VecPoint(pXYZ[0], pXYZ[1], pXYZ[2], pXYZ[3], pXYZ[4]));
        }
        #endregion

        #region Helper functions

        public byte axisSettingsToByte(enumAxisSettings axisSettingsArg)
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
                    bArg = (byte) 0;
                    break;
                case enumAxisSettings.AXIS_1:
                    bArg = (byte) 1;
                    break;
                case enumAxisSettings.AXIS_2:
                    bArg = (byte) 2;
                    break;
                case enumAxisSettings.AXIS_3:
                    bArg = (byte) 3;
                    break;
                case enumAxisSettings.AXIS_4:
                    bArg = (byte) 4;
                    break;
                case enumAxisSettings.AXIS_5:
                    bArg = (byte) 5;
                    break;
                case enumAxisSettings.AXIS_6:
                    bArg = (byte) 6;
                    break;
                case enumAxisSettings.AXIS_7:
                    bArg = (byte) 7;
                    break;
                case enumAxisSettings.AXIS_ALL:
                    bArg = (byte)'&';
                    break;
                default:
                    throw new ArgumentOutOfRangeException("axisSettingsArg group invalid.");
            }
            return(bArg);
        }

        public byte manualMovementToByte(enumManualModeWhat enumArg)
        {
            byte bArg;
            switch (enumArg)
            {
                case enumManualModeWhat.MANUAL_MOVE_BASE:
                    bArg = (byte)0;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_SHOULDER:
                    bArg = (byte)1;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_ELBOW:
                    bArg = (byte)2;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_WRISTPITCH:
                    bArg = (byte)3;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_WRISTROLL:
                    bArg = (byte)4;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_GRIPPER:
                    bArg = (byte)5;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT:
                    bArg = (byte)7;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_X:
                    bArg = (byte)0;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_Y:
                    bArg = (byte)1;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_Z:
                    bArg = (byte)2;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_PITCH:
                    bArg = (byte)3;
                    break;
                case enumManualModeWhat.MANUAL_MOVE_ROLL:
                    bArg = (byte)4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("enumArg group invalid.");
            }
            return(bArg);
        }
        #endregion
    }
}
