/** \file simulator.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;

namespace DSL
{
    public class Simulator : IRobot
    {
        #region properties

        
        public bool gripperClosed { get; set; }

        private IUI iuiOutput;
        public IUI IUIOutput
        {
            get { return iuiOutput; }
            set { iuiOutput = value; }
        }

        private VecPoint _currentposition;
        public VecPoint Currentposition
        {
            get { return _currentposition; }
            set { _currentposition = value; }
        }

        #endregion

        #region Methods
        public Simulator()
        {
            Currentposition = new VecPoint(0,0,0,0,0);
            iuiOutput = new ConsoleUI(); // Don´t move location.
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
            iuiOutput.writeLine("The robot is moving to X: " + _x + "  Y: " + _y + "  Z: " + _z + "  Pitch: " +
                              _pitch + " Roll: " + _roll);
            Currentposition.iX = _x;
            Currentposition.iY = _y;
            Currentposition.iZ = _z;
            Currentposition.iPitch = _pitch;
            Currentposition.iRoll = _roll;
            return true;
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
            Currentposition.iX = x;
            Currentposition.iY = y;
            Currentposition.iZ = z;
            Currentposition.iPitch = pitch;
            Currentposition.iRoll = roll;
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
            Currentposition.iX += _iX;
            Currentposition.iY += _iY;
            Currentposition.iZ += _iZ;
            Currentposition.iPitch += _iPitch;
            Currentposition.iRoll += _iRoll;
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
            Currentposition.iX = x;
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
            Currentposition.iY = y;
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
            Currentposition.iZ = z;
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
            Currentposition.iPitch = pitch;
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
            Currentposition.iRoll = roll;
            return true;
        }


        public bool Time(Wrapper.enumBGroup _bGroup, long _mTime)
        {
            iuiOutput.writeLine("New Timer for future movements: " + _mTime + "Miliseconds");
            return true;

        }

        public bool Speed(Wrapper.enumBGroup _bGroup, long _mSpeed)
        {
            iuiOutput.writeLine("New Speed for future movements: " + _mSpeed + "%");
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
            Currentposition.iX = _iX;
            Currentposition.iY = _iY;
            Currentposition.iZ = _iZ;
            return true;
        }

        /// <summary>
        /// NO COMMENt
        /// </summary>
        /// <returns></returns>
        public VecPoint getCurrentPosition()
        {
            return Currentposition;
        }

        public void moveByDatabasePosition(int _iCubeID)
        {
            throw new NotImplementedException();
        }

        public string getCurrentPositionAsString()
        {
            return Currentposition.ToString();
        }
        #endregion

    }
}