using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace SIR.Tests
{
    [TestFixture]
    public class logViewModelTests
    {
        // Tests
        #region Properties
        [Test]
        public void UIDispatcher_SetsValue_ValueIsSaved()
        {
            
        }
        [Test]
        [ExpectedException]
        public void UIDispatcher_SetsValueToNull_ThrowsException()
        {

        }
        [Test]
        public void DatabaseLogLink_SetsValue_ValueIsSaved()
        {

        }
        [Test]
        [ExpectedException]
        public void DatabaseLogLink_SetsValueToNull_ThrowsException()
        {

        }
        [Test]
        public void LogEvents_SetsValue_ValueIsSaved()
        {

        }
        [Test]
        [ExpectedException]
        public void LogEvents_SetsValueToNull_ThrowsException()
        {

        }
        [Test]
        public void CmdGetNormalEvents_SetsValue_ValueIsSaved()
        {

        }
        [Test]
        [ExpectedException]
        public void CmdGetNormalEvents_SetsValueToNull_ThrowsException()
        {

        }
        [Test]
        public void CmdGetDebugEvents_SetsValue_ValueIsSaved()
        {

        }
        [Test]
        [ExpectedException]
        public void CmdGetDebugEvents_SetsValueToNull_ThrowsException()
        {

        }
        [Test]
        public void CmdGetExceptionEvents_SetsValue_ValueIsSaved()
        {

        }
        [Test]
        [ExpectedException]
        public void CmdGetExceptionEvents_SetsValueToNull_ThrowsException()
        {

        }
        [Test]
        public void CmdGetAllEvents_SetsValue_ValueIsSaved()
        {

        }
        [Test]
        [ExpectedException]
        public void CmdGetAllEvents_SetsValueToNull_ThrowsException()
        {

        }
        #endregion
        #region Constructor
        [Test]
        [ExpectedException]
        public void LogViewModel_CalledWithDispatcherNull_ThrowsException()
        {
            
        }
        [Test]
        public void LogViewModel_CalledWithDispatcherValue_UIDispatcherPropertySetToSameValue()
        {
            
        }
        [Test]
        public void LogViewModel_Called_PropertyDatabaseLogLinkIsNotNull()
        {

        }
        [Test]
        public void LogViewModel_Called_PropertyLogEventsIsNotNull()
        {
            
        }
        [Test]
        public void LogViewModel_Called_PropertyCmdGetNormalEventsIsNotNull()
        {
            
        }
        [Test]
        public void LogViewModel_Called_PropertyCmdGetDebugEventsIsNotNull()
        {

        }
        [Test]
        public void LogViewModel_Called_PropertyCmdGetExceptionEventsIsNotNull()
        {

        }
        public void LogViewModel_Called_PropertyCmdGetAllEventsIsNotNull()
        {

        }
        #endregion
    }
}
