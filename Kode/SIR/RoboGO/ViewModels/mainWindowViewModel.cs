/** \file PositionViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Windows.Controls;
using System.Windows.Media;
using ControlSystem;

namespace RoboGO
{
    /// <summary>
    /// ViewModel for the mainwindow.
    /// </summary>
    public class MainWindowViewModel
    {
        // Members and properties.
        private ProgressBar _pbar;

        /// <summary>
        /// Constructor with progressbar for showing connection status.
        /// </summary>
        /// <param name="_pb">Progressbar for showing connection status</param>
        public MainWindowViewModel(ProgressBar _pb)
        {
            _pbar = _pb;
        }

        /// <summary>
        /// Sets the current robot as being a simulator.
        /// </summary>
        public void setSimulatorAsRobotInstance()
        {
            Factory.currentIRobotInstance = Factory.getSimulatorInstance;
        }

        /// <summary>
        /// Sets the curren robot as being the SCORBOT.
        /// </summary>
        /// <returns>False if DLL missing.</returns>
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

        /// <summary>
        /// Stops the robot from continuing any action.
        /// </summary>
        public void stopRobotInstance()
        {
            ThreadHandling tempThreadHandler = Factory.getThreadHandlingInstance;

            tempThreadHandler.abortAllAndWait();

            IRobot tempIRobot = Factory.currentIRobotInstance;

            if (tempIRobot == null)
                throw new Exception("No chosen running method found");
            if (!tempIRobot.stopAllMovement())
                throw new Exception("Could not stop robot");
        }

        /// <summary>
        /// Check for being online.(The robot.)
        /// </summary>
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

