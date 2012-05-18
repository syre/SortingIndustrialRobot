using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DSL;
using ControlSystem;


namespace RoboGO.ViewModels
{
    public class SimulatorViewModel
    {
        // Members and properties
        private IRobot _sim;
        private Canvas _simulatorcanvas;
        private Image _elbow;
        private Image _gripper;
        private Image _wrist;
        private Image _shoulder;
        private Image _base;
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
        public SimulatorViewModel(Canvas simcanvas,Image elbow, Image wrist, Image gripper, Image shoulder, Image base_)
        {
            _positionViewModel = new PositionViewModel();
            _simulatorcanvas = simcanvas;
            //_sim = Factory.currentIRobotInstance;
            suiSimulatorUI = new StringUI();
            Factory.getSimulatorInstance.IUIOutput = suiSimulatorUI;
            _sim = Factory.getSimulatorInstance;
            _elbow = elbow;
            _wrist = wrist;
            _gripper = gripper;
            _shoulder = shoulder;
            _base = base_;
            _xyCalculate = new XYCalculate(_sim.getCurrentPosition());
            
            drawBase();
            drawShoulder();
            drawElbow();
            drawWrist();
            drawGripper();
        }

        public void drawBase()
        {

        }
        public void drawShoulder()
        {

        }
        public void drawGripper()
        {
            Canvas.SetLeft(_gripper, _wrist.TransformToAncestor(_simulatorcanvas)
                              .Transform(new Point(80,150)).X);
            Canvas.SetTop(_gripper, _wrist.TransformToAncestor(_simulatorcanvas)
                              .Transform(new Point(80, 150)).Y);
            
        }

        public void drawElbow()
        {
            Canvas.SetLeft(_elbow, _shoulder.TransformToAncestor(_simulatorcanvas)
                              .Transform(new Point(170, 60)).X);
            Canvas.SetTop(_elbow, _gripper.TransformToAncestor(_simulatorcanvas)
                              .Transform(new Point(170, 60)).Y);
        }

        public void drawWrist()
        {
            Canvas.SetLeft(_wrist,_elbow.TransformToAncestor(_simulatorcanvas).Transform(new Point(110,60)).X);
            Canvas.SetTop(_wrist, _elbow.TransformToAncestor(_simulatorcanvas).Transform(new Point(110,60)).Y);
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
