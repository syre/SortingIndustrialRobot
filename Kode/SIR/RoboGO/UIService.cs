/** \file IOService.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Windows;

namespace RoboGO
{
    /// <summary>
    /// Service for UI related services.
    /// </summary>
    public static class UIService
    {
        // Functions
        public static MessageBoxResult showMessageBox(string _sMessage, string _sCaption, MessageBoxButton _msbEnum, MessageBoxImage _msbImage)
        {
            return(MessageBox.Show(_sMessage, _sCaption, _msbEnum, _msbImage));
        }
    }
}
