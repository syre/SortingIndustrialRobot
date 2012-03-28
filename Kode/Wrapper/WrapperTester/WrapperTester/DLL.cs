using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WrapperTester
{

    #region DLLWrapper

    public class DLL : IDLL
    {
        
        public int Initialization(short shrtMode, short shrtType, DLLImport.DgateCallBack funcprtCallBack, DLLImport.DgateCallBack funcptrCallBackError)
        {
            return DLLImport.initialization(shrtMode, shrtType, funcprtCallBack, funcptrCallBackError);
        }

        public int Control(byte bAxis, bool bIsOn)
        {
            return DLLImport.Control(bAxis, bIsOn);
        }

        public int Home(byte axis, DLLImport.DgateCallBackByteRefArg funcptrCallBack)
        {
            return DLLImport.Home(axis, funcptrCallBack);
        }

        public int OpenGripper()
        {
            return DLLImport.OpenGripper();
        }
        
        public int CloseGripper()
        {
            return DLLImport.CloseGripper();
        }
        
        public int GetJaw(ref short perc, ref short metric)
        {
            return DLLImport.GetJaw(ref perc, ref metric);
        }
    
        public int EnterManual(short shrtArg)
        {
            return DLLImport.EnterManual(shrtArg);
        }
      
        public int CloseManual()
        {
            return DLLImport.CloseManual();
        }
        
        public int MoveManual(byte bAxis, int lSpeed)
        {
            return DLLImport.MoveManual(bAxis, lSpeed);
        }
        
        public int Stop(byte axis)
        {
            return DLLImport.Stop(axis);
        }
        
        public DLLImport.DgateCallBackCharArg WatchMotion(DLLImport.DgateCallBackCharArg funcptrCallbackEnd, DLLImport.DgateCallBackCharArg funcptrCallbackStart)
        {
            return DLLImport.WatchMotion(funcptrCallbackEnd, funcptrCallbackStart);
        }

        public int WatchDigitalInput(DLLImport.DgateCallBackLongArg funcptrCallbackEvent)
        {
            return DLLImport.WatchDigitalInput(funcptrCallbackEvent);
        }
        
        public int CloseWatchDigitalInput()
        {
            return DLLImport.CloseWatchDigitalInput();
        }
      
        public int IsOnLineOk()
        {
            return DLLImport.IsOnLineOk();
        }
       
        public int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sSecondaryPos, short shrtPointToMoveTo)
        {
            return DLLImport.MoveLinear(sNameOfVectorThatGotPosition, shrtPointInVector, sSecondaryPos,
                                        shrtPointToMoveTo);
        }
    
        public int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector)
        {
            return DLLImport.DefineVector(bGroup, sVectorName, shrtSizeOfVector);
        }
      
        public int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType)
        {
            return DLLImport.Teach(sVectorName, shrtPoint, iaPointInfo, shrtSizeOfArray, iPointType);
        }

        public int GetCurrentPosition(ref int[] ibufEnc, ref int[] ibufJoint, ref int[] ibufXYZ)
        {
            return GetCurrentPosition(ref ibufEnc, ref ibufJoint, ref ibufXYZ);
        }
    }
    #endregion

    public class DLLImport
    {
        #region Imported references(Should use wrapped versions)
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
        public static extern int initialization(short shrtMode, short shrtType, DgateCallBack funcprtCallBack, DgateCallBack funcptrCallBackError);

        [DllImport("USBC.dll", EntryPoint = "?Control@@YAHEH@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Control(byte bAxis, bool bIsOn);

        [DllImport("USBC.dll", EntryPoint = "?Home@@YAHEP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Home(byte axis, DgateCallBackByteRefArg funcptrCallBack);

        [DllImport("USBC.dll", EntryPoint = "?OpenGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenGripper();

        [DllImport("USBC.dll", EntryPoint = "?CloseGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseGripper();

        [DllImport("USBC.dll", EntryPoint = "?GetJaw@@YAHPAF0@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetJaw(ref short perc, ref short metric);

        [DllImport("USBC.dll", EntryPoint = "?EnterManual@@YAHF@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int EnterManual(short shrtArg);

        [DllImport("USBC.dll", EntryPoint = "?CloseManual@@YAHXZ", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int CloseManual();

        [DllImport("USBC.dll", EntryPoint = "?MoveManual@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)] // In C++ speed is long
        public static extern int MoveManual(byte bAxis, int lSpeed);

        [DllImport("USBC.dll", EntryPoint = "?Stop@@YAHE@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Stop(byte axis);

        [DllImport("USBC.dll", EntryPoint = "?WatchMotion@@YAP6AXPAX@ZP6AX0@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern DgateCallBackCharArg WatchMotion(DgateCallBackCharArg funcptrCallbackEnd, DgateCallBackCharArg funcptrCallbackStart);

        [DllImport("USBC.dll", EntryPoint = "?WatchDigitalInp@@YAHP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WatchDigitalInput(DgateCallBackLongArg funcptrCallbackEvent);

        [DllImport("USBC.dll", EntryPoint = "?CloseWatchDigitalInp@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseWatchDigitalInput();

        [DllImport("USBC.dll", EntryPoint = "?IsOnLineOk@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsOnLineOk();

        [DllImport("USBC.dll", EntryPoint = "?MoveLinear@@YAHPADF0F@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sSecondaryPos, short shrtPointToMoveTo);

        [DllImport("USBC.dll", EntryPoint = "?DefineVector@@YAHEPADF@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector);

        [DllImport("USBC.dll", EntryPoint = "?Teach@@YAHPADFPAJFJ@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType); // long types used in C++ functions.

        [DllImport("USBC.dll", EntryPoint = "?GetCurrentPosition@@YAHPAY07J00@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCurrentPosition(ref int[] ibufEnc, ref int[] ibufJoint, ref int[] ibufXYZ);
        #endregion
    }
}
