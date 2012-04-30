/** \file MainWindow.xaml.cs */
/** \author Robotic Global Organization(RoboGO) */

using System;
using System.Windows;
using DSL;
using RoboGO.ViewModels;


namespace RoboGO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Members and properties
        private IDEViewModel idevmViewModelIDE;
        public IDEViewModel ViewModelIDE
        {
            get { return (idevmViewModelIDE); }
        }

        private GUIManualSteering gmsManualGUI;

        public MainWindow()
        {
            InitializeComponent();

            // Init
            idevmViewModelIDE = new IDEViewModel(IDETabs);

            // Data context
            tabIDE.DataContext = idevmViewModelIDE;
        }

        // A function within main that invokes function DisplayLogin
        private void WindowsLoaded(object sender, RoutedEventArgs e)
        {
            //DisplayLoginScreen();
        }

        // The Window which needs a login
        private void DisplayLoginScreen()
        {
            PasswordWindow psWindow = new PasswordWindow();

            psWindow.Owner = this;
            psWindow.ShowDialog();
            if (psWindow.DialogResult.HasValue && psWindow.DialogResult.Value)
                MessageBox.Show("User Logged In");
            else
                this.Close();

            psWindow.Close();
        }

        // Manual steering
        private void btnManuel_Click(object sender, RoutedEventArgs e)
        {
            // Initialize
            if (gmsManualGUI == null)
            {
                try
                {
                    gmsManualGUI = new GUIManualSteering();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    gmsManualGUI = null;
                    return;
                }
            }

            // If using Simulator
            if(ControlSystem.Factory.currentIRobotInstance == ControlSystem.Factory.getSimulatorInstance)
                this.tabctrlMain.SelectedItem = this.tabitmSimulator;
            gmsManualGUI.Show();
        }

        // Clean up
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Cleanup
            if(gmsManualGUI != null) // Not modal so have to be checked for, for can not close in function which opens it
                gmsManualGUI.Close();
        }

        private void mnuViewCommands1_Click(object sender, RoutedEventArgs e)
        {
            CommandsWindow window = new CommandsWindow();
            window.Owner = this;
            window.Show();
            window.NavigateToHelp();
        }
    }
}
