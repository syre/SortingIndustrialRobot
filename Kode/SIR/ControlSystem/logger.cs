/** \file logger.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading;
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
        /// <summary>
        /// Event for making log thread wait for signal
        /// </summary>
        private static ManualResetEvent manEvent = new ManualResetEvent(false);
        
        /// <summary>
        /// List of tuples that hold info of logs that needs to be sent to database
        /// </summary>
        private static ArrayList logQueue = new ArrayList();

        /// <summary>
        /// Wrapper which makes a syncronized interface to logqueue (thread safe)
        /// </summary>
        private ArrayList syncLogQueue = ArrayList.Synchronized(logQueue);

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

            Factory.getThreadHandlingInstance.addThread(LogThreadFunction, "LogThread");
            Factory.getThreadHandlingInstance.start("LogThread");
        }

        /// <summary>
        /// Main function to be called when logging to database is needed.
        /// </summary>
        /// <param name="sMsg">Message to be logged</param>
        /// <param name="_eltType">Type of log</param>
        public void log(string _sMsg, eLogType _eltType)
        {
            Tuple<string, string> tupleTemp;
            switch (_eltType)
            {
                case eLogType.LOG_INFO:
                    tupleTemp = new Tuple<string, string>("Info", _sMsg);
                    addItemToQueue(tupleTemp);
                    break;
                case eLogType.LOG_ERROR:
                    tupleTemp = new Tuple<string, string>("Error", _sMsg);
                    addItemToQueue(tupleTemp);
                    break;
                case eLogType.LOG_DEBUG:
                    tupleTemp = new Tuple<string, string>("Debug", _sMsg);
                    addItemToQueue(tupleTemp);
                    break;
            }

        }

         
        /// <summary>
        /// Function which adds a tuple to the queue, but at the same time sends event to log thread that another has been added
        /// </summary>
        /// <param name="_tubLog">Parameter of a tuble that holds data that needs to be added to queue</param>
        private void addItemToQueue(Tuple<string, string> _tubLog)
        {
            syncLogQueue.Add(_tubLog);
            manEvent.Set();
        }

        /// <summary>
        /// Function which will be running in its own thread, continuesly adding logs to database
        /// </summary>
        private void LogThreadFunction()
        {
                while (true)
                {
                    if (syncLogQueue.Count == 0)
                    {
                        manEvent.WaitOne(Timeout.Infinite, true);
                    }
                    manEvent.Reset();

                    Tuple<string, string> tupleTemp = (Tuple<string, string>)syncLogQueue[0];
                    createLog(tupleTemp.Item1 , tupleTemp.Item2);
                    syncLogQueue.RemoveAt(0);
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
