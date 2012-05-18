/** \file viewModelManualSteering.cs */
using System;
using System.Windows.Input;
using ControlSystem;

/** \author Robotic Global Organization(RoboGO) */
namespace RoboGO.ViewModels
{
    /// <summary>
    /// ViewModel for GUIManualSteering.
    /// 
    /// \todo Way to inform View about errors, like not being connected to robot.(Example messaging.)
    /// </summary>
    public class ViewModelManualSteering
    {
        // Properties
        private IManualController mcManualControl;
        /// <summary>
        /// Manual controller used for the functions.
        /// </summary>
        public IManualController ManualControl
        {
            get { return (mcManualControl); }
            set { mcManualControl = value; }
        }
        
        /// <summary>
        /// Speed of the movements
        /// 
        /// Value 0->100 is equal to percentage of max speed.
        /// </summary>
        public int Speed
        {
            get{return(mcManualControl.Speed);}
            set {mcManualControl.Speed = value;}
        }

        // Functions
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ViewModelManualSteering()
        {
            // Model
            try
            {
                mcManualControl = new ManualController();
                dcOpenGripper = new DelegateCommand(openGripper);
                dcCloseGripper = new DelegateCommand(closeGripper);
                dcHomeRobot = new DelegateCommand(seekHome);
            }
            catch (Exception)
            {
                /// \warning Must be checked up later.
            }
        }

        // -Steering: Made as simple interface as possible so minimum of logic required from View´s side.
        #region Axis
        /// <summary>
        /// Moves the base of the robot to the right.
        /// </summary>
        public void moveAxisBaseRight()
        {
            mcManualControl.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        
        /// <summary>
        /// Moves the base of the robot to the left.
        /// </summary>
        public void moveAxisBaseLeft()
        {
            mcManualControl.moveAxisBase(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        
        /// <summary>
        /// Moves the shoulder of the robot to the right.
        /// </summary>
        public void moveAxisShoulderRight()
        {
            mcManualControl.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        
        /// <summary>
        /// Moves the shoulder of the robot to the left.
        /// </summary>
        public void moveAxisShoulderLeft()
        {
            mcManualControl.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        
        /// <summary>
        /// Moves the elbow of the robot to the right.
        /// </summary>
        public void moveAxisElbowRight()
        {
            mcManualControl.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        
        /// <summary>
        /// Moves the elbow of the robot to the left.
        /// </summary>
        public void moveAxisElbowLeft()
        {
            mcManualControl.moveAxisElbow(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        
        /// <summary>
        /// Opens the gripper.
        /// </summary>
        public void openGripper()
        {
            mcManualControl.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);
        }
        
        /// <summary>
        /// Closes the gripper.
        /// </summary>
        public void closeGripper()
        {
            mcManualControl.moveAxisGripper(enumCloseOpen.MANUAL_CLOSE);
        }
        
        /// <summary>
        /// Moves the pitch of the robot hand up.
        /// </summary>
        public void moveAxisPitchUp()
        {
            mcManualControl.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP);
        }
        
        /// <summary>
        /// Moves the pitch of the robot hand down.
        /// </summary>
        public void moveAxisPitchDown()
        {
            mcManualControl.moveAxisPitch(enumUpDown.MANUAL_MOVE_DOWN);
        }
        
        /// <summary>
        /// Rolls the robot hand to the right.
        /// </summary>
        public void moveAxisRollRight()
        {
            mcManualControl.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        
        /// <summary>
        /// Rolls the robot hand to the left.
        /// </summary>
        public void moveAxisRollLeft()
        {
            mcManualControl.moveAxisRoll(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        
        /// <summary>
        /// Moves the conveyer to the right.
        /// </summary>
        public void moveAxisConveyerRight()
        {
            mcManualControl.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        
        /// <summary>
        /// Moves the conveyer to the left
        /// </summary>
        public void moveAxisConveyerLeft()
        {
            mcManualControl.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        #endregion
        #region Coordinates
        /// <summary>
        /// Moves the robot hand increasing in the X-axis.
        /// </summary>
        public void moveCoordXIncreasing()
        {
            mcManualControl.moveCoordX(enumIncDec.MANUAL_MOVE_INC);
        }
        
        /// <summary>
        /// Moves the robot hand decreasing in the X-axis.
        /// </summary>
        public void moveCoordXDecreasing()
        {
            mcManualControl.moveCoordX(enumIncDec.MANUAL_MOVE_DEC);
        }
        
        /// <summary>
        /// Moves the robot hand increasing in the Y-axis.
        /// </summary>
        public void moveCoordYIncreasing()
        {
            mcManualControl.moveCoordY(enumIncDec.MANUAL_MOVE_INC);
        }
        
        /// <summary>
        /// Moves the robot hand decreasing in the Y-axis.
        /// </summary>
        public void moveCoordYDecreasing()
        {
            mcManualControl.moveCoordY(enumIncDec.MANUAL_MOVE_DEC);
        }
        
        /// <summary>
        /// Moves the robot hand increasing the Z-axis.
        /// </summary>
        public void moveCoordZIncreasing()
        {
            mcManualControl.moveCoordZ(enumIncDec.MANUAL_MOVE_INC);
        }
        
        /// <summary>
        /// Moves the robot hand decreasing in the Z-axis.
        /// </summary>
        public void moveCoordZDecreasing()
        {
            mcManualControl.moveCoordZ(enumIncDec.MANUAL_MOVE_DEC);
        }
        
        /// <summary>
        /// Increase the pitch of the robot hand while keeping jaw fixed in position.
        /// </summary>
        public void moveCoordPitchIncreasing()
        {
            mcManualControl.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);
        }
        
        /// <summary>
        /// Decrease the pitch of the robot hand while keeping jaw fixed in position.
        /// </summary>
        public void moveCoordPitchDecreasing()
        {
            mcManualControl.moveCoordPitch(enumIncDec.MANUAL_MOVE_DEC);
        }
        
        /// <summary>
        /// Roll the robot hand.
        /// </summary>
        public void moveCoordRollIncreasing()
        {
            mcManualControl.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);
        }
        
        /// <summary>
        /// Roll the robot hand.
        /// </summary>
        public void moveCoordRollDecreasing()
        {
            mcManualControl.moveCoordRoll(enumIncDec.MANUAL_MOVE_DEC);
        }
        #endregion
        #region Other
        /// <summary>
        /// The robot begins seeking home for all axes.
        /// </summary>
        public void seekHome()
        {
            mcManualControl.RobotConnection.homeRobot();
        }
        
        /// <summary>
        /// Stops all movement of the robot.
        /// </summary>
        public void stopMovement()
        {
            mcManualControl.stopAllMovement();
        }
        #endregion
        #region commands
        private DelegateCommand dcOpenGripper;
        public ICommand OpenGripper { get { return (dcOpenGripper); } }
        private DelegateCommand dcCloseGripper;
        public ICommand CloseGripper { get { return (dcCloseGripper); } }
        private DelegateCommand dcHomeRobot;
        public ICommand SeekHome { get { return dcHomeRobot; } }
        #endregion
    }
}
