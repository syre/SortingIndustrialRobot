/** \file MainWindow.xaml.cs */
/** \author Robotic Global Organization(RoboGO) */

using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
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
        public IDEViewModel ViewModelIDE
        {
            get { return (idevmViewModelIDE); }
        }

        private InfoViewModel infoViewModel;
        public InfoViewModel ViewModelInfo
        {
            get { return (infoViewModel); }
        }

        private GUIManualSteering gmsManualGUI;

        public MainWindow()
        {
            InitializeComponent();

            // Init
            idevmViewModelIDE = new IDEViewModel(IDETabs);
            _simviewmodel = new SimulatorViewModel(DrawCanvas);
            infoViewModel = new InfoViewModel();
            // Data context
            tabIDE.DataContext = idevmViewModelIDE;
            tabInfo.DataContext = infoViewModel;
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
    }
}
