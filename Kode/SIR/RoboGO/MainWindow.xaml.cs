/** \file MainWindow.xaml.cs */
/** \author Robotic Global Organization(RoboGO) */

using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private SimulatorViewModel _simviewmodel;
        private MainWindowViewModel _mainwindowviewmodel;

        public IDEViewModel ViewModelIDE
        {
            get { return (idevmViewModelIDE); }
        }

        private InfoViewModel infoViewModel;
        public InfoViewModel ViewModelInfo
        {
            get { return (infoViewModel); }
        }

        public MainWindow()
        {
            InitializeComponent();

            // Init
            idevmViewModelIDE = new IDEViewModel(IDETabs);
            infoViewModel = new InfoViewModel(DatabaseTableValues);
            _simviewmodel = new SimulatorViewModel(DrawCanvas,Elbow,Wrist,Gripper,Shoulder,Base);
            _mainwindowviewmodel = new MainWindowViewModel(pgbStyresystem);
            // Data context
            tabIDE.DataContext = idevmViewModelIDE;
            tabitmSimulator.DataContext = _simviewmodel;
            tabInfo.DataContext = infoViewModel;
            pgbStyresystem.DataContext = _mainwindowviewmodel;

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
			if (psWindow.DialogResult.HasValue && !psWindow.DialogResult.Value)               
                this.Close();

            psWindow.Close();
        }

        // Manual steering
        private void btnManuel_Click(object sender, RoutedEventArgs e)
        {

            GUIManualSteering gmsManualGUI = new GUIManualSteering();

            // If using Simulator
            if(ControlSystem.Factory.currentIRobotInstance == ControlSystem.Factory.getSimulatorInstance)
                this.tabctrlMain.SelectedItem = this.tabitmSimulator;
            gmsManualGUI.ShowDialog();
        }

        // Clean up
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void mnuViewCommands1_Click(object sender, RoutedEventArgs e)
        {
            CommandsWindow window = new CommandsWindow();
            window.Owner = this;
            window.Show();
            window.NavigateToHelp();
        }

        private void tabctrlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((string)((TabItem)tabctrlMain.SelectedItem).Name == "tabInfo") && e.Handled == false)
            {
                infoViewModel.loadAllTables();
            }
        }

        private void DatabaseTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatabaseTables.SelectedIndex != -1)
            {
                infoViewModel.getTableInfo((string) ((DataRowView) DatabaseTables.CurrentCell.Item)[0]);
            }
            e.Handled = true;
        }

        private void DatabaseTableValuesSaveButton_Click(object sender, RoutedEventArgs e)
        {
            infoViewModel.tableSave();
        }

        private void SelectRobot_Click(object sender, RoutedEventArgs e)
        {
            if (cmbChoice.SelectedItem == cmbSimulator)
            {
                _mainwindowviewmodel.setSimulatorAsRobotInstance();
            }
            else if (cmbChoice.SelectedItem == cmbRobot)
            {
                if (!_mainwindowviewmodel.setRobotAsRobotInstance())
                    MessageBox.Show("Could not connect to Robot", "Connection Error", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _mainwindowviewmodel.stopRobotInstance();
        }

        private void btnCheckConnectivity_Click(object sender, RoutedEventArgs e)
        {
            _mainwindowviewmodel.checkIsOnline();
        }

        private void DatabasePrintButton_Click(object sender, RoutedEventArgs e)
        {
            infoViewModel.tablePrint();
        }
    }
}
