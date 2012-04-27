using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using RoboGO;

namespace Command
{
    public class InOutService
    {
        public RelayCommand SaveAs { get; private set; }
        public RelayCommand Open { get; private set; }
        public RelayCommand CloseTab { get; private set; }
        private MainWindow _mainWindow = new MainWindow();
        
        public InOutService()
        {
            _mainWindow = new MainWindow();
       
            SaveAs = new RelayCommand(
                () => SaveAs_Executed(),
                () => SaveAs_CanExecute);

            Open = new RelayCommand(
                    () => Open_Executed(),
                    () => Open_CanExecute);

            CloseTab = new RelayCommand(
                    () => CloseTab_Executed(),
                    () => CloseTab_CanExecute);
        }
        
        #region CommandHandlers

        protected bool SaveAs_CanExecute
        {
            get { return (_mainWindow.IDETabs.SelectedIndex >= 0); }
        }

        private void SaveAs_Executed()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            SaveAs_Executed_SaveFileDialog_Settings(saveDialog);

            if (saveDialog.ShowDialog() == true)
            {
                writeFile(saveDialog);
            }
        }

        private void writeFile(SaveFileDialog saveDialog)
        {
            StreamWriter writer = new StreamWriter(saveDialog.OpenFile());
            TextBox tempBox = (TextBox) ((TabItem) _mainWindow.IDETabs.Items[0]).Content;
            writer.Write(tempBox.Text);
            writer.Close();
        }

        private void SaveAs_Executed_SaveFileDialog_Settings(SaveFileDialog _saveDialog)
        {
            _saveDialog.Filter = "TXT Files(*.txt)|*.txt";
            _saveDialog.DefaultExt = "xml";
            _saveDialog.AddExtension = true;
        }

        protected bool Open_CanExecute
        {
            get { return true; }
        }

        private void Open_Executed()
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

            /// \warning Untested when richtextbox.
            TabItem newTab = new TabItem();
            RichTextBox tempBox = new RichTextBox();
            FlowDocument fdContent = new FlowDocument();
            fdContent.Blocks.Add(new Paragraph(new Run(tempReader.ReadToEnd())));
            tempBox.Document = fdContent;

            newTab.Content = tempBox;
            FileInfo fi = new FileInfo(file);
            newTab.Header = fi.Name;
            _mainWindow.IDETabs.Items.Add(newTab);
            tempReader.Close();
        }

        private static void open_Executed_Settings(OpenFileDialog openDialog)
        {
            openDialog.Filter = "TXT Files(*.txt)|*.txt";
            openDialog.CheckFileExists = true;
            openDialog.Multiselect = true;
        }

        protected bool CloseTab_CanExecute
        {
            get { return (_mainWindow.IDETabs.SelectedIndex >= 0); }
        }

        private void CloseTab_Executed()
        {
            _mainWindow.IDETabs.Items.Remove(_mainWindow.IDETabs.SelectedItem);
        }

        #endregion

    }
}
