using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/** \file MainWindow.xaml.cs */
/** \author Robotic Global Organization(RoboGO) */
using System.Windows;
using DSL;
using RoboGO.ViewModels;
using System.Xml.Serialization;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Command;


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

            this.DataContext = this;
            mnuFile1.DataContext = new InOutService();
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

        private void mnuViewCommands1_Click(object sender, RoutedEventArgs e)
        {
            CommandsWindow window = new CommandsWindow();
            window.Owner = this;
            window.Show();
            window.NavigateToHelp();
        }
    }
}
