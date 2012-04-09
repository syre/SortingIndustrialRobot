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
        MANUAL_MOVE_RIGTH
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
        /// <param name="_coGripper">To open or close.</param>
        void moveAxisGripper(enumCloseOpen _coGripper);
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
        void moveConveyer(enumLeftRight _elrDirection); // Left right? What else to call it.

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
    /// Note: Uses IRobot, so it is able to use both a Robot or a Simulator.
    /// </summary>
    public class ManualController : IManualController
    {
        public int Speed
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IRobot RobotConnection
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void moveAxisBase(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveAxisShoulder(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveAxisElbow(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveAxisGripper(enumCloseOpen _coGripper)
        {
            throw new NotImplementedException();
        }

        public void moveAxisPitch(enumUpDown _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveAxisRoll(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveConveyer(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveCoordX(enumIncDec _eidIncOrDec)
        {
            throw new NotImplementedException();
        }

        public void moveCoordY(enumIncDec _eidIncOrDec)
        {
            throw new NotImplementedException();
        }

        public void moveCoordZ(enumIncDec _eidIncOrDec)
        {
            throw new NotImplementedException();
        }

        public void moveCoordPitch(enumIncDec _eidIncOrDec)
        {
            throw new NotImplementedException();
        }

        public void moveCoordRoll(enumIncDec _eidIncOrDec)
        {
            throw new NotImplementedException();
        }
    }
}
