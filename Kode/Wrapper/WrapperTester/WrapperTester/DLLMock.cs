using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperTester
{
    public class DLLMock
    {

        private DLL _dll = null;

        public DLLMock(DLL testdll)
        {
            _dll = testdll;
        }
 
        public int Initialization(short shrtMode, short shrtType, DLLImport.DgateCallBack funcprtCallBack, DLLImport.DgateCallBack funcptrCallBackError)
        {
            _dll.Initialization(shrtMode, shrtType, funcprtCallBack, funcptrCallBackError);
            return 1;
        }

        public int Control(byte bAxis, bool bIsOn)
        {
            _dll.Control(bAxis, bIsOn);
            return 1;
        }

        public int Home(byte axis, DLLImport.DgateCallBackByteRefArg funcptrCallBack)
        {
            _dll.Home(axis, funcptrCallBack);
            return 1;
        }

        public int OpenGripper()
        {
            _dll.OpenGripper();
            return 1;
        }

        public int CloseGripper()
        {
            _dll.CloseGripper();
            return 1;
        }

        public int GetJaw(ref short perc, ref short metric)
        {
            _dll.GetJaw(ref perc, ref metric);
            return 1;
        }

        public int EnterManual(short shrtArg)
        {
            _dll.EnterManual(shrtArg);
            return 1;
        }

        public int CloseManual()
        {
            _dll.CloseManual();
            return 1;
        }

        public int MoveManual(byte bAxis, int lSpeed)
        {
            _dll.MoveManual(bAxis, lSpeed);
            return 1;
        }

        public int Stop(byte axis)
        {
            _dll.Stop(axis);
            return 1;
        }

        public DLLImport.DgateCallBackCharArg WatchMotion(DLLImport.DgateCallBackCharArg funcptrCallbackEnd, DLLImport.DgateCallBackCharArg funcptrCallbackStart)
        {
            return _dll.WatchMotion(funcptrCallbackEnd, funcptrCallbackStart);

        }

        public int WatchDigitalInput(DLLImport.DgateCallBackLongArg funcptrCallbackEvent)
        {
            _dll.WatchDigitalInput(funcptrCallbackEvent);
            return 1;
        }

        public int CloseWatchDigitalInput()
        {
            _dll.CloseWatchDigitalInput();
            return 1;
        }

        public int IsOnLineOk()
        {
            _dll.IsOnLineOk();
            return 1;
        }

        public int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string sNameOfVectorThatGotPosition, short shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string sSecondaryPos, short shrtPointToMoveTo)
        {
            _dll.MoveLinear(sNameOfVectorThatGotPosition, shrtPointInVector, sSecondaryPos,
                                       shrtPointToMoveTo);
            return 1;
        }

        public int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtSizeOfVector)
        {
            _dll.DefineVector(bGroup, sVectorName, shrtSizeOfVector);
            return 1;
        }

        public int Teach([MarshalAs(UnmanagedType.LPStr)] string sVectorName, short shrtPoint, int[] iaPointInfo, short shrtSizeOfArray, int iPointType)
        {
            _dll.Teach(sVectorName, shrtPoint, iaPointInfo, shrtSizeOfArray, iPointType);
            return 1;
        }

        public int GetCurrentPosition(ref int[] ibufEnc, ref int[] ibufJoint, ref int[] ibufXYZ)
        {
            GetCurrentPosition(ref ibufEnc, ref ibufJoint, ref ibufXYZ);
            return 1;
        }
    }
}
