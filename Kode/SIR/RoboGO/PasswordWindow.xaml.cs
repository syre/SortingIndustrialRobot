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
using RoboGO.ViewModels;

namespace RoboGO
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        private passwordWindowViewModel passWindowViewModel;
        public passwordWindowViewModel PasswordWindowViewModel
        {
            get { return (passWindowViewModel); }
        }

        public PasswordWindow()
        {
            InitializeComponent();

            passWindowViewModel = new passwordWindowViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Write code here to authenticate user
            // If authenticated, then set DialogResult=true
            if(passWindowViewModel.authenticate(txtUserName.Text, txtPassword.Password))
            {
                DialogResult = true;
                this.Close();
            }
            
        }
    }
}
