/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DSL;
using ControlSystem;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Windows;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// ViewModel between IDEView and ScriptRunner.
    /// </summary>
    public class IDEViewModel
    {
        /// <summary>
        /// Command that controls saveAs
        /// </summary>
        public RelayCommand saveAs { get; private set; }
        public RelayCommand open { get; private set; }
        public RelayCommand closeTab { get; private set; }

        private TabControl ideTabs;

        // Members and properties
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

        private string sCode;
        /// <summary>
        /// Code to execute.
        /// </summary>
        public string Code
        {
            get { return (sCode); }
            set { sCode = value; }
        }
        #region Commands

        private DelegateCommand ecDelegateComd;
        public DelegateCommand delegateComd
        {
            get { return (ecDelegateComd); }
        }
        #endregion

        // Functions
        public IDEViewModel(TabControl _ideTabs)
        {
            // Members settings
            ideTabs = _ideTabs;
            sCode = "";
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

            ecDelegateComd = new DelegateCommand(executeCode);
            #endregion
        }

        #region CommandHandlers
        /// <summary>
        /// Execute the code.
        /// </summary>
        public void executeCode()
        {
            isrScriptRunner.setScriptFromString(Code);
            isrScriptRunner.ExecuteScript();
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
            _saveDialog.Filter = "TXT Files(*.txt)|*.txt";
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
            openDialog.Filter = "TXT Files(*.txt)|*.txt";
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

        #endregion
    }
}
