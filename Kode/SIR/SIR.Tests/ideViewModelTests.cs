using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlSystem;
using DSL;
using NUnit.Framework;
using Rhino.Mocks;
using RoboGO.ViewModels;

namespace SIR.Tests
{
    [TestFixture]
    class IDEViewModelTests
    {
        // Members
        private IDEViewModel idevmTestObj;

        // Tests
        #region Constructor
        [Test]
        public void IDEViewModel_CallsIt_CodeIsNotNull()
        {
            // Test
            idevmTestObj = new IDEViewModel();

            // Verify
            Assert.IsTrue(idevmTestObj.Code != null);
        }
        [Test]
        public void IDEViewModel_CallsIt_ScriptRunnerIsSetToFactoryScriptRunner()
        {
            // Test
            idevmTestObj = new IDEViewModel();

            // Verify
            Assert.IsTrue(idevmTestObj.ScriptExecutioner == Factory.getScriptRunnerInstance);
        }
        #endregion
        #region Properties
        [Test]
        public void ScriptExecutioner_SetsIt_ScriptExecutionerIsSaved()
        {
            // Setup
            idevmTestObj = new IDEViewModel();
            IScriptRunner isrRunner = MockRepository.GenerateStub<IScriptRunner>();

            // Test
            idevmTestObj.ScriptExecutioner = isrRunner;

            // Verify
            Assert.AreSame(isrRunner, idevmTestObj.ScriptExecutioner);
        }
        [Test]
        [ExpectedException]
        public void ScriptExecutioner_SetsItToNull_ThrowsException()
        {
            // Setup
            idevmTestObj = new IDEViewModel();

            // Test
            idevmTestObj.ScriptExecutioner = null;
        }
        [Test]
        public void Code_SetsItToHello_CodeIsSaved()
        {
            // Setup
            idevmTestObj = new IDEViewModel();

            // Test
            idevmTestObj.Code = "Hello";

            // Verify
            Assert.AreEqual("Hello", idevmTestObj.Code);
        }
        #endregion
        #region executeCode
        [Test]
        public void executeCode_CallsIt_CallsScriptRunnersetScriptFromString()
        {
            // Setup
            idevmTestObj = new IDEViewModel();
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecutioner = isrRunner;

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.setScriptFromString(Arg<string>.Is.Anything));
        }
        [Test]
        public void executeCode_CallsIt_CallsScriptRunnerExecuteScript()
        {
            // Setup
            idevmTestObj = new IDEViewModel();
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecutioner = isrRunner;

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.ExecuteScript());
        }
        #endregion
    }
}
