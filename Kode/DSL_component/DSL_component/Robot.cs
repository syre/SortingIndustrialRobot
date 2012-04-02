using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WrapperTester;

namespace DSL_component
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

        public enum AxisSettings
        {
            AXIS_ROBOT,
            AXIS_PERIPHERALS,
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


    public interface IRobot
    {
        bool closeGripper();
        bool openGripper();
        bool Initialization();
        ManualModeType ManualMode { get; set; }
        ControlModeType ControlMode { get; set; }
	bool stopAllMovement();
        bool stopMove(AxisSettings axis);
        bool moveByCoordinates(int x, int y, int z, int pitch, int roll);
        short getJawOpeningWidthMilimeters();
        short getJawOpeningWidthPercentage();
        bool homeRobot();
        bool isOnline();

    }
    public class Robot : IRobot
    {
        private Wrapper _wrapper;
        DLLImport.DgateCallBack dgateEventHandlerSuccess = initSuccess;
        DLLImport.DgateCallBack dgateEventHandlerError = initError;
        DLLImport.DgateCallBackByteRefArg dgateEventHandlerHoming = homeEvent;
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
        static void initSuccess(IntPtr iptrArg)
        {
            System.Console.WriteLine("Initialized successfully.");
        }
        static void initError(IntPtr iptrArg)
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
        }
        /// <summary>
        /// initializes robot with default values MODE_ONLINE and SYSTEM_TYPE_DEFAULT
        /// </summary>
        /// <returns></returns>
        public bool Initialization() // implementing delegates
        {
            return _wrapper.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE,
                                                  Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT,
                                                  dgateEventHandlerSuccess,
                                                  dgateEventHandlerError);
        }
        /// <summary>
        /// homes robot
        /// </summary>
        /// <returns></returns>
        public bool homeRobot() // implementing delegates
        {
            return _wrapper.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming);
        }

        /// <summary>
        /// calls wrapper function for stopping all movement
        /// </summary>
        /// <returns></returns>
        public bool stopAllMovement()
        {
            return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT);
        }
        /// <summary>
        /// checks to see if robot is online
        /// </summary>
        /// <returns></returns>
        public bool isOnline()
        {
            return _wrapper.isOnlineOkWrapped();
        }
        #endregion

        #region Coordinate movements
        /// <summary>
        /// function for moving by coordinates
        /// </summary>
        /// <param name="x"> x-coordinate </param>
        /// <param name="y"> y-coordinate </param>
        /// <param name="z"> z-coordinate </param>
        /// <param name="pitch"> pitch robot arm</param>
        /// <param name="roll"> roll of robot arm</param>
        /// <returns></returns>
        public bool moveByCoordinates(int x, int y, int z, int pitch, int roll) // subject to change
        {
                ManualMode = ManualModeType.Coordinates;
                // implement move by coordinates
                
                ManualMode = ManualModeType.Off;

        }
        #endregion

        #region Axis movements
        /// <summary>
        /// separate function for moving base of robot
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool moveBase(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return  _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, speed);
        }
        /// <summary>
        /// separate function for moving shoulder of robot
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool moveShoulder(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_SHOULDER, speed);
        }
        /// <summary>
        /// separate function for moving elbow of robot
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool moveElbow(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_ELBOW, speed);
        }
        /// <summary>
        /// separate function for moving wrist pitch of robot
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool moveWristPitch(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTPITCH, speed);
        }
        /// <summary>
        /// separate function for moving wrist roll of robot
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool moveWristRoll(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTROLL, speed);
        }
        /// <summary>
        /// separate function for moving robot gripper
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool moveGripper(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, speed);
        }
        /// <summary>
        /// separate function for moving robot conveyer belt
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool moveConveyerBelt(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT, speed);
        }

        public bool stopMove(AxisSettings axis)
        {
            switch (axis)
            {
                case (AxisSettings.AXIS_0):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_0);

                case (AxisSettings.AXIS_1):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_1);

                case (AxisSettings.AXIS_2):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_2);

                case (AxisSettings.AXIS_3):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_3);

                case (AxisSettings.AXIS_4):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_4);

                case (AxisSettings.AXIS_5):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_5);

                case (AxisSettings.AXIS_6):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_6);

                case (AxisSettings.AXIS_7):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_7);

                case (AxisSettings.AXIS_ALL):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);

                case (AxisSettings.AXIS_PERIPHERALS):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_PERIPHERALS);

                case (AxisSettings.AXIS_ROBOT):
                    return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT);
                default:
                    return false;
            }
        }
        #endregion

        #region gripper methods
        /// <summary>
        ///  returns jaw opening in milimeters
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
        /// returns jaw opening in percentage
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
        ///  closes gripper
        /// </summary>
        /// <returns></returns>
        public bool closeGripper()
        {
           return _wrapper.closeGripperWrapped();
        }
        /// <summary>
        /// opens gripper
        /// </summary>
        /// <returns></returns>
        public bool openGripper()
        {
            return _wrapper.openGripperWrapped();
        }
        #endregion
        
        #region test methods
        public void testHelloName(string s)
        {
            Console.WriteLine("hello "+s);
        }
        #endregion
    }
}