/** \file viewModelManualSteering.cs */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ControlSystem;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// ViewModel for GUIManualSteering.
    /// 
    /// \todo Way to inform View about errors, like not being connected to robot.(Example messaging.)
    /// </summary>
    public class ViewModelManualSteering : INotifyPropertyChanged
    {
        // Properties
        private IManualController mcManualControl;
        public IManualController ManualControl
        {
            get { return (mcManualControl); }
            set { mcManualControl = value; }
        }
        private int Speed
        {
            get{return(mcManualControl.Speed);}
            set { mcManualControl.Speed = value; propertyChanged("Speed"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        // Functions
        public ViewModelManualSteering()
        {
            // Model
            mcManualControl = new ManualController();
        }

        public void propertyChanged(string _sWhat)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(_sWhat));
        }

        // -Steering: Made as simple interface as possible so minimum of logic required from View´s side.
        #region Axis
        public void moveAxisBaseRight()
        {
            throw new NotImplementedException();
        }
        public void moveAxisBaseLeft()
        {
            throw new NotImplementedException();
        }
        public void moveAxisShoulderRight()
        {
            throw new NotImplementedException();
        }
        public void moveAxisShoulderLeft()
        {
            throw new NotImplementedException();
        }
        public void moveAxisElbowRight()
        {
            throw new NotImplementedException();
        }
        public void moveAxisElbowLeft()
        {
            throw new NotImplementedException();
        }
        public void openGripper()
        {
            throw new NotImplementedException();
        }
        public void closeGripper()
        {
            throw new NotImplementedException();
        }
        public void moveAxisPitchUp()
        {
            throw new NotImplementedException();
        }
        public void moveAxisPitchDown()
        {
            throw new NotImplementedException();
        }
        public void moveAxisRollRight()
        {
            throw new NotImplementedException();
        }
        public void moveAxisRollLeft()
        {
            throw new NotImplementedException();
        }
        public void moveAxisConveyerRight()
        {
            throw new NotImplementedException();
        }
        public void moveAxisConveyerLeft()
        {
            throw new NotImplementedException();
        }
#endregion
        #region Coordinates
        public void moveCoordXIncreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordXDecreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordYIncreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordYDecreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordZIncreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordZDecreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordPitchIncreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordPitchDecreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordRollIncreasing()
        {
            throw new NotImplementedException(); 
        }
        public void moveCoordRollDecreasing()
        {
            throw new NotImplementedException(); 
        }
        #endregion
    }
}
