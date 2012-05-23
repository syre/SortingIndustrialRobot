using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ControlSystem;


namespace RoboGO.ViewModels
{
    public class SimulatorViewModel
    {
        // Members and properties
        private IRobot _sim;
        private XYCalculate _xyCalculate;
        private readonly ScaleTransform _04scale = new ScaleTransform(0.4,0.4);
        private StringUI suiSimulatorUI;
        private PositionViewModel _positionViewModel;
        
        /// <summary>
        /// Text simulator writes.
        /// </summary>
        public StringUI UIText
        {
            get{return(suiSimulatorUI);}
        }
        
        public string CurrentPosition
        {
            get 
            {   _positionViewModel.update();
                return _positionViewModel.getXYZPR();
            }
        }

        // Functions
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

    public class XYCalculate
    {
        private const int wristLenght = 45;
        private const int elbowLenght = 75;
        private VecPoint _point;
        public double elbowRotate;
        public double gripperX;
        public double gripperY;


        public XYCalculate(VecPoint p)
        {
            _point = p;
            _point.iX = 0;
            _point.iY = -50;
            elbow();
            
            gripper();

        }

        public void elbow()
        {
            if(_point.iX >= 0 && _point.iY <= 125)
            {
                
               elbowRotate = ((double)180/(double)125) * (-(double)_point.iY);
            
                
            }
            
        }

        public void gripper()
        {
            gripperX = _point.iX;
            gripperY = _point.iY;
        }
    }
}
