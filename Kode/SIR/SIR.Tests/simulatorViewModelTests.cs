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
    public class SimulatorViewModelTests
    {
        // Tests
        #region Constructor
        [Test]
        public void SimulatorViewModel_IsCalled_UITextPropertyIsNotNull()
        {
            // Test
            SimulatorViewModel svmTestObj = new SimulatorViewModel();

            // Verify
            Assert.IsNotNull(svmTestObj.UIText);
        }
        [Test]
        public void SimulatorViewModel_IsCalled_SetsFactorySimulatorUIOutputToUITextProperty()
        {
            // Test
            SimulatorViewModel svmTestObj = new SimulatorViewModel();

            // Verify
            Assert.AreSame(svmTestObj.UIText, Factory.getSimulatorInstance.IUIOutput);
        }
        [Test]
        public void SimulatorViewModel_IsCalled_SimulatorObjPropertyIsNotNull()
        {
            // Test
            SimulatorViewModel svmTestObj = new SimulatorViewModel();

            // Verify
            Assert.IsNotNull(svmTestObj.SimulatorObj);
        }
        [Test]
        public void SimulatorViewModel_IsCalled_SimulatorObjPropertyIsSameAsFactorys()
        {
            // Test
            SimulatorViewModel svmTestObj = new SimulatorViewModel();

            // Verify
            Assert.AreSame(svmTestObj.SimulatorObj, Factory.getSimulatorInstance);
        }
        #endregion
    }
}
