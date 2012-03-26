using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WrapperTester
{
    public class DLL
    {
        #region DLL
        // -Function pointers
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBack(IntPtr voidptrConfigData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBackCharArg(Byte bArg);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBackLongArg(long lArg); /// \warning Using long.

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate void DgateCallBackByteRefArg(ref byte bArg);

        // -Robot functions
        [DllImport("USBC.dll", EntryPoint = "?Initialization@@YAHFFP6AXPAX@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int initialization(short shrtMode, short shrtType, DgateCallBack funcprtCallBack, DgateCallBack funcptrCallBackError);

        [DllImport("USBC.dll", EntryPoint = "?Control@@YAHEH@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Control(byte bAxis, bool bIsOn);

        [DllImport("USBC.dll", EntryPoint = "?Home@@YAHEP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Home(byte axis, DgateCallBackByteRefArg funcptrCallBack);

        [DllImport("USBC.dll", EntryPoint = "?OpenGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        private static extern int OpenGripper();

        [DllImport("USBC.dll", EntryPoint = "?CloseGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CloseGripper();

        [DllImport("USBC.dll", EntryPoint = "?GetJaw@@YAHPAF0@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetJaw(ref short perc, ref short metric);

        [DllImport("USBC.dll", EntryPoint = "?EnterManual@@YAHF@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int EnterManual(short shrtArg);

        [DllImport("USBC.dll", EntryPoint = "?CloseManual@@YAHXZ", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int CloseManual();

        [DllImport("USBC.dll", EntryPoint = "?MoveManual@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)] // In C++ speed is long
        private static extern int MoveManual(byte bAxis, int lSpeed);

        [DllImport("USBC.dll", EntryPoint = "?Stop@@YAHE@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int Stop(byte axis);

        [DllImport("USBC.dll", EntryPoint = "?WatchMotion@@YAP6AXPAX@ZP6AX0@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern DgateCallBackCharArg WatchMotion(DgateCallBackCharArg funcptrCallbackEnd, DgateCallBackCharArg funcptrCallbackStart);

        [DllImport("USBC.dll", EntryPoint = "?WatchDigitalInp@@YAHP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WatchDigitalInput(DgateCallBackLongArg funcptrCallbackEvent);

        [DllImport("USBC.dll", EntryPoint = "?CloseWatchDigitalInp@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CloseWatchDigitalInput();

        [DllImport("USBC.dll", EntryPoint = "?IsOnLineOk@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        private static extern int IsOnLineOk();

        [DllImport("USBC.dll", EntryPoint = "?MoveLinear@@YAHPADF0F@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sSecondaryPos, short shrtPointToMoveTo);

        [DllImport("USBC.dll", EntryPoint = "?DefineVector@@YAHEPADF@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector);

        [DllImport("USBC.dll", EntryPoint = "?Teach@@YAHPADFPAJFJ@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType); // long types used in C++ functions.

        [DllImport("USBC.dll", EntryPoint = "?GetCurrentPosition@@YAHPAY07J00@Z", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetCurrentPosition(ref int[] ibufEnc, ref int[] ibufJoint, ref int[] ibufXYZ);
        #endregion
    }
}
