using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using SqlInteraction;

namespace SIR.Tests
{
    [TestFixture]
    public class DatabaseLogTests
    {
        // Tests
        [Test]
        public void getNormalEvents_ThreeAvailableEvents_TheThreeEventsAreReturned()
        {
            // Setup
            List<string> lstRow1 = new List<string>() { "1", "Empty", "12/12/2002", "xx", "52" };
            List<string> lstRow2 = new List<string>() { "2", "Empty", "12/12/2002", "xx", "52" };
            List<string> lstRow3 = new List<string>() { "3", "Empty", "12/12/2002", "xx", "52" };
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(lstRow1).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(lstRow2).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(lstRow3).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            List<ILogEvent> lstEvents = testObj.getNormalEvents();

            // Verify
            Assert.AreEqual(lstEvents[0].LogID, 1);
            Assert.AreEqual(lstEvents[1].LogID, 2);
            Assert.AreEqual(lstEvents[2].LogID, 3);
        }
        [Test]
        public void getNormalEvents_Called_CallsSqlHandlerrunQueryWithRightCommandText()
        {
            // Setup
            List<string> lstRow1 = new List<string>();
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            testObj.getNormalEvents();

            // Verify
            sqlHandler.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Matches(p => p.CommandText == "SELECT [LogID], [LogEvent], [LogTime], [Description], [PositionID] FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable] WHERE LogEvent = 'Normal' GO"), Arg<string>.Is.Anything));
        }
        [Test]
        public void getNormalEvents_Called_CallsSqlHandlerrunQueryWithRightCommandType()
        {
            // Setup
            List<string> lstRow1 = new List<string>();
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            testObj.getNormalEvents();

            // Verify
            sqlHandler.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Equal("read")));
        }
        [Test]        
        public void getDebugEvents_ThreeAvailableEvents_TheThreeEventsAreReturned()
        {
            // Setup
            List<string> lstRow1 = new List<string>() { "1", "Empty", "12/12/2002", "xx", "52" };
            List<string> lstRow2 = new List<string>() { "2", "Empty", "12/12/2002", "xx", "52" };
            List<string> lstRow3 = new List<string>() { "3", "Empty", "12/12/2002", "xx", "52" };
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(lstRow1).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(lstRow2).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(lstRow3).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            List<ILogEvent> lstEvents = testObj.getDebugEvents();

            // Verify
            Assert.AreEqual(lstEvents[0].LogID, 1);
            Assert.AreEqual(lstEvents[1].LogID, 2);
            Assert.AreEqual(lstEvents[2].LogID, 3);
        }
        [Test]
        public void getDebugEvents_Called_CallsSqlHandlerrunQueryWithRightCommandText()
        {
            // Setup
            List<string> lstRow1 = new List<string>();
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            testObj.getDebugEvents();

            // Verify
            sqlHandler.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Matches(p => p.CommandText == "SELECT [LogID], [LogEvent], [LogTime], [Description], [PositionID] FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable] WHERE LogEvent = 'Debug' GO"), Arg<string>.Is.Anything));
        }
        [Test]
        public void getDebugEvents_Called_CallsSqlHandlerrunQueryWithRightCommandType()
        {
            // Setup
            List<string> lstRow1 = new List<string>();
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            testObj.getDebugEvents();

            // Verify
            sqlHandler.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Equal("read")));
        }
        [Test]
        public void getExceptionEvents_ThreeAvailableEvents_TheThreeEventsAreReturned()
        {
            // Setup
            List<string> lstRow1 = new List<string>() { "1", "Empty", "12/12/2002", "xx", "52" };
            List<string> lstRow2 = new List<string>() { "2", "Empty", "12/12/2002", "xx", "52" };
            List<string> lstRow3 = new List<string>() { "3", "Empty", "12/12/2002", "xx", "52" };
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(lstRow1).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(lstRow2).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(lstRow3).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            List<ILogEvent> lstEvents = testObj.getExceptionEvents();

            // Verify
            Assert.AreEqual(lstEvents[0].LogID, 1);
            Assert.AreEqual(lstEvents[1].LogID, 2);
            Assert.AreEqual(lstEvents[2].LogID, 3);
        }
        [Test]
        public void getExceptionEvents_Called_CallsSqlHandlerrunQueryWithRightCommandText()
        {
            // Setup
            List<string> lstRow1 = new List<string>();
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            testObj.getExceptionEvents();

            // Verify
            sqlHandler.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Matches(p => p.CommandText == "SELECT [LogID], [LogEvent], [LogTime], [Description], [PositionID] FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable] WHERE LogEvent = 'Exception' GO"), Arg<string>.Is.Anything));
        }
        [Test]
        public void getExceptionEvents_Called_CallsSqlHandlerrunQueryWithRightCommandType()
        {
            // Setup
            List<string> lstRow1 = new List<string>();
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            testObj.getExceptionEvents();

            // Verify
            sqlHandler.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Equal("read")));
        }
        [Test]
        public void getAllLogs_ThreeAvailableEvents_AllEventsAreReturned()
        {
            // Setup
            List<string> lstRow1 = new List<string>() { "1", "Empty", "12/12/2002", "xx", "52" };
            List<string> lstRow2 = new List<string>() { "2", "Empty", "12/12/2002", "xx", "52" };
            List<string> lstRow3 = new List<string>() { "3", "Empty", "12/12/2002", "xx", "52" };
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(lstRow1).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(lstRow2).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(lstRow3).Repeat.Once();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            List<ILogEvent> lstEvents = testObj.getAllLogs();

            // Verify
            Assert.AreEqual(lstEvents[0].LogID, 1);
            Assert.AreEqual(lstEvents[1].LogID, 2);
            Assert.AreEqual(lstEvents[2].LogID, 3);
        }
        [Test]
        public void getAllLogs_Called_CallsSqlHandlerrunQueryWithRightCommandText()
        {
            // Setup
            List<string> lstRow1 = new List<string>();
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            testObj.getAllLogs();

            // Verify
            sqlHandler.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Matches(p => p.CommandText == "SELECT [LogID], [LogEvent], [LogTime], [Description], [PositionID] FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable] GO"), Arg<string>.Is.Anything));
        }
        [Test]
        public void getAllLogs_Called_CallsSqlHandlerrunQueryWithRightCommandType()
        {
            // Setup
            List<string> lstRow1 = new List<string>();
            ISQLReader sqlReader = MockRepository.GenerateStub<ISQLReader>();
            sqlReader.Stub(t => t.readRow()).Return(new List<string>()).Repeat.Once();
            ISQLHandler sqlHandler = MockRepository.GenerateStub<ISQLHandler>();
            sqlHandler.Stub(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Anything)).Return(sqlReader);
            DatabaseLog testObj = new DatabaseLog(sqlHandler);

            // Test
            testObj.getAllLogs();

            // Verify
            sqlHandler.AssertWasCalled(t => t.runQuery(Arg<SqlCommand>.Is.Anything, Arg<string>.Is.Equal("read")));
        }
    }
}
