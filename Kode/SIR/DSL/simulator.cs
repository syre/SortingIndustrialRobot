/** \file simulator.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;

namespace DSL
{
    public class Simulator : IRobot
    {
        #region properties

        private ManualModeType _type;
        public ManualModeType ManualMode
        {
            get { return _type; }
            set
            {

                if (value == ManualModeType.Axes || value == ManualModeType.Off ||
                    value == ManualModeType.Coordinates)
                {
                    Console.WriteLine("Manual mode set to: " + value);
                    _type = value;
                }

                else
                    Console.WriteLine("Out of Range, Manual mode unchanged");

            }

        }

        private ControlModeType _modeType;
        public ControlModeType ControlMode
        {
            get { return _modeType; }
            set
            {
                if (value == ControlModeType.Off || value == ControlModeType.On)
                {
                    Console.WriteLine("Control mode set to: " + value);
                    _modeType = value;
                }
                else
                    Console.WriteLine("Out of Range, Control mode unchanged");


            }
        }

        #endregion

        #region Methods
        public Simulator()
        {
            Console.WriteLine("Simulation is started!");
            ControlMode = ControlModeType.Off;
            ManualMode = ManualModeType.Off;


        }

        /// <summary>
        /// It will close the gripper
        /// </summary>
        /// <returns>Returns true if the gripper closes</returns>
        public bool closeGripper()
        {
            Console.WriteLine("Gripper closing!");
            return true;
        }

        /// <summary>
        /// Opens the robot gripper
        /// </summary>
        /// <returns>returns true if the gripper opens</returns>
        public bool openGripper()
        {
            Console.WriteLine("Gripper opening!");
            return true;
        }

        /// <summary>
        /// Initializes the robot to default values
        /// </summary>
        /// <returns>true if the initialization is ok!</returns>
        public bool Initialization()
        {
            Console.WriteLine("The system is online, and system types is set to default!");
            return true;
        }


        public bool stopAllMovement()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops one axe
        /// </summary>
        /// <param name="axis">The axe to be stopped</param>
        /// <returns>returns true if axe is stopped</returns>
        public bool stopMove(AxisSettings axis)
        {
            if (axis == AxisSettings.AXIS_0 || axis == AxisSettings.AXIS_1 || axis == AxisSettings.AXIS_2 ||
                axis == AxisSettings.AXIS_3 || axis == AxisSettings.AXIS_4 || axis == AxisSettings.AXIS_5 ||
                    axis == AxisSettings.AXIS_6 || axis == AxisSettings.AXIS_7 || axis == AxisSettings.AXIS_ROBOT ||
                        axis == AxisSettings.AXIS_ALL || axis == AxisSettings.AXIS_PERIPHERALS)
            {
                Console.WriteLine(axis.ToString() + " is stopped!");
                return true;
            }
            else
                Console.WriteLine("Unknown axis, the state is unchanged");


            return false;
        }


        /// <summary>
        /// function for moving by coordinates
        /// </summary>
        /// <param name="_x"> x-coordinate </param>
        /// <param name="_y"> y-coordinate </param>
        /// <param name="_z"> z-coordinate </param>
        /// <param name="_pitch"> pitch robot arm</param>
        /// <param name="_roll"> roll of robot arm</param>
        /// <returns></returns>
        public bool moveByCoordinates(int _x, int _y, int _z, int _pitch, int _roll)
        {
            //If ManualMode is set to on
            if (ManualMode != ManualModeType.Off)
            {
                Console.WriteLine("The robot is moving to X: " + _x + "  Y: " + _y + "  Z: " + _z + "  Pitch: " +
                                  _pitch + " Roll: " + _roll);
                return true;
            }
            //Message like exception.
            else
                Console.WriteLine("Please deactivate automode");

            return false;



        }

        public short getJawOpeningWidthMilimeters()
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        public short getJawOpeningWidthPercentage()
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        /// <summary>
        /// Robot to start position
        /// </summary>
        /// <returns>true if robot goes home.</returns>
        public bool homeRobot()
        {
            ManualMode = ManualModeType.Coordinates;
            moveByCoordinates(0, 0, 0, 0, 0);
            Console.WriteLine("Robot moved to home position");
            return true;
        }

        /// <summary>
        /// Connect to the robot
        /// </summary>
        /// <returns>true if there are connection</returns>
        public bool isOnline()
        {
            Console.WriteLine("Connected to the robot!");
            return true;
        }
        #endregion
    }




    //Two functions howto?
    //Søren = Integration with python code (fælles)?
    //
}