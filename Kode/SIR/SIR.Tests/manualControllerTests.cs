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
        #region Properties
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
        #endregion

        // -Functions
        #region Constructor
        [Test]
        public void ManualController_DefaultConstructorCalled_ChecksForBeingOnline()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

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
            IRobot irMock = MockRepository.GenerateStub<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();
            Assert.AreSame(mcTestObj.RobotConnection, Factory.currentIRobotInstance);
        }
        [Test]
        public void ManualController_DefaultConstructorCalled_RobotConnectionIsSameAsFactorycurrentIRobotInstance()
        {
            IRobot irMock = MockRepository.GenerateStub<IRobot>();
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();
            Assert.IsTrue(mcTestObj.RobotConnection == Factory.currentIRobotInstance);
        }

        #endregion
        #region Axis functions
        [Test]
        public void moveAxisBase_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }
        [Test]
        public void moveAxisBase_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }        
        [Test]
        public void moveAxisBase_CalledWithArgLeft_CallsRobotmoveBase()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
 
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveBase(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisBase_CalledWithArgLeft_CallsRobotmoveBaseWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
 
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveBase(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisBase_CalledWithArgRight_CallsRobotmoveBaseWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.moveBase(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisShoulder_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            
            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisShoulder_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveAxisShoulder_CalledWithArgLeft_CallsRobotmoveShoulder()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveShoulder(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisShoulder_CalledWithArgLeft_CallsRobotmoveShoulderWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveShoulder(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisShoulder_CalledWithArgRight_CallsRobotmoveShoulderWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.moveShoulder(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisElbow_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisElbow_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveAxisElbow_CalledWithArgLeft_CallsRobotmoveElbow()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveElbow(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisElbow_CalledWithArgLeft_CallsRobotmoveElbowWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveElbow(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisElbow_CalledWithArgRight_CallsRobotmoveElbowWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.moveElbow(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisGripper_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisGripper_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveAxisGripper_CalledWithArgOpen_CallsRobotmoveGripper()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);

            irMock.AssertWasCalled(t => t.moveGripper(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisGripper_CalledWithArgClose_CallsRobotmoveGripperWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_CLOSE);

            irMock.AssertWasCalled(t => t.moveGripper(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisGripper_CalledWithArgOpen_CallsRobotmoveGripperWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);

            irMock.AssertWasCalled(t => t.moveGripper(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisPitch_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisPitch_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveAxisPitch_CalledWithArgDown_CallsRobotmoveWristPitch()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_DOWN);

            irMock.AssertWasCalled(t => t.moveWristPitch(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisPitch_CalledWithArgDown_CallsRobotmoveWristPitchWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_DOWN);

            irMock.AssertWasCalled(t => t.moveWristPitch(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisPitch_CalledWithArgUp_CallsRobotmoveWristPitchWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP);

            irMock.AssertWasCalled(t => t.moveWristPitch(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisRoll_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }        
        [Test]
        public void moveAxisRoll_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveAxisRoll_CalledWithArgLeft_CallsRobotmoveWristRoll()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveWristRoll(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisRoll_CalledWithArgLeft_CallsRobotmoveWristRollWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveWristRoll(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisRoll_CalledWithArgRight_CallsRobotmoveWristRollWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.moveWristRoll(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisConveyer_Called_ManualModeInRobotSetToAxes()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Axes);
        }
        [Test]
        public void moveAxisConveyer_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveAxisConveyer_CalledWithArgLeft_CallsRobotmoveConveyerBelt()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveConveyerBelt(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisConveyer_CalledWithArgLeft_CallsRobotmoveConveyerBeltWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_LEFT);

            irMock.AssertWasCalled(t => t.moveConveyerBelt(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisConveyer_CalledWithArgRight_CallsRobotmoveConveyerBeltWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT);

            irMock.AssertWasCalled(t => t.moveConveyerBelt(mcTestObj.Speed));
        }
        #endregion
        #region Coordinate functions
        [Test]
        public void moveCoordX_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordX_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordX_CalledWithArgDecreasing_CallsRobotmoveByXCoordinate()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByXCordinate(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordX_CalledWithArgDecreasing_CallsRobotmoveByXCoordinateWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByXCordinate(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordX_CalledWithArgIncresing_CallsRobotmoveByXCoordinateWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.moveByXCordinate(mcTestObj.Speed));
        }
        [Test]
        public void moveCoordY_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordY_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordY_CalledWithArgDecreasing_CallsRobotmoveByYCoordinate()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByYCordinate(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordY_CalledWithArgDecreasing_CallsRobotmoveByYCoordinateWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByYCordinate(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordY_CalledWithArgIncresing_CallsRobotmoveByYCoordinateWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.moveByYCordinate(mcTestObj.Speed));
        }
        [Test]
        public void moveCoordZ_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordZ_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordZ_CalledWithArgDecreasing_CallsRobotmoveByZCoordinate()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByZCordinate(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordZ_CalledWithArgDecreasing_CallsRobotmoveByZCoordinateWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByZCordinate(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordZ_CalledWithArgIncresing_CallsRobotmoveByZCoordinateWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.moveByZCordinate(mcTestObj.Speed));
        }
        [Test]
        public void moveCoordPitch_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordPitch_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordPitch_CalledWithArgDecreasing_CallsRobotmoveByPitch()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByPitch(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordPitch_CalledWithArgDecreasing_CallsRobotmoveByPitchWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByPitch(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordPitch_CalledWithArgIncresing_CallsRobotmoveByPitchWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.moveByPitch(mcTestObj.Speed));
        }
        [Test]
        public void moveCoordRoll_Called_ManualModeInRobotSetToCoord()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.ManualMode = ManualModeType.Coordinates);
        }
        [Test]
        public void moveCoordRoll_Called_CallsRobotStopAllMovement()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }
        [Test]
        public void moveCoordRoll_CalledWithArgDecreasing_CallsRobotmoveByRoll()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;

            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByRoll(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordRoll_CalledWithArgDecreasing_CallsRobotmoveByRollWithNegativeSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_DEC);

            irMock.AssertWasCalled(t => t.moveByRoll(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordRoll_CalledWithArgIncresing_CallsRobotmoveByRollWithPositiveSpeedValue()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;

            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);

            irMock.AssertWasCalled(t => t.moveByRoll(mcTestObj.Speed));
        }
        #endregion
    }
}
