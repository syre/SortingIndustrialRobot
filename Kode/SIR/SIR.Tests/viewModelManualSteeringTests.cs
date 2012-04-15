using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using RoboGO.ViewModels;
using ControlSystem;

namespace SIR.Tests
{
    [TestFixture]
    public class ViewModelManualSteeringTests
    {
        // Members
        private ViewModelManualSteering vmmsTestObj;

        // Tests
        #region Properties
        [Test]
        public void ManualControl_SetsIt_GetsSaved()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            ManualController mcTmp = new ManualController();
            vmmsTestObj.ManualControl = mcTmp;

            Assert.AreSame(mcTmp, vmmsTestObj.ManualControl);
        }
        [Test]
        public void Speed_SetsItTo50_GetsSetInManualController()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            vmmsTestObj.Speed = 50;

            Assert.AreEqual(50, vmmsTestObj.Speed);
        }
        [Test]
        public void Speed_SetsItTo50_ManualControllerSpeedPropertyCalled()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcTmp = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcTmp;
            vmmsTestObj.Speed = 50;

            imcTmp.AssertWasCalled(t => t.Speed = 50);
        }
        #endregion

        // -Functions
        #region Axis
        [Test]
        public void moveAxisBaseRight_Called_CallsManualControllermoveAxisBaseWithMANUAL_MOVE_RIGHT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisBaseRight();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisBase(enumLeftRight.MANUAL_MOVE_RIGHT));
        }
        [Test]
        public void moveAxisBaseLeft_Called_CallsManualControllermoveAxisBaseWithMANUAL_MOVE_LEFT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisBaseLeft();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisBase(enumLeftRight.MANUAL_MOVE_LEFT));
        }
        [Test]
        public void moveAxisShoulderRight_Called_CallsManualControllermoveAxisShoulderWithMANUAL_MOVE_RIGHT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisShoulderRight();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_RIGHT));
        }
        [Test]
        public void moveAxisShoulderLeft_Called_CallsManualControllermoveAxisShoulderWithMANUAL_MOVE_LEFT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisShoulderLeft();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisShoulder(enumLeftRight.MANUAL_MOVE_LEFT));
        }
        [Test]
        public void moveAxisElbowRight_Called_CallsManualControllermoveAxisElbowWithMANUAL_MOVE_RIGHT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisElbowRight();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisElbow(enumLeftRight.MANUAL_MOVE_RIGHT));
        }
        [Test]
        public void moveAxisElbowLeft_Called_CallsManualControllermoveAxisElbowWithMANUAL_MOVE_LEFT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisElbowLeft();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisElbow(enumLeftRight.MANUAL_MOVE_LEFT));
        }
        [Test]
        public void openGripper_Called_CallsManualControllermoveAxisGripperWithMANUAL_OPEN()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.openGripper();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisGripper(enumCloseOpen.MANUAL_OPEN));
        }
        [Test]
        public void closeGripper_Called_CallsManualControllermoveAxisGripperWithMANUAL_CLOSE()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.closeGripper();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisGripper(enumCloseOpen.MANUAL_CLOSE));
        }
        [Test]
        public void moveAxisPitchUp_Called_CallsManualControllermoveAxisPitchWithMANUAL_MOVE_UP()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisPitchUp();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisPitch(enumUpDown.MANUAL_MOVE_UP));
        }
        [Test]
        public void moveAxisPitchDown_Called_CallsManualControllermoveAxisPitchWithMANUAL_MOVE_DOWN()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisPitchDown();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisPitch(enumUpDown.MANUAL_MOVE_DOWN));
        }
        [Test]
        public void moveAxisRollRight_Called_CallsManualControllermoveAxisRollWithMANUAL_MOVE_RIGHT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisRollRight();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisRoll(enumLeftRight.MANUAL_MOVE_RIGHT));
        }
        [Test]
        public void moveAxisRollLeft_Called_CallsManualControllermoveAxisRollWithMANUAL_MOVE_LEFT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisRollLeft();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisRoll(enumLeftRight.MANUAL_MOVE_LEFT));
        }
        [Test]
        public void moveAxisConveyerRight_Called_CallsManualControllermoveAxisConveyerWithMANUAL_MOVE_RIGHT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisConveyerRight();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_RIGHT));
        }
        [Test]
        public void moveAxisConveyerLeft_Called_CallsManualControllermoveAxisConveyerWithMANUAL_MOVE_LEFT()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveAxisConveyerLeft();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveAxisConveyer(enumLeftRight.MANUAL_MOVE_LEFT));
        }
        #endregion
        #region Coordinates
        [Test]
        public void moveCoordXIncreasing_Called_CallsManualControllermoveCoordXWithMANUAL_MOVE_INC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordXIncreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordX(enumIncDec.MANUAL_MOVE_INC));
        }
        [Test]
        public void moveCoordXDecreasing_Called_CallsManualControllermoveCoordXWithMANUAL_MOVE_DEC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordXDecreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordX(enumIncDec.MANUAL_MOVE_DEC));
        }
        [Test]
        public void moveCoordYIncreasing_Called_CallsManualControllermoveCoordYWithMANUAL_MOVE_INC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordYIncreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordY(enumIncDec.MANUAL_MOVE_INC));
        }
        [Test]
        public void moveCoordYDecreasing_Called_CallsManualControllermoveCoordYWithMANUAL_MOVE_DEC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordYDecreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordY(enumIncDec.MANUAL_MOVE_DEC));
        }
        [Test]
        public void moveCoordZIncreasing_Called_CallsManualControllermoveCoordZWithMANUAL_MOVE_INC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordZIncreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordZ(enumIncDec.MANUAL_MOVE_INC));
        }
        [Test]
        public void moveCoordZDecreasing_Called_CallsManualControllermoveCoordZWithMANUAL_MOVE_DEC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordZDecreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordZ(enumIncDec.MANUAL_MOVE_DEC));
        }
        [Test]
        public void moveCoordPitchIncreasing_Called_CallsManualControllermoveCoordPitchWithMANUAL_MOVE_INC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordPitchIncreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordPitch(enumIncDec.MANUAL_MOVE_INC));
        }
        [Test]
        public void moveCoordPitchDecreasing_Called_CallsManualControllermoveCoordPitchWithMANUAL_MOVE_DEC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordPitchDecreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordPitch(enumIncDec.MANUAL_MOVE_DEC));
        }
        [Test]
        public void moveCoordRollIncreasing_Called_CallsManualControllermoveCoordRollWithMANUAL_MOVE_INC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordRollIncreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordRoll(enumIncDec.MANUAL_MOVE_INC));
        }
        [Test]
        public void moveCoordRollDecreasing_Called_CallsManualControllermoveCoordRollWithMANUAL_MOVE_DEC()
        {
            // Setup
            vmmsTestObj = new ViewModelManualSteering();
            IManualController imcManualControllerMock = MockRepository.GenerateMock<IManualController>();
            vmmsTestObj.ManualControl = imcManualControllerMock;

            // Test
            vmmsTestObj.moveCoordRollDecreasing();

            // Verify
            imcManualControllerMock.AssertWasCalled(t => t.moveCoordRoll(enumIncDec.MANUAL_MOVE_DEC));
        }
        #endregion
    }
}
