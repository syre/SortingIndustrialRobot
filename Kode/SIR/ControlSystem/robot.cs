/** \file robot.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Data;
using System.Management.Instrumentation;
using SqlInteraction;

namespace ControlSystem
{
    /// <summary>
    /// The interface that Robot and Simulator are based on
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        ///  Closes gripper
        /// </summary>
        /// <returns></returns>
        bool closeGripper();
        /// <summary>
        /// Opens gripper
        /// </summary>
        /// <returns></returns>
        bool openGripper();
        /// <summary>
        /// Initializes robot with default values MODE_ONLINE and SYSTEM_TYPE_DEFAULT
        /// </summary>
        /// <returns></returns>
        bool initialization();
        
        /// <summary>
        /// Calls wrapper function for stopping all movement
        /// </summary>
        /// <returns></returns>
        bool stopAllMovement();
        /// <summary>
        /// Moves by x coordinate only
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        bool moveByXCoordinate(int x);
        /// <summary>
        /// Moves by y coordinate only
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        bool moveByYCoordinate(int y);
        /// <summary>
        /// Moves by z coordinate only
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        bool moveByZCoordinate(int z);
        /// <summary>
        /// Moves by pitch only
        /// </summary>
        /// <param name="pitch"></param>
        /// <returns></returns>
        bool moveByPitch( int pitch);
        /// <summary>
        /// Moves by roll only
        /// </summary>
        /// <param name="roll"></param>
        /// <returns></returns>
        bool moveByRoll(int roll);
        /// <summary>
        /// moves by coordinates x, y and z
        /// </summary>
        /// <param name="_iX"></param>
        /// <param name="_iY"></param>
        /// <param name="_iZ"></param>
        /// <returns></returns>
        bool movebyCoordinates(int _iX, int _iY, int _iZ);
        /// <summary>
        /// Function for moving by absolute coordinates
        /// </summary>
        /// <param name="_iX"> x-coordinate </param>
        /// <param name="y"> y-coordinate </param>
        /// <param name="z"> z-coordinate </param>
        /// <param name="pitch"> pitch robot arm</param>
        /// <param name="roll"> roll of robot arm</param>
        /// <returns></returns>
        bool moveByAbsoluteCoordinates(int x, int y, int z, int pitch, int roll);
        /// <summary>
        /// function for moving by relative coordinates
        /// </summary>
        /// <param name="_iX"></param>
        /// <param name="_iY"></param>
        /// <param name="_iZ"></param>
        /// <param name="_iPitch"></param>
        /// <param name="_iRoll"></param>
        /// <returns></returns>
        bool moveByRelativeCoordinates(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll);
        /// <summary>
        ///  Returns jaw opening in milimeters
        /// </summary>
        /// <returns></returns>
        short getJawOpeningWidthMilimeters();
        /// <summary>
        ///  Returns jaw opening in percentage
        /// </summary>
        /// <returns></returns>
        short getJawOpeningWidthPercentage();
        /// <summary>
        /// Homes robot
        /// </summary>
        /// <returns></returns>
        bool homeRobot();
        /// <summary>
        /// Checks to see if robot is online
        /// </summary>
        /// <returns></returns>
        bool isOnline();
        /// <summary>
        /// Separate function for moving base of robot
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        bool moveBase(int speed);
        /// <summary>
        /// Separate function for moving shoulder of robot
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        bool moveShoulder(int speed);
        /// <summary>
        /// Separate function for moving wrist pitch of robot
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        bool moveWristPitch(int speed);
        /// <summary>
        /// Separate function for moving wrist roll of robot
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        bool moveWristRoll(int speed);
        /// <summary>
        /// Separate function for moving elbow of robot
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        bool moveElbow(int speed);
        /// <summary>
        /// Separate function for moving robot gripper
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        bool moveGripper(int speed);
        
        /// <summary>
        /// Separate function for moving robot conveyer belt
        /// </summary>
        /// <param name="_iSpeed"></param>
        /// <returns></returns>
        bool moveConveyerBelt(int speed);
        
        /// <summary>
        /// Separate function for getting Current position in string
        /// </summary>
        /// <returns></returns>
        string getCurrentPositionAsString();

        /// <summary>
        /// function for getting current position as VecPoint
        /// </summary>
        /// <returns></returns>
        VecPoint getCurrentPosition();

        /// <summary>
        /// Moves to position from Cube ID.(From Database.)
        /// </summary>
        /// <param name="_iCubeID">ID of Cube.</param>
        bool moveToCubePosition(int _iCubeID);

        /// <summary>
        /// Sets the time future movement should take.
        /// </summary>
        /// <param name="_bGroup">bool ucGroup
        ///       Axis group to which the time should be applied
        ///       '&' for all axes
        ///       '0'-'7' for axis movements
        ///       'A' for robot movements
        ///       'B' for peripheral movements
        ///       'G' for gripper movements
        /// <param name="_mTime">
        ///       Time in milliseconds</param>
        bool Time(Wrapper.enumBGroup _bGroup, long _mTime);

        /// <summary>
        ///     Sets the speed future movement should take.
        /// </summary>
        /// <param name="_bGroup">bool ucGroup
        ///       Axis group to which the time should be applied
        ///       '&' for all axes
        ///       '0'-'7' for axis movements
        ///       'A' for robot movements
        ///       'B' for peripheral movements
        ///       'G' for gripper movements
        /// <param name="_mSpeed">
        ///      Speed in percent of max speed</param>
        bool Speed(Wrapper.enumBGroup _bGroup, long _mSpeed);


        /// <summary>
        ///     Get the Weight from serielSTK
        /// </summary>
        /// <returns>the wieght in double</returns>
        double getWeight();


    }
    public class Robot : IRobot
    {
        private IWrapper _wrapper;
        private SerialSTK _serialStk;
        DLL.DgateCallBack dgateEventHandlerSuccess = initSuccess;
        DLL.DgateCallBack dgateEventHandlerError = initError;
        DLL.DgateCallBackByteRefArg dgateEventHandlerHoming = homeEvent;

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
        /// <summary>
        /// gets an instance of the wrapper,
        /// runs initialization method,
        /// enables movement control for the AXIS_Robot and homes the robot
        /// </summary>
        public Robot()
        {
            _serialStk = new SerialSTK();
            _wrapper = Wrapper.getInstance();
            initialization();
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, true);
           Time(Wrapper.enumBGroup.GroupAnd, 60000);
            //homeRobot();
        }

        public bool initialization() // implementing delegates
        {
            return _wrapper.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE,
                                                  Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT,
                                                  dgateEventHandlerSuccess,
                                                  dgateEventHandlerError);
        }

        public bool homeRobot() // implementing delegates
        {
            return _wrapper.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming);
        }


        public bool stopAllMovement()
        {
            return _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT);
        }

        public bool isOnline()
        {
            return _wrapper.isOnlineOkWrapped();
        }

        public VecPoint getCurrentPosition()
        {
            return _wrapper.getCurrentPosition();
        }

        public bool Time(Wrapper.enumBGroup _bGroup, long _mTime)
        {
            return _wrapper.timeWrapped(_bGroup, _mTime);
        }

        public bool Speed(Wrapper.enumBGroup _bGroup, long _mSpeed)
        {
            return _wrapper.speedWrapped(_bGroup, _mSpeed);
        }

        public double getWeight()
        {
            return _serialStk.ReadADC();
        }

        #endregion

        #region Coordinate movements

        public bool moveByAbsoluteCoordinates(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll) // subject to change
        {
            // ONLY PARTIALLY IMPLEMENTED - NOT WORKING
            
            SIRVector tempCordVector = new AbsCoordSirVector("absoluteVector");
            tempCordVector.addPoint(new VecPoint(_iX,_iY,_iZ,_iPitch,_iRoll));
            _wrapper.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, "absoluteVector",5); // shrtlength??
            _wrapper.moveLinearWrapped("defaultVector", 5); // index?? 
            return false; 
        }

        public bool moveByRelativeCoordinates(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll)
        {
            // ONLY PARTIALLY IMPLEMENTED - NOT WORKING

            SIRVector tempRelVector = new RelCoordSirVector("relativeVector");
            tempRelVector.addPoint(new VecPoint(_iX, _iY, _iZ, _iPitch, _iRoll));

            return false;
        }

        public bool movebyCoordinates(int _iX, int _iY, int _iZ)
        {
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_X, _iX))
                return false;
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y, _iY))
                return false;
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z, _iZ))
                return false;
            return true;
        }

        public bool moveToCubePosition(int _iCubeID)
        {
            var sqlcmdCommand = SQLHandler.GetInstance.makeCommand("SELECT ID FROM Position WHERE ID = " + _iCubeID);
            var isqlrdrReader = SQLHandler.GetInstance.runQuery(sqlcmdCommand, "Read");
            var lstCoordinates = isqlrdrReader.readRow();
            // element 0 is ID, so starts from element 1(X)
            if (lstCoordinates.Count != 0)
            {
                movebyCoordinates((int)lstCoordinates[1], (int)lstCoordinates[2], (int)lstCoordinates[3]);
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Axis movements
        public bool moveBase(int _iSpeed)
        {
            //ManualMode = ManualModeType.Axes;
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            return  _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, _iSpeed);
        }

        public bool moveShoulder(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_SHOULDER, _iSpeed);
        }

        public bool moveElbow(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_ELBOW, _iSpeed);
        }

        public bool moveWristPitch(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTPITCH, _iSpeed);
        }

        public bool moveWristRoll(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTROLL, _iSpeed);
        }

        public bool moveGripper(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, _iSpeed);
        }

        public bool moveConveyerBelt(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT, _iSpeed);
        }



        public string getCurrentPositionAsString()
        {
            
            VecPoint _vect;
            _vect = _wrapper.getCurrentPosition();
      
            return (_vect.iX.ToString() + " " + _vect.iY.ToString() + " " + _vect.iZ.ToString() + " " + _vect.iPitch.ToString() + " " + _vect.iRoll.ToString());

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

        #region MovejustOneCordinate
        public bool moveByXCoordinate(int _iX)
        {
            //ManualMode = ManualModeType.Coordinates;
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_X, _iX))
                return false;
            return true;
        }

        public bool moveByYCoordinate(int _iY)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y, _iY))
                return false;
            return true;
        }


        public bool moveByZCoordinate(int _iZ)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z, _iZ))
                return false;
            return true;
        }

        public bool moveByPitch(int _pitch)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_PITCH, _pitch))
                return false;
            return true;
        }

        public bool moveByRoll(int _roll)
        {
                        _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_ROLL, _roll))
                return false;
            return true;
        }

        #endregion
    }
}