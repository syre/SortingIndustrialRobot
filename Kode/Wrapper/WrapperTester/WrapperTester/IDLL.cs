using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperTester
{
    public interface IDLL
    {
        #region Interfaced DLL's
        
        // -Robot functions
        int initialization(DLL.DgateCallBack funcprtCallBack, DLL.DgateCallBack funcptrCallBackError);

        int Control(byte bAxis, bool bIsOn);

        int Home(byte axis, DLL.DgateCallBackByteRefArg funcptrCallBack);

        int OpenGripper();

        int CloseGripper();

        int GetJaw(ref short perc, ref short metric);

        int EnterManual(short shrtArg);

        int CloseManual();

        int MoveManual(byte bAxis, int lSpeed);

        int Stop(byte axis);

        DLL.DgateCallBackCharArg WatchMotion(DLL.DgateCallBackCharArg funcptrCallbackEnd, DLL.DgateCallBackCharArg funcptrCallbackStart);

        int WatchDigitalInput(DLL.DgateCallBackLongArg funcptrCallbackEvent);

        int CloseWatchDigitalInput();

        int IsOnLineOk();

        int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sSecondaryPos, short shrtPointToMoveTo);

        int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector);

        int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType); // long types used in C++ functions.

        int GetCurrentPosition(ref int[] ibufEnc, ref int[] ibufJoint, ref int[] ibufXYZ);
        
        #endregion

    }
}
