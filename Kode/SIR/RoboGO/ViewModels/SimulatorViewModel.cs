using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DSL;
using ControlSystem;
namespace RoboGO.ViewModels
{
    public class SimulatorViewModel
    {
        private Canvas _simulatorcanvas;
        private Image _gripper;
        private TransformGroup _grippertransform;
        private Image _base;
        private TransformGroup _basetransform;
        private Image _shoulder;
        private TransformGroup _shouldertransform;
        private Image _wrist;
        private TransformGroup _wristtransform;
        private Image _elbow;
        private TransformGroup _elbowtransform;
        private readonly ScaleTransform _04scale = new ScaleTransform(0.4,0.4);

        public SimulatorViewModel(Canvas canvas)
        {
            _simulatorcanvas = canvas;
            _base = new Image();
            _gripper = new Image();
           _shoulder = new Image();
           _wrist = new Image();
           _elbow = new Image();
            _basetransform = new TransformGroup();
            _shouldertransform = new TransformGroup();
            _wristtransform = new TransformGroup();
            _elbowtransform = new TransformGroup();
            _simulatorcanvas.Children.Add(_base);
            _simulatorcanvas.Children.Add(_shoulder);
            _simulatorcanvas.Children.Add(_elbow);
            _simulatorcanvas.Children.Add(_wrist);
            _simulatorcanvas.Children.Add(_gripper);
            drawBase();
            drawShoulder();
            drawElbow();
            drawWrist();
            drawGripper();
        }

        public VecPoint getCurrentPosition()
        {
            return Factory.currentIRobotInstance.getCurrentPosition();
        }

        public void drawBase()
        {
            Canvas.SetLeft(_base,220);
            Canvas.SetTop(_base,230);
            _base.RenderTransform = _04scale;
            _base.Source = new BitmapImage(new Uri(@"\Images\base.png",UriKind.RelativeOrAbsolute));
        }
        public void drawShoulder()
        {
            Canvas.SetLeft(_shoulder, 280);
            Canvas.SetTop(_shoulder, 75);
            _shouldertransform.Children.Add(_04scale);
            _shouldertransform.Children.Add(new RotateTransform());
            _shoulder.RenderTransform = new ScaleTransform(0.4, 0.4); 
            _shoulder.Source = new BitmapImage(new Uri(@"\Images\shoulder.png", UriKind.RelativeOrAbsolute));
        }
        public void drawGripper()
        {
            Canvas.SetLeft(_gripper, 90);
            Canvas.SetTop(_gripper, 135);
            _gripper.RenderTransform = new ScaleTransform(0.4, 0.4);
            _gripper.Source = new BitmapImage(new Uri(@"\Images\gripper.png", UriKind.RelativeOrAbsolute));
        }

        public void drawElbow()
        {
            Canvas.SetLeft(_elbow, 175);
            Canvas.SetTop(_elbow, 40);
            _elbow.RenderTransform = new ScaleTransform(0.4, 0.4);
            _elbow.Source = new BitmapImage(new Uri(@"\Images\elbow.png", UriKind.RelativeOrAbsolute));
        }

        public void drawWrist()
        {
            Canvas.SetLeft(_wrist, 115);
            Canvas.SetTop(_wrist, 40);
            _wrist.RenderTransform = new ScaleTransform(0.4, 0.4); 
            _wrist.Source = new BitmapImage(new Uri(@"\Images\wrist.png", UriKind.RelativeOrAbsolute));
        }

    }
}
