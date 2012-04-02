/** \file dllMock.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DSL
{
    public class DLLMock
    {
        // Extra classes
        /// <summary>
        /// Class to contain point for use in one of the vector classes.
        /// </summary>
        class VecPoint
        {
            public int iX;
            public int iY;
            public int iZ;
            public int iPitch;
            public int iRoll;
            public VecPoint(int _iX, int _iY, int _iZ, int _iPitch, int _iRoll)
            {
                iX = _iX;
                iY = _iY;
                iZ = _iZ;
                iPitch = _iPitch;
                iRoll = _iRoll;
            }
        }
        /// <summary>
        /// Base class for vector used in wrapper.
        /// 
        /// Should use the derived classes.
        /// </summary>
        class SIRVector
        {
            // Members
            protected string sName;
            protected List<VecPoint> lstPoints;
            protected int iType; // What kind of pointer

            // Functions
            public int getSize()
            {
                return (lstPoints.Count);
            }
            public void addPoint(VecPoint pNewVecPoint)
            {
                lstPoints.Add(pNewVecPoint);
            }
            public VecPoint getPoint(int iIndex)
            {
                return (lstPoints[iIndex]);
            }
            public string Name
            {
                get { return sName; }
            }
            public int Type
            {
                get { return iType; }
            }

        }

        /// <summary>
        /// SIRVector class for absolute positions.
        /// </summary>
        class AbsCoordSirVector : SIRVector
        {
            // Functions 
            public AbsCoordSirVector(string _sName)
            {
                sName = _sName;
                lstPoints = new List<VecPoint>();
                iType = -32766;
            }
        }

        /// <summary>
        /// SIRVector class for relative positions.
        /// </summary>
        class RelCoordSirVector : SIRVector
        {
            // Functions 
            public RelCoordSirVector(string _sName)
            {
                sName = _sName;
                lstPoints = new List<VecPoint>();
                iType = -32767;
            }
        }

        /// <summary>
        /// Contains a wrapper for the C++ functions in the dll file(USBC.dll).
        /// 
        /// Good idea to check USBC-documentation.pdf.
        /// 
        /// Notes:  Uses IntPtr arg for different types of C++ pointers.
        ///         Same function names as C++ but has "Wrapped" at the end.
        ///         Try to have handlers in delegates which in entire use of wrapper, so the memory for the handler doesn´t get removed by GC.
        /// 
        /// \todo Add behind factory class.
        /// </summary>

        // Settings constants
        // -Initialization mode
        /// <summary>
        /// For mode in initialization. 
        ///
        /// (MODE_ONLINE is normally used)
        /// </summary>
        public enum enumSystemModes
        {
            MODE_DEFAULT = 0, // Last used mode
            MODE_ONLINE = 1, // Force online mode(Normally used)
            MODE_SIMULAT = 2 // Simulator mode
        }
        // -Initialization system type
        /// <summary>
        /// For type in initialization.
        /// 
        /// (SYSTEM_TYPE_DEFAULT normally used)
        /// </summary>
        public enum enumSystemTypes
        {
            SYSTEM_TYPE_DEFAULT = 0, // Detect it(Normally used)
            SYSTEM_TYPE_ER4USB = 41 // ER-4
        }
        // -Axis control settings
        /// <summary>
        /// For chosing axis group in certain functions.
        /// </summary>
        public enum enumAxisSettings
        {
            AXIS_ROBOT,
            AXIS_PERIPHERALS,
            AXIS_GRIPPER,
            AXIS_0,
            AXIS_1,
            AXIS_2,
            AXIS_3,
            AXIS_4,
            AXIS_5,
            AXIS_6,
            AXIS_7,
            AXIS_ALL
        }
        // -Manual movement
        /// <summary>
        /// For chosing type of movement when enabling manual movement.
        /// </summary>
        public enum enumManualType
        {
            MANUAL_TYPE_AXES,
            MANUAL_TYPE_COORD
        }
        /// <summary>
        /// For chosing what part to move when moving manually.
        /// 
        /// Note: Some used for moving by axes and some used for moving by coordinates.
        /// </summary>
        public enum enumManualModeWhat
        {
            MANUAL_MOVE_BASE, // Axes
            MANUAL_MOVE_SHOULDER,
            MANUAL_MOVE_ELBOW,
            MANUAL_MOVE_WRISTPITCH,
            MANUAL_MOVE_WRISTROLL,
            MANUAL_MOVE_GRIPPER,
            MANUAL_MOVE_CONVEYERBELT,
            MANUAL_MOVE_X, // Coordinates
            MANUAL_MOVE_Y,
            MANUAL_MOVE_Z,
            MANUAL_MOVE_PITCH,
            MANUAL_MOVE_ROLL
        }

        // Functions
        // -Constructors and destructors

        private DLL _dll = null;

        public DLLMock(DLL testdll)
        {
            _dll = testdll;
        }

        public int testcase(int x)
        {
            int y = x;
            return y;
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

        public int WatchMotion(DLLImport.DgateCallBackCharArg funcptrCallbackEnd, DLLImport.DgateCallBackCharArg funcptrCallbackStart)
        {
            _dll.WatchMotion(funcptrCallbackEnd, funcptrCallbackStart);

            return 1;
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
