using System;
using System.Collections.Generic;
using System.IO;
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

        
        public MainWindow()
        {
            InitializeComponent();

<<<<<<< HEAD
            mnuFile1.DataContext = new InOutService();
=======
            SaveAs = new RelayCommand(
                () => SaveAs_Executed(),
                () => SaveAs_CanExecute);

            Open = new RelayCommand(
                () => Open_Executed(),
                () => Open_CanExecute);

            CloseTab = new RelayCommand(
                () => CloseTab_Executed(),
                () => CloseTab_CanExecute);

            this.DataContext = this;
>>>>>>> f37be85dfa02a1fc449c493df8eea8b1b7563802
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

<<<<<<< HEAD
=======
        #region CommandHandlers

        protected bool SaveAs_CanExecute
        {
            get { return (IDETabs.SelectedIndex >= 0); }
        }

        private void SaveAs_Executed()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "TXT Files(*.txt)|*.txt";
            saveDialog.DefaultExt = "txt";
            saveDialog.AddExtension = true;

            if (saveDialog.ShowDialog() == true)
            {
                StreamWriter writer = new StreamWriter(saveDialog.OpenFile());
                TextBox tempBox = (TextBox)((TabItem)IDETabs.Items[0]).Content;
                writer.Write(tempBox.Text);
                writer.Close();
            }
        }

        protected bool Open_CanExecute
        {
            get { return true; }
        }

        private void Open_Executed()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "TXT Files(*.txt)|*.txt";
            openDialog.CheckFileExists = true;
            openDialog.Multiselect = true;

            if (openDialog.ShowDialog() == true)
            {

                foreach (string file in openDialog.FileNames)
                {
                    Stream tempStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader tempReader = new StreamReader(tempStream);

                    TabItem newTab = new TabItem();
                    TextBox tempBox = new TextBox();
                    tempBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    tempBox.Text = tempReader.ReadToEnd();

                    newTab.Content = tempBox;
                    FileInfo fi = new FileInfo(file);
                    newTab.Header = fi.Name;
                    IDETabs.Items.Add(newTab);
                    tempReader.Close();
                }
            }

        }

        protected bool CloseTab_CanExecute
        {
            get { return (IDETabs.SelectedIndex >= 0); }
        }

        private void CloseTab_Executed()
        {
            IDETabs.Items.Remove(IDETabs.SelectedItem);
        }

        #endregion


>>>>>>> f37be85dfa02a1fc449c493df8eea8b1b7563802
    }
}
