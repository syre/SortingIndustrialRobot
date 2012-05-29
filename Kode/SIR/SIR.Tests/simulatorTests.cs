using ControlSystem;
using NUnit.Framework;
using Rhino.Mocks;

namespace SIR.Tests
{
    [TestFixture]
    public class SimulatorTests
    {
        // Members
        private Simulator simTestObj;

        [SetUp]
        public void setup()
        {
            simTestObj = new Simulator();
        }
        [TearDown]
        public void tearDown()
        {
            simTestObj = null;
        }

        // Tests
#region Primary tests of functions that always returns true, since simple simulator with no detection mechanism, and writing to screen.
        [Test]
        public void stopAllMovement_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.stopAllMovement());
        }
        [Test]
        public void stopAllMovement_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.stopAllMovement();

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void closeGripper_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.closeGripper()); 
        }
        [Test]
        public void closeGripper_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.closeGripper();

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void openGripper_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.openGripper()); 
        }
        [Test]
        public void openGripper_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.openGripper();

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void isOnline_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.isOnline());
        }
        [Test]
        public void isOnline_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.isOnline();

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void homeRobot_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;
            simTestObj.homeRobot();

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        #region Revision V2 of IRobot
        [Test]
        public void moveByAbsoluteCoordinates_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.homeRobot());
            Assert.IsTrue(simTestObj.moveByAbsoluteCoordinates(1, 1, 1, 1, 1));
        }
        [Test]
        public void moveByAbsoluteCoordinates_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByAbsoluteCoordinates(1, 1, 1, 1, 1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
#endregion
        [Test]
        public void moveByRelativeCoordinates_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.moveByRelativeCoordinates(1, 1, 1, 1, 1));
        }
        [Test]
        public void moveByRelativeCoordinates_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByRelativeCoordinates(1, 1, 1, 1, 1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveBase_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.moveBase(1));
        }
        [Test]
        public void moveBase_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveBase(1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveShoulder_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.moveShoulder(1));
        }
        [Test]
        public void moveShoulder_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveShoulder(1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveWristPitch_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.moveWristPitch(1));
        }
        [Test]
        public void moveWristPitch_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveWristPitch(1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveWristRoll_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.moveWristRoll(1));
        }
        [Test]
        public void moveWristRoll_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveWristRoll(1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveElbow_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.moveElbow(1));
        }
        [Test]
        public void moveElbow_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveElbow(1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveGripper_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.moveGripper(1));
        }
        [Test]
        public void moveGripper_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveGripper(1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveConveyerBelt_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.moveConveyerBelt(1));
        }
        [Test]
        public void moveConveyerBelt_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveConveyerBelt(1);

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveByCoordinates_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByCoordinates(1,1,1,1,1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        #endregion
        #region Revision V2 of IRobot
        [Test]
        public void getCurrentPosition_CallingIt_DoesNotReturnNull()
        {
            Assert.IsTrue(simTestObj.getCurrentPosition() != null);
        }

        [Test]
        public void getCurrentPosition_CallingIt_WritesToUI()
        {
           IUI iuiMock = MockRepository.GenerateMock<IUI>();
	
            simTestObj.IUIOutput = iuiMock;

            simTestObj.getCurrentPosition();
            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        #endregion
    }
}
