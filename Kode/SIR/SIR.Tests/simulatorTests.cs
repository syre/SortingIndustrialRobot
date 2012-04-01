using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DSL;
using Rhino.Mocks;

namespace SIR.Tests
{
    [TestFixture]
    class SimulatorTests
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
        public void initialization_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.initialization());
        }
        [Test]
        public void initialization_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.initialization();

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void stopMove_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.stopMove(AxisSettings.AXIS_ROBOT)); // Dummy value
        }
        [Test]
        public void stopMove_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.stopMove(AxisSettings.AXIS_ROBOT); // Dummy value

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
        public void homeRobot_CallingIt_ReturnsTrue()
        {
            Assert.IsTrue(simTestObj.homeRobot());
        }
        [Test]
        public void homeRobot_CallingIt_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;

            simTestObj.homeRobot();

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
#endregion
        #region Tests of functions that not only returns true and writes to screen.
        [Test]
        public void moveByCoordinates_CallingItWithManuaModeAxes_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;
            simTestObj.ManualMode = ManualModeType.Axes;

            simTestObj.moveByCoordinates(1,1,1,1,1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveByCoordinates_CallingItWithManuaModeCoord_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;
            simTestObj.ManualMode = ManualModeType.Coordinates;

            simTestObj.moveByCoordinates(1, 1, 1, 1, 1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveByCoordinates_CallingItWithManuaModeOff_WritesToUI()
        {
            IUI iuiMock = MockRepository.GenerateMock<IUI>();
            simTestObj.IUIOutput = iuiMock;
            simTestObj.ManualMode = ManualModeType.Coordinates;

            simTestObj.moveByCoordinates(1, 1, 1, 1, 1); // Dummy values

            iuiMock.AssertWasCalled(t => t.writeLine(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void moveByCoordinates_ManualModeIsAxes_ReturnsTrue()
        {
            simTestObj.ManualMode = ManualModeType.Axes;
            Assert.IsTrue(simTestObj.moveByCoordinates(1,1,1,1,1));
        }
        [Test]
        public void moveByCoordinates_ManualModeIsCoord_ReturnsTrue()
        {
            simTestObj.ManualMode = ManualModeType.Coordinates;
            Assert.IsTrue(simTestObj.moveByCoordinates(1,1,1,1,1));
        }
        [Test]
        public void moveByCoordanates_ManualModeOff_ReturnsFalse()
        {
            simTestObj.ManualMode = ManualModeType.Off;
            Assert.IsFalse(simTestObj.moveByCoordinates(1,1,1,1,1));
        }
        #endregion
    }
}
