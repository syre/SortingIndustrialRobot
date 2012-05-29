using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using SqlInteraction;

namespace SIR.Tests
{
    [TestFixture]
    public class RobotSQLConnectionTests
    {
        // Tests
        #region Constructor
        [Test]
        public void RobotSQLConnection_IsCalled_RobotConnectionStateIsNotNull()
        {
            // Test

            // Verify
        }
        #endregion
        #region Properties
        [Test]
        public void Connectionstring_SetsIt_GetsSaved()
        {
            
        }
        #endregion
        #region Functions
        [Test]
        public void ConnectionOpen_IsCalled_CallsRobotConnectionOpen()
        {
            
        }
        [Test]
        public void ConnectionClose_IsCalled_CallsRobotConnectionClose()
        {
            
        }
        [Test]
        public void CreateCommand_IsCalled_CallsRobotConnectionStateCreate()
        {
            
        }
        [Test] public void TimeOut_IsCalled_CallsRobotConnectionConnectionTimeout()
        {
            
        }
        #endregion
    }
}
