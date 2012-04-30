/** \file viewModelManualSteering.cs */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ControlSystem;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// Class skeleton for commands.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private Action aMethod;
        public DelegateCommand(Action _aMethodToExecute)
        {
            aMethod = _aMethodToExecute;
        }

        public bool CanExecute(object _objParam)
        {
            return (true);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object _objParam)
        {
            aMethod.Invoke();
        }
    }

    /// <summary>
    /// ViewModel for GUIManualSteering.
    /// 
    /// \todo Way to inform View about errors, like not being connected to robot.(Example messaging.)
    /// </summary>
    public class ViewModelManualSteering
    {
        // Properties
        private IManualController mcManualControl;
        public IManualController ManualControl
        {
            get { return (mcManualControl); }
            set { mcManualControl = value; }
        }
        public int Speed
        {
            get{return(mcManualControl.Speed);}
            set {mcManualControl.Speed = value;}
        }

        #region Commands
        private DelegateCommand dcMoveAxisBaseRight;
        public ICommand MoveAxisBaseRight
        {
            get { return (dcMoveAxisBaseRight); }
        }

        private DelegateCommand dcMoveAxisBaseLeft;
        public ICommand MoveAxisBaseLeft
        {
            get { return (dcMoveAxisBaseLeft); }
        }

        private DelegateCommand dcMoveAxisShoulderRight;
        public ICommand MoveAxisShoulderRight
        {
            get { return (dcMoveAxisShoulderRight); }
        }

        private DelegateCommand dcMoveAxisShoulderLeft;
        public ICommand MoveAxisShoulderLeft
        {
            get { return (dcMoveAxisShoulderLeft); }
        }

        private DelegateCommand dcMoveAxisElbowRight;
        public ICommand MoveAxisElbowRight
        {
            get { return (dcMoveAxisElbowRight); }
        }

        private DelegateCommand dcMoveAxisElbowLeft;
        public ICommand MoveAxisElbowLeft
        {
            get { return (dcMoveAxisElbowLeft); }
        }

        private DelegateCommand dcOpenGripper;
        public ICommand OpenGripper
        {
            get { return (dcOpenGripper); }
        }

        private DelegateCommand dcCloseGripper;
        public ICommand CloseGripper
        {
            get { return (dcCloseGripper); }
        }

        private DelegateCommand dcMoveAxisPitchUp;
        public ICommand MoveAxisPitchUp
        {
            get { return (dcMoveAxisPitchUp); }
        }

        private DelegateCommand dcMoveAxisPitchDown;
        public ICommand MoveAxisPitchDown
        {
            get { return (dcMoveAxisPitchDown); }
        }

        private DelegateCommand dcMoveAxisRollRight;
        public ICommand MoveAxisRollRight
        {
            get { return (dcMoveAxisRollRight); }
        }

        private DelegateCommand dcMoveAxisRollLeft;
        public ICommand MoveAxisRollLeft
        {
            get { return (dcMoveAxisRollLeft); }
        }

        private DelegateCommand dcMoveAxisConveyerRight;
        public ICommand MoveAxisConveyerRight
        {
            get { return (dcMoveAxisConveyerRight); }
        }

        private DelegateCommand dcMoveAxisConveyerLeft;
        public ICommand MoveAxisConveyerLeft
        {
            get { return (dcMoveAxisConveyerLeft); }
        }

        private DelegateCommand dcMoveCoordXIncreasing;
        public ICommand MoveCoordXIncreasing
        {
            get { return (dcMoveCoordXIncreasing); }
        }

        private DelegateCommand dcMoveCoordXDecreasing;
        public ICommand MoveCoordXDecreasing
        {
            get { return (dcMoveCoordXDecreasing); }
        }

        private DelegateCommand dcMoveCoordYIncreasing;
        public ICommand MoveCoordYIncreasing
        {
            get { return (dcMoveCoordYIncreasing); }
        }

        private DelegateCommand dcMoveCoordYDecreasing;
        public ICommand MoveCoordYDecreasing
        {
            get { return (dcMoveCoordYDecreasing); }
        }

        private DelegateCommand dcMoveCoordZIncreasing;
        public ICommand MoveCoordZIncreasing
        {
            get { return (dcMoveCoordZIncreasing); }
        }

        private DelegateCommand dcMoveCoordZDecreasing;
        public ICommand MoveCoordZDecreasing
        {
            get { return (dcMoveCoordZDecreasing); }
        }

        private DelegateCommand dcMoveCoordPitchIncreasing;
        public ICommand MoveCoordPitchIncreasing
        {
            get { return (dcMoveCoordPitchIncreasing); }
        }

        private DelegateCommand dcMoveCoordPitchDecreasing;
        public ICommand MoveCoordPitchDecreasing
        {
            get { return (dcMoveCoordPitchDecreasing); }
        }

        private DelegateCommand dcMoveCoordRollIncreasing;
        public ICommand MoveCoordRollIncreasing
        {
            get { return (dcMoveCoordRollIncreasing); }
        }

        private DelegateCommand dcMoveCoordRollDecreasing;
        public ICommand MoveCoordRollDecreasing
        {
            get { return (dcMoveCoordRollDecreasing); }
        }
        #endregion

        // Functions
        public ViewModelManualSteering()
        {
            // Model
            try
            {
                mcManualControl = new ManualController();
            }
            catch (Exception)
            {
                throw new Exception("Manualcontroller could not be set up.");
            }
            

            // Commands
            dcMoveAxisBaseRight = new DelegateCommand(moveAxisBaseRight);
            dcMoveAxisBaseLeft = new DelegateCommand(moveAxisBaseLeft);
            dcMoveAxisShoulderRight = new DelegateCommand(moveAxisShoulderRight);
            dcMoveAxisShoulderLeft = new DelegateCommand(moveAxisShoulderLeft);
            dcMoveAxisElbowRight = new DelegateCommand(moveAxisElbowRight);
            dcMoveAxisElbowLeft = new DelegateCommand(moveAxisElbowLeft);
            dcOpenGripper = new DelegateCommand(openGripper);
            dcCloseGripper = new DelegateCommand(closeGripper);
            dcMoveAxisPitchUp = new DelegateCommand(moveAxisPitchUp);
            dcMoveAxisPitchDown = new DelegateCommand(moveAxisPitchDown);
            dcMoveAxisRollRight = new DelegateCommand(moveAxisRollRight);
            dcMoveAxisRollLeft = new DelegateCommand(moveAxisRollLeft);
            dcMoveAxisConveyerRight = new DelegateCommand(moveAxisConveyerRight);
            dcMoveAxisConveyerLeft = new DelegateCommand(moveAxisConveyerLeft);
            dcMoveCoordXIncreasing = new DelegateCommand(moveCoordXIncreasing);
            dcMoveCoordXDecreasing = new DelegateCommand(moveCoordXDecreasing);
            dcMoveCoordYIncreasing = new DelegateCommand(moveCoordYIncreasing);
            dcMoveCoordYDecreasing = new DelegateCommand(moveCoordYDecreasing);
            dcMoveCoordZIncreasing = new DelegateCommand(moveCoordZIncreasing);
            dcMoveCoordZDecreasing = new DelegateCommand(moveCoordZDecreasing);
            dcMoveCoordPitchIncreasing = new DelegateCommand(moveCoordPitchIncreasing);
            dcMoveCoordPitchDecreasing = new DelegateCommand(moveCoordPitchDecreasing);
            dcMoveCoordRollIncreasing = new DelegateCommand(moveCoordRollIncreasing);
            dcMoveCoordRollDecreasing = new DelegateCommand(moveCoordRollDecreasing);
        }

        // -Steering: Made as simple interface as possible so minimum of logic required from View´s side.
        #region Axis
        public void moveAxisBaseRight()
        {
            mcManualControl.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        public void moveAxisBaseLeft()
        {
            mcManualControl.moveAxisBase(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        public void moveAxisShoulderRight()
        {
            mcManualControl.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        public void moveAxisShoulderLeft()
        {
            mcManualControl.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        public void moveAxisElbowRight()
        {
            mcManualControl.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        public void moveAxisElbowLeft()
        {
            mcManualControl.moveAxisElbow(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        public void openGripper()
        {
            mcManualControl.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);
        }
        public void closeGripper()
        {
            mcManualControl.moveAxisGripper(enumCloseOpen.MANUAL_CLOSE);
        }
        public void moveAxisPitchUp()
        {
            mcManualControl.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP);
        }
        public void moveAxisPitchDown()
        {
            mcManualControl.moveAxisPitch(enumUpDown.MANUAL_MOVE_DOWN);
        }
        public void moveAxisRollRight()
        {
            mcManualControl.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        public void moveAxisRollLeft()
        {
            mcManualControl.moveAxisRoll(enumLeftRight.MANUAL_MOVE_LEFT);
        }
        public void moveAxisConveyerRight()
        {
            mcManualControl.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT);
        }
        public void moveAxisConveyerLeft()
        {
            mcManualControl.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_LEFT);
        }
#endregion
        #region Coordinates
        public void moveCoordXIncreasing()
        {
            mcManualControl.moveCoordX(enumIncDec.MANUAL_MOVE_INC);
        }
        public void moveCoordXDecreasing()
        {
            mcManualControl.moveCoordX(enumIncDec.MANUAL_MOVE_DEC);
        }
        public void moveCoordYIncreasing()
        {
            mcManualControl.moveCoordY(enumIncDec.MANUAL_MOVE_INC);
        }
        public void moveCoordYDecreasing()
        {
            mcManualControl.moveCoordY(enumIncDec.MANUAL_MOVE_DEC);
        }
        public void moveCoordZIncreasing()
        {
            mcManualControl.moveCoordZ(enumIncDec.MANUAL_MOVE_INC);
        }
        public void moveCoordZDecreasing()
        {
            mcManualControl.moveCoordZ(enumIncDec.MANUAL_MOVE_DEC);
        }
        public void moveCoordPitchIncreasing()
        {
            mcManualControl.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);
        }
        public void moveCoordPitchDecreasing()
        {
            mcManualControl.moveCoordPitch(enumIncDec.MANUAL_MOVE_DEC);
        }
        public void moveCoordRollIncreasing()
        {
            mcManualControl.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);
        }
        public void moveCoordRollDecreasing()
        {
            mcManualControl.moveCoordRoll(enumIncDec.MANUAL_MOVE_DEC);
        }
        #endregion
    }
}
