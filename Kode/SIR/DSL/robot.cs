/** \file robot.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;

namespace DSL
{
    public enum ManualModeType
    {   
        Off,
        Axes,
        Coordinates
    }
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
    public enum ControlModeType
    {
        Off,
        On
    }

    public interface IRobot
    {
        bool closeGripper();
        bool openGripper();
        bool initialization();
        ManualModeType ManualMode { get; set; }
        ControlModeType ControlMode { get; set; }
        bool stopAllMovement();
        bool moveByAbsoluteCoordinates(int x, int y, int z, int pitch, int roll);
        bool moveByRelativeCoordinates(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll);
        short getJawOpeningWidthMilimeters();
        short getJawOpeningWidthPercentage();
        bool homeRobot();
        bool isOnline();
        bool moveBase(int speed);
        bool moveShoulder(int speed);
        bool moveWristPitch(int speed);
        bool moveWristRoll(int speed);
        bool moveElbow(int speed);
        bool moveGripper(int speed);
        bool moveConveyerBelt(int speed);

    }
    public class Robot : IRobot
    {
        private Wrapper _wrapper;
        DLL.DgateCallBack dgateEventHandlerSuccess = initSuccess;
        DLL.DgateCallBack dgateEventHandlerError = initError;
        DLL.DgateCallBackByteRefArg dgateEventHandlerHoming = homeEvent;
        #region Robot mode properties
        public ManualModeType ManualMode
        {
            get
            {
                return ManualMode;
            }
            set
            {
                bool status = false;
                if (value == ManualModeType.Axes)
                {
                    status = _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
                    if (status)
                        ManualMode = value;
                }

                else if (value == ManualModeType.Coordinates)
                {
                    status = _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
                    if (status)
                        ManualMode = value;
                }

                else if (value == ManualModeType.Off)
                {
                    status = _wrapper.closeManualWrapped();
                    if (status)
                        ManualMode = value;
                }

                else
                    throw new ArgumentOutOfRangeException("value", "ManualMode didnt set correctly");

                if (!status)
                    throw new Exception("Manual Mode Set returned false");
            }
        }
        public ControlModeType ControlMode
        {
            get { return ControlMode; }
            set
            {
                bool status;
                if (value == ControlModeType.Off)
                {
                    status = _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, true);
                    if (status)
                        ControlMode = value;
                }
                else if (value == ControlModeType.On)
                {
                    status = _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, false);
                    if (status)
                        ControlMode = value;
                }
                else
                    throw new ArgumentOutOfRangeException("value", "ControlMode didnt set correctly");

                if (!status)
                    throw new Exception("Control Mode Set returned false");
            }
        }
        #endregion

        #region delegate functions
        static void initSuccess(IntPtr _iptrArg)
        {
            System.Console.WriteLine("Initialized successfully.");
        }
        static void initError(IntPtr _iptrArg)
        {
            System.Console.WriteLine("Initialize error.");
        }
        static void homeEvent(ref byte _bArg)
        {
            System.Console.WriteLine("Home Event: " + _bArg);
        }
        #endregion

        #region general robot methods

        public Robot()
        {
            _wrapper = Wrapper.getInstance();
            homeRobot();

        }
        /// <summary>
        /// Initializes robot with default values MODE_ONLINE and SYSTEM_TYPE_DEFAULT
        /// </summary>
        /// <returns></returns>
        public bool initialization() // implementing delegates
        {
            return _wrapper.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE,
                                                  Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT,
                                                  dgateEventHandlerSuccess,
                                                  dgateEventHandlerError);
        }
        /// <summary>
        /// Homes robot
        /// </summary>
        /// <returns></returns>
        public bool homeRobot() // implementing delegates
        {
            return _wrapper.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming);
        }

        /// <summary>
        /// Calls wrapper function for stopping all movement
        /// </summary>
        /// <returns></returns>
        public bool stopAllMovement()
        {
            return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT);
        }
        /// <summary>
        /// Checks to see if robot is online
        /// </summary>
        /// <returns></returns>
        public bool isOnline()
        {
            return _wrapper.isOnlineOkWrapped();
        }
        #endregion

        #region Coordinate movements
        /// <summary>
        /// Function for moving by coordinates
        /// </summary>
        /// <param name="_iX"> x-coordinate </param>
        /// <param name="y"> y-coordinate </param>
        /// <param name="z"> z-coordinate </param>
        /// <param name="pitch"> pitch robot arm</param>
        /// <param name="roll"> roll of robot arm</param>
        /// <returns></returns>
        public bool moveByAbsoluteCoordinates(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll) // subject to change
        {
            // ONLY PARTIALLY IMPLEMENTED - NOT WORKING
            
            ManualMode = ManualModeType.Coordinates;
            SIRVector tempCordVector = new AbsCoordSirVector("absoluteVector");
            tempCordVector.addPoint(new VecPoint(_iX,_iY,_iZ,_iPitch,_iRoll));
            _wrapper.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, "absoluteVector",5); // shrtlength??
            _wrapper.moveLinearWrapped("defaultVector", 5); // index??    
            ManualMode = ManualModeType.Off;
            return false; 
        }

        public bool moveByRelativeCoordinates(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll)
        {
            // ONLY PARTIALLY IMPLEMENTED - NOT WORKING
            ManualMode = ManualModeType.Coordinates;
            
            SIRVector tempRelVector = new RelCoordSirVector("relativeVector");
            tempRelVector.addPoint(new VecPoint(_iX,_iY,_iZ,_iPitch,_iRoll ));

            ManualMode = ManualModeType.Off;
            return false;
        }
        #endregion

        #region Axis movements
        /// <summary>
        /// Separate function for moving base of robot
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool moveBase(int _iSpeed)
        {
            ManualMode = ManualModeType.Axes;
            return  _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, _iSpeed);
        }
        /// <summary>
        /// Separate function for moving shoulder of robot
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        public bool moveShoulder(int _iSpeed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_SHOULDER, _iSpeed);
        }
        /// <summary>
        /// Separate function for moving elbow of robot
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        public bool moveElbow(int _iSpeed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_ELBOW, _iSpeed);
        }
        /// <summary>
        /// Separate function for moving wrist pitch of robot
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        public bool moveWristPitch(int _iSpeed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTPITCH, _iSpeed);
        }
        /// <summary>
        /// Separate function for moving wrist roll of robot
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        public bool moveWristRoll(int _iSpeed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTROLL, _iSpeed);
        }
        /// <summary>
        /// Separate function for moving robot gripper
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        public bool moveGripper(int _iSpeed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, _iSpeed);
        }
        /// <summary>
        /// Separate function for moving robot conveyer belt
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        public bool moveConveyerBelt(int _iSpeed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT, _iSpeed);
        }

        #endregion

        #region gripper methods
        /// <summary>
        ///  Returns jaw opening in milimeters
        /// </summary>
        /// <returns></returns>
        public short getJawOpeningWidthMilimeters()
        {
            short milimeters = 0, dummypercentage = 0;
            bool status = _wrapper.getJawWrapped(ref milimeters, ref dummypercentage);
            if (status)
                return milimeters;
            else
                throw new Exception("Error getting JawOpeningWidth in Milimeters"); 

        }
        /// <summary>
        ///  Returns jaw opening in percentage
        /// </summary>
        /// <returns></returns>
        public short getJawOpeningWidthPercentage()
        {
            short dummymilimeters = 0, percentage = 0;
            bool status = _wrapper.getJawWrapped(ref dummymilimeters, ref percentage);
            if (status)
                return percentage;
            else
                throw new Exception("Error getting JawOpeningWidth in Percentage");
        }
        /// <summary>
        ///  Closes gripper
        /// </summary>
        /// <returns></returns>
        public bool closeGripper()
        {
           return _wrapper.closeGripperWrapped();
        }
        /// <summary>
        /// Opens gripper
        /// </summary>
        /// <returns></returns>
        public bool openGripper()
        {
            return _wrapper.openGripperWrapped();
        }
        #endregion
    }
}