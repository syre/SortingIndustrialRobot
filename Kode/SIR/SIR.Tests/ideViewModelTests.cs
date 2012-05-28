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

        // Tests
        #region Constructor
        [Test]
        [STAThread]
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
        [STAThread]
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
        [STAThread]
        [ExpectedException]
        public void ScriptExecuter_SetsItToNull_ThrowsException()
        {
            // Setup
            idevmTestObj = new IDEViewModel();

            // Test
            idevmTestObj.ScriptExecuter = null;
        }
        [Test]
        [STAThread]
        public void ExecuteComd_CallsIt_DoesNotReturnNull()
        {
            // Setup
            idevmTestObj = new IDEViewModel();
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
            //TabControl tcControlDummy = new TabControl();
            idevmTestObj = new IDEViewModel();
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;
            TextBox txtBoxDummy = new TextBox();
            TabItem tiItemDummy = new TabItem();
            tiItemDummy.Content = txtBoxDummy;
            txtBoxDummy.Text = "Test";
            //tcControlDummy.Items.Add(tiItemDummy);
            //tcControlDummy.SelectedItem = tiItemDummy;

            // Test
            idevmTestObj.executeCode();

            // Verify
            isrRunner.AssertWasCalled(t => t.setScriptFromString(Arg<string>.Is.Anything));
        }
        [Test]
        [STAThread]
        public void executeCode_CallsIt_CallsScriptRunnerExecuteScript()
        {
            // Setup
            //TabControl tcControlDummy = new TabControl();
            idevmTestObj = new IDEViewModel();
            IScriptRunner isrRunner = MockRepository.GenerateMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;
            TextBox txtBoxDummy = new TextBox();
            TabItem tiItemDummy = new TabItem();
            tiItemDummy.Content = txtBoxDummy;
            txtBoxDummy.Text = "Test";
            //tcControlDummy.Items.Add(tiItemDummy);
            //tcControlDummy.SelectedItem = tiItemDummy;

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
            //TabControl tcControlDummy = new TabControl();
            idevmTestObj = new IDEViewModel();
            MockRepository mockrepo = new MockRepository();
            IScriptRunner isrRunner = mockrepo.DynamicMock<IScriptRunner>();
            idevmTestObj.ScriptExecuter = isrRunner;
            TextBox txtBoxDummy = new TextBox();
            TabItem tiItemDummy = new TabItem();
            tiItemDummy.Content = txtBoxDummy;
            txtBoxDummy.Text = "Test";
            //tcControlDummy.Items.Add(tiItemDummy);
            //tcControlDummy.SelectedItem = tiItemDummy;

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
