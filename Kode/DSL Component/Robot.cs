using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WrapperTester;

namespace DSL_component
{
       public enum JawOpeningParameter
        {
            Milimeters,
            Percentage
        }

        public enum ManualModeType
        {   
            Off,
            Axes,
            Coordinates
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
        bool Control(AxisSettings axis, bool control);
        ManualModeType ManualMode { get; set; }
        bool moveManual(byte partmoved, int speed);
        bool stopMove(AxisSettings axis);
        short getJawOpeningWidth(JawOpeningParameter parameter);

    }
    public class Robot : IRobot
    {
        private Wrapper _wrapper;

     
        public Robot()
        {
            _wrapper = Wrapper.getInstance();
        }

        public bool Initialization()
        {
            return _wrapper.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE,
                                                  Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT);
        }

        public bool Control(AxisSettings axis, bool control)
        {
            switch (axis)
            {
                case (AxisSettings.AXIS_0):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_0,control);

                case (AxisSettings.AXIS_1):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_1, control);

                case (AxisSettings.AXIS_2):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_2,control);

                case (AxisSettings.AXIS_3):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_3,control);

                case (AxisSettings.AXIS_4):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_4,control);

                case (AxisSettings.AXIS_5):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_5, control);

                case (AxisSettings.AXIS_6):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_6,control);

                case (AxisSettings.AXIS_7):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_7, control);

                case (AxisSettings.AXIS_ALL):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, control);

                case (AxisSettings.AXIS_PERIPHERALS):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_PERIPHERALS, control);

                case (AxisSettings.AXIS_ROBOT):
                    return _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, control);
                default:
                    return false;
            }
        }

        public ManualModeType ManualMode
        {
            get
            {
                return ManualMode;
            }
            set
            {
                if (value == ManualModeType.Axes)
                {
                    if (_wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES))
                        ManualMode = value;
                }

                else if (value == ManualModeType.Coordinates)
                {
                    if (_wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD))
                        ManualMode = value;
                }

                else if (value == ManualModeType.Off)
                {
                    if (_wrapper.closeManualWrapped())
                        ManualMode = value;
                }

                else
                    throw new Exception("ManualMode didnt set correctly"); 
            }
        }

        public bool moveManual(int partmoved, int speed)
        {
            byte temp_part = (byte) partmoved;

            if (ManualMode == ManualModeType.Axes && partmoved <= 7 && partmoved >= 0)
            {
                return _wrapper.moveManualWrapped(temp_part, speed);

            }
            else if (ManualMode == ManualModeType.Coordinates && partmoved <= 4 && partmoved >= 0)
            {
                return _wrapper.moveManualWrapped(temp_part, speed);
            }
            else
                throw new ArgumentOutOfRangeException("partmoved", "moveManual parameters incorrect");
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

        public short getJawOpeningWidth(JawOpeningParameter mode)
        {
            short milimeters = 0, percentage = 0;
            bool status = _wrapper.getJawWrapped(ref milimeters, ref percentage);

            if (mode == JawOpeningParameter.Percentage && status)
                return percentage;
            else if (mode == JawOpeningParameter.Milimeters && status)
                return milimeters;
            else
                throw new Exception();
            
        }

        public bool closeGripper()
        {
           return _wrapper.closeGripperWrapped();
        }

        public bool openGripper()
        {
            return _wrapper.openGripperWrapped();
        }

        public void testHelloName(string s)
        {
            Console.WriteLine("hello "+s);
        }
    }
}