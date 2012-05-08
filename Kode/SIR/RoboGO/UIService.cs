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
        public static void showMessageBox(string _sMessage, string _sCaption)
        {
            MessageBox.Show(_sMessage, _sCaption);
        }
    }
}
