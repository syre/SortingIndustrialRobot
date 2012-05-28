/** \file ideViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using ControlSystem;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Windows;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// ViewModel between IDEView and ScriptRunner.
    /// </summary>
    public class IDEViewModel : INotifyPropertyChanged
    {
        #region Properties

        private TabItem currentlyselectedtab;
        public TabItem currentlySelectedTab
        {
            get { return currentlyselectedtab; }
            set 
            { 
                currentlyselectedtab = value;
                notifyPropertyChanged("currentlySelectedTab");
            }
        }

        private ObservableCollection<TabItem> obscollectiontabs;
        public ObservableCollection<TabItem> obsCollectionTabs
        {
            get { return obscollectiontabs; }
            set { obscollectiontabs = value; }

        }

		/// <summary>
		/// Called when dependency properties changed.(Used in view.)
		/// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void notifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private string sDSLOutput;
        /// <summary>
        /// Where print statements gets printed.
        /// </summary>
        public string CodeOutput
        {
            get { return sDSLOutput; }
            set { sDSLOutput = value; notifyPropertyChanged("CodeOutput");}
        }

        private IScriptRunner isrScriptRunner;
        /// <summary>
        /// ScriptRunner using to execute code.
        /// </summary>
        public IScriptRunner ScriptExecuter
        {
            get
            {
                return (isrScriptRunner);
            }
            set
            {
                if (value == null)
                    throw new Exception();
                else
                    isrScriptRunner = value;
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ecDelegateComd;
		/// <summary>
		/// Executes the code.
		/// </summary>
        public DelegateCommand ExecuteComd
        {
            get { return (ecDelegateComd); }
        }
        
        /// <summary>
        /// Save file from current tab.
        /// </summary>
        public RelayCommand saveAs { get; private set; }
        
        /// <summary>
        /// Open file.
        /// </summary>
        public RelayCommand open { get; private set; }
        
        /// <summary>
        /// Close current tab.
        /// </summary>
        public RelayCommand closeTab { get; private set; }
        
        /// <summary>
        /// Make a new tab.
        /// </summary>
        public RelayCommand newTab { get; private set; }
        
        /// <summary>
        /// Build the current code.
        /// </summary>
        public RelayCommand build { get; private set; }
        #endregion

        // Functions
        /// <summary>
        /// Constructor which uses TabControl.
        /// 
        /// TabControl so can add and remove tab content.
        /// </summary>
        /// <param name="_ideTabs">TabControl used in main program in the IDE.</param>
        public IDEViewModel()
        {
            obsCollectionTabs = new ObservableCollection<TabItem>();
            newTab_Executed();
            currentlySelectedTab = obsCollectionTabs[0];

            // Members settings
            isrScriptRunner = Factory.getScriptRunnerInstance;
            Factory.getThreadHandlingInstance.addThread(executeCodeThread, "ExecuteScript");

            #region Commands
            

            saveAs = new RelayCommand(
                    () => saveAs_Executed(),
                    () => saveAs_CanExecute);

            open = new RelayCommand(
                    () => open_Executed(),
                    () => open_CanExecute);

            closeTab = new RelayCommand(
                    () => closeTab_Executed(),
                    () => closeTab_CanExecute);
            
            newTab = new RelayCommand(
                    () => newTab_Executed(),
                    () => newTab_CanExecute);

            ecDelegateComd = new DelegateCommand(executeCode);
            #endregion
        }

        #region CommandHandlers
        /// <summary>
        /// Execute the code.
        /// </summary>
        public void executeCode()
        {
        	try
        	{
        	   isrScriptRunner.setScriptFromString(((TextBox)currentlySelectedTab.Content).Text);

               if (Factory.getThreadHandlingInstance.find("ExecuteScript").threadPlaceHolder.IsAlive == true)
               {
                   if (UIService.showMessageBox("Another program already running, please wait for it to finish\n\nWould you like to abort it and continue?", "Build", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                   {
                       Factory.getThreadHandlingInstance.abortAndWait("ExecuteScript");
                   }
                   else
                   {
                       return;
                   }
               }
               Factory.getThreadHandlingInstance.start("ExecuteScript");
        	}
        	catch(Exception e)
        	{
        	    UIService.showMessageBox(e.Message, "ScriptRunner", MessageBoxButton.OK, MessageBoxImage.Error);
        	}

        }

        private void executeCodeThread()
        {
            isrScriptRunner.ExecuteScript();
            CodeOutput = isrScriptRunner.readFromOutputStream();
        }
        
        /// <summary>
        /// Clear the code output shown.
        /// </summary>
        public void CodeClear()
        {
            isrScriptRunner.clearOutputStream();
        }

		/// <summary>
		/// Tells if able to save code.
		/// </summary>
		/// <returns>True if tab selected./returns>
        protected bool saveAs_CanExecute
        {
            get { return (obsCollectionTabs != null); }
        }

        private void saveAs_Executed()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveAs_Executed_SaveFileDialog_Settings(saveDialog);

            if (saveDialog.ShowDialog() == true)
            {
                writeFile(saveDialog);
                currentlySelectedTab.Header = Path.GetFileName(saveDialog.FileName);
            }
        }

        private void writeFile(SaveFileDialog saveDialog)
        {
            StreamWriter writer = new StreamWriter(saveDialog.OpenFile());
            TextBox tempBox = (TextBox)(currentlySelectedTab.Content);
            writer.Write(tempBox.Text);
            writer.Close();
        }

        private void saveAs_Executed_SaveFileDialog_Settings(SaveFileDialog _saveDialog)
        {
            _saveDialog.Filter = "IronPython Files(*.py)|*.py";
            _saveDialog.DefaultExt = "xml";
            _saveDialog.AddExtension = true;
        }

		/// <summary>
		/// Tells if able to open file.
		/// </summary>
		/// <returns>Returns true if number of tabs is below 9</returns>
        protected bool open_CanExecute
        {
            get { return obsCollectionTabs.Count < 9; }
        }

        private void open_Executed()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            open_Executed_Settings(openDialog);

            if (openDialog.ShowDialog() == true)
            {

                foreach (string file in openDialog.FileNames)
                {
                    openFile(file);
                }
            }
        }

        private void openFile(string file)
        {
            Stream tempStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader tempReader = new StreamReader(tempStream);

            TabItem newTab = new TabItem();
            TextBox tempBox = new TextBox();
            tempBox.Text = tempReader.ReadToEnd();

            newTab.Content = tempBox;
            FileInfo fi = new FileInfo(file);
            newTab.Header = fi.Name;
            obsCollectionTabs.Add(newTab);
            currentlySelectedTab = newTab;
            tempReader.Close();
        }

        private static void open_Executed_Settings(OpenFileDialog openDialog)
        {
            openDialog.Filter = "IronPython Files(*.py)|*.py";
            openDialog.CheckFileExists = true;
            openDialog.Multiselect = true;
        }

		/// <summary>
		/// Tells if able to close tab.
		/// </summary>
		/// <returns>True if 1 or more tabs.</returns>
        protected bool closeTab_CanExecute
        {
            get { return (currentlySelectedTab != null); }
        }

        private void closeTab_Executed()
        {
            if(obsCollectionTabs.Count == 1)
                newTab_Executed();

            obsCollectionTabs.Remove(currentlySelectedTab);
        }

		/// <summary>
		/// Tells if able to open a new tab.
		/// </summary>
		/// <returns>Returns true if number of tabs is below 9 </returns>
        protected bool newTab_CanExecute
        {
            get { return obsCollectionTabs.Count < 9; }
        }

        private void newTab_Executed()
        {
            TabItem tiItem = new TabItem();
            TextBox tbBox = new TextBox();
            tiItem.Content = tbBox;
            tiItem.Header = "New file";
            obsCollectionTabs.Add(tiItem);
            currentlySelectedTab = tiItem;
        }
        #endregion
    }
}
