/** \file manualController.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        // Settings
        /// <summary>
        /// Speed in percentage.
        /// </summary>
        int speed
        {
            get; set;
        }

        // Movement
        // -Axes
        /// <summary>
        /// Move the base in the desired direction in 'speed' percentage of maximum speed.
        /// </summary>
        /// <param name="_elrDirection">Where ya wanna go?</param>
        void moveBase(enumLeftRight _elrDirection);
        /// <summary>
        /// !i!i!i!i!
        /// </summary>
        /// <param name="_elrDirection"></param>
        void moveShoulder(enumLeftRight _elrDirection);
        void moveElbow(enumLeftRight _elrDirection);
        void moveGripper(enumCloseOpen _coGripper);
        void moveAxePitch(enumLeftRight _elrDirection); // Maybe a bit weird there is one for Axe and Coord
        void moveAxeRoll(enumLeftRight _elrDirection); // Maybe a bit weird there is one for Axe and Coord
        void moveConveyer(enumLeftRight _elrDirection); // Left right? What else to call it.

        // -Coordinates
        void moveX(enumIncDec eidIncOrDec);
        void moveY(enumIncDec eidIncOrDec);
        void moveZ(enumIncDec eidIncOrDec);
        void moveCoordPitch(enumIncDec eidIncOrDec);
        void moveCoordRoll(enumIncDec eidIncOrDec);
    }

    /// <summary>
    /// Class to functions as encapsulation for controlling manual movement. Instead of using directly Robot or Simulator by interface.
    /// 
    /// Note: Uses IRobot, so it is able to use both a Robot or a simulator.
    /// </summary>
    public class ManualController : IManualController
    {
        public int speed
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void moveBase(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveShoulder(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveElbow(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveGripper(enumCloseOpen _coGripper)
        {
            throw new NotImplementedException();
        }

        public void moveAxePitch(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveAxeRoll(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveConveyer(enumLeftRight _elrDirection)
        {
            throw new NotImplementedException();
        }

        public void moveX(enumIncDec eidIncOrDec)
        {
            throw new NotImplementedException();
        }

        public void moveY(enumIncDec eidIncOrDec)
        {
            throw new NotImplementedException();
        }

        public void moveZ(enumIncDec eidIncOrDec)
        {
            throw new NotImplementedException();
        }

        public void moveCoordPitch(enumIncDec eidIncOrDec)
        {
            throw new NotImplementedException();
        }

        public void moveCoordRoll(enumIncDec eidIncOrDec)
        {
            throw new NotImplementedException();
        }
    }
}
