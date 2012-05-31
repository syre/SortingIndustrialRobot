using System;
using System.IO;
using System.Windows;

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
            helpbrowser.Navigate(new Uri(String.Format("file:///{0}/DSLFiler/commands.html"
                                                       , Directory.GetCurrentDirectory())));
        }
    }
}
