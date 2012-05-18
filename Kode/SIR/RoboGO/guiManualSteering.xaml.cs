/** \file guiManualSteering.xaml.cs */

using System.Windows;
using System.Windows.Input;
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
        public ViewModelManualSteering ViewModel
        {
            get { return (vmmsViewModel); }
            set { vmmsViewModel = value; }
        }

        public GUIManualSteering()
        {
            InitializeComponent();

            // ViewModel
            vmmsViewModel = new ViewModelManualSteering();
            this.DataContext = vmmsViewModel;
       }
        
        #region Events
        void AxisBtnBaseLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisBaseLeft();
        }
        
        void StopMovement(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.stopMovement();
        }
        
        void AxisBtnBaseRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisBaseRight();
        }
        
        void AxisBtnShoulderLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisShoulderLeft();
        }
        
        void AxisBtnShoulderRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisShoulderRight();
        }
        
        void AxisBtnElbowLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisElbowLeft();
        }
        
        void AxisBtnElbowRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisElbowRight();
        }
        
        void AxisBtnWristPitchUp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisPitchUp();
        }
        
        void AxisBtnWristPitchDown_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisPitchDown();
        }
        
        void AxisWristRollLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisRollLeft();
        }
        
        void AxisBtnWristRollRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisRollRight();
        }
        
        void AxisBtnConveyerLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisConveyerLeft();
        }
        
        void AxisBtnConveyerRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveAxisConveyerRight();
        }
        
        void CoordBtnXInc_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordXIncreasing();
        }
        
        void CoordBtnXDec_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordXDecreasing();
        }
        
        void CoordBtnYInc_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordYIncreasing();
        }
        
        void CoordBtnYDec_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordYDecreasing();
        }
        
        void CoordBtnZInc_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordZIncreasing();
        }
        
        void CoordBtnZDec_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordZDecreasing();
        }
        
        void CoordBtnWristPitchInc_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordPitchIncreasing();
        }
        
        void CoordBtnWristPitchDec_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordPitchDecreasing();
        }
        
        void CoordBtnWristRollInc_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordRollIncreasing();
        }
        
        void CoordBtnWristRollDec_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vmmsViewModel.moveCoordRollDecreasing();
        }
        #endregion
    }
}
