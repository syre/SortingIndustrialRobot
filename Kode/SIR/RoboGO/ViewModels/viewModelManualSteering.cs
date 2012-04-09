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
    /// </summary>
    public class ViewModelManualSteering : INotifyPropertyChanged
    {
        // Properties
        private ManualController mcManualControl;
        public ManualController ManualControl
        {
            get { return (mcManualControl); }
            set { mcManualControl = value; }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
