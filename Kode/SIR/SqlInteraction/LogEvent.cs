/** \file LogEvent.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlInteraction
{
    public class LogEvent : ILogEvent
    {
        private int iLogId;
        public int LogID
        {
            get { return (iLogId); }
            set { iLogId = value; }
        }

        private string sLogEventType;
        public string LogEventType
        {
            get { return (sLogEventType); }
            set { sLogEventType = value; }
        }

        private DateTime dtLogTime;
        public DateTime LogTime
        {
            get { return (dtLogTime); }
            set { dtLogTime = value; }
        }

        private string sDescription;
        public string Description
        {
            get { return (sDescription); }
            set { sDescription = value; }
        }

        private int iPositionID;
        public int PositionID
        {
            get { return (iPositionID); }
            set { iPositionID = value; }
        }
    }
}
