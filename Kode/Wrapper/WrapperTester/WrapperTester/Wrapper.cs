/** \file Wrapper.cs */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WrapperTester // Has to be changed
{
    /// <summary>
    /// Contains a wrapper for the C++ functions in the dll file(USBC.dll).
    /// 
    /// Notes:  Uses IntPtr for different types of C++ pointers.
    ///         Same function names as C++ but has "Wrapped" at the end.
    /// </summary>
    class Wrapper
    {
        // Members
        private dgateCallBack callbackfuncSucessful;
        private dgateCallBack callbackfuncError;
        public static void successfulCall(IntPtr intptrConfigData)
        {
            System.Console.WriteLine("Hello");
        } 
        public static void errorCall(IntPtr intptrConfigData)
        {
            System.Console.WriteLine("Error");
        } 

        // Settings constants
        // -Initialization mode
        public const short MODE_DEFAULT = 0; // Last used mode
        public const short MODE_ONLINE = 1; // Force online mode
        public const short MODE_SIMULAT = 2; // Simulator mode
        // -Initialization system type
        public const short SYSTEM_TYPE_DEFAULT = 0; // Detect it
        public const short SYSTEM_TYPE_ER4USB= 41; // ER-4

        // Functions
        // -Constructors and destructors
        public Wrapper()
        {
            // Initializes callback functions
            callbackfuncSucessful = new dgateCallBack(successfulCall);
            callbackfuncError = new dgateCallBack(errorCall);
        }

        // Wrapped functions
        /// <summary>
        /// Initializes the robot.
        /// </summary>
        /// <param name="shrtMode">Mode. For example simulator.(Use one of constants)</param>
        /// <param name="shrtType">Type of connection.(Use one of constants)</param>
        /// <returns>Returns 1 on successful call.(But errors can still happen)</returns>
        public int initializationWrapped(short shrtMode, short shrtType)
        {
            int iReturnValue = initialization(shrtMode, shrtType, callbackfuncSucessful, callbackfuncError);
            return (iReturnValue);
        }
        /// <summary>
        /// Sets the location of the "Par" folder.
        /// </summary>
        /// <param name="charptrFolder">String with location of file.</param>
        /// <returns>Returns 1 on success.</returns>
        public int setParameterFolderWrapped(string sFolderAdress)
        {
            IntPtr intptrTmp = Marshal.StringToHGlobalAnsi(sFolderAdress);
            return(setParameterFolder(intptrTmp));
        }

        // -Imported(ShouldUseWrappedVersions)
        [UnmanagedFunctionPointer( CallingConvention.Cdecl, CharSet = CharSet.Ansi )] /** \todo Wrap timer around */
        private delegate void dgateCallBack(IntPtr voidptrConfigData);

        [DllImport("USBC.dll", EntryPoint = "?Initialization@@YAHFFP6AXPAX@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int initialization(short shrtMode, short shrtType, dgateCallBack funcprtCallBack, dgateCallBack funcptrCallBackError);

        [DllImport("USBC.dll", EntryPoint = "?SetParameterFolder@@YAHPAD@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int setParameterFolder(IntPtr charptrFolder);
    }
}
