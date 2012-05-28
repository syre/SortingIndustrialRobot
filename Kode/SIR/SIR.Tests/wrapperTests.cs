using System;
using ControlSystem;
using NUnit.Framework;
using Rhino.Mocks;

namespace SIR.Tests
{
    [TestFixture]
    public class WrapperTests
    {
        // For parameter passing
        static void initSuccess(IntPtr _iptrArg)
        {
            
        }
        static void initError(IntPtr _iptrArg)
        {
           
        }
        static void homeEvent(ref byte _bArg)
        {
           
        }
        static void watchStart(byte _bArg)
        {
            
        }
        static void watchEnd(byte _bArg)
        {

        }
        static void watchDig(long _lArg)
        {
            
        }

        // Tests
        #region Constructor(Singleton)
        [Test]
        public void Wrapper_IsCalled_DLLPropertyIsNotNull()
        {
            // Test
            Wrapper wTestObj = Wrapper.getInstance();

            // Verify
            Assert.IsNotNull(wTestObj.DLL);
        }
        #endregion

        #region Properties
        [Test]
        public void DLL_SetsValue_IsSaved()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllStub = MockRepository.GenerateStub<IDLL>();

            // Test
            wTestObj.DLL = idllStub;

            // Verify
            Assert.AreSame(wTestObj.DLL, idllStub);
        }
        #endregion

        #region Initialization and settings
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitialization()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything)).Return(1);

            // Test
            wTestObj.initializationWrapped(Wrapper.enumSystemModes.MODE_DEFAULT, Wrapper.enumSystemTypes.SYSTEM_TYPE_ER4USB, initSuccess, initError);

            // Verify
            idllMock.AssertWasCalled(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything,Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything));
        }
        [Test]
        public void initializationWrapped_DllInitializationReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.initializationWrapped(Wrapper.enumSystemModes.MODE_DEFAULT, Wrapper.enumSystemTypes.SYSTEM_TYPE_ER4USB, initSuccess, initError);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void initializationWrapped_DllInitializationReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything)).Return(0);

            // Test
            bool bReturnValue = wTestObj.initializationWrapped(Wrapper.enumSystemModes.MODE_DEFAULT, Wrapper.enumSystemTypes.SYSTEM_TYPE_ER4USB, initSuccess, initError);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitializationWithRightSystemModeParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything)).Return(1);

            // Test
            wTestObj.initializationWrapped(Wrapper.enumSystemModes.MODE_DEFAULT, Wrapper.enumSystemTypes.SYSTEM_TYPE_ER4USB, initSuccess, initError);

            // Verify
            idllMock.AssertWasCalled(t => t.Initialization(Arg<short>.Is.Equal(Wrapper.enumSystemModes.MODE_DEFAULT), Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything));
        }
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitializationWithRightSystemTypeParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything)).Return(1);

            // Test
            wTestObj.initializationWrapped(Wrapper.enumSystemModes.MODE_DEFAULT, Wrapper.enumSystemTypes.SYSTEM_TYPE_ER4USB, initSuccess, initError);

            // Verify
            idllMock.AssertWasCalled(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Equal(Wrapper.enumSystemTypes.SYSTEM_TYPE_ER4USB), Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything));
        }
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitializationWithRightSuccessCallBackParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything)).Return(1);
            DLL.DgateCallBack dgSuccess = initSuccess;

            // Test
            wTestObj.initializationWrapped(Wrapper.enumSystemModes.MODE_DEFAULT, Wrapper.enumSystemTypes.SYSTEM_TYPE_ER4USB, initSuccess, initError);

            // Verify
            idllMock.AssertWasCalled(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Equal(dgSuccess), Arg<DLL.DgateCallBack>.Is.Anything));
        }
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitializationWithRightErrorCallBackParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything)).Return(1);
            DLL.DgateCallBack dgError = initError;

            // Test
            wTestObj.initializationWrapped(Wrapper.enumSystemModes.MODE_DEFAULT, Wrapper.enumSystemTypes.SYSTEM_TYPE_ER4USB, initSuccess, initError);

            // Verify
            idllMock.AssertWasCalled(t => t.Initialization(Arg<short>.Is.Anything, Arg<short>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Anything, Arg<DLL.DgateCallBack>.Is.Equal(dgError)));
        }
        [Test]
        public void controlWrapped_IsCalled_CallsDllControl()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Control(Arg<byte>.Is.Anything, Arg<bool>.Is.Anything)).Return(1);

            // Test
            wTestObj.controlWrapped(Wrapper.enumAxisSettings.AXIS_1, true);

            // Verify
            idllMock.AssertWasCalled(t => t.Control(Arg<byte>.Is.Anything, Arg<bool>.Is.Anything));
        }
        [Test]
        public void controlWrapped_DllControlReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Control(Arg<byte>.Is.Anything, Arg<bool>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.controlWrapped(Wrapper.enumAxisSettings.AXIS_1, true);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void controlWrapped_DllControlReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Control(Arg<byte>.Is.Anything, Arg<bool>.Is.Anything)).Return(0);

            // Test
            bool bReturnValue = wTestObj.controlWrapped(Wrapper.enumAxisSettings.AXIS_1, true);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void controlWrapped_IsCalled_CallsDllWithRightAxisSettingsParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Control(Arg<byte>.Is.Anything, Arg<bool>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, true);

            // Verify
            idllMock.AssertWasCalled(t => t.Control(Arg<byte>.Is.Equal('A'), Arg<bool>.Is.Anything));
        }
        [Test]
        public void controlWrapped_IsCalled_CallsDllWithRightControlParam()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Control(Arg<byte>.Is.Anything, Arg<bool>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.controlWrapped(Wrapper.enumAxisSettings.AXIS_1, true);

            // Verify
            idllMock.AssertWasCalled(t => t.Control(Arg<byte>.Is.Anything, Arg<bool>.Is.Equal(true)));
        }
        [Test]
        public void isOnlineOkWrapped_IsCalled_CallsDllIsOnLineOk()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.IsOnLineOk()).Return(1);

            // Test
            wTestObj.isOnlineOkWrapped();

            // Verify
            idllMock.AssertWasCalled(t => t.IsOnLineOk());
        }
        [Test]
        public void isOnlineOkWrapped_DllIsOnLineOkReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.IsOnLineOk()).Return(0);

            // Test
            bool bReturnValue = wTestObj.isOnlineOkWrapped();

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void isOnlineOkWrapped_DllIsOnLineOkReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.IsOnLineOk()).Return(1);

            // Test
            bool bReturnValue = wTestObj.isOnlineOkWrapped();

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void timeWrapped_IsCalled_CallsDllTime()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Time(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(1);

            // Test
            wTestObj.timeWrapped(Wrapper.enumAxisSettings.AXIS_4, 1000);

            // Verify
            idllMock.AssertWasCalled(t => t.Time(Arg<byte>.Is.Anything, Arg<long>.Is.Anything));
        }
        [Test]
        public void timeWrapped_DllTimeReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Time(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(0);

            // Test
            bool bReturnValue = wTestObj.timeWrapped(Wrapper.enumAxisSettings.AXIS_4, 1000);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void timeWrapped_DllTimeReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Time(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(1);

            // Test
            bool bReturnValue = wTestObj.timeWrapped(Wrapper.enumAxisSettings.AXIS_4, 1000);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void timeWrapped_IsCalled_CallsDllTimeWithRightGroupParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Time(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(1);

            // Test
            wTestObj.timeWrapped(Wrapper.enumAxisSettings.AXIS_5, 1000);

            // Verify
            idllMock.AssertWasCalled(t => t.Time(Arg<byte>.Is.Equal(5), Arg<long>.Is.Anything));
        }
        [Test]
        public void timeWrapped_IsCalled_CallsDllTimeWithRightTimeParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Time(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(1);

            // Test
            wTestObj.timeWrapped(Wrapper.enumAxisSettings.AXIS_4, 1000);

            // Verify
            idllMock.AssertWasCalled(t => t.Time(Arg<byte>.Is.Anything, Arg<long>.Is.Equal(1000)));
        }
        [Test]
        public void speedWrapped_IsCalled_CallsDllSpeed()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Speed(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(1);

            // Test
            wTestObj.speedWrapped(Wrapper.enumAxisSettings.AXIS_4, 1000);

            // Verify
            idllMock.AssertWasCalled(t => t.Speed(Arg<byte>.Is.Anything, Arg<long>.Is.Anything));
        }
        [Test]
        public void speedWrapped_DllSpeedReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Speed(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(0);

            // Test
            bool bReturnValue = wTestObj.speedWrapped(Wrapper.enumAxisSettings.AXIS_4, 1000);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void speedWrapped_DllSpeedReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Speed(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(1);

            // Test
            bool bReturnValue = wTestObj.speedWrapped(Wrapper.enumAxisSettings.AXIS_4, 1000);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void speedWrapped_IsCalled_CallsDllSpeedWithRightGroupParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Speed(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(1);

            // Test
            wTestObj.speedWrapped(Wrapper.enumAxisSettings.AXIS_5, 50);

            // Verify
            idllMock.AssertWasCalled(t => t.Speed(Arg<byte>.Is.Equal(5), Arg<long>.Is.Anything));
        }
        [Test]
        public void speedWrapped_IsCalled_CallsDllSpeedWithRightSpeedParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Speed(Arg<byte>.Is.Anything, Arg<long>.Is.Anything))
                .Return(1);

            // Test
            wTestObj.speedWrapped(Wrapper.enumAxisSettings.AXIS_4, 1000);

            // Verify
            idllMock.AssertWasCalled(t => t.Speed(Arg<byte>.Is.Anything, Arg<long>.Is.Equal(1000)));
        }
        #endregion
        #region Movement
        [Test]
        public void homeWrapped_IsCalled_CallsDllHome()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Home(Arg<byte>.Is.Anything, Arg<DLL.DgateCallBackByteRefArg>.Is.Anything)).Return(1);

            // Test
            wTestObj.homeWrapped(Wrapper.enumAxisSettings.AXIS_1, homeEvent);

            // Verify
            idllMock.AssertWasCalled(t => t.Home(Arg<byte>.Is.Anything, Arg<DLL.DgateCallBackByteRefArg>.Is.Anything));
        }
        [Test]
        public void homeWrapped_DllHomeReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Home(Arg<byte>.Is.Anything, Arg<DLL.DgateCallBackByteRefArg>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.homeWrapped(Wrapper.enumAxisSettings.AXIS_1, homeEvent);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void homeWrapped_DllHomeReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Home(Arg<byte>.Is.Anything, Arg<DLL.DgateCallBackByteRefArg>.Is.Anything)).Return(0);

            // Test
            bool bReturnValue = wTestObj.homeWrapped(Wrapper.enumAxisSettings.AXIS_1, homeEvent);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void homeWrapped_IsCalled_CallsDllHomeWithRightAxisSettingsParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Home(Arg<byte>.Is.Anything, Arg<DLL.DgateCallBackByteRefArg>.Is.Anything)).Return(1);

            // Test
            wTestObj.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, homeEvent);

            // Verify
            idllMock.AssertWasCalled(t => t.Home(Arg<byte>.Is.Equal('A'), Arg<DLL.DgateCallBackByteRefArg>.Is.Anything));
        }
        [Test]
        public void homeWrapped_IsCalled_CallsDllHomeWithRightCallBackParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            DLL.DgateCallBackByteRefArg dgHome = homeEvent;
            idllMock.Stub(t => t.Home(Arg<byte>.Is.Anything, Arg<DLL.DgateCallBackByteRefArg>.Is.Anything)).Return(1);

            // Test
            wTestObj.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, homeEvent);

            // Verify
            idllMock.AssertWasCalled(t => t.Home(Arg<byte>.Is.Anything, Arg<DLL.DgateCallBackByteRefArg>.Is.Equal(dgHome)));
        }
        [Test]
        public void enterManualWrapped_IsCalled_CallsDllEnterManual()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.EnterManual(Arg<short>.Is.Anything)).Return(1);

            // Test
            wTestObj.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);

            // Verify
            idllMock.AssertWasCalled(t => t.EnterManual(Arg<short>.Is.Anything));
        }
        [Test]
        public void enterManualWrapped_DllEnterManualReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.EnterManual(Arg<short>.Is.Anything)).Return(0);

            // Test
            bool bReturnValue = wTestObj.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void enterManualWrapped_DllEnterManualReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.EnterManual(Arg<short>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void enterManualWrapped_ParamIsAxes_DllEnterManualCalledWith0()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.EnterManual(Arg<short>.Is.Anything)).Return(1);

            // Test
            wTestObj.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES);

            // Verify
            idllMock.AssertWasCalled(t => t.EnterManual(0));
        }
        [Test]
        public void enterManualWrapped_ParamIsCoord_DllEnterManualCalledWith1()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.EnterManual(Arg<short>.Is.Anything)).Return(1);

            // Test
            wTestObj.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_COORD);

            // Verify
            idllMock.AssertWasCalled(t => t.EnterManual(1));
        }

        [Test]
        public void closeManualWrapped_IsCalled_CallsDllCloseManual()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseManual()).Return(1);

            // Test
            wTestObj.closeManualWrapped();

            // Verify
            idllMock.AssertWasCalled(t => t.CloseManual());
        }
        [Test]
        public void closeManual_DllCloseManualReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseManual()).Return(0);

            // Test
            bool bReturnValue = wTestObj.closeManualWrapped();

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void closeManual_DllCloseManualReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseManual()).Return(1);

            // Test
            bool bReturnValue = wTestObj.closeManualWrapped();

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void moveManualWrapped_IsCalled_CallsDllMoveManual()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveManual(Arg<byte>.Is.Anything, Arg<int>.Is.Anything)).Return(1);

            // Test
            wTestObj.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, 10);

            // Verify
            idllMock.AssertWasCalled(t => t.MoveManual(Arg<byte>.Is.Anything, Arg<int>.Is.Anything));
        }
        [Test]
        public void moveManualWrapped_DllMoveManualReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveManual(Arg<byte>.Is.Anything, Arg<int>.Is.Anything)).Return(0);

            // Test
            bool bReturnValue = wTestObj.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, 10);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void moveManualWrapped_DllMoveManualReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveManual(Arg<byte>.Is.Anything, Arg<int>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, 10);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void moveManualWrapped_IsCalled_CallsDllMoveManualWithRightManualModeParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveManual(Arg<byte>.Is.Anything, Arg<int>.Is.Anything)).Return(1);

            // Test
            wTestObj.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, 10);

            // Verify
            idllMock.AssertWasCalled(t => t.MoveManual(Arg<byte>.Is.Equal(5), Arg<int>.Is.Anything));
        }
        [Test]
        public void moveManualWrapped_IsCalled_CallsDllMoveManualWithRightSpeedParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveManual(Arg<byte>.Is.Anything, Arg<int>.Is.Anything)).Return(1);

            // Test
            wTestObj.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER, 10);

            // Verify
            idllMock.AssertWasCalled(t => t.MoveManual(Arg<byte>.Is.Anything, Arg<int>.Is.Equal(10)));
        }
        [Test]
        public void stopWrapped_IsCalled_CallsDllStop()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Stop(Arg<byte>.Is.Anything)).Return(1);

            // Test
            wTestObj.stopWrapped(Wrapper.enumAxisSettings.AXIS_2);

            // Verify
            idllMock.AssertWasCalled(t => t.Stop(Arg<byte>.Is.Anything));
        }
        [Test]
        public void stopWrapped_DllStopReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Stop(Arg<byte>.Is.Anything)).Return(0);

            // Test
            bool bReturnValue = wTestObj.stopWrapped(Wrapper.enumAxisSettings.AXIS_2);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void stopWrapped_DllStopReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Stop(Arg<byte>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.stopWrapped(Wrapper.enumAxisSettings.AXIS_2);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void stopWrapped_IsCalled_CallsDllStopWithRightAxisParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.Stop(Arg<byte>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.stopWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT);

            // Verify
            idllMock.AssertWasCalled(t => t.Stop(Arg<byte>.Is.Equal('A')));
        }
        [Test]
        public void moveLinearWrapped_IsCalled_CallsDllMoveLinear()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything)).Return(1);

            // Test
            wTestObj.moveLinearWrapped("test", 0);

            // Verify
            idllMock.AssertWasCalled(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything));
        }
        [Test]
        public void moveLinearWrapped_DllMoveLinearReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything)).Return(0);

            // Test
            bool bReturnValue = wTestObj.moveLinearWrapped("test", 0);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void moveLinearWrapped_DllMoveLinearReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.moveLinearWrapped("test", 0);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void moveLinearWrapped_IsCalled_DllMoveLinearIsCalledWithRightStringParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything)).Return(1);

            // Test
            wTestObj.moveLinearWrapped("test", 0);

            // Verify
            idllMock.AssertWasCalled(t => t.MoveLinear(Arg<string>.Is.Equal("test"), Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything));
        }
        [Test]
        public void moveLinearWrapped_IsCalled_DllMoveLinearIsCalledWithRightIntParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything)).Return(1);

            // Test
            wTestObj.moveLinearWrapped("test", 0);

            // Verify
            idllMock.AssertWasCalled(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Equal(0), Arg<string>.Is.Anything, Arg<short>.Is.Anything));
        }
        [Test]
        public void moveLinearWrapped_IsCalled_DllMoveLinearIsCalledWithNullSecondaryPos()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything)).Return(1);

            // Test
            wTestObj.moveLinearWrapped("test", 0);

            // Verify
            idllMock.AssertWasCalled(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Null, Arg<short>.Is.Anything));
        }
        [Test]
        public void moveLinearWrapped_IsCalled_DllMoveLinearIsCalledWithZeroValueForSecondaryIndex()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything)).Return(1);

            // Test
            wTestObj.moveLinearWrapped("test", 0);

            // Verify
            idllMock.AssertWasCalled(t => t.MoveLinear(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Equal(0)));
        }

        [Test]
        public void getCurrentPosition_IsCalled_DLLGetCurrentPosition()
        {

            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.GetCurrentPosition(ref Arg<int[]>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new int[2] {1,2}).Dummy, ref Arg<int[]>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new int[2] {1,2}).Dummy, ref Arg<int[]>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new int[2]{1,2}).Dummy)).Return(1);

            // Test
            wTestObj.getCurrentPosition();

            // Verify
            idllMock.AssertWasCalled(t => t.GetCurrentPosition(ref Arg<int[]>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new int[] {1}).Dummy, ref Arg<int[]>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new int[] { 1}).Dummy, ref Arg<int[]>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new int[]{1}).Dummy));
        }
        #endregion
        #region Gripper
        [Test]
        public void openGripperWrapped_IsCalled_CallsDllOpenGripper()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.OpenGripper()).Return(1);

            // Test
            wTestObj.openGripperWrapped();

            // Verify
            idllMock.AssertWasCalled(t => t.OpenGripper());
        }
        [Test]
        public void openGripperWrapped_DllOpenGripperReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.OpenGripper()).Return(0);

            // Test
            bool bReturnValue = wTestObj.openGripperWrapped();

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void openGripperWrapped_DllOpenGripperReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.OpenGripper()).Return(1);

            // Test
            bool bReturnValue = wTestObj.openGripperWrapped();

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void closeGripperWrapped_IsCalled_CallsDllCloseGripper()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseGripper()).Return(1);

            // Test
            wTestObj.closeGripperWrapped();

            // Verify
            idllMock.AssertWasCalled(t => t.CloseGripper());
        }
        [Test]
        public void closeGripperWrapped_DllCloseGripperReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseGripper()).Return(0);

            // Test
            bool bReturnValue = wTestObj.closeGripperWrapped();

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void closeGripperWrapped_DllCloseGripperReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseGripper()).Return(1);

            // Test
            bool bReturnValue = wTestObj.closeGripperWrapped();

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void getJawWrapped_IsCalled_CallsDllGetJaw()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            short shrtRefArg1 = 2;
            short shrtRefArg2 = 2;
            idllMock.Stub(t => t.GetJaw(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy)).Return(1);

            // Test
            wTestObj.getJawWrapped(ref shrtRefArg1, ref shrtRefArg2);

            // Verify
            idllMock.AssertWasCalled(t => t.GetJaw(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy));
        }
        [Test]
        public void getJawWrapped_DllGetJawReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            short shrtRefArg1 = 2;
            short shrtRefArg2 = 2;
            idllMock.Stub(t => t.GetJaw(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy)).Return(0);

            // Test
            bool bReturnValue = wTestObj.getJawWrapped(ref shrtRefArg1, ref shrtRefArg2);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void getJawWrapped_DllGetJawReturns1_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            short shrtRefArg1 = 2;
            short shrtRefArg2 = 2;
            idllMock.Stub(t => t.GetJaw(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy)).Return(1);

            // Test
            bool bReturnValue = wTestObj.getJawWrapped(ref shrtRefArg1, ref shrtRefArg2);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void getJawWrapped_IsCalled_CallsDllGetJawWithRightPercentageParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            short shrtRefArg1 = 2;
            short shrtRefArg2 = 44;

            // Test
            wTestObj.getJawWrapped(ref shrtRefArg1, ref shrtRefArg2);

            // Verify
            idllMock.AssertWasCalled(t => t.GetJaw(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Equal(shrtRefArg1), 2).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy));
        }
        [Test]
        public void getJawWrapped_IsCalled_CallsDllGetJawWithRightWidthParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            short shrtRefArg1 = 2;
            short shrtRefArg2 = 44;

            // Test
            wTestObj.getJawWrapped(ref shrtRefArg1, ref shrtRefArg2);

            // Verify
            idllMock.AssertWasCalled(t => t.GetJaw(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), 2).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Equal(shrtRefArg2), 2).Dummy));
        }
        #endregion
        #region Event handling
        [Test]
        public void watchMotionWrapped_IsCalled_CallsDllWatchMotion()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            DLL.DgateCallBackCharArg dgWatch = watchStart;
            idllMock.Stub(t => t.WatchMotion(Arg<DLL.DgateCallBackCharArg>.Is.Anything, Arg<DLL.DgateCallBackCharArg>.Is.Anything)).Return(dgWatch);

            // Test
            wTestObj.watchMotionWrapped(watchEnd, watchStart);

            // Verify
            idllMock.AssertWasCalled(t => t.WatchMotion(Arg<DLL.DgateCallBackCharArg>.Is.Anything, Arg<DLL.DgateCallBackCharArg>.Is.Anything));
        }
        [Test]
        public void watchMotionWrapped_IsCalled_CallsDllWatchMotionWitRightEndCallBack()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            DLL.DgateCallBackCharArg dgWatch = watchEnd;
            idllMock.Stub(t => t.WatchMotion(Arg<DLL.DgateCallBackCharArg>.Is.Anything, Arg<DLL.DgateCallBackCharArg>.Is.Anything)).Return(dgWatch);

            // Test
            wTestObj.watchMotionWrapped(watchEnd, watchStart);

            // Verify
            idllMock.AssertWasCalled(t => t.WatchMotion(Arg<DLL.DgateCallBackCharArg>.Is.Equal(dgWatch), Arg<DLL.DgateCallBackCharArg>.Is.Anything));
        }
        [Test]
        public void watchMotionWrapped_IsCalled_CallsDllWatchMotionWitRightStartCallBack()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            DLL.DgateCallBackCharArg dgWatch = watchStart;
            idllMock.Stub(t => t.WatchMotion(Arg<DLL.DgateCallBackCharArg>.Is.Anything, Arg<DLL.DgateCallBackCharArg>.Is.Anything)).Return(dgWatch);

            // Test
            wTestObj.watchMotionWrapped(watchEnd, watchStart);

            // Verify
            idllMock.AssertWasCalled(t => t.WatchMotion(Arg<DLL.DgateCallBackCharArg>.Is.Anything, Arg<DLL.DgateCallBackCharArg>.Is.Equal(dgWatch)));
        }
        [Test]
        public void watchDigitalInputWrapped_IsCalled_CallsDllWatchDigitalInput()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.WatchDigitalInput(Arg<DLL.DgateCallBackLongArg>.Is.Anything)).Return(1);

            // Test
            wTestObj.watchDigitalInputWrapped(watchDig);

            // Verify
            idllMock.AssertWasCalled(t => t.WatchDigitalInput(Arg<DLL.DgateCallBackLongArg>.Is.Anything));
        }
        [Test]
        public void watchDigitalInputWrapped_DllWtachDigitalInputsReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.WatchDigitalInput(Arg<DLL.DgateCallBackLongArg>.Is.Anything)).Return(0);

            // Test
            bool bReturnValue = wTestObj.watchDigitalInputWrapped(watchDig);

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void watchDigitalInputWrapped_DllWtachDigitalInputsReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.WatchDigitalInput(Arg<DLL.DgateCallBackLongArg>.Is.Anything)).Return(1);

            // Test
            bool bReturnValue = wTestObj.watchDigitalInputWrapped(watchDig);

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        [Test]
        public void watchDigitalInputWrapped_IsCalled_DllWatchDigitalInputWithRightCallBackParameter()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            DLL.DgateCallBackLongArg dgWatch = watchDig;
            idllMock.Stub(t => t.WatchDigitalInput(Arg<DLL.DgateCallBackLongArg>.Is.Anything)).Return(1);

            // Test
            wTestObj.watchDigitalInputWrapped(watchDig);

            // Verify
            idllMock.AssertWasCalled(t => t.WatchDigitalInput(Arg<DLL.DgateCallBackLongArg>.Is.Equal(dgWatch)));
        }
        [Test]
        public void closeWatchDigitalInputWrapped_IsCalled_CallsDllCloseWatchDigitalInput()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseWatchDigitalInput()).Return(1);

            // Test
            wTestObj.closeWatchDigitalInputWrapped();

            // Verify
            idllMock.AssertWasCalled(t => t.CloseWatchDigitalInput());
        }
        [Test]
        public void closeWatchDigitalInputWrapped_DllCloseWatchDigitalInputReturns0_ReturnsFalse()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseWatchDigitalInput()).Return(0);

            // Test
            bool bReturnValue = wTestObj.closeWatchDigitalInputWrapped();

            // Verify
            Assert.IsFalse(bReturnValue);
        }
        [Test]
        public void closeWatchDigitalInputWrapped_DllCloseWatchDigitalInputReturns1_ReturnsTrue()
        {
            // Setup
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.CloseWatchDigitalInput()).Return(1);

            // Test
            bool bReturnValue = wTestObj.closeWatchDigitalInputWrapped();

            // Verify
            Assert.IsTrue(bReturnValue);
        }
        #endregion
        #region Vectors
        
        [Test]
        public void defineVectorWrapped_isCalled_DLLdefineVector()
        {
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            idllMock.Stub(t => t.DefineVector(Arg<byte>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything)).Return(1);
            
            // Test
            wTestObj.defineVectorWrapped(Wrapper.enumAxisSettings.AXIS_ALL, "VectorOne", 100);

            idllMock.AssertWasCalled(t => t.DefineVector(Arg<byte>.Is.Anything, Arg<string>.Is.Anything, Arg<short>.Is.Anything));
        }


        [Test]
        public void teachWrapped_IsCalledTrue_DLLTeach()
        {
            //Initialize
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            //Make RelCoordSirVectors
            RelCoordSirVector _relCoordSirVector = MockRepository.GenerateStub<RelCoordSirVector>("TestOne");
            //Vecpoints
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);

            //insert into RelCoordSirVector (Note add's 2 times, cause forloop counts from 1)
            _relCoordSirVector.addPoint(_vecPoints);

            
            idllMock.Stub(t => t.Teach(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<int[]>.Is.Anything, Arg<short>.Is.Anything, Arg<int>.Is.Anything)).Return(1);

            wTestObj.teachWrapped(_relCoordSirVector);

            
            idllMock.AssertWasCalled(t => t.Teach(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<int[]>.Is.Anything, Arg<short>.Is.Anything, Arg<int>.Is.Anything));

        }

        [Test]
        public void teachWrapped_ReturnFalse_DLLTeach()
        {
            //Initialize
            Wrapper wTestObj = Wrapper.getInstance();
            IDLL idllMock = MockRepository.GenerateMock<IDLL>();
            wTestObj.DLL = idllMock;
            //Make RelCoordSirVectors
            RelCoordSirVector _relCoordSirVector = MockRepository.GenerateStub<RelCoordSirVector>("TestOne");
            //Vecpoints
            VecPoint _vecPoints = MockRepository.GenerateStub<VecPoint>(10, 10, 10, 10, 10);
            //insert into RelCoordSirVector (Note add's 2 times, cause forloop counts from 1)
            _relCoordSirVector.addPoint(_vecPoints);
            _relCoordSirVector.addPoint(_vecPoints);

            idllMock.Stub(t => t.Teach(Arg<string>.Is.Anything, Arg<short>.Is.Anything, Arg<int[]>.Is.Anything, Arg<short>.Is.Anything, Arg<int>.Is.Anything)).Return(0);

            Assert.IsFalse(wTestObj.teachWrapped(_relCoordSirVector));

        }

        #endregion
    }
}
