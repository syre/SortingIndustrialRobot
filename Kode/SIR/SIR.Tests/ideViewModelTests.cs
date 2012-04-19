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
    public class IDEViewModelTests
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
            Assert.IsTrue(idevmTestObj.ScriptExecuter == Factory.getScriptRunnerInstance);
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
            idevmTestObj.ScriptExecuter = isrRunner;

            // Verify
            Assert.AreSame(isrRunner, idevmTestObj.ScriptExecuter);
        }
        [Test]
        [ExpectedException]
        public void ScriptExecuter_SetsItToNull_ThrowsException()
        {
            // Setup
            idevmTestObj = new IDEViewModel();

            // Test
            idevmTestObj.ScriptExecuter = null;
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
            idevmTestObj.ScriptExecuter = isrRunner;

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
            idevmTestObj.ScriptExecuter = isrRunner;

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.ExecuteScript());
        }
        #endregion
    }
}
