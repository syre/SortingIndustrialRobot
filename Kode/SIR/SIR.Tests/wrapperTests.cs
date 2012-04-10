using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSL;
using NUnit.Framework;

namespace SIR.Tests
{
    [TestFixture]
    public class WrapperTests
    {
        


        [SetUp]
        public void SetupTest()
        {
            Wrapper stubCalcHelper = MockRepository.GenerateStub<ICalcHelper>();
        }

        [TearDown]
        public void ExitTest()
        {
            //_dll = null;
            //_dllMock = null;
        }

        [Test]
        public void test()
        {
            int y = _dllMock.testcase(3);
            Assert.AreEqual(3, y);
        }
        
        
        [Test]
        public void Initialization()
        {
            Assert.AreEqual(1, _dllMock.Initialization((short) DLLMock.enumSystemModes.MODE_ONLINE,
                                               (short) DLLMock.enumSystemTypes.SYSTEM_TYPE_DEFAULT,
                                               dgateEventHandlerSuccess, dgateEventHandlerError));
        }

        [Test]
        public void Control()
        {
           Assert.AreEqual(1, _dllMock.Control((byte)DLLMock.enumAxisSettings.AXIS_ROBOT, true)); 
        }
        
        [Test]
        public void Home()
        {
            Assert.AreEqual(1, _dllMock.Home((byte)DLLMock.enumAxisSettings.AXIS_ROBOT, dgateEventHandlerHoming));
        }
        
        [Test]
        public void OpenGripper()
        {
            Assert.AreEqual(1, _dllMock.OpenGripper());

        }

        [Test]
        public void CloseGripper()
        {
            Assert.AreEqual(1, _dllMock.CloseGripper());
        }   

        [Test]
        public void GetJaw()
        {
          
        }
        
        [Test]
        public void EnterManual()
        {

        }

        [Test]
        public void CloseManual()
        {

        }

        [Test]
        public void MoveManual()
        {

        }

        [Test]
        public void WatchMotion()
        {

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

        }
        
    }
}
