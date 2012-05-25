using System;
using Rhino.Mocks;
using NUnit.Framework;
using ControlSystem;
using SqlInteraction;
using System.Data.SqlClient;

namespace SIR.Tests
{
    [TestFixture]
    public class DatabaseLoggerTests
    {
        // Tests
        #region Properties
        [Test]
        public void SQLHandlerObj_SetsValue_GetsSaved()
        {
            // Setup
            DatabaseLogger dlTestObj = new DatabaseLogger();
            ISQLHandler isqlhStub = MockRepository.GenerateStub<ISQLHandler>();

            // Test
            dlTestObj.SQLHandlerObj = isqlhStub;

            // Verify
            Assert.AreSame(dlTestObj.SQLHandlerObj, isqlhStub);
        }
        #endregion
        #region Functions
        [Test]
        public void DatabaseLogger_IsCalled_SQLHandlerObjIsNotNull()
        {
            // Setup
            DatabaseLogger dlTestObj = new DatabaseLogger();

            // Verify
            Assert.IsNotNull(dlTestObj.SQLHandlerObj);
        }
        [Test]
        public void log_IsCalled_CallsSQLHandlerObjmakeCommand()
        {
            // Setup
            DatabaseLogger dlTestObj = new DatabaseLogger();
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();
            ISQLReader isqlrdrStub = MockRepository.GenerateStub<ISQLReader>();
            isqlhMock.Stub(t => t.makeCommand(Arg<string>.Is.Anything)).Return(new SqlCommand());
            isqlhMock.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(isqlrdrStub);

            // Test
            dlTestObj.log("Hola", eLogType.LOG_DEBUG);

            // Verify
            isqlhMock.AssertWasCalled(t => t.makeCommand(Arg<string>.Is.Anything));
        }
        [Test]
        public void log_IsCalled_CallsSQLHandlerObjrunQuery()
        {
            // Setup
            DatabaseLogger dlTestObj = new DatabaseLogger();
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();
            ISQLReader isqlrdrStub = MockRepository.GenerateStub<ISQLReader>();
            isqlhMock.Stub(t => t.makeCommand(Arg<string>.Is.Anything)).Return(new SqlCommand());
            isqlhMock.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(isqlrdrStub);

            // Test
            dlTestObj.log("Hola", eLogType.LOG_DEBUG);

            // Verify
            isqlhMock.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void log_IsCalledWithLogTypeLOG_INFO_CallsSQLHandlerObjmakeCommandWithCorrectText()
        {
            // Setup
            DatabaseLogger dlTestObj = new DatabaseLogger();
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();
            ISQLReader isqlrdrStub = MockRepository.GenerateStub<ISQLReader>();
            isqlhMock.Stub(t => t.makeCommand(Arg<string>.Is.Anything)).Return(new SqlCommand());
            isqlhMock.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(isqlrdrStub);

            // Test
            dlTestObj.log("Hola", eLogType.LOG_DEBUG);

            // Verify
            isqlhMock.AssertWasCalled(t => t.makeCommand(Arg<string>.Matches(Rhino.Mocks.Constraints.Text.Contains("Info"))));
        }
        [Test]
        public void log_IsCalledWithLogTypeLOG_DEBUG_CallsSQLHandlerObjmakeCommandWithCorrectText()
        {
            // Setup
            DatabaseLogger dlTestObj = new DatabaseLogger();
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();
            ISQLReader isqlrdrStub = MockRepository.GenerateStub<ISQLReader>();
            isqlhMock.Stub(t => t.makeCommand(Arg<string>.Is.Anything)).Return(new SqlCommand());
            isqlhMock.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(isqlrdrStub);

            // Test
            dlTestObj.log("Hola", eLogType.LOG_DEBUG);

            // Verify
            isqlhMock.AssertWasCalled(t => t.makeCommand(Arg<string>.Matches(Rhino.Mocks.Constraints.Text.Contains("Debug"))));
        }
        [Test]
        public void log_IsCalledWithLogTypeLOG_ERROR_CallsSQLHandlerObjmakeCommandWithCorrectText()
        {
            // Setup
            DatabaseLogger dlTestObj = new DatabaseLogger();
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();
            ISQLReader isqlrdrStub = MockRepository.GenerateStub<ISQLReader>();
            isqlhMock.Stub(t => t.makeCommand(Arg<string>.Is.Anything)).Return(new SqlCommand());
            isqlhMock.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(isqlrdrStub);

            // Test
            dlTestObj.log("Hola", eLogType.LOG_DEBUG);

            // Verify
            isqlhMock.AssertWasCalled(t => t.makeCommand(Arg<string>.Matches(Rhino.Mocks.Constraints.Text.Contains("Error"))));
        }
        #endregion
    }
}