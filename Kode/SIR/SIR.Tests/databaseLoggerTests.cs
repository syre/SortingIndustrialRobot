using System;
using System.Data;
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
        private DatabaseLogger testObj;

        [SetUp]
        public void Setup()
        {
            testObj = new DatabaseLogger("TestThread");
        }

        [TearDown]
        public void Teardown()
        {
            testObj = null;
            Factory.getThreadHandlingInstance.removeThread("TestThread");
        }

        // Tests
        #region Properties
        [Test]
        public void SQLHandlerObj_SetsValue_GetsSaved()
        {
            // Setup
            ISQLHandler isqlhStub = MockRepository.GenerateStub<ISQLHandler>();

            // Test
            testObj.SQLHandlerObj = isqlhStub;

            // Verify
            Assert.AreSame(testObj.SQLHandlerObj, isqlhStub);
        }
        #endregion
        #region Functions
        [Test]
        public void DatabaseLogger_IsCalled_SQLHandlerObjIsNotNull()
        {
            // Setup
            // Lavet i setup

            // Verify
            Assert.IsNotNull(testObj.SQLHandlerObj);
        }
        [Test]
        public void log_IsCalled_CallsSQLHandlerObjmakeCommand()
        {
            // Setup
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();
            ISQLReader isqlrdrStub = MockRepository.GenerateStub<ISQLReader>();
            isqlhMock.Stub(t => t.makeCommand(Arg<string>.Is.Anything)).Return(new SqlCommand());
            isqlhMock.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(isqlrdrStub);

            // Test
            testObj.SQLHandlerObj = isqlhMock;
            testObj.log("Hola", eLogType.LOG_DEBUG);
            testObj.prepForShutdownApp();

            // Verify
            isqlhMock.AssertWasCalled(t => t.makeCommand(Arg<string>.Is.Anything));
        }
        [Test]
        public void log_IsCalled_CallsSQLHandlerObjrunQuery()
        {
            // Setup
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();
            ISQLReader isqlrdrStub = MockRepository.GenerateStub<ISQLReader>();
            isqlhMock.Stub(t => t.makeCommand(Arg<string>.Is.Anything)).Return(new SqlCommand());
            isqlhMock.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(isqlrdrStub);

            // Test
            testObj.SQLHandlerObj = isqlhMock;
            testObj.log("Hola", eLogType.LOG_DEBUG);
            testObj.prepForShutdownApp();

            // Verify
            isqlhMock.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything));
        }
        [Test]
        public void log_IsCalledWithLogTypeLOG_INFO_CallsSQLHandlerObjmakeCommandWithCorrectText()
        {
            // Setup
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();

            // Test
            testObj.SQLHandlerObj = isqlhMock;
            testObj.log("Hola", eLogType.LOG_INFO);
            testObj.prepForShutdownApp();

            // Verify
            isqlhMock.AssertWasCalled(t => t.addParameter(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Matches(Rhino.Mocks.Constraints.Text.Contains("Info")), Arg<SqlDbType>.Is.Anything));
        }
        [Test]
        public void log_IsCalledWithLogTypeLOG_DEBUG_CallsSQLHandlerObjmakeCommandWithCorrectText()
        {
            // Setup
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();

            // Test
            testObj.SQLHandlerObj = isqlhMock;
            testObj.log("Hola", eLogType.LOG_DEBUG);
            testObj.prepForShutdownApp();

            // Verify
            isqlhMock.AssertWasCalled(t => t.addParameter(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Matches(Rhino.Mocks.Constraints.Text.Contains("Debug")), Arg<SqlDbType>.Is.Anything));
        }
        [Test]
        public void log_IsCalledWithLogTypeLOG_ERROR_CallsSQLHandlerObjmakeCommandWithCorrectText()
        {
            // Setup
            ISQLHandler isqlhMock = MockRepository.GenerateMock<ISQLHandler>();

            // Test
            testObj.SQLHandlerObj = isqlhMock;
            testObj.log("Hola", eLogType.LOG_ERROR);
            testObj.prepForShutdownApp();

            // Verify
            isqlhMock.AssertWasCalled(t => t.addParameter(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Matches(Rhino.Mocks.Constraints.Text.Contains("Error")), Arg<SqlDbType>.Is.Anything));
        }
        #endregion
    }
}