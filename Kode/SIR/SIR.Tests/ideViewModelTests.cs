using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using ControlSystem;
using DSL;
using NUnit.Framework;
using Rhino.Mocks;
using RoboGO;
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
        [STAThread]
        public void IDEViewModel_CallsIt_CodeIsNotNull()
        {
            // Test
            idevmTestObj = new IDEViewModel(new TabControl());

            // Verify
            Assert.IsTrue(idevmTestObj.Code != null);
        }
        [Test]
        [STAThread]
        public void IDEViewModel_CallsIt_ScriptRunnerIsSetToFactoryScriptRunner()
        {
            // Test
            idevmTestObj = new IDEViewModel(new TabControl());

            // Verify
            Assert.IsTrue(idevmTestObj.ScriptExecuter == Factory.getScriptRunnerInstance);
        }
        #endregion
        #region Properties
        [Test]
        [STAThread]
        public void ScriptExecuter_SetsIt_ScriptExecutionerIsSaved()
        {
            // Setup
            idevmTestObj = new IDEViewModel(new TabControl());
            IScriptRunner isrRunner = MockRepository.GenerateStub<IScriptRunner>();

            // Test
            idevmTestObj.ScriptExecuter = isrRunner;

            // Verify
            Assert.AreSame(isrRunner, idevmTestObj.ScriptExecuter);
        }
        [Test]
        [STAThread]
        [ExpectedException]
        public void ScriptExecuter_SetsItToNull_ThrowsException()
        {
            // Setup
            idevmTestObj = new IDEViewModel(new TabControl());

            // Test
            idevmTestObj.ScriptExecuter = null;
        }
        [Test]
        [STAThread]
        public void Code_SetsItToHello_CodeIsSaved()
        {
            // Setup
            idevmTestObj = new IDEViewModel(new TabControl());

            // Test
            idevmTestObj.Code = "Hello";

            // Verify
            Assert.AreEqual("Hello", idevmTestObj.Code);
        }
        [Test]
        [STAThread]
        public void ExecuteComd_CallsIt_DoesNotReturnNull()
        {
            // Setup
            idevmTestObj = new IDEViewModel(new TabControl());
            ExecuteCommand ecHolder;

            // Test
            ecHolder = idevmTestObj.ExecuteComd;

            // Verify
            Assert.AreNotEqual(null, ecHolder);

        }
        #endregion
        #region executeCode
        [Test]
        [STAThread]
        public void executeCode_CallsIt_CallsScriptRunnersetScriptFromString()
        {
            // Setup
            idevmTestObj = new IDEViewModel(new TabControl());
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.setScriptFromString(Arg<string>.Is.Anything));
        }
        [Test]
        [STAThread]
        public void executeCode_CallsIt_CallsScriptRunnersetScriptFromStringEqualCodeProperty()
        {
            // Setup
            idevmTestObj = new IDEViewModel(new TabControl());
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;
            idevmTestObj.Code = "Hello";

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.setScriptFromString(idevmTestObj.Code));
        }
        [Test]
        [STAThread]
        public void executeCode_CallsIt_CallsScriptRunnerExecuteScript()
        {
            // Setup
            idevmTestObj = new IDEViewModel(new TabControl());
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.ExecuteScript());
        }
        [Test]
        [STAThread]
        public void executeCode_CallsIt_ScriptRunnersetScriptFromStringIsCalledBeforeExecuteScript()
        {
            // Setup
            idevmTestObj = new IDEViewModel(new TabControl());
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
