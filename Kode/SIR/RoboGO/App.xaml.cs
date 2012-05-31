using System;
using System.Windows;
using System.Windows.Threading;
using ControlSystem;

namespace RoboGO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Global exception handling  
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppUnhandledException);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;

            string errorMessage = string.Format("An application error occurred.\nPlease check whether your data is correct and repeat the action. If this error occurs again there seems to be a more serious malfunction in the application, and you better close it.\n\nError: {0}\n\nDo you want to continue?\n\nYes: You will continue with your work, but might run into problems later on.\nNo: The application will close.",args.Exception.Message);
            MessageBoxResult tempResult = UIService.showMessageBox(errorMessage, "Error", MessageBoxButton.YesNo,MessageBoxImage.Error);

            Factory.getLogInstance.log(args.Exception.Message, eLogType.LOG_ERROR);

            if (tempResult == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
        }

        private void AppUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = args.ExceptionObject as Exception;

            Factory.getLogInstance.log(e.Message, eLogType.LOG_ERROR);

            string errorMessage = string.Format("An application error occurred...\n\nStackTrace:\n{0}\n\nProgram is shutting down...\n\nCause: Unhandled Thread Exception\nException Error: {1}", e.StackTrace, e.Message);
            UIService.showMessageBox(errorMessage, "Program Malfunction", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
