using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            IDatabaseSQLConnection idscStub = MockRepository.GenerateStub<IDatabaseSQLConnection>();
            RobotSqlConnection rscTestObj = new RobotSqlConnection(idscStub);

            // Verify
            Assert.IsNotNull(rscTestObj.RobotConnectionState);
        }
        #endregion
        #region Properties
        [Test]
        public void Connectionstring_IsCalled_ReturnsRobotConnectionConnectionStringValue()
        {
            // Setup
            IDatabaseSQLConnection idscMock = MockRepository.GenerateMock<IDatabaseSQLConnection>();
            idscMock.Stub(t => t.ConnectionString).Return("Test");
            RobotSqlConnection rscTestObj = new RobotSqlConnection(idscMock);

            // Test
            string sConString = rscTestObj.Connectionstring;

            // Verify
            Assert.AreEqual("Test", sConString);
        }
        [Test]
        public void Connection_SetsIt_GetsSaved()
        {
            // Setup
            IDatabaseSQLConnection idscStub = MockRepository.GenerateStub<IDatabaseSQLConnection>();
            RobotSqlConnection rscTestObj = new RobotSqlConnection(idscStub);
            IDatabaseSQLConnection idscStub2 = MockRepository.GenerateMock<IDatabaseSQLConnection>();

            // Test
            rscTestObj.Connection = idscStub2;

            // Verify
            Assert.AreEqual(idscStub2, rscTestObj.Connection);
        }
        [Test]
        public void RobotConnectionState_IsCalled_ReturnsRightStateFromConnection()
        {
            // Setup
            IDatabaseSQLConnection idscStub = MockRepository.GenerateStub<IDatabaseSQLConnection>();
            RobotSqlConnection rscTestObj = new RobotSqlConnection(idscStub);
            idscStub.Stub(t => t.State).Return(ConnectionState.Open);

            // Test
            ConnectionState csState = rscTestObj.RobotConnectionState;

            // Verify
            Assert.AreEqual(ConnectionState.Open, rscTestObj.RobotConnectionState);
        }
        #endregion
        #region Functions
        [Test]
        public void ConnectionOpen_IsCalled_CallsConnectionOpen()
        {
            // Setup
            IDatabaseSQLConnection idscMock = MockRepository.GenerateMock<IDatabaseSQLConnection>();
            RobotSqlConnection rscTestObj = new RobotSqlConnection(idscMock);

            // Test
            rscTestObj.ConnectionOpen();

            // Verify
            idscMock.AssertWasCalled(t => t.open());
        }
        [Test]
        public void ConnectionClose_IsCalled_CallsRobotConnectionClose()
        {
            // Setup
            IDatabaseSQLConnection idscMock = MockRepository.GenerateMock<IDatabaseSQLConnection>();
            RobotSqlConnection rscTestObj = new RobotSqlConnection(idscMock);

            // Test
            rscTestObj.ConnectionClose();

            // Verify
            idscMock.AssertWasCalled(t => t.close());
        }
        [Test]
        public void CreateCommand_IsCalled_CallsRobotConnectioncreateCommand()
        {
            // Setup
            IDatabaseSQLConnection idscMock = MockRepository.GenerateMock<IDatabaseSQLConnection>();
            RobotSqlConnection rscTestObj = new RobotSqlConnection(idscMock);

            // Test
            rscTestObj.CreateCommand();

            // Verify
            idscMock.AssertWasCalled(t => t.createCommand());
        }
        [Test]
        public void TimeOut_IsCalled_ReturnsRobotConnectionTimeoutValue()
        {
            // Setup
            IDatabaseSQLConnection idscMock = MockRepository.GenerateMock<IDatabaseSQLConnection>();
            RobotSqlConnection rscTestObj = new RobotSqlConnection(idscMock);
            idscMock.Stub(t => t.connectionTimeout).Return(2);

            // Test
            int iTimeOut = rscTestObj.TimeOut;

            // Verify
            Assert.AreEqual(2, iTimeOut);
        }
        #endregion
    }
}
