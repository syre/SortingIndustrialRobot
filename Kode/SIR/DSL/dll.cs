/** \file dll.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Runtime.InteropServices;

namespace DSL
{
    #region DLLWrapper
    /// <summary>
    /// Class that with the IDLL interface calls the actual functions from the USBC.dll.
    /// 
    /// Note: Uses static imports from DLLImport.
    /// </summary>
    public class DLL : IDLL
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

        public int Initialization(short _shrtMode, short _shrtType, DgateCallBack _funcprtCallBack, DgateCallBack _funcptrCallBackError)
        {
            return DLLImport.initialization(_shrtMode, _shrtType, _funcprtCallBack, _funcptrCallBackError);
        }

        public int Control(byte _bAxis, bool _bIsOn)
        {
            return DLLImport.Control(_bAxis, _bIsOn);
        }

        public int Home(byte _axis, DgateCallBackByteRefArg _funcptrCallBack)
        {
            return DLLImport.Home(_axis, _funcptrCallBack);
        }

        public int OpenGripper()
        {
            return DLLImport.OpenGripper();
        }
        
        public int CloseGripper()
        {
            return DLLImport.CloseGripper();
        }
        
        public int GetJaw(ref short _perc, ref short _metric)
        {
            return DLLImport.GetJaw(ref _perc, ref _metric);
        }
    
        public int EnterManual(short _shrtArg)
        {
            return DLLImport.EnterManual(_shrtArg);
        }
      
        public int CloseManual()
        {
            return DLLImport.CloseManual();
        }
        
        public int MoveManual(byte _bAxis, int _lSpeed)
        {
            return DLLImport.MoveManual(_bAxis, _lSpeed);
        }
        
        public int Stop(byte _axis)
        {
            return DLLImport.Stop(_axis);
        }
        
        public DgateCallBackCharArg WatchMotion(DgateCallBackCharArg _funcptrCallbackEnd, DgateCallBackCharArg _funcptrCallbackStart)
        {
            return DLLImport.WatchMotion(_funcptrCallbackEnd, _funcptrCallbackStart);
        }

        public int WatchDigitalInput(DgateCallBackLongArg _funcptrCallbackEvent)
        {
            return DLLImport.WatchDigitalInput(_funcptrCallbackEvent);
        }
        
        public int CloseWatchDigitalInput()
        {
            return DLLImport.CloseWatchDigitalInput();
        }
      
        public int IsOnLineOk()
        {
            return DLLImport.IsOnLineOk();
        }
       
        public int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string _sNameOfVectorThatGotPosition, short _shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string _sSecondaryPos, short _shrtPointToMoveTo)
        {
            return DLLImport.MoveLinear(_sNameOfVectorThatGotPosition, _shrtPointInVector, _sSecondaryPos,
                                        _shrtPointToMoveTo);
        }
    
        public int DefineVector(byte _bGroup, [MarshalAs(UnmanagedType.LPStr)] string _sVectorName, short _shrtSizeOfVector)
        {
            return DLLImport.DefineVector(_bGroup, _sVectorName, _shrtSizeOfVector);
        }
      
        public int Teach([MarshalAs(UnmanagedType.LPStr)] string _sVectorName, short _shrtPoint, int[] _iaPointInfo, short _shrtSizeOfArray, int _iPointType)
        {
            return DLLImport.Teach(_sVectorName, _shrtPoint, _iaPointInfo, _shrtSizeOfArray, _iPointType);
        }

        public int GetCurrentPosition(ref int[] _ibufEnc, ref int[] _ibufJoint, ref int[] _ibufXYZ)
        {
            return DLLImport.GetCurrentPosition(ref _ibufEnc, ref _ibufJoint, ref _ibufXYZ);
        }

        public int Time(byte _bGroup, long _mTime)
        {
            return DLLImport.Time(_bGroup, _mTime);
        }

        public int Speed(byte _bGroup, long mSpeed)
        {
            return DLLImport.Speed(_bGroup, mSpeed);
        }
    }
    #endregion

    public class DLLImport
    {
        // Constants
        private const string sDllFileLocation = @"USBC.dll";

        // -Robot functions
        [DllImport(sDllFileLocation, EntryPoint = "?Initialization@@YAHFFP6AXPAX@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initialization(short shrtMode, short shrtType, DLL.DgateCallBack funcptrCallBack, DLL.DgateCallBack funcptrCallBackError);

        [DllImport(sDllFileLocation, EntryPoint = "?Control@@YAHEH@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Control(byte bAxis, bool bIsOn);

        [DllImport("USBC.dll", EntryPoint = "?Home@@YAHEP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Home(byte axis, DLL.DgateCallBackByteRefArg funcptrCallBack);

        [DllImport(sDllFileLocation, EntryPoint = "?OpenGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenGripper();

        [DllImport(sDllFileLocation, EntryPoint = "?CloseGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseGripper();

        [DllImport(sDllFileLocation, EntryPoint = "?GetJaw@@YAHPAF0@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetJaw(ref short perc, ref short metric);

        [DllImport(sDllFileLocation, EntryPoint = "?EnterManual@@YAHF@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int EnterManual(short shrtArg);

        [DllImport(sDllFileLocation, EntryPoint = "?CloseManual@@YAHXZ", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int CloseManual();

        [DllImport(sDllFileLocation, EntryPoint = "?MoveManual@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)] // In C++ speed is long
        public static extern int MoveManual(byte bAxis, int lSpeed);

        [DllImport(sDllFileLocation, EntryPoint = "?Stop@@YAHE@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Stop(byte axis);

        [DllImport(sDllFileLocation, EntryPoint = "?WatchMotion@@YAP6AXPAX@ZP6AX0@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern DLL.DgateCallBackCharArg WatchMotion(DLL.DgateCallBackCharArg funcptrCallbackEnd, DLL.DgateCallBackCharArg funcptrCallbackStart);

        [DllImport(sDllFileLocation, EntryPoint = "?WatchDigitalInp@@YAHP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WatchDigitalInput(DLL.DgateCallBackLongArg funcptrCallbackEvent);

        [DllImport(sDllFileLocation, EntryPoint = "?CloseWatchDigitalInp@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseWatchDigitalInput();

        [DllImport(sDllFileLocation, EntryPoint = "?IsOnLineOk@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsOnLineOk();

        [DllImport(sDllFileLocation, EntryPoint = "?MoveLinear@@YAHPADF0F@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sSecondaryPos, short shrtPointToMoveTo);

        [DllImport(sDllFileLocation, EntryPoint = "?DefineVector@@YAHEPADF@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector);

        [DllImport(sDllFileLocation, EntryPoint = "?Teach@@YAHPADFPAJFJ@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType); // long types used in C++ functions.

        [DllImport(sDllFileLocation, EntryPoint = "?GetCurrentPosition@@YAHPAY07J00@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCurrentPosition(ref int[] ibufEnc, ref int[] ibufJoint, ref int[] ibufXYZ);

        [DllImport(sDllFileLocation, EntryPoint = "?Time@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Time(byte _bGroup, long _mTime);

        [DllImport(sDllFileLocation, EntryPoint = "?Speed@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Speed(byte _bGroup, long _mSpeed);


    #endregion
    }
}
