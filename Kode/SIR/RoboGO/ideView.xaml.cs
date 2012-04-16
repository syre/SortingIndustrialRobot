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
    /// Interaction logic for ideView.xaml
    /// </summary>
    public partial class IDEView : Window
    {
        // Members and properties
        private IDEViewModel idevmViewModel;
        public IDEViewModel ViewModel
        {
            get { return (idevmViewModel); }
        }

        public IDEView()
        {
            InitializeComponent();
            idevmViewModel = new IDEViewModel();
            this.DataContext = idevmViewModel;
        }
    }
}
