/** \file robot.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Instrumentation;
using System.Threading;
using SqlInteraction;

namespace ControlSystem
{
    /// <summary>
    /// The interface that Robot and Simulator are based on
    /// </summary>
    public interface IRobot
    {
        SIRVector vectorlist { get; set; } 
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
        bool moveElbow(int _iSpeed);
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
        bool Time(Wrapper.enumAxisSettings _bGroup, long _mTime);

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
        bool Speed(Wrapper.enumAxisSettings _bGroup, long _mSpeed);


        /// <summary>
        ///     Get the Weight from serielSTK
        /// </summary>
        /// <returns>the wieght in double</returns>
        double getWeight();


    }
    
    /// <summary>
    /// IRobot implementation using the SCORBOT for operations.
    /// </summary>
    public class Robot : IRobot
    {
        private IWrapper _wrapper;
        private SerialSTK _serialStk;
        public IWrapper wrapper
        {
            get { return _wrapper; }
            set { _wrapper = value; }
        }

        public SerialSTK STK
        {
            get { return _serialStk; }
            set { _serialStk = value; }
        }

        private DLL.DgateCallBack dgateEventHandlerSuccess = initSuccess;
        private DLL.DgateCallBack dgateEventHandlerError = initError;
        private DLL.DgateCallBackByteRefArg dgateEventHandlerHoming = homeEvent;
        private DLL.DgateCallBackByteRefArg dgateMovementStarted = takeMovementLock;
        private DLL.DgateCallBackByteRefArg dgateMovementStopped = releaseMovementLock;
        private static Semaphore movementlock;

        public SIRVector vectorlist { get; set; }


        public Semaphore Sem
        {
            get { return movementlock; }
        }

        #region delegate functions
        static void initSuccess(IntPtr _iptrArg)
        {
            Console.WriteLine("Initialized successfully.");
        }
        static void initError(IntPtr _iptrArg)
        {
            Console.WriteLine("Initialize error.");
        }
        static void homeEvent(ref byte _bArg)
        {
            Console.WriteLine("Home Event: " + _bArg);
        }
        /// <summary>
        /// method to be called when the robot starts its movement, implemented in each move-method instead (DUMMY)
        /// </summary>
        /// <param name="b"></param>
        private static void takeMovementLock(ref byte b)
        {
      
        }
        /// <summary>
        /// method to be called when the robot stops its movement
        /// </summary>
        /// <param name="b"></param>
        private static void releaseMovementLock(ref byte b)
        {
            Thread.Sleep(1000);
            movementlock.Release();
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
            vectorlist = new SIRVector();
            initialization();
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, true);
            Time(Wrapper.enumAxisSettings.AXIS_ROBOT, 60000);
            movementlock = new Semaphore(1,1);
           _wrapper.watchMotionWrapped(dgateMovementStopped, dgateMovementStarted);
        }

        private void initialization()
        {
            _wrapper.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE,
                                                  Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT,
                                                  dgateEventHandlerSuccess,
                                                  dgateEventHandlerError);
        }

        public bool homeRobot()
        {
            return _wrapper.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming);
        }

        public void moveToAPosition(SIRVector dummy)
        {
            SIRVector vector = new SIRVector();

            vector.LstPoints.Add(new VecPoint(1000, 20005, 3000, 4000, 5000));
            vector.LstPoints.Add(new VecPoint(2000, 2000, 3000, 4000, 5000));
            vector.LstPoints.Add(new VecPoint(3000, 2000, 3000, 4000, 5000));
            vector.LstPoints.Add(new VecPoint(4000, 2000, 30003, 4000, 5000));
            vector.LstPoints.Add(new VecPoint(5000, 2000, 3000, 40005, 5000));

            DLLImport.WatchMotion(dgateMovementStarted, dgateMovementStopped);

            DLLImport.DefineVector(Convert.ToByte('A'), "firstOne", Convert.ToInt16(vector.getSize()));
         
           
            Int16 i = 1;
            foreach (VecPoint point in vector.LstPoints)
            {
                int[] aInts = new int[]{point.iX,point.iY,point.iZ,point.iPitch,point.iRoll};
                DLLImport.Teach("firstOne", i++, aInts, 5, -32766);
            }
    
            //Home robotten før vi kører den til den givne position
            DLLImport.Home(Convert.ToByte('A'), dgateEventHandlerHoming);

            Int16 y = 1;
            //Flyt robotten til positionen
            foreach (VecPoint point in vector.LstPoints)
            {
                movementlock.WaitOne();
                DLLImport.MoveLinear("firstOne", y, null, 0);
                y++;
            }

        }

        public bool stopAllMovement()
        {
            bool status = _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT);
            // releasing semaphore here since movement functions wont release 
            if (status)
                movementlock.Release();
            return status;
        }

        public bool isOnline()
        {
            return _wrapper.isOnlineOkWrapped();
        }

        public VecPoint getCurrentPosition()
        {
            return _wrapper.getCurrentPosition();
        }

        public bool Time(Wrapper.enumAxisSettings _bGroup, long _mTime)
        {
            return _wrapper.timeWrapped(_bGroup, _mTime);
        }

        public bool Speed(Wrapper.enumAxisSettings _bGroup, long _mSpeed)
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

            SIRVector tempCordVector = new AbsCoordSirVector("absoluteVector");
            tempCordVector.addPoint(new VecPoint(_iX,_iY,_iZ,_iPitch,_iRoll));
            _wrapper.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, "absoluteVector",5); // shrtlength??
            _wrapper.moveLinearWrapped("defaultVector", 5); // index?? 
            return false; 
        }

        public bool defineAbsoluteVector(string vectorname, int points)
        {
            bool status = _wrapper.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, vectorname, (short) points);
            return status;
        }

        public bool teach(SIRVector vector)
        {
            return _wrapper.teachWrapped(vector);
        }

        public bool moveLinear(string vectorname, int pointindex)
        {
        	movementlock.WaitOne();
            return _wrapper.moveLinearWrapped(vectorname, pointindex);
        }

        #endregion

        #region Axis movements
        public bool moveBase(int _iSpeed)
        {
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
           movementlock.WaitOne(); 
           return _wrapper.closeGripperWrapped();
        }

        public bool openGripper()
        {
            movementlock.WaitOne();
            return _wrapper.openGripperWrapped();
        }
        #endregion

        #region MovejustOneCordinate
        public bool moveByXCoordinate(int _iX)
        {
            movementlock.WaitOne();
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_X, _iX))
                return false;
            return true;
        }

        public bool moveByYCoordinate(int _iY)
        {
            movementlock.WaitOne();
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y, _iY))
                return false;
            return true;
        }


        public bool moveByZCoordinate(int _iZ)
        {
            movementlock.WaitOne();
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z, _iZ))
                return false;
            return true;
        }

        public bool moveByPitch(int _pitch)
        {
            movementlock.WaitOne();
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ALL, true);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_PITCH, _pitch))
                return false;
            return true;
        }

        public bool moveByRoll(int _roll)
        {
            movementlock.WaitOne();
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