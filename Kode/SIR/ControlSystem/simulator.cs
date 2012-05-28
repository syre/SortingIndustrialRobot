/** \file simulator.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Threading;

namespace ControlSystem
{
    /// <summary>
    /// IRobot implementation using IUI output interface for simulating robot behavior.
    /// </summary>
    public class Simulator : IRobot
    {
        #region Properties and members

        private IUI iuiOutput;
        /// <summary>
        /// Output for writing robot operations.
        /// </summary>
        public IUI IUIOutput
        {
            get { return iuiOutput; }
            set { iuiOutput = value; }
        }

        private VecPoint _currentposition;
        /// <summary>
        /// Its current position.
        /// </summary>
        public VecPoint Currentposition
        {
            get { return _currentposition; }
            set { _currentposition = value; }
        }
        private bool bGripperIsOpen;
        #endregion

        #region Methods
        /// <summary>
        /// Default constructor.
        /// 
        /// Note: Uses console output as standard.
        /// </summary>
        public Simulator()
        {

            Currentposition = new VecPoint(0,0,0,0,0);
            iuiOutput = new ConsoleUI(); // Don´t move location.
            iuiOutput.writeLine("Simulation is started!");
            bGripperIsOpen = false;
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
        /// Close the gripper
        /// </summary>
        /// <returns>Returns true if the gripper closes</returns>
        public bool closeGripper()
        {
            iuiOutput.writeLine("Gripper closing!");
            bGripperIsOpen = false;
            return true;
        }

        /// <summary>
        /// Opens the gripper
        /// </summary>
        /// <returns>returns true if the gripper opens</returns>
        public bool openGripper()
        {
            iuiOutput.writeLine("Gripper opening!");
            bGripperIsOpen = true;
            return true;
        }

        /// <summary>
        /// Initializes the robot to default values
        /// </summary>
        /// <returns>true if the initialization is ok!</returns>
        private bool initialization()
        {
            iuiOutput.writeLine("The system is online, and system types is set to default!");
            return true;
        }

        /// <summary>
        /// Function for moving by coordinates
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

        /// <summary>
        /// Gets width of jaw opening in milimeters
        /// </summary>
        /// <returns></returns>
        public short getJawOpeningWidthMilimeters()
        {
            if(bGripperIsOpen)
                return(200);
            return(0);
        }

        /// <summary>
        /// Gets width of jaw opening in percent
        /// </summary>
        /// <returns></returns>
        public short getJawOpeningWidthPercentage()
        {
            if(bGripperIsOpen)
                return(100);
            return(0);
        }

        /// <summary>
        /// Setting robot to start position
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
        /// <param name="speed">speed</param>
        /// <returns>Always true.</returns>
        public bool moveBase(int speed)
        {
           iuiOutput.writeLine("Base moving");
           return true;
        }

        /// <summary>
        /// Moving the shoulder
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>Always true.</returns>
        public bool moveShoulder(int speed)
        {
            iuiOutput.writeLine("Shoulder moving");
            return true;
        }

        /// <summary>
        /// Moving the wrist Pitch
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>Always true.</returns>
        public bool moveWristPitch(int speed)
        {
            iuiOutput.writeLine("Wrist Pitch moving");
            return true;
        }

        /// <summary>
        /// Moving the Wrist Roll
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>Always true.</returns>
        public bool moveWristRoll(int speed)
        {
            iuiOutput.writeLine("Wrist Roll moving");
            return true;
        }

        /// <summary>
        /// Moving the elbow
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>Always true.</returns>
        public bool moveElbow(int speed)
        {
            iuiOutput.writeLine("Elbow moving");
            return true;
        }

        /// <summary>
        /// Moving the gripper
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>Always true.</returns>
        public bool moveGripper(int speed)
        {
            iuiOutput.writeLine("Gripper moving");
            if(speed > 0)
                bGripperIsOpen = true;
            else if(speed < 0)
                bGripperIsOpen = false;
            return true;
        }

        /// <summary>
        /// Moving the Conveyer Belt
        /// </summary>
        /// <param name="speed">speed</param>
        /// <returns>Always true.</returns>
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
        /// <returns>Always true.</returns>
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
        /// Moving from the relative position (current position)
        /// </summary>
        /// <params name="ALL">represents each coordinate values</params>
        /// <returns>Always true.</returns>
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
        /// Moves just X coordinate
        /// </summary>
        /// <param name="x">value for the x coordinate</param>
        /// <returns>Always true.</returns>
        public bool moveByXCoordinate(int x)
        {
            iuiOutput.writeLine("New Coordinate X: "+ x);
            Currentposition.iX = x;
            return true;
        }

        /// <summary>
        /// Moves just Y coordinate
        /// </summary>
        /// <param name="y">value for the y coordinate</param>
        /// <returns>Always true.</returns>
        public bool moveByYCoordinate(int y)
        {
            iuiOutput.writeLine("New Coordinate Y: " + y);
            Currentposition.iY = y;
            return true;
        }
        /// <summary>
        /// Moves just Z coordinate
        /// </summary>
        /// <param name="z">value for the z coordinate</param>
        /// <returns>Always true.</returns>
        public bool moveByZCoordinate(int z)
        {
            iuiOutput.writeLine("New Coordinate Y: " + z);
            Currentposition.iZ = z;
            return true;
        }

        /// <summary>
        /// Moves just pitch coordinate
        /// </summary>
        /// <param name="pitch">value for the pitch coordinate</param>
        /// <returns>Always true.</returns>
        public bool moveByPitch(int pitch)
        {
            iuiOutput.writeLine("New Coordinate Pitch: " + pitch);
            Currentposition.iPitch = pitch;
            return true;
        }

        /// <summary>
        /// Moves just roll coordinate
        /// </summary>
        /// <param name="roll">value for the roll coordinate</param>
        /// <returns>Always true.</returns>
        public bool moveByRoll(int roll)
        {
            iuiOutput.writeLine("New Coordinate Roll: " + roll);
            Currentposition.iRoll = roll;
            return true;
        }

        /// <summary>
        /// Time for future movements in miliseconds
        /// </summary>
        /// <param name="_bGroup">Part of the robot </param> 
        /// /// <param name="_mTime">Value for time</param>
        /// <returns>Always true.</returns>
        public bool Time(Wrapper.enumAxisSettings _bGroup, long _mTime)
        {
            iuiOutput.writeLine("New Time for future movements: " + _mTime + "Miliseconds");
            return true;

        }

        /// <summary>
        /// Speed for future movements in percent
        /// </summary>
        /// <param name="_bGroup">Part of the robot </param> 
        /// /// <param name="_mSpeed">Value for speed</param>
        /// <returns>Always true.</returns>
        public bool Speed(Wrapper.enumAxisSettings _bGroup, long _mSpeed)
        {
            iuiOutput.writeLine("New Speed for future movements: " + _mSpeed + "%");
            return true;
        }

        /// <summary>
        /// Moves all coordinates
        /// </summary>
        /// <params>value for the three coordinates</params>
        /// <returns>Always true.</returns>
        public bool movebyCoordinates(int _iX, int _iY, int _iZ)
        {
            iuiOutput.writeLine("New Coordinates X: "+_iX+"  Y: "+_iY+"  Z: "+_iZ);
            Currentposition.iX = _iX;
            Currentposition.iY = _iY;
            Currentposition.iZ = _iZ;
            return true;
        }

        /// <summary>
        /// Gets current position
        /// </summary>
        /// <returns>Currentposition</returns>
        public VecPoint getCurrentPosition()
        {
            iuiOutput.writeLine("Current Position: {0}", Currentposition.ToString());
            return Currentposition;
        }

        
        public bool moveToCubePosition(int _iCubeID)
        {
            iuiOutput.writeLine("Moved to position with cube ID"+_iCubeID);
            return(true);
        }

        public string getCurrentPositionAsString()
        {

            return Currentposition.ToString();
        }

        public double getWeight()
        {
            iuiOutput.writeLine("Weight: 10,12");
            return 10.12;
        }

        #endregion

    }
}