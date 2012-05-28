/** \file logger.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
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
        
        public void log(string sMsg, eLogType _eltType)
        {
            string sLogString;
            switch(_eltType)
            {
                case eLogType.LOG_INFO:
                    sLogString = "Info";
                    break;
                case eLogType.LOG_ERROR:
                    sLogString = "Error";
                    break;
                case eLogType.LOG_DEBUG:
                    sLogString = "Debug";
                    break;
            }
        }

        //private void 
    }
}
