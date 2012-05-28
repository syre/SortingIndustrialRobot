/** \file logger.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using SqlInteraction;

namespace ControlSystem
{
    /// <summary>
    /// Type of log.
    /// </summary>
    public enum eLogType
    {
        LOG_INFO,
        LOG_DEBUG,
        LOG_ERROR
    }

    /// <summary>
    /// Interface for logging operations.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log a message.
        /// </summary>
        /// <param name="sMsg">Description/message.</param>
        /// <param name="_eltType">Type of log.</param>
        void log(string sMsg, eLogType _eltType);
    }

    /// <summary>
    /// Logs events to the database.
    /// </summary>
    public class DatabaseLogger : ILogger
    {
        // Members and properties
        /// <summary>
        /// SQLHandler used for database operations.
        /// </summary>
        public ISQLHandler SQLHandlerObj
        {
            get;
            set;
        }

        // Functions
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DatabaseLogger()
        {
            SQLHandlerObj = SQLHandler.GetInstance;
        }

        /// <summary>
        /// Main function to be called when logging to database is needed.
        /// </summary>
        /// <param name="sMsg">Message to be logged</param>
        /// <param name="_eltType">Type of log</param>
        public void log(string sMsg, eLogType _eltType)
        {
            string sLogString;
            switch (_eltType)
            {
                case eLogType.LOG_INFO:
                    sLogString = "Info";
                    createLog(sLogString, sMsg);
                    break;
                case eLogType.LOG_ERROR:
                    sLogString = "Error";
                    createLog(sLogString, sMsg);
                    break;
                case eLogType.LOG_DEBUG:
                    sLogString = "Debug";
                    createLog(sLogString, sMsg);
                    break;
            }

        }

        /// <summary>
        /// Private helping function to utilize database connection, query and execution
        /// </summary>
        /// <param name="_sLog">Log level parameter</param>
        /// <param name="_sMsg">Description for log</param>
        private void createLog(string _sLog, string _sMsg)
        {
            DateTime dtNow = DateTime.Now;
            SqlCommand sqlCmdTemp = SQLHandlerObj.makeCommand("INSERT INTO Logs VALUES (@pLog, @pDateTime, @pMsg)");
            SQLHandlerObj.addParameter(sqlCmdTemp, "@pLog", _sLog, SqlDbType.VarChar);
            SQLHandlerObj.addParameter(sqlCmdTemp, "@pDateTime", dtNow, SqlDbType.DateTime);
            SQLHandlerObj.addParameter(sqlCmdTemp, "@pMsg", _sMsg, SqlDbType.VarChar);

            SQLHandlerObj.runQuery(sqlCmdTemp, "write");
        }
    }
}
