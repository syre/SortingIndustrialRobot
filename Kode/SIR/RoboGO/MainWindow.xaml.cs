using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

        private PasswordWindow psWindow;
        private GUIManualSteering gmsManualGUI;

        public MainWindow()
        {
            InitializeComponent();

            // Members initialize
            psWindow = new PasswordWindow();
            gmsManualGUI = new GUIManualSteering();
            // Init
            idevmViewModelIDE = new IDEViewModel();

            // Data context
            tabIDE.DataContext = idevmViewModelIDE;
        }

        // A function within main that invokes function DisplayLogin
        private void WindowsLoaded(object sender, RoutedEventArgs e)
        {
            DisplayLoginScreen();
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
        }

        // Manual steering
        private void btnManuel_Click(object sender, RoutedEventArgs e)
        {
            // If using Simulator
            if(ControlSystem.Factory.currentIRobotInstance == ControlSystem.Factory.getSimulatorInstance)
                this.tabctrlMain.SelectedItem = this.tabitmSimulator;
            gmsManualGUI.Show();
        }

        // Clean up
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Close children
            gmsManualGUI.Close();
            psWindow.Close();
        }
    }
}
