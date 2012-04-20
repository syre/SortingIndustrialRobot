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

namespace RoboGO
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CommandsWindow : Window
    {
        public CommandsWindow()
        {
            InitializeComponent();
        }

        public void NavigateToHelp()
        {
            helpbrowser.Navigate(new Uri(String.Format("file:///{0}/../../commands.html", Directory.GetCurrentDirectory())));
        }
    }
}
