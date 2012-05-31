using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlSystem;
using NUnit.Framework;
using Rhino.Mocks;
using System.IO.Ports;
using System.Threading;

namespace SIR.Tests
{
    [TestFixture]
    public class robotTest
    {

        #region Property
        [Test]
        public void Wrapper_PropertyTest()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.AreEqual(wp, robot.wrapper);
        }

        [Test]
        public void STK_PropertyTest()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.AreEqual(serialSTK, robot.STK);
        }
        #endregion

        #region Home And settings
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

        [Test]
        public void stopAllMovement_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.stopWrapped(Arg<Wrapper.enumAxisSettings>.Is.Equal(Wrapper.enumAxisSettings.AXIS_ROBOT))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            robot.Sem.WaitOne();
            Assert.IsTrue(robot.stopAllMovement());
        }

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
        #endregion

        #region Coordinate movements
        [Test]
        public void movebyCoordinates_IsCalled_ReturnTrue()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();
            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Anything, Arg<int>.Is.Anything)).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;


            //Assert.IsTrue(robot.movebyCoordinates(10, 20, 30));
        }

        [Test]
        public void movebyCoordinates_IsCalled_ReturnFalseOnMoveX()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_X, 10)).Return(false);
            

            robot.wrapper = wp;
            robot.STK = serialSTK;
     
            
            //Assert.IsFalse(robot.movebyCoordinates(10, 20, 30));
           
        }

        [Test]
        public void movebyCoordinates_IsCalled_ReturnFalseOnMoveY()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_X, 10)).Return(true);
            wp.Stub(t => t.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y, 20)).Return(false);
            

            robot.wrapper = wp;
            robot.STK = serialSTK;


            //Assert.IsFalse(robot.movebyCoordinates(10, 20, 30));

        }

        [Test]
        public void movebyCoordinates_IsCalled_ReturnFalseOnMoveZ()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_X, 10)).Return(true);
            wp.Stub(t => t.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y, 20)).Return(true);
            wp.Stub(t => t.moveManualWrapped(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z, 30)).Return(false);

            robot.wrapper = wp;
            robot.STK = serialSTK;


            //Assert.IsFalse(robot.movebyCoordinates(10, 20, 30));

        }
        #endregion

        #region Axis movements
        [Test]
        public void moveBase_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_BASE), Arg<int>.Is.Equal(100))).Return(true);
            
            robot.wrapper = wp;
            robot.STK = serialSTK;
            
            Assert.IsTrue(robot.moveBase(100));
        }

        [Test]
        public void moveShoulder_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_SHOULDER), Arg<int>.Is.Equal(100))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.moveShoulder(100));
        }

        [Test]
        public void moveElbow_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_ELBOW), Arg<int>.Is.Equal(100))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.moveElbow(100));
        }

        [Test]
        public void moveWristPitch_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTPITCH), Arg<int>.Is.Equal(100))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.moveWristPitch(100));
        }

        [Test]
        public void moveWristRoll_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_WRISTROLL), Arg<int>.Is.Equal(100))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.moveWristRoll(100));
        }

        [Test]
        public void moveGripper_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_GRIPPER), Arg<int>.Is.Equal(100))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.moveGripper(100));
        }

        [Test]
        public void moveConveyerBelt_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_CONVEYERBELT), Arg<int>.Is.Equal(100))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsTrue(robot.moveConveyerBelt(100));
        }

        #endregion

        #region gripper methods
        [Test]
        public void getJawOpeningWidthMilimeters_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.getJawWrapped(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy)).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            robot.getJawOpeningWidthMilimeters();

            wp.AssertWasCalled(t => t.getJawWrapped(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy));
        }

        [Test]
        public void getJawOpeningWidthMilimeters_IsCalled_ReturnFalse_ExpectException()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(
                t =>
                t.getJawWrapped(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy,
                                ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy)).Return(
                                    false);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.Catch(() => robot.getJawOpeningWidthMilimeters());
        }

        [Test]
        public void getJawOpeningWidthPercentage_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.getJawWrapped(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy)).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            robot.getJawOpeningWidthPercentage();

            wp.AssertWasCalled(t => t.getJawWrapped(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy, ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy));
        }

        [Test]
        public void getJawOpeningWidthPercentage_IsCalled_ReturnFalse_ExpectException()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(
                t =>
                t.getJawWrapped(ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy,
                                ref Arg<short>.Ref(Rhino.Mocks.Constraints.Is.Anything(), new short()).Dummy)).Return(
                                    false);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.Catch(() => robot.getJawOpeningWidthPercentage());
        }

        [Test]
        public void closeGripper_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.closeGripperWrapped()).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            robot.closeGripper();

            wp.AssertWasCalled(t => t.closeGripperWrapped());
        }

        [Test]
        public void openGripper_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.openGripperWrapped()).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            robot.openGripper();

            wp.AssertWasCalled(t => t.openGripperWrapped());
        }



        #endregion

        #region MovejustOneCordinate

        [Test]
        public void moveByXCoordinate_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_X), Arg<int>.Is.Equal(10))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;
            robot.homeRobot();

            robot.moveByXCoordinate(10);

            wp.AssertWasCalled(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_X), Arg<int>.Is.Equal(10)));
        }

        [Test]
        public void moveByXCoordinate_IsCalled_ReturnFalse()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_X), Arg<int>.Is.Equal(10))).Return(false);

            robot.wrapper = wp;
            robot.STK = serialSTK;
            robot.homeRobot();

            Assert.IsFalse(robot.moveByXCoordinate(10));
        }

        [Test]
        public void moveByYCoordinate_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y), Arg<int>.Is.Equal(10))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;
            robot.homeRobot();

            robot.moveByYCoordinate(10);

            wp.AssertWasCalled(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y), Arg<int>.Is.Equal(10)));
        }

        [Test]
        public void moveByYCoordinate_IsCalled_ReturnFalse()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_Y), Arg<int>.Is.Equal(10))).Return(false);

            robot.wrapper = wp;
            robot.STK = serialSTK;
            robot.homeRobot();

            Assert.IsFalse(robot.moveByYCoordinate(10));
        }

        [Test]
        public void moveByZCoordinate_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z), Arg<int>.Is.Equal(10))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;
            robot.homeRobot();

            robot.moveByZCoordinate(10);

            wp.AssertWasCalled(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z), Arg<int>.Is.Equal(10)));
        }

        [Test]
        public void moveByZCoordinate_IsCalled_ReturnFalse()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_Z), Arg<int>.Is.Equal(10))).Return(false);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsFalse(robot.moveByZCoordinate(10));
        }

        [Test]
        public void moveByPitch_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_PITCH), Arg<int>.Is.Equal(10))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;
            robot.homeRobot();

            robot.moveByPitch(10);

            wp.AssertWasCalled(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_PITCH), Arg<int>.Is.Equal(10)));
        }

        [Test]
        public void moveByPitch_IsCalled_ReturnFalse()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_PITCH), Arg<int>.Is.Equal(10))).Return(false);

            robot.wrapper = wp;
            robot.STK = serialSTK;
            robot.homeRobot();

            Assert.IsFalse(robot.moveByPitch(10));
        }

        [Test]
        public void moveByRoll_IsCalled()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_ROLL), Arg<int>.Is.Equal(10))).Return(true);

            robot.wrapper = wp;
            robot.STK = serialSTK;
            robot.homeRobot();

            robot.moveByRoll(10);

            wp.AssertWasCalled(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_ROLL), Arg<int>.Is.Equal(10)));
        }

        [Test]
        public void moveByRoll_IsCalled_ReturnFalse()
        {
            IWrapper wp = MockRepository.GenerateStub<IWrapper>();
            SerialSTK serialSTK = MockRepository.GenerateStub<SerialSTK>(9600, Parity.None, 8, StopBits.One);
            Robot robot = new Robot();

            wp.Stub(t => t.moveManualWrapped(Arg<Wrapper.enumManualModeWhat>.Is.Equal(Wrapper.enumManualModeWhat.MANUAL_MOVE_ROLL), Arg<int>.Is.Equal(10))).Return(false);

            robot.wrapper = wp;
            robot.STK = serialSTK;

            Assert.IsFalse(robot.moveByRoll(10));
        }

        #endregion
    }
}
