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

        /// <summary>
        /// Preps logger for app shutdown
        /// </summary>
        void prepForShutdownApp();
    }

    /// <summary>
    /// Logs events to the database.
    /// </summary>
    public class DatabaseLogger : ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        private volatile bool boolStop = false;

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
        /// Makes the log thread finish up and stop, making sure all logs have been sent to database
        /// </summary>
        public void prepForShutdownApp()
        {
            boolStop = true;
            manEvent.Set();
            Factory.getThreadHandlingInstance.find("LogThread").threadPlaceHolder.Join();
        }

        /// <summary>
        /// Main function to be called when logging to database is needed.
        /// </summary>
        /// <param name="sMsg">Message to be logged</param>
        /// <param name="_eltType">Type of log</param>
        public void log(string _sMsg, eLogType _eltType)
        {
            Tuple<string, string, DateTime> tupleTemp;
            switch (_eltType)
            {
                case eLogType.LOG_INFO:
                    tupleTemp = new Tuple<string, string, DateTime>("Info", _sMsg, DateTime.Now);
                    addItemToQueue(tupleTemp);
                    break;
                case eLogType.LOG_ERROR:
                    tupleTemp = new Tuple<string, string, DateTime>("Error", _sMsg, DateTime.Now);
                    addItemToQueue(tupleTemp);
                    break;
                case eLogType.LOG_DEBUG:
                    tupleTemp = new Tuple<string, string, DateTime>("Debug", _sMsg, DateTime.Now);
                    addItemToQueue(tupleTemp);
                    break;
            }

        }

         
        /// <summary>
        /// Function which adds a tuple to the queue, but at the same time sends event to log thread that another has been added
        /// </summary>
        /// <param name="_tubLog">Parameter of a tuble that holds data that needs to be added to queue</param>
        private void addItemToQueue(Tuple<string, string, DateTime> _tubLog)
        {
            syncLogQueue.Add(_tubLog);
            manEvent.Set();
        }

        /// <summary>
        /// Function which will be running in its own thread, continuesly adding logs to database
        /// </summary>
        private void LogThreadFunction()
        {
                while (boolStop == false || (boolStop == true && syncLogQueue.Count > 0))
                {
                    if (syncLogQueue.Count == 0 && boolStop == false)
                    {
                        manEvent.WaitOne(Timeout.Infinite, true);
                    }
                    manEvent.Reset();

                    if(syncLogQueue.Count > 0)
                    {
                        Tuple<string, string, DateTime> tupleTemp = (Tuple<string, string, DateTime>)syncLogQueue[0];
                        createLog(tupleTemp.Item1, tupleTemp.Item2, tupleTemp.Item3);
                        syncLogQueue.RemoveAt(0);
                    }
                }
        }

        /// <summary>
        /// Private helping function to utilize database connection, query and execution
        /// </summary>
        /// <param name="_sLog">Log level parameter</param>
        /// <param name="_sMsg">Description for log</param>
        private void createLog(string _sLog, string _sMsg, DateTime _dTime)
        {
            SqlCommand sqlCmdTemp = SQLHandlerObj.makeCommand("INSERT INTO Logs VALUES (@pLog, @pDateTime, @pMsg)");
            SQLHandlerObj.addParameter(sqlCmdTemp, "@pLog", _sLog, SqlDbType.VarChar);
            SQLHandlerObj.addParameter(sqlCmdTemp, "@pDateTime", _dTime, SqlDbType.DateTime);
            SQLHandlerObj.addParameter(sqlCmdTemp, "@pMsg", _sMsg, SqlDbType.VarChar);

            SQLHandlerObj.runQuery(sqlCmdTemp, "write");
        }
    }
}
