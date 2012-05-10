/** \file manualController.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using DSL;

namespace ControlSystem
{
    // Helper constants and enums
    /// <summary>
    /// What direction to move in when moving by axes.
    /// </summary>
    public enum enumLeftRight
    {
        MANUAL_MOVE_LEFT,
        MANUAL_MOVE_RIGHT
    }

    /// <summary>
    /// What direction to move in when moving by axes.(Wrist)
    /// </summary>
    public enum enumUpDown
    {
        MANUAL_MOVE_UP,
        MANUAL_MOVE_DOWN
    }

    /// <summary>
    /// Move increasing or decreasing when moving by coordinates.
    /// </summary>
    public enum enumIncDec
    {
        MANUAL_MOVE_INC,
        MANUAL_MOVE_DEC
    }

    /// <summary>
    /// To close or open gripper.
    /// </summary>
    public enum enumCloseOpen
    {
        MANUAL_CLOSE,
        MANUAL_OPEN
    }

    // Classes and interfaces
    /// <summary>
    /// Interface for what manual movement functions there should be available.
    /// </summary>
    public interface IManualController
    {
        // Properties
        /// <summary>
        /// Speed in percentage.
        /// 
        /// So should be between 0 and 100.
        /// </summary>
        int Speed{ get; set; }

        /// <summary>
        /// What to steer.
        /// 
        /// Could be for example Robot(ER4) or Simulator.
        /// </summary>
        IRobot RobotConnection { get; set; }

        // Movement
        // -Axes
        /// <summary>
        /// Move the base in the desired direction in 'speed' percentage of maximum speed.
        /// </summary>
        /// <param name="_elrDirection">Where ya wanna go?</param>
        void moveAxisBase(enumLeftRight _elrDirection);
        /// <summary>
        /// Moves the shoulder in the desired direction.
        /// </summary>
        /// <param name="_elrDirection">What direction to move in.</param>
        void moveAxisShoulder(enumLeftRight _elrDirection);
        /// <summary>
        /// Moves the elbow in the desired direction.
        /// </summary>
        /// <param name="_elrDirection">What direction to move in.</param>
        void moveAxisElbow(enumLeftRight _elrDirection);
        /// <summary>
        /// Opens or closes the gripper.
        /// </summary>
        /// <param name="_ecoGripper">To open or close.</param>
        void moveAxisGripper(enumCloseOpen _ecoGripper);
        /// <summary>
        /// Moves the wrists pitch in the desired direction.
        /// </summary>
        /// <param name="_eudDirection">What direction to move in.</param>
        void moveAxisPitch(enumUpDown _eudDirection);
        /// <summary>
        /// Rolls the wrist in the desired direction.
        /// </summary>
        /// <param name="_elrDirection">What direction to move in.</param>
        void moveAxisRoll(enumLeftRight _elrDirection);
        /// <summary>
        /// Move the conveyer belt in the desired direction.
        /// </summary>
        /// <param name="_elrDirection">What direction to move in.</param>
        void moveAxisConveyer(enumLeftRight _elrDirection); // Left right? What else to call it.

        // -Coordinates
        /// <summary>
        /// Change the robots X-coordinate.
        /// </summary>
        /// <param name="_eidIncOrDec">Increasing or decreasing.</param>
        void moveCoordX(enumIncDec _eidIncOrDec);
        /// <summary>
        /// Change the robots Y-coordinate.
        /// </summary>
        /// <param name="_eidIncOrDec">Increasing or decreasing.</param>
        void moveCoordY(enumIncDec _eidIncOrDec);
        /// <summary>
        /// Change the robots Z-coordinate.
        /// </summary>
        /// <param name="_eidIncOrDec">Increasing or decreasing.</param>
        void moveCoordZ(enumIncDec _eidIncOrDec);
        /// <summary>
        /// Change the wrists pitch.
        /// </summary>
        /// <param name="_eidIncOrDec">Increasing or decreasing.</param>
        void moveCoordPitch(enumIncDec _eidIncOrDec);
        /// <summary>
        /// Change the roll of the wrist.
        /// </summary>
        /// <param name="_eidIncOrDec">Increasing or decreasing.</param>
        void moveCoordRoll(enumIncDec _eidIncOrDec);
    }

    /// <summary>
    /// Class that encapsulates controlling manual movement. Instead of using directly Robot or Simulator by interface.
    /// 
    /// Note: Uses IRobot, so it is able to use either a Robot or a Simulator.
    /// </summary>
    public class ManualController : IManualController
    {
        // Members
        private int speed;
        private IRobot robot;

        public ManualController()
        {
            robot = Factory.currentIRobotInstance;
            throw new Exception("Remove later!");
            IsOnline();
        }
		
        private void IsOnline()
        {
            if (!robot.isOnline()) 
                throw new Exception();
        }

        // Properties
        public int Speed
        {
            get { return speed; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    speed = value;
                }
            }
        }
        public IRobot RobotConnection
        {
            get { return robot; }
            set { robot = value; }
        }
        #region Axis
        public void moveAxisBase(enumLeftRight _elrDirection)
        {
            bool ifEverythingOk = false;

            if (_elrDirection == enumLeftRight.MANUAL_MOVE_RIGHT)
                ifEverythingOk = robot.moveBase(speed);

            if (_elrDirection == enumLeftRight.MANUAL_MOVE_LEFT)
                ifEverythingOk = robot.moveBase(-speed);

            if (!ifEverythingOk) 
                throw new Exception();

        }

        public void moveAxisShoulder(enumLeftRight _elrDirection)
        {
            bool ifEverythingOk = false;

            if (_elrDirection == enumLeftRight.MANUAL_MOVE_RIGHT)
                ifEverythingOk = robot.moveShoulder(speed);

            if (_elrDirection == enumLeftRight.MANUAL_MOVE_LEFT)
                ifEverythingOk = robot.moveShoulder(-speed);

            if (!ifEverythingOk) 
                throw new Exception();
        }

        public void moveAxisElbow(enumLeftRight _elrDirection)
        {
            bool ifEverythingOk = false;

            if (_elrDirection == enumLeftRight.MANUAL_MOVE_RIGHT)
                ifEverythingOk = robot.moveElbow(speed);
            
            if (_elrDirection == enumLeftRight.MANUAL_MOVE_LEFT)
                ifEverythingOk = robot.moveElbow(-speed);

            if (!ifEverythingOk) 
                throw new Exception();
        }

        public void moveAxisGripper(enumCloseOpen _ecoGripper)
        {
            bool ifEverythingOk = false;

            if (_ecoGripper == enumCloseOpen.MANUAL_OPEN)
                ifEverythingOk = robot.moveGripper(speed);
            
            if (_ecoGripper == enumCloseOpen.MANUAL_CLOSE)
                ifEverythingOk = robot.moveGripper(-speed);

            if (!ifEverythingOk) 
                throw new Exception();
        }

        public void moveAxisPitch(enumUpDown _eudDirection)
        {
            bool ifEverythingOk = false;

            if (_eudDirection == enumUpDown.MANUAL_MOVE_UP)
                ifEverythingOk = robot.moveWristPitch(speed);

            if (_eudDirection == enumUpDown.MANUAL_MOVE_DOWN)
                ifEverythingOk = robot.moveWristPitch(-speed);

            if (!ifEverythingOk) 
                throw new Exception();
        }

        public void moveAxisRoll(enumLeftRight _elrDirection)
        {
            bool ifEverythingOk = false;

            if (_elrDirection == enumLeftRight.MANUAL_MOVE_RIGHT)
                ifEverythingOk = robot.moveWristRoll(speed);

            if (_elrDirection == enumLeftRight.MANUAL_MOVE_LEFT)
                ifEverythingOk = robot.moveWristRoll(-speed);

            if (!ifEverythingOk) 
                throw new Exception();
        }

        public void moveAxisConveyer(enumLeftRight _elrDirection)
        {
            bool ifEverythingOk = false;

            if (_elrDirection == enumLeftRight.MANUAL_MOVE_RIGHT)
                ifEverythingOk = robot.moveConveyerBelt(speed);
            
            if (_elrDirection == enumLeftRight.MANUAL_MOVE_LEFT)
                ifEverythingOk = robot.moveConveyerBelt(-speed);

            if (!ifEverythingOk) 
                throw new Exception();
        }
        #endregion
        #region Coordinates
        public void moveCoordX(enumIncDec _eidIncOrDec)
        {
            if (_eidIncOrDec == enumIncDec.MANUAL_MOVE_INC)
            {
                if (!robot.moveByXCoordinate(Speed))
                    throw new Exception();
            }
            else
            {
                if (!robot.moveByXCoordinate(-Speed))
                    throw new Exception();
            }
        }

        public void moveCoordY(enumIncDec _eidIncOrDec)
        {
            if (_eidIncOrDec == enumIncDec.MANUAL_MOVE_INC)
            {
                if (!robot.moveByYCoordinate(Speed))
                    throw new Exception();
            }
            else
            {
                if (!robot.moveByYCoordinate(-Speed))
                    throw new Exception();
            }
        }

        public void moveCoordZ(enumIncDec _eidIncOrDec)
        {
            if (_eidIncOrDec == enumIncDec.MANUAL_MOVE_INC)
            {
                if (!robot.moveByZCoordinate(Speed))
                    throw new Exception();
            }
            else
            {
                if (!robot.moveByZCoordinate(-Speed))
                    throw new Exception();
            }
        }

        public void moveCoordPitch(enumIncDec _eidIncOrDec)
        {
            if (_eidIncOrDec == enumIncDec.MANUAL_MOVE_INC)
            {
                if (!robot.moveByPitch(Speed))
                    throw new Exception();
            }
            else
            {
                if (!robot.moveByPitch(-Speed))
                    throw new Exception();
            }
        }

        public void moveCoordRoll(enumIncDec _eidIncOrDec)
        {
            if (_eidIncOrDec == enumIncDec.MANUAL_MOVE_INC)
            {
                if (!robot.moveByRoll(Speed))
                    throw new Exception();
            }
            else
            {
                if (!robot.moveByRoll(-Speed))
                    throw new Exception();
            }
        }
        #endregion
    }
}
