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
        public void ScriptExecuter_SetsIt_ScriptExecutionerIsSaved()
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
        [Test]
        public void ExecuteComd_CallsIt_DoesNotReturnNull()
        {
            // Setup
            idevmTestObj = new IDEViewModel();
            ExecuteCommand ecHolder;

            // Test
            ecHolder = idevmTestObj.ExecuteComd;

            // Verify
            Assert.AreNotEqual(null, ecHolder);

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
        public void executeCode_CallsIt_CallsScriptRunnersetScriptFromStringEqualCodeProperty()
        {
            // Setup
            idevmTestObj = new IDEViewModel();
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;
            idevmTestObj.Code = "Hello";

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.setScriptFromString(idevmTestObj.Code));
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
        [Test]
        public void executeCode_CallsIt_ScriptRunnersetScriptFromStringIsCalledBeforeExecuteScript()
        {
            // Setup
            idevmTestObj = new IDEViewModel();
            MockRepository mockrepo = new MockRepository();
            IScriptRunner isrRunner = mockrepo.DynamicMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;

            // Record
            using(mockrepo.Ordered())
            {
                isrRunner.Expect(t => t.setScriptFromString(Arg<string>.Is.Anything));
                isrRunner.Expect(t => t.ExecuteScript());
            }
            isrRunner.Replay();

            // Run
            idevmTestObj.executeCode();

            // Verify
            mockrepo.VerifyAll();
        }
        #endregion
    }
}
