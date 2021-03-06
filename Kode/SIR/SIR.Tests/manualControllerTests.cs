﻿/** \file manualControllerTests.cs */
/** \author Robotic Global Organization(RoboGO) */
using NUnit.Framework;
using Rhino.Mocks;
using ControlSystem;

namespace SIR.Tests
{
    // Temp notes: Test for calling right IRobot movement function and with right arguments(Left, Right). ATM not possible because of lacking interface.
    [TestFixture]
    public class ManualControllerTests
    {
        // Members
        private ManualController mcTestObj;

        [SetUp]
        public void SetUp()
        {
            IRobot rob = MockRepository.GenerateMock<IRobot>();
            rob.Stub(t => t.isOnline()).Return(true);
            Factory.currentIRobotInstance = rob;
        }

        
        // Tests
        #region Properties
        [Test]
        public void Speed_SettingSpeedTo0_SpeedIsSaved()
        {
            // Setup
            mcTestObj = new ManualController();

            // Test
            mcTestObj.Speed = 0;
            
            // Verify
            Assert.AreEqual(mcTestObj.Speed, 0);
        }
        [Test]
        public void Speed_SettingSpeedTo50_SpeedIsSaved()
        {
            // Setup
            mcTestObj = new ManualController();

            // Test
            mcTestObj.Speed = 50;

            // Verify
            Assert.AreEqual(mcTestObj.Speed, 50);
        }
        [Test]
        public void Speed_SettingSpeedTo100_SpeedIsSaved()
        {
            // Setup
            mcTestObj = new ManualController();

            // Test
            mcTestObj.Speed = 100;

            // Verify
            Assert.AreEqual(mcTestObj.Speed, 100);
        }
        [Test]
        public void Speed_SettingSpeedTo101_SpeedIsNotSaved()
        {
            // Setup
            mcTestObj = new ManualController();

            // Test
            mcTestObj.Speed = 101;

            // Verify
            Assert.AreNotEqual(mcTestObj.Speed, 101);
        }
        #endregion

        // -Functions
        #region Constructor
        [Test]
        public void ManualController_DefaultConstructorCalled_ChecksForBeingOnline()
        {
            // Setup 
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            irMock.Stub(t => t.isOnline()).Return(true);

            // Test
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            // Verify
            irMock.AssertWasCalled(t => t.isOnline());
        }
        [Test]
        [ExpectedException]
        public void ManualController_DefaultConstructorCalledWhenNoConnection_ThrowsExceptionIfOffline()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateStub<IRobot>();
            irMock.Stub(t => t.isOnline()).Return(false);

            // Test
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();
        }
        [Test]
        public void ManualController_DefaultConstructorCalled_SetsIRobotInstanceToFactoryIRobotInstance()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateStub<IRobot>();
            irMock.Stub(t => t.isOnline()).Return(true);

            // Test
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            // Verify
            Assert.AreSame(mcTestObj.RobotConnection, Factory.currentIRobotInstance);
        }
        [Test]
        public void ManualController_DefaultConstructorCalled_RobotConnectionIsSameAsFactorycurrentIRobotInstance()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateStub<IRobot>();
            irMock.Stub(t => t.isOnline()).Return(true);

            // Test
            Factory.currentIRobotInstance = irMock;
            mcTestObj = new ManualController();

            // Verify
            Assert.IsTrue(mcTestObj.RobotConnection == Factory.currentIRobotInstance);
        }
        [Test]
        public void ManualController_CallsIt_SpeedIsSetTo50()
        {
            // Setup and test
            mcTestObj = new ManualController();
            mcTestObj.Speed = 50;

            // Verify
            Assert.AreEqual(50, mcTestObj.Speed);
        }
        #endregion
        #region Axis functions
        [Test]
        public void moveAxisBase_CalledWithArgLeft_CallsRobotmoveBase()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveBase(Arg<int>.Is.Anything)).Return(true);
 
            // Test
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveBase(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisBase_CalledWithArgLeft_CallsRobotmoveBaseWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveBase(Arg<int>.Is.Anything)).Return(true);
 
            // Test
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_LEFT);
            
            // Verify
            irMock.AssertWasCalled(t => t.moveBase(-mcTestObj.Speed));
        }

        [Test]
        public void moveAxisBase_ReturnsFalse_CastExceptions()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveBase(Arg<int>.Is.Anything)).Return(false);

            // Verify
            Assert.Catch(() => mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_LEFT));
        }



        [Test]
        public void moveAxisBase_CalledWithArgRight_CallsRobotmoveBaseWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveBase(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT);
            
            // Verify
            irMock.AssertWasCalled(t => t.moveBase(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisShoulder_CalledWithArgLeft_CallsRobotmoveShoulder()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveShoulder(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveShoulder(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisShoulder_CalledWithArgLeft_CallsRobotmoveShoulderWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveShoulder(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveShoulder(-mcTestObj.Speed));
        }

        [Test]
        public void moveAxisShoulder_ReturnsFalse_CastExceptions()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveShoulder(Arg<int>.Is.Anything)).Return(false);

            // Verify
            Assert.Catch(() => mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_LEFT));
        }
        
        [Test]
        public void moveAxisShoulder_CalledWithArgRight_CallsRobotmoveShoulderWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveShoulder(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT);

            // Verify
            irMock.AssertWasCalled(t => t.moveShoulder(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisElbow_CalledWithArgLeft_CallsRobotmoveElbow()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveElbow(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveElbow(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisElbow_CalledWithArgLeft_CallsRobotmoveElbowWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveElbow(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveElbow(-mcTestObj.Speed));
        }

        [Test]
        public void moveAxisElbow_ReturnsFalse_CastExceptions()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveElbow(Arg<int>.Is.Anything)).Return(false);

            // Verify
            Assert.Catch(() => mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_LEFT));

        }

        [Test]
        public void moveAxisElbow_CalledWithArgRight_CallsRobotmoveElbowWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveElbow(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT);

            // Verify
            irMock.AssertWasCalled(t => t.moveElbow(mcTestObj.Speed));
        }
        [Test]
        public void moveAxisGripper_CalledWithArgOpen_CallsRobotopenGripper()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.openGripper()).Return(true);

            // Test
            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_OPEN);

            // Verify
            irMock.AssertWasCalled(t => t.openGripper());
        }
        [Test]
        public void moveAxisGripper_CalledWithArgClose_CallsRobotcloseGripper()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.closeGripper()).Return(true);

            // Test
            mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_CLOSE);

            // Verify
            irMock.AssertWasCalled(t => t.closeGripper());
        }

        [Test]
        public void moveAxisGripper_ReturnsFalseOnCloseGripper_CastExceptions()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.closeGripper()).Return(false);

            // Verify
            Assert.Catch(() => mcTestObj.moveAxisGripper(enumCloseOpen.MANUAL_CLOSE));
        }

        [Test]
        public void moveAxisPitch_CalledWithArgDown_CallsRobotmoveWristPitch()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveWristPitch(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_DOWN);

            // Verify
            irMock.AssertWasCalled(t => t.moveWristPitch(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisPitch_CalledWithArgDown_CallsRobotmoveWristPitchWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveWristPitch(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_DOWN);

            // Verify
            irMock.AssertWasCalled(t => t.moveWristPitch(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisPitch_CalledWithArgUp_CallsRobotmoveWristPitchWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveWristPitch(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP);

            // Verify
            irMock.AssertWasCalled(t => t.moveWristPitch(mcTestObj.Speed));
        }

        [Test]
        public void moveAxisPitch_ReturnsFalse_CastExceptions()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveWristPitch(Arg<int>.Is.Anything)).Return(false);

            // Verify
            Assert.Catch(() => mcTestObj.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP));
        }

        [Test]
        public void moveAxisRoll_CalledWithArgLeft_CallsRobotmoveWristRoll()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveWristRoll(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveWristRoll(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisRoll_CalledWithArgLeft_CallsRobotmoveWristRollWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveWristRoll(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveWristRoll(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisRoll_CalledWithArgRight_CallsRobotmoveWristRollWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveWristRoll(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT);

            // Verify
            irMock.AssertWasCalled(t => t.moveWristRoll(mcTestObj.Speed));
        }

        [Test]
        public void moveAxisRoll_ReturnsFalse_CastExceptions()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveWristRoll(Arg<int>.Is.Anything)).Return(false);

            // Verify
            Assert.Catch(() => mcTestObj.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT));
        }
        
        [Test]
        public void moveAxisConveyer_CalledWithArgLeft_CallsRobotmoveConveyerBelt()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveConveyerBelt(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveConveyerBelt(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveAxisConveyer_CalledWithArgLeft_CallsRobotmoveConveyerBeltWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveConveyerBelt(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_LEFT);

            // Verify
            irMock.AssertWasCalled(t => t.moveConveyerBelt(-mcTestObj.Speed));
        }
        [Test]
        public void moveAxisConveyer_CalledWithArgRight_CallsRobotmoveConveyerBeltWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveConveyerBelt(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT);

            // Verify
            irMock.AssertWasCalled(t => t.moveConveyerBelt(mcTestObj.Speed));
        }

        [Test]
        public void moveAxisConveyer_ReturnsFalse_CastExceptions()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveConveyerBelt(Arg<int>.Is.Anything)).Return(false);

            // Verify
            Assert.Catch(() => mcTestObj.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT));
        }

        #endregion
        #region Move Coordinates
        [Test]
        public void moveCoordX_CalledWithArgDecreasing_CallsRobotmoveByXCoordinate()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByXCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByXCoordinate(Arg<int>.Is.Anything));
        }

        [Test]
        public void moveCoordX_CalledWithArgDecreasing_CallsRobotmoveByXCoordinate_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByXCoordinate(Arg<int>.Is.Anything)).Return(false);

            
            Assert.Catch(() => mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_DEC));
        }

        [Test]
        public void moveCoordX_CalledWithArgIncreasing_CallsRobotmoveByXCoordinate()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByXCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByXCoordinate(Arg<int>.Is.Anything));
        }
        
        [Test]
        public void moveCoordX_CalledWithArgDecreasing_CallsRobotmoveByXCoordinateWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByXCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByXCoordinate(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordX_CalledWithArgIncreasing_CallsRobotmoveByXCoordinateWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByXCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByXCoordinate(mcTestObj.Speed));
        }


        [Test]
        public void moveCoordX_CalledWithArgDecreasing_CallsRobotmoveByXCoordinateWithNegativeSpeedValue_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByXCoordinate(Arg<int>.Is.Anything)).Return(false);


            Assert.Catch(() => mcTestObj.moveCoordX(enumIncDec.MANUAL_MOVE_INC));
        }

        [Test]
        public void moveCoordY_CalledWithArgDecreasing_CallsRobotmoveByYCoordinate()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByYCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByYCoordinate(Arg<int>.Is.Anything));
        }

        [Test]
        public void moveCoordY_CalledWithArgDecreasing_CallsRobotmoveByYCoordinate_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByYCoordinate(Arg<int>.Is.Anything)).Return(false);

            // Test
            Assert.Catch(() => mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_DEC));
        }

        [Test]
        public void moveCoordY_CalledWithArgIncreasing_CallsRobotmoveByYCoordinate()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByYCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByYCoordinate(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordY_CalledWithArgDecreasing_CallsRobotmoveByYCoordinateWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByYCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByYCoordinate(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordY_CalledWithArgIncreasing_CallsRobotmoveByYCoordinateWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByYCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByYCoordinate(mcTestObj.Speed));
        }

        [Test]
        public void moveCoordY_CalledWithArgIncreasing_CallsRobotmoveByYCoordinateWithPositiveSpeedValue_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByYCoordinate(Arg<int>.Is.Anything)).Return(false);

            // Test
            Assert.Catch(() => mcTestObj.moveCoordY(enumIncDec.MANUAL_MOVE_INC));
        }

        [Test]
        public void moveCoordZ_CalledWithArgDecreasing_CallsRobotmoveByZCoordinate()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByZCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByZCoordinate(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordZ_CalledWithArgIncreasing_CallsRobotmoveByZCoordinate()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByZCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByZCoordinate(Arg<int>.Is.Anything));
        }

        [Test]
        public void moveCoordZ_CalledWithArgIncreasing_CallsRobotmoveByZCoordinate_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByZCoordinate(Arg<int>.Is.Anything)).Return(false);

            // Test
            Assert.Catch(() => mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_INC));
        }

        [Test]
        public void moveCoordZ_CalledWithArgDecreasing_CallsRobotmoveByZCoordinateWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByZCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByZCoordinate(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordZ_CalledWithArgIncreasing_CallsRobotmoveByZCoordinateWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByZCoordinate(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByZCoordinate(mcTestObj.Speed));
        }

        [Test]
        public void moveCoordZ_CalledWithArgDecreasing_CallsRobotmoveByZCoordinateWithPositiveSpeedValue_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByZCoordinate(Arg<int>.Is.Anything)).Return(false);

            // Test
            Assert.Catch(() => mcTestObj.moveCoordZ(enumIncDec.MANUAL_MOVE_DEC));

        }

        [Test]
        public void moveCoordPitch_CalledWithArgDecreasing_CallsRobotmoveByPitch()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByPitch(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByPitch(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordPitch_CalledWithArgIncreasing_CallsRobotmoveByPitch()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByPitch(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByPitch(Arg<int>.Is.Anything));
        }

        [Test]
        public void moveCoordPitch_CalledWithArgIncreasing_CallsRobotmoveByPitch_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByPitch(Arg<int>.Is.Anything)).Return(false);

            // Test
            Assert.Catch(() => mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC));

        }

        [Test]
        public void moveCoordPitch_CalledWithArgDecreasing_CallsRobotmoveByPitchWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByPitch(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByPitch(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordPitch_CalledWithArgIncreasing_CallsRobotmoveByPitchWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByPitch(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByPitch(mcTestObj.Speed));
        }

        [Test]
        public void moveCoordPitch_CalledWithArgDecreasing_CallsRobotmoveByPitchWithPositiveSpeedValue_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByPitch(Arg<int>.Is.Anything)).Return(false);

            // Test
            Assert.Catch(() => mcTestObj.moveCoordPitch(enumIncDec.MANUAL_MOVE_DEC));

        }

        [Test]
        public void moveCoordRoll_CalledWithArgDecreasing_CallsRobotmoveByRoll()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByRoll(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByRoll(Arg<int>.Is.Anything));
        }
        [Test]
        public void moveCoordRoll_CalledWithArgIncreasing_CallsRobotmoveByRoll()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByRoll(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByRoll(Arg<int>.Is.Anything));
        }

        [Test]
        public void moveCoordRoll_CalledWithArgIncreasing_CallsRobotmoveByRoll_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.moveByRoll(Arg<int>.Is.Anything)).Return(false);

            // Test
            Assert.Catch(() => mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC));
        }

        [Test]
        public void moveCoordRoll_CalledWithArgDecreasing_CallsRobotmoveByRollWithNegativeSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByRoll(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_DEC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByRoll(-mcTestObj.Speed));
        }
        [Test]
        public void moveCoordRoll_CalledWithArgIncreasing_CallsRobotmoveByRollWithPositiveSpeedValue()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByRoll(Arg<int>.Is.Anything)).Return(true);

            // Test
            mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC);

            // Verify
            irMock.AssertWasCalled(t => t.moveByRoll(mcTestObj.Speed));
        }

        [Test]
        public void moveCoordRoll_CalledWithArgDecreasing_CallsRobotmoveByRollWithPositiveSpeedValue_ReturnFalse_ExpectException()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            mcTestObj.Speed = 50;
            irMock.Stub(t => t.moveByRoll(Arg<int>.Is.Anything)).Return(false);

            // Test
            Assert.Catch(() => mcTestObj.moveCoordRoll(enumIncDec.MANUAL_MOVE_DEC));
        }

        #endregion

        #region Other Functions
        //Stop All movements
        [Test]
        public void stopAllMovement_CheckForWasCalled()
        {
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.stopAllMovement()).Return(true);

            mcTestObj.stopAllMovement();

            irMock.AssertWasCalled(t => t.stopAllMovement());
        }


        //Time Test
        [Test]
        public void setTimeSecond_Checking_ForBeingCalled()
        {
            // Setup
            IRobot irMock = MockRepository.GenerateMock<IRobot>();
            mcTestObj = new ManualController();
            mcTestObj.RobotConnection = irMock;
            irMock.Stub(t => t.Time(Arg<Wrapper.enumAxisSettings>.Is.Anything, Arg<long>.Is.Anything)).Return(true);

            // Test
            mcTestObj.setTimeSecond(200);

            // Verify
            irMock.AssertWasCalled(t => t.Time(Arg<Wrapper.enumAxisSettings>.Is.Anything, Arg<long>.Is.Anything));
        }
        #endregion
    }
}
