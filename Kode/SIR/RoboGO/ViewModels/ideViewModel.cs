/** \author Robotic Global Organization(RoboGO) */
using System;
using System.ComponentModel;
using System.IO;
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
        /// <summary>
        /// Command that controls saveAs
        /// </summary>
        public RelayCommand saveAs { get; private set; }
        public RelayCommand open { get; private set; }
        public RelayCommand closeTab { get; private set; }
        public RelayCommand newTab { get; private set; }

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        private void notifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private TabControl ideTabs;
        /// <summary>
        /// TabControl for all the textboxes.
        /// </summary>
        public TabControl IdeTabs
        {
            get { return ideTabs; }
            set { ideTabs = value; }
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
            get { return (isrScriptRunner); }
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
        public DelegateCommand ExecuteComd
        {
            get { return (ecDelegateComd); }
        }
        #endregion

        // Functions
        /// <summary>
        /// Constructor which uses TabControl.
        /// 
        /// TabControl so can add and remove tab content.
        /// </summary>
        /// <param name="_ideTabs">TabControl used in main program in the IDE.</param>
        public IDEViewModel(TabControl _ideTabs)
        {
            // Members settings
            ideTabs = _ideTabs;
            isrScriptRunner = Factory.getScriptRunnerInstance;

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
        	   isrScriptRunner.setScriptFromString(((TextBox)(ideTabs.SelectedContent)).Text);
        	}
        	catch(Exception e)
        	{
        	    UIService.showMessageBox(e.Message, "ScriptRunner", MessageBoxButton.OK, MessageBoxImage.Error);
        	}
            isrScriptRunner.ExecuteScript();
            CodeOutput = isrScriptRunner.readFromOutputStream();
        }
        
        public void CodeClear()
        {
            isrScriptRunner.clearOutputStream();
        }

        protected bool saveAs_CanExecute
        {
            get { return (ideTabs.SelectedIndex >= 0); }
        }

        private void saveAs_Executed()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveAs_Executed_SaveFileDialog_Settings(saveDialog);

            if (saveDialog.ShowDialog() == true)
            {
                writeFile(saveDialog);
                ((TabItem) ideTabs.SelectedItem).Header = Path.GetFileName(saveDialog.FileName);
            }
        }

        private void writeFile(SaveFileDialog saveDialog)
        {
            StreamWriter writer = new StreamWriter(saveDialog.OpenFile());
            TextBox tempBox = (TextBox)(ideTabs.SelectedContent);
            writer.Write(tempBox.Text);
            writer.Close();
        }

        private void saveAs_Executed_SaveFileDialog_Settings(SaveFileDialog _saveDialog)
        {
            _saveDialog.Filter = "IronPython Files(*.py)|*.py";
            _saveDialog.DefaultExt = "xml";
            _saveDialog.AddExtension = true;
        }

        protected bool open_CanExecute
        {
            get { return true; }
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
            ideTabs.Items.Add(newTab);
            tempReader.Close();
        }

        private static void open_Executed_Settings(OpenFileDialog openDialog)
        {
            openDialog.Filter = "IronPython Files(*.py)|*.py";
            openDialog.CheckFileExists = true;
            openDialog.Multiselect = true;
        }

        protected bool closeTab_CanExecute
        {
            get { return (ideTabs.SelectedIndex >= 0); }
        }

        private void closeTab_Executed()
        {
            ideTabs.Items.Remove(ideTabs.SelectedItem);
        }

        protected bool newTab_CanExecute
        {
            get { return true; }
        }

        private void newTab_Executed()
        {
            TabItem tiItem = new TabItem();
            TextBox tbBox = new TextBox();
            tiItem.Content = tbBox;
            tiItem.Header = "New file";
            ideTabs.Items.Add((tiItem));
        }
        #endregion
    }
}
