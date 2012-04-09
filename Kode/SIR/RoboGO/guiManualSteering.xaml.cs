/** \file guiManualSteering.xaml.cs */
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
    /// Interaction logic for GUIManualSteering.xaml
    /// </summary>
    public partial class GUIManualSteering : Window
    {
        // Members
        private ViewModelManualSteering vmmsViewModel;

        public GUIManualSteering()
        {
            InitializeComponent();

            // ViewModel
            vmmsViewModel = new ViewModelManualSteering();
            this.DataContext = vmmsViewModel;
        }

        // Events: Most pass to ViewModel functions.(Can also use commands instead.)
    }
}
