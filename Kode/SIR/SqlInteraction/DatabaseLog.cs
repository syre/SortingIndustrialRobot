/** \file DatabaseLog.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlInteraction
{
    public interface IDatabaseLog
    {
        List<ILogEvent> getNormalEvents();
        List<ILogEvent> getDebugEvents();
        List<ILogEvent> getExceptionEvents();
        List<ILogEvent> getAllLogs();
    }

    public class DatabaseLog : IDatabaseLog
    {
        // Members
        private ISQLHandler isqlHandler;

        // Functions
        public DatabaseLog(ISQLHandler _isqlHandler)
        {
            isqlHandler = _isqlHandler;
        }

        public List<ILogEvent> getNormalEvents()
        {
            List<ILogEvent> lstNormal = new List<ILogEvent>();
            ISQLReader sqlReader = isqlHandler.runQuery(new SqlCommand("SELECT [LogID], [LogEvent], [LogTime], [Description], [PositionID] FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable] WHERE LogEvent = 'Normal' GO"), "read");
            List<string> lstRow = new List<string>();
            while((lstRow = sqlReader.readRow()).Count != 0)
            {
                // Set data
                LogEvent leTmpEvent = new LogEvent();
                leTmpEvent.LogID = int.Parse(lstRow[0]);
                leTmpEvent.LogEventType = lstRow[1];
                leTmpEvent.LogTime = DateTime.Parse(lstRow[2]); /// \warning Database format unknown.
                leTmpEvent.Description = lstRow[3];
                leTmpEvent.PositionID = int.Parse(lstRow[4]);

                // Add to list
                lstNormal.Add(leTmpEvent);
            }
            return lstNormal;
        }

        public List<ILogEvent> getDebugEvents()
        {
            List<ILogEvent> lstDebug = new List<ILogEvent>();
            ISQLReader sqlReader = isqlHandler.runQuery(new SqlCommand("SELECT [LogID], [LogEvent], [LogTime], [Description], [PositionID] FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable] WHERE LogEvent = 'Debug' GO"), "read");
            List<string> lstRow = new List<string>();
            while ((lstRow = sqlReader.readRow()).Count != 0)
            {
                // Set data
                LogEvent leTmpEvent = new LogEvent();
                leTmpEvent.LogID = int.Parse(lstRow[0]);
                leTmpEvent.LogEventType = lstRow[1];
                leTmpEvent.LogTime = DateTime.Parse(lstRow[2]); /// \warning Database format unknown.
                leTmpEvent.Description = lstRow[3];
                leTmpEvent.PositionID = int.Parse(lstRow[4]);

                // Add to list
                lstDebug.Add(leTmpEvent);
            }
            return lstDebug;
        }

        public List<ILogEvent> getExceptionEvents()
        {
            List<ILogEvent> lstException = new List<ILogEvent>();
            ISQLReader sqlReader = isqlHandler.runQuery(new SqlCommand("SELECT [LogID], [LogEvent], [LogTime], [Description], [PositionID] FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable] WHERE LogEvent = 'Exception' GO"), "read");
            List<string> lstRow = new List<string>();
            while ((lstRow = sqlReader.readRow()).Count != 0)
            {
                // Set data
                LogEvent leTmpEvent = new LogEvent();
                leTmpEvent.LogID = int.Parse(lstRow[0]);
                leTmpEvent.LogEventType = lstRow[1];
                leTmpEvent.LogTime = DateTime.Parse(lstRow[2]); /// \warning Database format unknown.
                leTmpEvent.Description = lstRow[3];
                leTmpEvent.PositionID = int.Parse(lstRow[4]);

                // Add to list
                lstException.Add(leTmpEvent);
            }
            return lstException;
        }

        public List<ILogEvent> getAllLogs()
        {
            List<ILogEvent> lstAll = new List<ILogEvent>();
            ISQLReader sqlReader = isqlHandler.runQuery(new SqlCommand("SELECT [LogID], [LogEvent], [LogTime], [Description], [PositionID] FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable] GO"), "read");
            List<string> lstRow = new List<string>();
            while ((lstRow = sqlReader.readRow()).Count != 0)
            {
                // Set data
                LogEvent leTmpEvent = new LogEvent();
                leTmpEvent.LogID = int.Parse(lstRow[0]);
                leTmpEvent.LogEventType = lstRow[1];
                leTmpEvent.LogTime = DateTime.Parse(lstRow[2]); /// \warning Database format unknown.
                leTmpEvent.Description = lstRow[3];
                leTmpEvent.PositionID = int.Parse(lstRow[4]);

                // Add to list
                lstAll.Add(leTmpEvent);
            }
            return lstAll;
        }
    }
}
