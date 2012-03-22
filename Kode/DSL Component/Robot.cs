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

        #region general robot methods
        
        public Robot()
        {
            _wrapper = Wrapper.getInstance();
        }

        public bool Initialization()
        {
            return _wrapper.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE,
                                                  Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT,
                                                  Wrapper.DgateCallBack,
                                                  Wrapper.DgateCallBack error);
        }

        public bool homeRobot()
        {
            _wrapper.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, Wrapper.DgateCallBackCharArg arg)
        }

        public bool isOnline()
        {
            return _wrapper.isOnlineOkWrapped();
        }
        #endregion

        #region Coordinate movements
        public bool moveByCoordinates(int x, int y, int z, int pitch, int roll) // subject to change
        {

            if (ManualMode == ManualModeType.Coordinates)
            {

            }       
        }
        #endregion

        #region Axis movements
        
        public bool moveBase(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return  _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, speed);
        }

        public bool moveShoulder(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_SHOULDER, speed);
        }

        public bool moveElbow(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_ELBOW, speed);
        }

        public bool moveWristPitch(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTPITCH, speed);
        }

        public bool moveWristRoll(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTROLL, speed);
        }

        public bool moveGripper(int speed)
        {
            ManualMode = ManualModeType.Axes;
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, speed);
        }

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
        public short getJawOpeningWidthMilimeters()
        {
            short milimeters = 0, dummypercentage = 0;
            bool status = _wrapper.getJawWrapped(ref milimeters, ref dummypercentage);
            if (status)
                return milimeters;
            else
                throw new Exception("Error getting JawOpeningWidth in Milimeters"); 

        }
        public short getJawOpeningWidthPercentage()
        {
            short dummymilimeters = 0, percentage = 0;
            bool status = _wrapper.getJawWrapped(ref dummymilimeters, ref percentage);
            if (status)
                return percentage;
            else
                throw new Exception("Error getting JawOpeningWidth in Percentage");
        }

        public bool closeGripper()
        {
           return _wrapper.closeGripperWrapped();
        }

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