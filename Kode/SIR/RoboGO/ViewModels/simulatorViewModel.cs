/** \file simulatorViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ControlSystem;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// ViewModel for the simulator class.
    /// 
    /// Note: Uses StringUI class for output.
    /// </summary>
    public class SimulatorViewModel
    {
        // Members and properties
        private IRobot _sim;
        private StringUI suiSimulatorUI;
      
        /// <summary>
        /// Simulator output.
        /// </summary>
        public StringUI UIText
        {
            get{return(suiSimulatorUI);}
        }

        /// <summary>
        /// Simulator object used.
        /// </summary>
        public IRobot SimulatorObj
        {
            get { return (_sim); }
        }
        
       // Functions
        /// <summary>
        /// Default constructor setting up simulator.
        /// 
        /// Note: Edits Factory->Simulator.
        /// </summary>
        public SimulatorViewModel()
        {
            //_sim = Factory.currentIRobotInstance;
            suiSimulatorUI = new StringUI();
            Factory.getSimulatorInstance.IUIOutput = suiSimulatorUI;
            _sim = Factory.getSimulatorInstance;
        }
    }
}
