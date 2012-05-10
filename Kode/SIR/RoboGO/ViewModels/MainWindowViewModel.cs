using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

        public void setRobotAsRobotInstance()
        {
            Factory.currentIRobotInstance = Factory.getRobotInstance;
        }

        public void stopRobotInstance()
        {
            Factory.currentIRobotInstance.stopAllMovement();
            Factory.currentIRobotInstance = null;
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

