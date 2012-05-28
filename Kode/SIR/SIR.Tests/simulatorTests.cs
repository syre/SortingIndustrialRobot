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
        public void moveGripper_CallingIt_bGripper_ReturnsTrue()
        {
            simTestObj.moveGripper(1);
            Assert.IsTrue(simTestObj.bGripperIsOpen);
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
        public void moveGripper_CallingItWithNegativeInt_bGripper__ReturnsFalse()
        {
            simTestObj.moveGripper(-1);
            Assert.IsFalse(simTestObj.bGripperIsOpen);
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
        public void moveByCoordinates_WithFiveArguments_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByCoordinates(1,1,1,1,1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void moveByCoordinates_WithThreeArguments_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.movebyCoordinates(1, 1, 1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void moveByXCoordinate_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByXCoordinate(1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void moveByYCoordinate_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByYCoordinate(1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void moveByZCoordinate_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByZCoordinate(1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void moveByPitch_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByPitch(1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void moveByRoll_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveByRoll(1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void Time_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.Time(Wrapper.enumAxisSettings.AXIS_ALL, 100); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void Speed_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.Speed(Wrapper.enumAxisSettings.AXIS_ALL, 100); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        
        [Test]
        public void moveToCubePosition_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.moveToCubePosition(100); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void getCurrentPositionAsString_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.getCurrentPositionAsString(); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void getWeightg_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.getWeight(); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void getJawOpeningWidthMilimeters_bGripperIsOpen_Return200()
        {
            simTestObj.openGripper();
            Assert.AreEqual(200, simTestObj.getJawOpeningWidthMilimeters());
        }

        [Test]
        public void getJawOpeningWidthMilimeters_bGripperIsClosed_Return0()
        {
            simTestObj.closeGripper();
            Assert.AreEqual(0, simTestObj.getJawOpeningWidthMilimeters());
        }
        
        [Test]
        public void getJawOpeningWidthPercentage_bGripperIsOpen_Return200()
        {
            simTestObj.openGripper();
            Assert.AreEqual(100, simTestObj.getJawOpeningWidthPercentage());
        }

        [Test]
        public void getJawOpeningWidthPercentage_bGripperIsClosed_Return0()
        {
            simTestObj.closeGripper();
            Assert.AreEqual(0, simTestObj.getJawOpeningWidthPercentage());
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
