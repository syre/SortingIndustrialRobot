using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WrapperTester
{
    public interface IDLL
    {
        #region Interfaced DLL's
        
        // -Robot functions
        int Initialization(short shrtMode, short shrtType, DLLImport.DgateCallBack funcprtCallBack, DLLImport.DgateCallBack funcptrCallBackError);

        int Control(byte bAxis, bool bIsOn);

        int Home(byte axis, DLLImport.DgateCallBackByteRefArg funcptrCallBack);

        int OpenGripper();

        int CloseGripper();

        int GetJaw(ref short perc, ref short metric);

        int EnterManual(short shrtArg);

        int CloseManual();

        int MoveManual(byte bAxis, int lSpeed);

        int Stop(byte axis);

        DLLImport.DgateCallBackCharArg WatchMotion(DLLImport.DgateCallBackCharArg funcptrCallbackEnd, DLLImport.DgateCallBackCharArg funcptrCallbackStart);

        int WatchDigitalInput(DLLImport.DgateCallBackLongArg funcptrCallbackEvent);

        int CloseWatchDigitalInput();

        int IsOnLineOk();

        int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sSecondaryPos, short shrtPointToMoveTo);

        int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector);

        int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType); // long types used in C++ functions.

        int GetCurrentPosition(ref int[] ibufEnc, ref int[] ibufJoint, ref int[] ibufXYZ);
        
        #endregion

    }
}
