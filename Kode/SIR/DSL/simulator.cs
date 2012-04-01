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
                    iuiOutput.writeLine("Manual mode set to: " + value);
                    _type = value;
                }

                else
                    iuiOutput.writeLine("Out of Range, Manual mode unchanged");
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
                    iuiOutput.writeLine("Control mode set to: " + value);
                    _modeType = value;
                }
                else
                    iuiOutput.writeLine("Out of Range, Control mode unchanged");
            }
        }

        private IUI iuiOutput;
        public IUI IUIOutput
        {
            get { return iuiOutput; }
            set { iuiOutput = value; }
        }

        #endregion

        #region Methods
        public Simulator()
        {
            iuiOutput = new ConsoleUI(); // Don´t move location.
            ControlMode = ControlModeType.Off;
            ManualMode = ManualModeType.Off;
            iuiOutput.writeLine("Simulation is started!");
        }

        /// <summary>
        /// Writes out that All movement is stopped
        /// </summary>
        /// <returns> returns true if it is stopped</returns>
        public bool stopAllMovement()
        {
            iuiOutput.writeLine("All axes stopped, no one moves");
            return true;
        }

        /// <summary>
        /// It will close the gripper
        /// </summary>
        /// <returns>Returns true if the gripper closes</returns>
        public bool closeGripper()
        {
            iuiOutput.writeLine("Gripper closing!");
            return true;
        }

        /// <summary>
        /// Opens the robot gripper
        /// </summary>
        /// <returns>returns true if the gripper opens</returns>
        public bool openGripper()
        {
            iuiOutput.writeLine("Gripper opening!");
            return true;
        }

        /// <summary>
        /// Initializes the robot to default values
        /// </summary>
        /// <returns>true if the initialization is ok!</returns>
        public bool initialization()
        {
            iuiOutput.writeLine("The system is online, and system types is set to default!");
            return true;
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
                iuiOutput.writeLine(axis.ToString() + " is stopped!");
                return true;
            }
            else
                iuiOutput.writeLine("Unknown axis, the state is unchanged");


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
                iuiOutput.writeLine("The robot is moving to X: " + _x + "  Y: " + _y + "  Z: " + _z + "  Pitch: " +
                                  _pitch + " Roll: " + _roll);
                return true;
            }
            //Message like exception.
            else
                iuiOutput.writeLine("Please deactivate automode");

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
                iuiOutput.writeLine(e.Message);
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
                iuiOutput.writeLine(e.Message);
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
            iuiOutput.writeLine("Robot moved to home position");
            return true;
        }

        /// <summary>
        /// Connect to the robot
        /// </summary>
        /// <returns>true if there are connection</returns>
        public bool isOnline()
        {
            iuiOutput.writeLine("Connected to the robot!");
            return true;
        }
        #endregion
    }




    //Two functions howto?
    //Søren = Integration with python code (fælles)?
    //
}