/** \file dll.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Runtime.InteropServices;

namespace ControlSystem
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
        
        public DgateCallBackByteRefArg WatchMotion(DgateCallBackByteRefArg _funcptrCallbackEnd, DgateCallBackByteRefArg _funcptrCallbackStart)
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
       
        public int MoveLinear(string _sNameOfVectorThatGotPosition, short _shrtPointInVector, string _sSecondaryPos, short _shrtPointToMoveTo)
        {
            return DLLImport.MoveLinear(_sNameOfVectorThatGotPosition, _shrtPointInVector, _sSecondaryPos, _shrtPointToMoveTo);
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
            return DLLImport.GetCurrentPosition(_ibufEnc, _ibufJoint, _ibufXYZ);
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

    /// <summary>
    /// Class providing interface for USBC.dll functions.
    /// 
    /// Note: Please use the Wrapper instead.
    /// </summary>
    public class DLLImport
    {
        // Constants
        private const string sDllFileLocation = @"USBC.dll";

        // -Robot functions
        /// <summary>
        /// Initialize the robot.
        /// </summary>
        /// <param name="shrtMode">Mode of the robot.</param>
        /// <param name="shrtType">Type of connection.</param>
        /// <param name="funcptrCallBack">Function to call when initialized.</param>
        /// <param name="funcptrCallBackError">Function to call when errors happen.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?Initialization@@YAHFFP6AXPAX@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initialization(short shrtMode, short shrtType, DLL.DgateCallBack funcptrCallBack, DLL.DgateCallBack funcptrCallBackError);

        /// <summary>
        /// Turn control on/off for axis group.
        /// </summary>
        /// <param name="bAxis">Axis to apply setting.</param>
        /// <param name="bIsOn">On or off.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?Control@@YAHEH@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Control(byte bAxis, bool bIsOn);

        /// <summary>
        /// Home the axis group or the whole robot.
        /// </summary>
        /// <param name="axis">Axis to home.</param>
        /// <param name="funcptrCallBack">Function to call when homing axis.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport("USBC.dll", EntryPoint = "?Home@@YAHEP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Home(byte axis, DLL.DgateCallBackByteRefArg funcptrCallBack);

        /// <summary>
        /// Opens the gripper.
        /// </summary>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?OpenGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenGripper();

        /// <summary>
        /// Closes the gripper.
        /// </summary>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?CloseGripper@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseGripper();

        /// <summary>
        /// Get values for how open the gripper are.
        /// </summary>
        /// <param name="perc">In percentage.</param>
        /// <param name="metric">In metric value.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?GetJaw@@YAHPAF0@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetJaw(ref short perc, ref short metric);

        /// <summary>
        /// Enter manual steering mode.
        /// </summary>
        /// <param name="shrtArg">Type: Axis(0) or by coordinates(1).</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?EnterManual@@YAHF@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int EnterManual(short shrtArg);

        /// <summary>
        /// Stops manual steering mode.
        /// </summary>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?CloseManual@@YAHXZ", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int CloseManual();

        /// <summary>
        /// Move an axis group.
        /// 
        /// Note: Call Control and EnterManual first.
        /// </summary>
        /// <param name="bAxis">What axis to move.</param>
        /// <param name="lSpeed">Speed to move in. Negative value for opposite direction.(This value is a percentage of max speed.)</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?MoveManual@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)] // In C++ speed is long
        public static extern int MoveManual(byte bAxis, int lSpeed);

        /// <summary>
        /// Stop movement of an axis group.
        /// </summary>
        /// <param name="axis">What axis to stop.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?Stop@@YAHE@Z", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Stop(byte axis);

        /// <summary>
        /// Adds callback for robot movement functions.
        /// </summary>
        /// <param name="funcptrCallbackEnd">Function to call when movement has ended.</param>
        /// <param name="funcptrCallbackStart">Function to call when movement starts.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?WatchMotion@@YAP6AXPAX@ZP6AX0@Z1@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern DLL.DgateCallBackByteRefArg WatchMotion(DLL.DgateCallBackByteRefArg funcptrCallbackEnd, DLL.DgateCallBackByteRefArg funcptrCallbackStart);

        /// <summary>
        /// Adds callback for digital input signal.
        /// </summary>
        /// <param name="funcptrCallbackEvent">Function to call when signal.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?WatchDigitalInp@@YAHP6AXPAX@Z@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WatchDigitalInput(DLL.DgateCallBackLongArg funcptrCallbackEvent);

        /// <summary>
        /// Stops watching for digital input.
        /// </summary>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?CloseWatchDigitalInp@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseWatchDigitalInput();

        /// <summary>
        /// Cehcks for being online.
        /// </summary>
        /// <returns>Online(1)/offline(0):</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?IsOnLineOk@@YAHXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IsOnLineOk();

        /// <summary>
        /// Moves the robot towards a position and then another.
        /// </summary>
        /// <param name="sNameOfVectorThatGotPosition">Vector with first position.</param>
        /// <param name="shrtPointInVector">What point in the vector to move to.(Index.)</param>
        /// <param name="sSecondaryPos">Vector with second position</param>
        /// <param name="shrtPointToMoveTo">What point in the vector to move to.(Index.)</param>
        /// <returns>True on successfull call.</returns>
        //[DllImport(sDllFileLocation, EntryPoint = "?MoveLinear@@YAHPADF0F@Z", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotFirstPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotSecondPosition, short shrtPointToMoveTo);
        [DllImport(sDllFileLocation, EntryPoint = "?MoveLinear@@YAHPADF0F@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int MoveLinear(string sNameOfVectorThatGotFirstPosition, short shrtPointInVector, string sNameOfVectorThatGotSecondPosition, short shrtPointToMoveTo);
        /// <summary>
        /// Define a new vector in the robot.
        /// </summary>
        /// <param name="bGroup">Group for the vector type.('A' robot, '&' all axes, 'B' peripherals.)</param>
        /// <param name="sVectorName">Name of vector.</param>
        /// <param name="shrtSizeOfVector">Size of vector.(Number of points.)</param>
        /// <returns>True on successfull call.</returns>
        //[DllImport(sDllFileLocation, EntryPoint = "?DefineVector@@YAHEPADF@Z", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector);
        [DllImport(sDllFileLocation, EntryPoint = "?DefineVector@@YAHEPADF@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DefineVector(byte bGroup, string sVectorName, short shrtSizeOfVector);
        /// <summary>
        /// Saves a point in the vector.
        /// </summary>
        /// <param name="sVectorName">Name of vector.</param>
        /// <param name="shrtPoint">What point.(Index.)</param>
        /// <param name="iaPointInfo">Point values.</param>
        /// <param name="shrtSizeOfArray">Size of point values.(How many values.)</param>
        /// <param name="iPointType">What kind of point.(Relative(-32767) or Absolute(-32766).)</param>
        /// <returns>True on successfull call.</returns>
        //[DllImport(sDllFileLocation, EntryPoint = "?Teach@@YAHPADFPAJFJ@Z", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType); // long types used in C++ functions.
        [DllImport(sDllFileLocation, EntryPoint = "?Teach@@YAHPADFPAJFJ@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Teach(string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, long iPointType); // long types used in C++ functions.
        /// <summary>
        /// Get the current position.
        /// </summary>
        /// <param name="ibufEnc">Buffer to save values.</param>
        /// <param name="ibufJoint">Buffer to save values.</param>
        /// <param name="ibufXYZ">Buffer to save values.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?GetCurrentPosition@@YAHPAY07J00@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCurrentPosition( int[] ibufEnc, int[] ibufJoint, int[] ibufXYZ);

        /// <summary>
        /// Sets the value for how long movement should take place.(Manual steering.)
        /// </summary>
        /// <param name="_bGroup">What axis group.</param>
        /// <param name="_mTime">Time in milliseconds.</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?Time@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Time(byte _bGroup, long _mTime);

        /// <summary>
        /// Sets the speed of movement.
        /// </summary>
        /// <param name="_bGroup">What axis group.</param>
        /// <param name="_mSpeed">Speed of movement.(Value in percentage.)</param>
        /// <returns>True on successfull call.</returns>
        [DllImport(sDllFileLocation, EntryPoint = "?Speed@@YAHEJ@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Speed(byte _bGroup, long _mSpeed);
        #endregion
    }
}
