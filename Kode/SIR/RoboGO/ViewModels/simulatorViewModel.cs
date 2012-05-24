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
        private XYCalculate _xyCalculate;
        private readonly ScaleTransform _04scale = new ScaleTransform(0.4,0.4);
        private StringUI suiSimulatorUI;
        private PositionViewModel _positionViewModel;
        
        /// <summary>
        /// Simulator output.
        /// </summary>
        public StringUI UIText
        {
            get{return(suiSimulatorUI);}
        }
        
        /// <summary>
        /// Current position of the simulator.
        /// </summary>
        public string CurrentPosition
        {
            get 
            {   _positionViewModel.update();
                return _positionViewModel.getXYZPR();
            }
        }

        // Functions
        /// <summary>
        /// Default constructor setting up simulator.
        /// 
        /// Note: Edits Factory->Simulator.
        /// </summary>
        public SimulatorViewModel()
        {
            _positionViewModel = new PositionViewModel();
            //_sim = Factory.currentIRobotInstance;
            suiSimulatorUI = new StringUI();
            Factory.getSimulatorInstance.IUIOutput = suiSimulatorUI;
            _sim = Factory.getSimulatorInstance;
            _xyCalculate = new XYCalculate(_sim.getCurrentPosition());

        }

    }

    /// <summary>
    /// Class for calculating position of robot.
    /// 
    /// Note: Used by simulator.
    /// </summary>
    public class XYCalculate
    {
        private const int wristLenght = 45;
        private const int elbowLenght = 75;
        private VecPoint _point;
        
        /// <summary>
        /// Rotation of the elbow.
        /// </summary>
        public double elbowRotate;
        
        /// <summary>
        /// Gripper x position.
        /// </summary>
        public double gripperX;
        
        /// <summary>
        /// Gripper y position.
        /// </summary>
        public double gripperY;

        /// <summary>
        /// Constructor taking a VecPoint that it uses for calculting position.
        /// </summary>
        /// <param name="p"></param>
        public XYCalculate(VecPoint p)
        {
            _point = p;
            _point.iX = 0;
            _point.iY = -50;
            elbow();
            
            gripper();
        }

        /// <summary>
        /// Calculate elbow position.
        /// </summary>
        public void elbow()
        {
            if(_point.iX >= 0 && _point.iY <= 125)
            {
               elbowRotate = ((double)180/(double)125) * (-(double)_point.iY);
            }
        }

        /// <summary>
        /// Calculate gripper position.
        /// </summary>
        public void gripper()
        {
            gripperX = _point.iX;
            gripperY = _point.iY;
        }
    }
}
