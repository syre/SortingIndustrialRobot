/** \file simulator.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;

namespace DSL
{
    public class Simulator : IRobot
    {
        #region properties

        private VecPoint _currentposition;
        private ManualModeType _type;
        public bool gripperClosed { get; set; }
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
            _currentposition = new VecPoint(0,0,0,0,0);
            iuiOutput = new ConsoleUI(); // Don´t move location.
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
            gripperClosed = true;
            return true;
        }

        /// <summary>
        /// Opens the robot gripper
        /// </summary>
        /// <returns>returns true if the gripper opens</returns>
        public bool openGripper()
        {
            iuiOutput.writeLine("Gripper opening!");
            gripperClosed = false;
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
                _currentposition.iX = _x;
                _currentposition.iY = _y;
                _currentposition.iZ = _z;
                _currentposition.iPitch = _pitch;
                _currentposition.iRoll = _roll;
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

        /// <summary>
        /// Moving the base
        /// </summary>
        /// <param name="speed"> speed</param>
        /// <returns>always true</returns>
        public bool moveBase(int speed)
        {
           iuiOutput.writeLine("Base moving");
           return true;
        }

        /// <summary>
        /// Moving the shoulder
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>lways true</returns>
        public bool moveShoulder(int speed)
        {
            iuiOutput.writeLine("Shoulder moving");
            return true;
        }

        /// <summary>
        /// Moving the wrist Pitch
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>always true</returns>
        public bool moveWristPitch(int speed)
        {
            iuiOutput.writeLine("Wrist Pitch moving");
            return true;
        }

        /// <summary>
        /// Moving the Wrist Roll
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>always true</returns>
        public bool moveWristRoll(int speed)
        {
            iuiOutput.writeLine("Wrist Roll moving");
            return true;
        }

        /// <summary>
        /// Moving the elbow
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>always true</returns>
        public bool moveElbow(int speed)
        {
            iuiOutput.writeLine("Elbow moving");
            return true;
        }

        /// <summary>
        /// Moving the gripper
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>always true</returns>
        public bool moveGripper(int speed)
        {
            iuiOutput.writeLine("Gripper moving");
            return true;
        }

        /// <summary>
        /// Moving the Conveyer Belt
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>always true</returns>
        public bool moveConveyerBelt(int speed)
        {
            iuiOutput.writeLine("Conveyer Belt moving");
            return true;
        }

        // Interface V2 additions
        /// <summary>
        /// Moving from the absolute position (home position)
        /// </summary>
        /// <params name="ALL">represents each coordinate values</params>
        /// <returns>always true</returns>
        public bool moveByAbsoluteCoordinates(int x, int y, int z, int pitch, int roll)
        {
            iuiOutput.writeLine("Absolute Coordinate X: {0}, Y: {1}, Z: {2}, Pitch: {3}, Roll: {4} ", x,y,z,pitch,roll);
            _currentposition.iX = x;
            _currentposition.iY = y;
            _currentposition.iZ = z;
            _currentposition.iPitch = pitch;
            _currentposition.iRoll = roll;
            return true;
        }

        /// <summary>
        /// Moving from the realative position (current position)
        /// </summary>
        /// <params name="ALL">represents each coordinate values</params>
        /// <returns>always true</returns>
        public bool moveByRelativeCoordinates(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll)
        {
            iuiOutput.writeLine("Absolute Coordinate X: {0}, Y: {1}, Z: {2}, Pitch: {3}, Roll: {4} ", _iX, _iY, _iZ, _iPitch, _iRoll);
            _currentposition.iX += _iX;
            _currentposition.iY += _iY;
            _currentposition.iZ += _iZ;
            _currentposition.iPitch += _iPitch;
            _currentposition.iRoll += _iRoll;
            return true;
        }

        /// <summary>
        /// Moves Just X coordinate
        /// </summary>
        /// <param name="x">value for the x coordinate</param>
        /// <returns>always true</returns>
        public bool moveByXCoordinate(int x)
        {
            iuiOutput.writeLine("New Cordinate X: "+ x);
            _currentposition.iX = x;
            return true;
        }

        /// <summary>
        /// Moves Just Y coordinate
        /// </summary>
        /// <param name="y">value for the y coordinate</param>
        /// <returns>always true</returns>
        public bool moveByYCoordinate(int y)
        {
            iuiOutput.writeLine("New Cordinate Y: " + y);
            _currentposition.iY = y;
            return true;
        }
        /// <summary>
        /// Moves Just Z coordinate
        /// </summary>
        /// <param name="z">value for the z coordinate</param>
        /// <returns>always true</returns>
        public bool moveByZCoordinate(int z)
        {
            iuiOutput.writeLine("New Cordinate Y: " + z);
            _currentposition.iZ = z;
            return true;
        }

        /// <summary>
        /// Moves Just pitch coordinate
        /// </summary>
        /// <param name="pitch">value for the pitch coordinate</param>
        /// <returns>always true</returns>
        public bool moveByPitch(int pitch)
        {
            iuiOutput.writeLine("New Cordinate Pitch: " + pitch);
            _currentposition.iPitch = pitch;
            return true;
        }

        /// <summary>
        /// Moves Just roll coordinate
        /// </summary>
        /// <param name="roll">value for the roll coordinate</param>
        /// <returns>always true</returns>
        public bool moveByRoll(int roll)
        {
            iuiOutput.writeLine("New Cordinate Roll: " + roll);
            _currentposition.iRoll = roll;
            return true;
        }

        /// <summary>
        /// Moves all coordinates
        /// </summary>
        /// <params>value for the three coordinates</params>
        /// <returns>always true</returns>
        public bool movebyCoordinates(int _iX, int _iY, int _iZ)
        {
            iuiOutput.writeLine("New Coordinates X: "+_iX+"  Y: "+_iY+"  Z: "+_iZ);
            _currentposition.iX = _iX;
            _currentposition.iY = _iY;
            _currentposition.iZ = _iZ;
            return true;
        }

        /// <summary>
        /// NO COMMENt
        /// </summary>
        /// <returns></returns>
        public VecPoint getCurrentPosition()
        {
            return _currentposition;
        }

        public string getCurrentPositionAsString()
        {
            return _currentposition.ToString();
        }
        #endregion

    }
}