/** \file wrapper.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;

namespace ControlSystem
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
        
        /// <summary>
        /// Constructor setting up the point location.
        /// </summary>
        /// <param name="_iX">X coordinate.</param>
        /// <param name="_iY">Y coordinate.</param>
        /// <param name="_iZ">Z coordinate.</param>
        /// <param name="_iPitch">Pitch.</param>
        /// <param name="_iRoll">Roll.</param>
        public VecPoint(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll)
        {
            iX = _iX;
            iY = _iY;
            iZ = _iZ;
            iPitch = _iPitch;
            iRoll = _iRoll;
        }
        
        /// <summary>
        /// The position in string format.
        /// </summary>
        /// <returns>The position.("(x,y,z,pitch,roll)")</returns>
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
		/// <summary>
		/// Type of vector.(Should be set in classes inheriting from this.)
		/// </summary>
        protected int iType; // What kind of pointer

        // Functions
        /// <summary>
        /// Number of points in vector.
        /// </summary>
        /// <returns>Number of points.</returns>
        public int getSize()
        {
            return (lstPoints.Count);
        }
        
        /// <summary>
        /// Add a point to the vector.
        /// </summary>
        /// <param name="pNewVecPoint"></param>
        public void addPoint(VecPoint pNewVecPoint)
        {
            lstPoints.Add(pNewVecPoint);     
        }
        
        /// <summary>
        /// Gets a point from the vector.
        /// </summary>
        /// <param name="iIndex">Index of the point.</param>
        /// <returns>The point.</returns>
        public VecPoint getPoint(int iIndex)
        {
            return (lstPoints[iIndex]);
        }
        
        /// <summary>
        /// Name of the vector.
        /// </summary>
        public string Name
        {
            get { return sName; }
        }
        
        /// <summary>
        /// Type of the vector.(Relative or Absolute.)
        /// </summary>
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
		/// <summary>
		/// Contructors whichs sets up type and name of vector.
		/// </summary>
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
		/// <summary>
		/// Contructors whichs sets up type and name of vector.
		/// </summary>
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
		/// <summary>
		/// Dll used for functions.
		/// </summary>
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

        public bool controlWrapped(enumAxisSettings _axisSettingsGroup, bool _bControlOnOrOff) 
        { 
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = _dll.Control(bArg, _bControlOnOrOff); // Note: Bool arg in unwrapped version.
            return ((iReturnValue == 1) ? true : false);
        }

        public bool isOnlineOkWrapped()
        {
            int iReturnValue;
            iReturnValue = _dll.IsOnLineOk();
            return ((iReturnValue == 1) ? true : false);
        }

        public bool timeWrapped(enumAxisSettings _bGroup, long _mTime)
        {
            int iReturnValue;
            iReturnValue = _dll.Time(axisSettingsToByte(_bGroup), _mTime);
            return ((iReturnValue == 1) ? true : false);
        }

        public bool speedWrapped(enumAxisSettings _bGroup, long _mSpeed)
        {
            int iReturnValue;
            iReturnValue = _dll.Speed(axisSettingsToByte(_bGroup), _mSpeed);
            return ((iReturnValue == 1) ? true : false);
        }

        #endregion

        #region Movement
        public bool homeWrapped(enumAxisSettings _axisSettingsGroup, DLL.DgateCallBackByteRefArg _funcptrHomingEventHandler)
        {
            byte bArg = axisSettingsToByte(_axisSettingsGroup);
            int iReturnValue;
            iReturnValue = _dll.Home((byte) bArg, _funcptrHomingEventHandler);
            return ((iReturnValue == 1) ? true : false);
        }

        public bool enterManualWrapped(enumManualType _enummanMoveType)
        {
            short shrtTmp = 0;
            int iReturnValue;
            switch(_enummanMoveType)
            {
                case enumManualType.MANUAL_TYPE_AXES:
                    shrtTmp = 0;
                    break;
                case enumManualType.MANUAL_TYPE_COORD:
                    shrtTmp = 1;
                    break;
                   
            }
            iReturnValue = _dll.EnterManual(shrtTmp);
            return ((iReturnValue == 1) ? true : false);
        }

        public bool closeManualWrapped()
        {
            int iReturnValue;
            iReturnValue = _dll.CloseManual();
            return ((iReturnValue == 1) ? true : false);
        }

        public bool moveManualWrapped(enumManualModeWhat _enumWhatToMove, int _lSpeed)
        {
            int iReturnValue;
            iReturnValue = _dll.MoveManual(manualMovementToByte(_enumWhatToMove), _lSpeed);
            return ((iReturnValue == 1) ? true : false);
        }

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
        public bool openGripperWrapped()
        {
            int iReturnValue;
            iReturnValue = _dll.OpenGripper();
            return ((iReturnValue == 1) ? true : false);
        }

        public bool closeGripperWrapped()
        {
            int iReturnValue;
            iReturnValue = _dll.CloseGripper();
            return ((iReturnValue == 1) ? true : false);
        }

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
        public void watchMotionWrapped(DLL.DgateCallBackByteRefArg _funcptrCallbackEnd, DLL.DgateCallBackByteRefArg _funcptrCallbackStart)
        {
            _dll.WatchMotion(_funcptrCallbackEnd, _funcptrCallbackStart);
        }

        public bool watchDigitalInputWrapped(DLL.DgateCallBackLongArg _funcptrCallbackEvent)
        {
            int iReturnValue;
            iReturnValue = _dll.WatchDigitalInput(_funcptrCallbackEvent);
            return ((iReturnValue == 1) ? true : false);
        }

        public bool closeWatchDigitalInputWrapped()
        {
            int iReturnValue;
            iReturnValue = _dll.CloseWatchDigitalInput();
            return ((iReturnValue == 1) ? true : false);
        }
        #endregion

        #region Vectors
        public bool defineVectorWrapped(enumAxisSettings _enumGroup, string _sVectorName, short _shrtLength)
        {
            int iReturn;
            iReturn = _dll.DefineVector(axisSettingsToByte(_enumGroup), _sVectorName, _shrtLength);
            return (iReturn == 1);
        }

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
                int[] iArray = new int[]{x, y, z, pitch, roll,0,0,0};

                iReturn = _dll.Teach(vecTheSirVector.Name, (short) i, iArray, 8, vecTheSirVector.Type); // 8 because of 8 axes
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

        private byte manualMovementToByte(enumManualModeWhat enumArg)
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
