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
            picBoxLogo.Image =  System.Drawing.Image.FromFile(@"Images\R3oboGOAnimated.gif");
            }
            catch(System.Exception e)
            {
                UIService.showMessageBox("Resource 'Images\\RoboGOAnimated.gif' not found.", "Loading image", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
