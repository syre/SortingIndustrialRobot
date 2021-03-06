﻿/** \file MainWindow.xaml.cs */
/** \author Robotic Global Organization(RoboGO) */

using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ControlSystem;
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
        private MainWindowViewModel _mainwindowviewmodel;
        private IntellisenseViewModel ivmIntellisenseLogic;
        private TextBox txtboxCurrentIDECode;

        public IntellisenseViewModel ViewModelIntellisense
        {
            get { return ivmIntellisenseLogic; }
            set { ivmIntellisenseLogic = value; }
        }

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
            idevmViewModelIDE = new IDEViewModel();
            infoViewModel = new InfoViewModel(DatabaseTableValues);
            _mainwindowviewmodel = new MainWindowViewModel(pgbStyresystem);
            ViewModelIntellisense = new IntellisenseViewModel();
            // Data context
            tabIDE.DataContext = idevmViewModelIDE;
            tabInfo.DataContext = infoViewModel;
            pgbStyresystem.DataContext = _mainwindowviewmodel;
            list.DataContext = ViewModelIntellisense.UpdatedCollection;
        }

        #region Events
        //Handle textbox IntelliSense.
        private void IDETabs_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender.GetType() == typeof(TabControl))
            {
                txtboxCurrentIDECode = (TextBox)((TabItem)((TabControl)sender).SelectedItem).Content;
                if (txtboxCurrentIDECode != null)
                    ViewModelIntellisense.showMethodsPopUP(txtboxCurrentIDECode.GetRectFromCharacterIndex(txtboxCurrentIDECode.CaretIndex), txtboxCurrentIDECode, popup, list, e);
            }
        }

        //Choose a function from the list
        private void list_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            ViewModelIntellisense.list_KeyDown(listBox, e, txtboxCurrentIDECode, popup);
        }

        // Displaying login screen on load
        private void WindowsLoaded(object sender, RoutedEventArgs e)
        {
            Factory.getLogInstance.log("Program starting up!", eLogType.LOG_INFO);

            DisplayLoginScreen();
        }

        // Show manual steering controls
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
            Factory.getLogInstance.log("Program being shut down by user!", eLogType.LOG_INFO);
            Factory.getLogInstance.prepForShutdownApp();

            Factory.getThreadHandlingInstance.abortAllAndWait();
        }

        // Show available commands
        private void mnuViewCommands1_Click(object sender, RoutedEventArgs e)
        {
            CommandsWindow window = new CommandsWindow();
            window.Owner = this;
            window.Show();
            window.NavigateToHelp();
        }

        // Load tables from database when right tab
        private void tabctrlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((string)((TabItem)tabctrlMain.SelectedItem).Name == "tabInfo") && e.Handled == false)
            {
                infoViewModel.loadAllTables();
            }
        }

        // Get information from selected table
        private void DatabaseTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatabaseTables.SelectedIndex != -1)
            {
                infoViewModel.getTableInfo((string) ((DataRowView) DatabaseTables.CurrentCell.Item)[0]);
            }
            e.Handled = true;
        }

        // Save table to databse
        private void DatabaseTableValuesSaveButton_Click(object sender, RoutedEventArgs e)
        {
            infoViewModel.tableSave();
        }

        // Select between simulator and robot
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

        // Stop all robot action
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _mainwindowviewmodel.stopRobotInstance();
        }

        // Check connection of robot
        private void btnCheckConnectivity_Click(object sender, RoutedEventArgs e)
        {
            _mainwindowviewmodel.checkIsOnline();
        }

        // Print current shown table
        private void DatabasePrintButton_Click(object sender, RoutedEventArgs e)
        {
            infoViewModel.tablePrint();
        }

        // Show about box
        private void mnuAboutBox_Click(object sender, RoutedEventArgs e)
        {
            aboutBox abBox = new aboutBox();
            abBox.Owner = this;
            abBox.ShowDialog();
        }

        // Scroll to newest text in code output
        private void DSLOutputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DSLOutputBox.ScrollToEnd();
        }
        #endregion

        // Helper function for displaying login screen
        private void DisplayLoginScreen()
        {
            PasswordWindow psWindow = new PasswordWindow();

            psWindow.Owner = this;
            psWindow.ShowDialog();
            if (psWindow.DialogResult.HasValue && !psWindow.DialogResult.Value)
                this.Close();

            psWindow.Close();
        }
        
        void Button_Click(object sender, RoutedEventArgs e)
        {
            idevmViewModelIDE.CodeClear();
            DSLOutputBox.Clear();
        }
        
      
        
    }
}
