using System;
using System.Windows.Controls;
using ControlSystem;
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

        // Setup
        [SetUp]
        public void setup()
        {
            idevmTestObj = new IDEViewModel();
        }

        [TearDown]
        public void tearDown()
        {
            Factory.getThreadHandlingInstance.removeThread("ExecuteScript");
        }

        // Tests
        #region Constructor
        [Test]
        [STAThread]
        public void IDEViewModel_CallsIt_ScriptRunnerIsSetToFactoryScriptRunner()
        {
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
            IScriptRunner isrRunner = MockRepository.GenerateStub<IScriptRunner>();

            // Test
            idevmTestObj.ScriptExecuter = isrRunner;
            IScriptRunner isrTmp = idevmTestObj.ScriptExecuter;

            // Verify
            Assert.AreSame(isrRunner, isrTmp);
        }
        [Test]
        [STAThread]
        [ExpectedException]
        public void ScriptExecuter_SetsItToNull_ThrowsException()
        {
            // Test
            idevmTestObj.ScriptExecuter = null;
        }
        [Test]
        [STAThread]
        public void ExecuteComd_CallsIt_DoesNotReturnNull()
        {
            // Setup
            DelegateCommand ecHolder;

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
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.setScriptFromString(Arg<string>.Is.Anything));
        }
        #endregion
        #region Others
        [STAThread]
        [Test]
        public void CodeClear_IsCalled_CallsScriptRunnerclearOutputStream()
        {
            // Setup
            idevmTestObj.ScriptExecuter = MockRepository.GenerateMock<IScriptRunner>();

            // Test
            idevmTestObj.CodeClear();

            // Verify
            idevmTestObj.ScriptExecuter.AssertWasCalled(t => t.clearOutputStream());
        }
        #endregion
    }
}
