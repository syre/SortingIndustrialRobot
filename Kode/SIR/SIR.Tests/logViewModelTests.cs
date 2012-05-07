using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;
using NUnit.Framework;
using Rhino.Mocks;
using RoboGO.ViewModels;
using SqlInteraction;

namespace SIR.Tests
{
    [TestFixture]
    public class logViewModelTests
    {
        // Tests
        #region Properties
        [Test]
        public void DatabaseLogLink_SetsValue_ValueIsSaved()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();
            ISQLHandler ishHandler = MockRepository.GenerateStub<ISQLHandler>();
            DatabaseLog dblStub = new DatabaseLog(ishHandler);

            // Test
            lvmTestObj.DatabaseLogLink = dblStub;

            // Verify
            Assert.AreEqual(dblStub, lvmTestObj.DatabaseLogLink);
        }
        [Test, ExpectedException]
        public void DatabaseLogLink_SetsValueToNull_ThrowsException()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();

            // Test
            lvmTestObj.DatabaseLogLink = null;
        }
        [Test]
        public void LogEvents_SetsValue_ValueIsSaved()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();
            ILogEventCollection ilecCollection = new ILogEventCollection();

            // Test
            lvmTestObj.LogEvents = ilecCollection;

            // Verify
            Assert.AreEqual(ilecCollection, lvmTestObj.LogEvents);
        }
        [Test, ExpectedException]
        public void LogEvents_SetsValueToNull_ThrowsException()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();

            // Test
            lvmTestObj.LogEvents = null;
        }
        #endregion
        #region Constructor
        [Test]
        public void LogViewModel_Called_PropertyDatabaseLogLinkIsNotNull()
        {
            // Test
            LogViewModel lvmTestObj = new LogViewModel();

            // Verify
            Assert.AreNotEqual(null, lvmTestObj.DatabaseLogLink);
        }
        [Test]
        public void LogViewModel_Called_PropertyLogEventsIsNotNull()
        {
            // Test
            LogViewModel lvmTestObj = new LogViewModel();

            // Verify
            Assert.AreNotEqual(null, lvmTestObj.LogEvents);
        }
        [Test]
        public void LogViewModel_Called_PropertyCmdGetNormalEventsIsNotNull()
        {
            // Test
            LogViewModel lvmTestObj = new LogViewModel();

            // Verify
            Assert.AreNotEqual(null, lvmTestObj.CmdGetNormalEvents);
        }
        [Test]
        public void LogViewModel_Called_PropertyCmdGetDebugEventsIsNotNull()
        {
            // Test
            LogViewModel lvmTestObj = new LogViewModel();

            // Verify
            Assert.AreNotEqual(null, lvmTestObj.CmdGetDebugEvents);
        }
        [Test]
        public void LogViewModel_Called_PropertyCmdGetExceptionEventsIsNotNull()
        {
            // Test
            LogViewModel lvmTestObj = new LogViewModel();

            // Verify
            Assert.AreNotEqual(null, lvmTestObj.CmdGetExceptionEvents);
        }
        [Test]
        public void LogViewModel_Called_PropertyCmdGetAllEventsIsNotNull()
        {
            // Test
            LogViewModel lvmTestObj = new LogViewModel();

            // Verify
            Assert.AreNotEqual(null, lvmTestObj.CmdGetAllEvents);
        }
        [Test]
        public void LogViewModel_Called_PropertyCmdSaveLogIsNotNull()
        {
            // Test
            LogViewModel lvmTestObj = new LogViewModel();

            // Verify
            Assert.AreNotEqual(null, lvmTestObj.CmdSaveLog);
        }
        #endregion
        #region Functions
        [Test]
        public void updateWithNormalEvents_IsCalled_GetNormalEventsIsCalledFromDatabaseLog()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();
            IDatabaseLog dlMock = MockRepository.GenerateMock<IDatabaseLog>();
            lvmTestObj.DatabaseLogLink = dlMock;

            // Tests
            lvmTestObj.updateWithNormalEvents();

            // Verify
            dlMock.AssertWasCalled(t => t.getNormalEvents());
        }

        [Test]
        public void updateWithNormalEvents_IsCalled_IsSavedToLogEventsProperty()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();
            IDatabaseLog dlMock = MockRepository.GenerateMock<IDatabaseLog>();
            lvmTestObj.DatabaseLogLink = dlMock;
            List<ILogEvent> ilecEvents = new List<ILogEvent>();
            ILogEvent ileEventTmp = new LogEvent();
            ileEventTmp.LogID = 7;
            ilecEvents.Add(ileEventTmp);
            dlMock.Stub(t => t.getNormalEvents()).Return(ilecEvents);

            // Tests
            lvmTestObj.updateWithNormalEvents();

            // Verify
            Assert.AreEqual(7, lvmTestObj.LogEvents[0].LogID);
        }

        [Test]
        public void updateWithDebugEvents_IsCalled_GetDebugEventsIsCalledFromDatabaseLog()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();
            IDatabaseLog dlMock = MockRepository.GenerateMock<IDatabaseLog>();
            lvmTestObj.DatabaseLogLink = dlMock;

            // Test
            lvmTestObj.updateWithDebugEvents();

            // Verify
            dlMock.AssertWasCalled(t => t.getDebugEvents());
        }
        [Test]
        public void updateWithExceptionEvents_IsCalled_GetExceptionEventsIsCalledFromDatabaseLog()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();
            IDatabaseLog dlMock = MockRepository.GenerateMock<IDatabaseLog>();
            lvmTestObj.DatabaseLogLink = dlMock;

            // Test
            lvmTestObj.updateWithExceptionEvents();

            // Verify
            dlMock.AssertWasCalled(t => t.getExceptionEvents());
        }
        [Test]
        public void updateWithAllEvents_IsCalled_GetAllEventsIsCalledFromDatabaseLog()
        {
            // Setup
            LogViewModel lvmTestObj = new LogViewModel();
            IDatabaseLog dlMock = MockRepository.GenerateMock<IDatabaseLog>();
            lvmTestObj.DatabaseLogLink = dlMock;

            // Test
            lvmTestObj.updateWithAllEvents();

            // Verify
            lvmTestObj.AssertWasCalled(t => t.updateWithAllEvents());
        }
        #endregion
    }
}
