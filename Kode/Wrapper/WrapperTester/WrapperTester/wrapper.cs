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
    /** \author Robotic Global Organization(RoboGO) */
    class Wrapper
    {
        // Members
        private dgateCallBack callbackfuncSucessful;
        private dgateCallBack callbackfuncError;
        private dgateCallBackCharArg callbackfuncHoming;

        // -Events
        public static void eventSuccess(IntPtr intptrConfigData)
        {
            System.Console.WriteLine("Hello");
        } 
        public static void eventError(IntPtr intptrConfigData)
        {
            System.Console.WriteLine("Error");
        }
        public void eventHoming(Byte homeNotif)
        {
            Console.WriteLine("Home Status: " + homeNotif);
        }

        // Settings constants
        // -Initialization mode
        public const short MODE_DEFAULT = 0; // Last used mode
        public const short MODE_ONLINE = 1; // Force online mode
        public const short MODE_SIMULAT = 2; // Simulator mode
        // -Initialization system type
        public const short SYSTEM_TYPE_DEFAULT = 0; // Detect it
        public const short SYSTEM_TYPE_ER4USB= 41; // ER-4
        // -Axis control
        public enum enumAxis
        {
            AXIS_ROBOT,
            AXIS_PERIPHERALS,
            AXIS_ALL
        }

        // Functions
        // -Constructors and destructors
        public Wrapper()
        {
            // Initializes callback functions
            callbackfuncSucessful = new dgateCallBack(eventSuccess);
            callbackfuncError = new dgateCallBack(eventError);
            callbackfuncHoming = new dgateCallBackCharArg(eventHoming);
        }

        // Wrapped functions
        /// <summary>
        /// Initializes the robot.
        /// </summary>
        /// <param name="shrtMode">Mode. For example simulator.(Use one of constants)</param>
        /// <param name="shrtType">Type of connection.(Use one of constants)</param>
        /// <returns>Returns true on successful call.(But errors can still happen)</returns>
        public bool initializationWrapped(short shrtMode, short shrtType)
        {
            int iReturnValue = initialization(shrtMode, shrtType, callbackfuncSucessful, callbackfuncError);
            return ((iReturnValue == 1)? true : false);
        }
        /// <summary>
        /// Sets the location of the "Par" folder.
        /// </summary>
        /// <param name="charptrFolder">String with location of file.</param>
        /// <returns>Returns true on success.</returns>
        public bool setParameterFolderWrapped(string sFolderAddress)
        {
            IntPtr intptrTmp = Marshal.StringToHGlobalAnsi(sFolderAddress);
            int iReturnValue = setParameterFolder(intptrTmp);
            return ((iReturnValue == 1) ? true : false);
        }
        /// <summary>
        /// Returns address of "Par" folder.
        /// </summary>
        /// <param name="sFolderAddress">Buffer for address.</param>
        /// <returns>Returns true on success.</returns>
        public bool getParameterFolderWrapped(out string sFolderAddress)
        {
            IntPtr intptrTmp = Marshal.AllocHGlobal(200);
            int iReturnValue = GetParameterFolder(intptrTmp);
            sFolderAddress = Marshal.PtrToStringAuto(intptrTmp);
            return ((iReturnValue == 1) ? true : false);
        }

        /// <summary>
        /// Turns control on and off for certain axis group.
        /// </summary>
        /// <param name="bAxis">Axis to affect.(Use enums)</param>
        /// <param name="bIsOn">To have it turned off or on.</param>
        /// <returns></returns>
        public bool controlWrapped(enumAxis axisGroup, bool bIsOn)
        {
            byte bArg = (byte)'A';
            int iReturnValue;
            switch (axisGroup)
            {
                case enumAxis.AXIS_ROBOT:
                    bArg = (byte)'A';
                    break;
                case enumAxis.AXIS_PERIPHERALS:
                    bArg = (byte)'B';
                    break;
                case enumAxis.AXIS_ALL:
                    bArg = (byte)'&';
                    break;
                default:
                    return (false);
            }
            iReturnValue = Control(bArg, bIsOn);
            return ((iReturnValue == 1) ? true : false);
        }
        public bool homeWrapped(enumAxis axisGroup)
        {
            byte bArg = (byte)'A';
            int iReturnValue;
            switch (axisGroup)
            {
                case enumAxis.AXIS_ROBOT:
                    bArg = (byte)'A';
                    break;
                case enumAxis.AXIS_PERIPHERALS:
                    bArg = (byte)'B';
                    break;
                case enumAxis.AXIS_ALL:
                    bArg = (byte)'&';
                    break;
                default:
                    return (false);
            }
            iReturnValue = Home((byte) axisGroup, callbackfuncHoming);
            return ((iReturnValue == 1) ? true : false);
        }

        // -Imported(ShouldUseWrappedVersions)
        [UnmanagedFunctionPointer( CallingConvention.Cdecl, CharSet = CharSet.Ansi )] /** \todo Wrap timer around */
        private delegate void dgateCallBack(IntPtr voidptrConfigData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate void dgateCallBackCharArg(Byte bArg);

        [DllImport("USBC.dll", EntryPoint = "?Initialization@@YAHFFP6AXPAX@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int initialization(short shrtMode, short shrtType, dgateCallBack funcprtCallBack, dgateCallBack funcptrCallBackError);

        [DllImport("USBC.dll", EntryPoint = "?SetParameterFolder@@YAHPAD@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int setParameterFolder(IntPtr charptrFolder);

        [DllImport("USBC.dll", EntryPoint = "?GetParameterFolder@@YAHPAD@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetParameterFolder(IntPtr charptrFolderBuffer);

        [DllImport("USBC.dll", EntryPoint = "?Control@@YAHEH@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Control(byte bAxis, bool bIsOn);

        [DllImport("USBC.dll", EntryPoint = "?Home@@YAHEP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Home(byte axis, dgateCallBackCharArg funcptrCallBack);
    }
}
