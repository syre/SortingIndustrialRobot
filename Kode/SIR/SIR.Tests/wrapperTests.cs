using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSL;
using NUnit.Framework;
using Rhino.Mocks;

namespace SIR.Tests
{

    [TestFixture]
    public class WrapperTests
    {
        
        private DLL.DgateCallBack dgateEventHandlerSuccess = initSuccess;
        private DLL.DgateCallBack dgateEventHandlerError = initError;
        private DLL.DgateCallBackByteRefArg dgateEventHandlerHoming = homeEvent;

        static void initSuccess(IntPtr _iptrArg)
        {
            
        }
        static void initError(IntPtr _iptrArg)
        {
           
        }
        static void homeEvent(ref byte _bArg)
        {
           
        }

        private IWrapper _wrapper;

        [SetUp]
        public void SetUp()
        {
            Wrapper stubCalcHelper = MockRepository.GenerateStub<ICalcHelper>();
            _wrapper = MockRepository.GenerateMock<Wrapper>();
        }

        [TearDown]
        public void TearDown()
        {
            //_dll = null;
            //_dllMock = null;
            _wrapper = null;
        }

        
        [Test]
        public void test()
        public void Initialization_TestWasCalled_True()
        {
            int y = _dllMock.testcase(3);
            Assert.AreEqual(3, y);
            _wrapper.AssertWasCalled(t => t.initializationWrapped(Wrapper.enumSystemModes.MODE_ONLINE,
                                                  Wrapper.enumSystemTypes.SYSTEM_TYPE_DEFAULT,
                                                  dgateEventHandlerSuccess,
                                                  dgateEventHandlerError));
        }
        
        
        [Test]
        public void Initialization()
        public void Control_TestWasCalled_True()
        {
            Assert.AreEqual(1, _dllMock.Initialization((short) DLLMock.enumSystemModes.MODE_ONLINE,
                                               (short) DLLMock.enumSystemTypes.SYSTEM_TYPE_DEFAULT,
                                               dgateEventHandlerSuccess, dgateEventHandlerError));
            _wrapper.AssertWasCalled(t => t.controlWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, true));
        }

        [Test]
        public void Control()
        {
           Assert.AreEqual(1, _dllMock.Control((byte)DLLMock.enumAxisSettings.AXIS_ROBOT, true)); 
        }
        
        [Test]
        public void Home()
        public void Home_TestWasCalled_True()
        {
            Assert.AreEqual(1, _dllMock.Home((byte)DLLMock.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming));
            _wrapper.AssertWasCalled(t => t.homeWrapped(Wrapper.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming));
        }
        
        [Test]
        public void OpenGripper()
        public void OpenGripper_TestWasCalled_True()
        {
            Assert.AreEqual(1, _dllMock.OpenGripper());

           _wrapper.AssertWasCalled(t => t.openGripperWrapped());
        }

        [Test]
        public void CloseGripper()
        public void CloseGripper_TestWasCalled_True()
        {
            Assert.AreEqual(1, _dllMock.CloseGripper());
            _wrapper.AssertWasCalled(t => t.closeGripperWrapped());
        }   

        [Test]
        public void GetJaw()
        public void GetJaw_Milimeters_TestWasCalled_true()
        {
          
            short milimeters = 0, dummypercentage = 0;
            _wrapper.AssertWasCalled(t => t.getJawWrapped(ref milimeters, ref dummypercentage));
        }
        
        [Test]
        public void EnterManual()
        {

            _wrapper.AssertWasCalled(t => t.enterManualWrapped(Wrapper.enumManualType.MANUAL_TYPE_AXES));
        }

        [Test]
        public void CloseManual()
        {

            _wrapper.AssertWasCalled(t => t.closeManualWrapped());
        }

        [Test]
        public void MoveManual()
        {

            _wrapper.AssertWasCalled(t => t.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE, 1000));
        }

        [Test]
        public void WatchMotion()
        {

           // _wrapper.AssertWasCalled(t => t.watchMotionWrapped());
        }

        [Test]
        public void WatchDigitalInput()
        {

        }

        [Test]
        public void CLoseWatchDigitalInput()
        {

        }

        [Test]
        public void IsOnLineOK()
        {

            _wrapper.AssertWasCalled(t => t.isOnlineOkWrapped());
        }

        [Test]
        public void MoveLinear()
        {

        }

        [Test]
        public void DefineVector()
        {

        }

        [Test]
        public void Teach()
        {

        }

        [Test]
        public void GetCurrentPosition()
        {

            _wrapper.AssertWasCalled(t => t.getCurrentPosition());
        }
        
    }

     
}
