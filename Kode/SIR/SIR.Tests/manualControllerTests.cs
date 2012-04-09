using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using ControlSystem;
using DSL;

namespace SIR.Tests
{
    // Temp notes: Test for calling right IRobot movement function and with right arguments(Left, Right). ATM not possible because of lacking interface.
    [TestFixture]
    public class ManualControllerTests
    {
        // Members
        private ManualController mcTestObj;

        // Tests
        // -Properties
        [Test]
        public void Speed_SettingSpeedTo0_SpeedIsSaved()
        {
            mcTestObj = new ManualController();
            mcTestObj.Speed = 0;
            Assert.AreEqual(mcTestObj.Speed, 0);
        }
        [Test]
        public void Speed_SettingSpeedTo50_SpeedIsSaved()
        {
            mcTestObj = new ManualController();
            mcTestObj.Speed = 50;
            Assert.AreEqual(mcTestObj.Speed, 50);
        }
        [Test]
        public void Speed_SettingSpeedTo100_SpeedIsSaved()
        {
            mcTestObj = new ManualController();
            mcTestObj.Speed = 100;
            Assert.AreEqual(mcTestObj.Speed, 100);
        }
        [Test]
        public void Speed_SettingSpeedTo101_SpeedIsNotSaved()
        {
            mcTestObj = new ManualController();
            int iTmpSpeed = mcTestObj.Speed;
            mcTestObj.Speed = 101;
            Assert.AreNotEqual(mcTestObj.Speed, iTmpSpeed);
        }

        // -Functions
        #region Constructor
        [Test]
        public void ManualController_DefaultConstructorCalled_ChecksForBeingOnline()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            irMock.AssertWasCalled(t => t.isOnline());
        }
        [Test]
        [ExpectedException]
        public void ManualController_DefaultConstructorCalledWhenNoConnection_ThrowsExceptionIfOffline()
        {
            IRobot irMock = MockRepository.GenerateStub<IRobot>();
            irMock.Stub(t => t.isOnline()).Return(false);
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();
        }
        [Test]
        public void ManualController_DefaultConstructorCalled_SetsIRobotInstanceToFactoryIRobotInstance()
        {
            mcTestObj = new ManualController();
            Assert.AreSame(mcTestObj.RobotConnection, Factory.currentIRobotInstance);
        }

        #endregion
        #region Axis functions
        [Test]
        public void moveAxisBase_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();
            
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        [Test]
        public void moveAxisBase_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();
            
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }        
        [Test]
        public void moveAxisShoulder_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();
            
            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisShoulder_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }        
        [Test]
        public void moveAxisElbow_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisElbow_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }        
        [Test]
        public void moveAxisGripper_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisGripper_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }        
        [Test]
        public void moveAxisPitch_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisPitch_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }        
        [Test]
        public void moveAxisRoll_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisRoll_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }        
        [Test]
        public void moveAxisConveyer_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }
        [Test]
        public void moveAxisConveyer_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        #endregion
        #region Coordinate functions
        [Test]
        public void moveCoordX_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordX_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordY_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordY_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordZ_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordZ_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordPitch_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordPitch_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordRoll_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordRoll_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        #endregion
    }
}
