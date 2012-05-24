using System.Windows;
using System.Windows.Forms;

namespace RoboGO
{
    /// <summary>
    /// Interaction logic for aboutBox.xaml
    /// </summary>
    public partial class aboutBox : Window
    {
        public aboutBox()
        {
            InitializeComponent();
            try
            {
                picBoxLogo.Image =  System.Drawing.Image.FromFile(@"Images\RoboGOAnimated.gif");
            }
            catch(System.Exception)
            {
                UIService.showMessageBox("Resource 'Images\\RoboGOAnimated.gif' not found.", "Loading image", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
