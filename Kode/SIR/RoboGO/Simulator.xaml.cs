using System.Windows.Controls;
using RoboGO.ViewModels;

namespace RoboGO
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : UserControl
    {
        private SimulatorViewModel simulator = new SimulatorViewModel();
        public Simulator()
        {
            InitializeComponent();
            DataContext = simulator;
        }
        
        // Scroll to end
        void TxtblockUIOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtblockUIOutput.Focus();
            txtblockUIOutput.CaretIndex = txtblockUIOutput.Text.Length;
            txtblockUIOutput.ScrollToEnd();
        }
        
        void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            simulator.UIText.clearString();
            txtblockUIOutput.Clear();
           
        }
    }
}
