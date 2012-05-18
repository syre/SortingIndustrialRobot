using System;
using System.Windows.Controls;
using System.Windows.Media;
using ControlSystem;
using DSL;

namespace RoboGO
{
    public class MainWindowViewModel
    {

        public IRobot RobotInstance
        {
            get { return Factory.currentIRobotInstance; }
        }

        private ProgressBar _pbar;

        public MainWindowViewModel(ProgressBar _pb)
        {
            _pbar = _pb;
        }

        public bool IsOnline
        {
            get { return Factory.currentIRobotInstance.isOnline(); }
        }

        public void setSimulatorAsRobotInstance()
        {
            Factory.currentIRobotInstance = Factory.getSimulatorInstance;
        }

        public bool setRobotAsRobotInstance()
        {
            try
            {
                Factory.currentIRobotInstance = Factory.getRobotInstance;
            }
            catch(DllNotFoundException)
            {
                return false;
            }
            return true;
        }

        public void stopRobotInstance()
        {
            Factory.currentIRobotInstance.stopAllMovement();
        }

        public void checkIsOnline()
        {
            if(Factory.currentIRobotInstance == null)
                _pbar.Background = new SolidColorBrush(Colors.Gray);
            else if(Factory.currentIRobotInstance.isOnline())
            {
                _pbar.Background = new SolidColorBrush(Colors.Green);
            }
            else
                _pbar.Background = new SolidColorBrush(Colors.Red);
        }
    }
}

