using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using RoboGO.ViewModels;

namespace SIR.Tests
{
    [TestFixture]
    public class DelegateCommandTests
    {
        // Helper functions
        public void nothing()
        {
            // Told you so
        }

        // Tests
        #region Functions
        [Test]
        public void Execute_IsCalled_CallsActionFromConstructor()
        {
            // Setup
            //Action aTmp = nothing;
            Action aTmp = MockRepository.GenerateMock<Action>();
            DelegateCommand dcTestObj = new DelegateCommand(aTmp);

            // Test
            dcTestObj.Execute(null);

            // Verify
            aTmp.AssertWasCalled(t => t.Invoke());
        }

        #endregion
    }
}
