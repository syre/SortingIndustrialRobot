using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlSystem;
using NUnit.Framework;
using Rhino.Mocks;
using System.IO.Ports;

namespace SIR.Tests
{
    [TestFixture]
    public class robotTest
    {
        [Test]
        public void HomeRobot_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None,8, StopBits.One);
            Robot robot = new Robot();
            
            wp.Stub(t => t.homeWrapped(Arg<Wrapper.enumAxisSettings>.Is.Equal(Wrapper.enumAxisSettings.AXIS_ROBOT), Arg<DLL.DgateCallBackByteRefArg>.Is.Anything)).Return(true);
           
            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.homeRobot());
        }

        /*[Test]
        public void stopAllMovement_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.stopWrapped(Arg<Wrapper.enumAxisSettings>.Is.Equal(Wrapper.enumAxisSettings.AXIS_ROBOT))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.stopAllMovement());
        }*/

        [Test]
        public void isOnline_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.isOnlineOkWrapped()).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.isOnline());
        }

        [Test]
        public void Time_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.timeWrapped(Arg<Wrapper.enumAxisSettings>.Is.Anything, Arg<int>.Is.Anything)).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.Time(Wrapper.enumAxisSettings.AXIS_ALL, 1000));
        }

        [Test]
        public void Speed_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.speedWrapped(Arg<Wrapper.enumAxisSettings>.Is.Anything, Arg<int>.Is.Anything)).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.Speed(Wrapper.enumAxisSettings.AXIS_ALL, 1000));
        }

        


    }
}
