using System;
using DSL;
using NUnit.Framework;
using Rhino.Mocks;

namespace SIR.Tests
{
    [TestFixture]
    public class WrapperTests
    {
        private DLL.DgateCallBack dgateEventHandlerSuccess = initSuccess;
        private DLL.DgateCallBack dgateEventHandlerError = initError;
        private DLL.DgateCallBackByteRefArg dgateEventHandlerHoming = homeEvent;

        static void initSuccess(IntPtr _iptrArg)
        {
            
        }
        static void initError(IntPtr _iptrArg)
        {
           
        }
        static void homeEvent(ref byte _bArg)
        {
           
        }

        // Tests
        #region Constructor
        [Test]
        public void Wrapper_IsCalled_DLLPropertyIsNotNull()
        {
            
        }
        #endregion

        #region Initialization and settings
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitialization()
        {

        }
        [Test]
        public void initializationWrapped_DllInitializationReturns1_ReturnsTrue()
        {

        }
        [Test]
        public void initializationWrapped_DllInitializationReturns0_ReturnsFalse()
        {

        }
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitializationWithRightSystemModeParameter()
        {
            
        }
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitializationWithRightSystemTypeParameter()
        {

        }
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitializationWithRightSuccessCallBackParameter()
        {

        }
        [Test]
        public void initializationWrapped_IsCalled_CallsDllInitializationWithRightErrorCallBackParameter()
        {

        }
        [Test]
        public void controlWrapped_IsCalled_CallsDllControl()
        {

        }
        [Test]
        public void controlWrapped_DllControlReturns1_ReturnsTrue()
        {
            
        }
        [Test]
        public void controlWrapped_DllControlReturns0_ReturnsFalse()
        {

        }
        [Test]
        public void controlWrapped_IsCalled_CallsDllWithRightAxisSettingsParameter()
        {

        }
        [Test]
        public void controlWrapped_IsCalled_CallsDllWithRightControlParam()
        {

        }
        [Test]
        public void isOnlineOkWrapped_IsCalled_CallsDllIsOnLineOk()
        {
            
        }
        [Test]
        public void isOnlineOkWrapped_DllIsOnLineOkReturns0_ReturnsFalse()
        {

        }
        [Test]
        public void isOnlineOkWrapped_DllIsOnLineOkReturns1_ReturnsTrue()
        {

        }
        #endregion
        #region Movement
        [Test]
        public void homeWrapped_IsCalled_CallsDllHome()
        {
            
        }
        [Test]
        public void homeWrapped_DllHomeReturns1_ReturnsTrue()
        {

        }
        [Test]
        public void homeWrapped_DllHomeReturns0_ReturnsFalse()
        {

        }
        [Test]
        public void homeWrapped_IsCalled_CallsDllHomeWithRightAxisSettingsParameter()
        {

        }
        [Test]
        public void homeWrapped_IsCalled_CallsDllHomeWithRightCallBackParameter()
        {

        }
        [Test]
        public void enterManualWrapped_IsCalled_CallsDllEnterManual()
        {
            
        }
        [Test]
        public void enterManualWrapped_DllEnterManualReturns0_ReturnsFalse()
        {
            
        }
        [Test]
        public void enterManualWrapped_DllEnterManualReturns1_ReturnsTrue()
        {

        }
        [Test]
        public void enterManualWrapped_ParamIsAxes_DllEnterManualCalledWith0()
        {

        }
        [Test]
        public void enterManualWrapped_ParamIsCoord_DllEnterManualCalledWith1()
        {

        }
        [Test]
        public void closeManualWrapped_IsCalled_CallsDllCloseManual()
        {
            
        }
        [Test]
        public void closeManual_DllCloseManualReturns0_ReturnsFalse()
        {
            
        }
        [Test]
        public void closeManual_DllCloseManualReturns1_ReturnsTrue()
        {

        }
        [Test]
        public void moveManualWrapped_IsCalled_CallsDllMoveManual()
        {
            
        }
        [Test]
        public void moveManualWrapped_DllMoveManualReturns0_ReturnsFalse()
        {

        }
        [Test]
        public void moveManualWrapped_DllMoveManualReturns1_ReturnsTrue()
        {

        }
        [Test]
        public void moveManualWrapped_IsCalled_CallsDllMoveManualWithRightManualModeParameter()
        {

        }
        [Test]
        public void moveManualWrapped_IsCalled_CallsDllMoveManualWithRightSpeedParameter()
        {

        }
        [Test]
        public void stopWrapped_IsCalled_CallsDllStop()
        {
            
        }
        [Test]
        public void stopWrapped_DllStopReturns0_ReturnsFalse()
        {

        }
        [Test]
        public void stopWrapped_DllStopReturns1_ReturnsTrue()
        {

        }
        [Test]
        public void stopWrapped_IsCalled_CallsDllStopWithRightAxisParameter()
        {

        }
        [Test]
        public void moveLinearWrapped_IsCalled_CallsDllMoveLinear()
        {
            
        }
        [Test]
        public void moveLinearWrapped_DllMoveLinearReturns0_ReturnsFalse()
        {

        }
        [Test]
        public void moveLinearWrapped_DllMoveLinearReturns1_ReturnsTrue()
        {

        }
        [Test]
        public void moveLinearWrapped_IsCalled_DllMoveLinearIsCalledWithRightStringParameter()
        {

        }
        [Test]
        public void moveLinearWrapped_IsCalled_DllMoveLinearIsCalledWithRightIntParameter()
        {

        }
        #endregion
        #region Gripper
        #endregion
        #region Event handling
        #endregion
        #region Vectors
        #endregion
    }
}
