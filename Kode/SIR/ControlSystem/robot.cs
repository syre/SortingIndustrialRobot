﻿/** \file robot.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Instrumentation;
using System.Threading;
using SqlInteraction;
using System.Windows.Forms;

namespace ControlSystem
{
    /// <summary>
    /// The interface that Robot and Simulator are based on
    /// </summary>
    public interface IRobot
    {
        bool moveToAPosition();
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
        bool moveByAbsoluteCoordinates(string vectorname,int x, int y, int z, int pitch, int roll);
        /// <summary>
        /// function for moving by relative coordinates
        /// </summary>
        /// <param name="_iX"></param>
        /// <param name="_iY"></param>
        /// <param name="_iZ"></param>
        /// <param name="_iPitch"></param>
        /// <param name="_iRoll"></param>
        /// <returns></returns>
        bool moveByRelativeCoordinates(string vectorname,int _iX, int _iY, int _iZ, int _iPitch, int _iRoll);
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
        /// Moves to position from Cube ID.(From Database.)
        /// </summary>
        /// <param name="_iCubeID">ID of Cube.</param>
        bool moveToCubePosition(string name,int _iCubeID);

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
        private bool hasHomed = false;
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
        private DLL.DgateCallBackLongArg dgateWatchDigitalInput = digitalInputChanged;
        private static Semaphore movementlock;
        public static bool cubeAtConveyorBelt { get; set; }

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
        /// Method to be called when the robot starts its movement. EDIT: Implemented in each move-method instead (DUMMY)
        /// </summary>
        /// <param name="b"></param>
        private static void takeMovementLock(ref byte b)
        {
      
        }
        /// <summary>
        /// Method to be called when the robot stops its movement.
        /// </summary>
        /// <param name="b"></param>
        private static void releaseMovementLock(ref byte b)
        {
            try
            {
                movementlock.Release();

            }
            catch (SemaphoreFullException)
            {

            }
        }

        private static void digitalInputChanged(long l)
        {
            cubeAtConveyorBelt = !cubeAtConveyorBelt;
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
			// Initialize and set settings
            _serialStk = new SerialSTK();
            _wrapper = Wrapper.getInstance();
            initialization();
            _wrapper.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, true);
            movementlock = new Semaphore(1,1);
            cubeAtConveyorBelt = true;
            _wrapper.watchMotionWrapped(dgateMovementStopped, dgateMovementStarted);
            _wrapper.watchDigitalInputWrapped(dgateWatchDigitalInput);
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
            bool bTemp = _wrapper.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming);
            hasHomed = true;
            return bTemp;
        }

        public bool moveToAPosition()
        {
            ////skal placeres med income position
            //int[] iArray = new int[] { 200000, 200000, 100000, 100000, 1000000 };
            //int[] iArray2 = new int[] { -200000, -200000, -100000, -100000, -1000000 };
            ////Define navnet på en vector som har KUN 1 position i sig og 'A' for at sige det er robotten
            //int check = DLLImport.DefineVector((byte)'A', "firstOne", 2);
            //if (check == 0) return false;

            ////Home robotten før vi kører den til den givne position
            //check = DLLImport.Home((byte)'A', dgateEventHandlerHoming);
            //if (check == 0) return false;
            
            //check = DLLImport.Teach("firstOne", 1, iArray, 5, -32767);
            //if (check == 0) return false;

            //check = DLLImport.Teach("firstOne", 2, iArray2, 5, -32767);
            //if (check == 0) return false;

            ////Flyt robotten til positionen
            //movementlock.WaitOne();
            //check =DLLImport.MoveLinear("firstOne", 1, null,0);
            //if (check == 0) return false;

            //MessageBox.Show("Moo", "Error", MessageBoxButtons.OK);
            //movementlock.WaitOne();
            //check = DLLImport.MoveLinear("firstOne",2,null,0);
            //if (check == 0) return false;

            //return (check == 1);
            
            SIRVector temp = new RelCoordSirVector("yusuf");
            temp.addPoint(new VecPoint(10000,10000,70000,10000,10000));
            
            if (!_wrapper.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, temp.Name, (short)temp.getSize()))
                return false;
            if (!_wrapper.teachWrapped(temp))
                return false;
            for (int i = 0; i < temp.getSize(); i++)
            {
                movementlock.WaitOne();
                if (!_wrapper.moveLinearWrapped(temp.Name,i+1))
                    return false;
            }
            return true;
        }

        public bool stopAllMovement()
        {
            try
            {
                movementlock.Release();
            }
            catch (SemaphoreFullException)
            {
 
            }
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
        public bool moveByAbsoluteCoordinates(string vectorname,int _iX, int _iY, int _iZ, int _iPitch, int _iRoll) // subject to change
        {
            SIRVector temp = new AbsCoordSirVector(vectorname);
            temp.addPoint(new VecPoint(_iX,_iY,_iZ,_iPitch,_iRoll));
            if (!defineVector(temp))
                return false;
            if (!teach(temp))
                return false;
            if (!moveLinear(vectorname, 1))
                return false;
            return true;
        }

        public bool moveByRelativeVector(RelCoordSirVector rel)
        {
            if (!teach(rel))
                return false;
         
            for (int i = 0; i < rel.getSize(); i++)
            {
                movementlock.WaitOne();

                if (!_wrapper.moveLinearWrapped(rel.Name, (i+1)))
                    return false;
            }
            return true;
        }

        public bool defineRelativeVector(RelCoordSirVector rel)
        {
            return _wrapper.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, rel.Name, (short)rel.getSize());
        }

        public bool moveByAbsoluteVector(AbsCoordSirVector abs)
        {
            if (!teach(abs))
                return false;

            for (int i = 0; i < abs.getSize(); i++)
            {
                movementlock.WaitOne();

                if (!_wrapper.moveLinearWrapped(abs.Name, (i + 1)))
                    return false;
            }
            return true;
        }

        public bool defineVector(SIRVector sir)
        {
            return _wrapper.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, sir.Name, (short) sir.getSize());
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

        public bool moveByRelativeCoordinates(string vectorname,int _iX, int _iY, int _iZ, int _iPitch, int _iRoll)
        {
            SIRVector temp = new RelCoordSirVector(vectorname);
            temp.addPoint(new VecPoint(_iX, _iY, _iZ, _iPitch, _iRoll));
            if (!defineVector(temp))
                return false;
            if (!teach(temp))
                return false;
            if (!moveLinear(vectorname, 1))
                return false;
            return true;
        }

        public bool moveToCubePosition(string vectorname,int _iCubeID)
        {
            var sqlcmdCommand = SQLHandler.GetInstance.makeCommand("SELECT * FROM Position WHERE PositionID = " + _iCubeID);
            var isqlrdrReader = SQLHandler.GetInstance.runQuery(sqlcmdCommand, "Read");
            var lstCoordinates = isqlrdrReader.readRow();
            isqlrdrReader.close();
            // element 0 is ID, so starts from element 1(X)
            if (lstCoordinates.Count != 0)
            {
                // needs implementation
                if (moveByAbsoluteCoordinates(vectorname,(int)lstCoordinates[1], (int)lstCoordinates[2], (int)lstCoordinates[3], (int)lstCoordinates[4], (int)lstCoordinates[5]))
                    return true;
             
                return false;
            }
            else
                return false;
        }

        #endregion

        #region Axis movements
        public bool moveBase(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            return  _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, _iSpeed);
        }

        public bool moveShoulder(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_SHOULDER, _iSpeed);
        }

        public bool moveElbow(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_ELBOW, _iSpeed);
        }

        public bool moveWristPitch(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTPITCH, _iSpeed);
        }

        public bool moveWristRoll(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTROLL, _iSpeed);
        }

        public bool moveGripper(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, _iSpeed);
        }

        public bool moveConveyerBelt(int _iSpeed)
        {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);
            return _wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT, _iSpeed);
        }

        public string getCurrentPositionAsString()
        {
            VecPoint _vect;
            _vect = _wrapper.getCurrentPosition();
      
            return _vect.ToString();
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
            if(hasHomed)
            {
                _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
                _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
                if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_X, _iX))
                    return false;
                return true;
            }
            return false;
        }

        public bool moveByYCoordinate(int _iY)
        {
            if(hasHomed)
            {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y, _iY))
                return false;
            return true;
            }
            return false;
        }

        public bool moveByZCoordinate(int _iZ)
        {
            if(hasHomed)
            {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z, _iZ))
                return false;
            return true;
            }
            return false;
        }

        public bool moveByPitch(int _pitch)
        {
            if(hasHomed)
            {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_PITCH, _pitch))
                return false;
            return true;
            }
            return false;
        }

        public bool moveByRoll(int _roll)
        {
            if(hasHomed)
            {
            _wrapper.stopWrapped(Wrapper.enumAxisSettings.AXIS_ALL);
            _wrapper.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);
            if (!_wrapper.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_ROLL, _roll))
                return false;
            return true;
            }
            return false;
        }

        #endregion       
    }
}