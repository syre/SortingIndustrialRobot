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
    }
}
