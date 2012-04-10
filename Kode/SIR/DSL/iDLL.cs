/** \file iDLL.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DSL
{
    /// <summary>
    /// Interface for the functions in the USBC.dll files, in C# format.
    /// </summary>
    public interface IDLL
    {
        #region Interfaced DLL's

        // -Robot functions
        int Initialization(short _shrtMode, short _shrtType, DLL.DgateCallBack _funcprtCallBack, DLL.DgateCallBack _funcptrCallBackError);

        int Control(byte _bAxis, bool _bIsOn);

        int Home(byte _axis, DLL.DgateCallBackByteRefArg _funcptrCallBack);

        int OpenGripper();

        int CloseGripper();

        int GetJaw(ref short _perc, ref short _metric);

        int EnterManual(short _shrtArg);

        int CloseManual();

        int MoveManual(byte _bAxis, int _lSpeed);

        int Stop(byte _axis);

        DLL.DgateCallBackCharArg WatchMotion(DLL.DgateCallBackCharArg _funcptrCallbackEnd, DLL.DgateCallBackCharArg _funcptrCallbackStart);

        int WatchDigitalInput(DLL.DgateCallBackLongArg _funcptrCallbackEvent);

        int CloseWatchDigitalInput();

        int IsOnLineOk();

        int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string _sNameOfVectorThatGotPosition, short _shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string _sSecondaryPos, short _shrtPointToMoveTo);

        int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string _sVectorName, short _shrtSizeOfVector);

        int Teach([MarshalAs(UnmanagedType.LPStr)] string _sVectorName, short _shrtPoint, int[] _iaPointInfo, short _shrtSizeOfArray, int _iPointType); // long types used in C++ functions.

        int GetCurrentPosition(ref int[] _ibufEnc, ref int[] _ibufJoint, ref int[] _ibufXYZ);
        
        #endregion

    }
}
