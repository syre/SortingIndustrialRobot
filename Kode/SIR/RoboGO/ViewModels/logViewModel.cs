/** \file logViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Input;
using System.Windows.Threading;
using SqlInteraction;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// Holds a collection of ILogEvents and notifies about changes to the collection.
    /// </summary>
    public class ILogEventCollection : ObservableCollection<ILogEvent>
        {}

    /// <summary>
    /// ViewModel for the log functionality.
    /// </summary>
    public class LogViewModel
    {
        #region Members and properties
        private IDatabaseLog dlDatabaseLogLink;
        /// <summary>
        /// DatabaseLog used for communication with database.
        /// </summary>
        public IDatabaseLog DatabaseLogLink
        {
            get { return (dlDatabaseLogLink); }
            set 
            { 
                if(value == null)
                    throw new NullReferenceException("DatabaseLogLink can not be null.");
                dlDatabaseLogLink = value; 
            }
        }

        private ILogEventCollection lstLogEvents;
        /// <summary>
        /// List of events last gotten from database.
        /// </summary>
        public ILogEventCollection LogEvents
        {
            get { return (lstLogEvents); }
            set 
            {
                if (value == null)
                    throw new NullReferenceException("LogEvents can not be null.");
                lstLogEvents = value; 
            }
        }
        #region Commands
        private DelegateCommand dcCmdGetNormalEvents;
        /// <summary>
        /// Command for getting all Normal events from the database.
        /// </summary>
        public ICommand CmdGetNormalEvents
        {
            get { return dcCmdGetNormalEvents; }
        }

        private DelegateCommand dcCmdGetDebugEvents;
        /// <summary>
        /// Command for getting all Debug events from the database.
        /// </summary>
        public ICommand CmdGetDebugEvents
        {
            get { return dcCmdGetDebugEvents; }
        }

        private DelegateCommand dcCmdGetExceptionEvents;
        /// <summary>
        /// Command for getting all Exception events from the database.
        /// </summary>
        public ICommand CmdGetExceptionEvents
        {
            get { return dcCmdGetExceptionEvents; }
        }

        private DelegateCommand dcCmdGetAllEvents;
        /// <summary>
        /// Command for getting all event types from the database.
        /// </summary>
        public ICommand CmdGetAllEvents
        {
            get { return dcCmdGetAllEvents; }
        }

        private DelegateCommand dcCmdSaveLog;
        /// <summary>
        /// Command for saving the log.
        /// </summary>
        public ICommand CmdSaveLog
        {
            get { return dcCmdSaveLog; }
        }

        #endregion
        #endregion

        // Functions
        /// <summary>
        /// Normal constructor.
        /// </summary>
        /// <param name="_dUIDispatcher">Dispatcher for the UI.</param>
        public LogViewModel()
        {
            // Init
            dcCmdSaveLog = new DelegateCommand(saveLogs);
            dcCmdGetAllEvents = new DelegateCommand(updateWithAllEvents);
            dcCmdGetExceptionEvents = new DelegateCommand(updateWithExceptionEvents);
            dcCmdGetDebugEvents = new DelegateCommand(updateWithDebugEvents);
            dcCmdGetNormalEvents = new DelegateCommand(updateWithNormalEvents);

            lstLogEvents = new ILogEventCollection();

            SQLHandler sqlHandler = new SQLHandler(); // Test for this?
            dlDatabaseLogLink = new DatabaseLog(sqlHandler);
        }

        //
        // Should use threadhandler because of getting from database.
        //
        /// <summary>
        /// Fills LogEvents with Normal events from the database.
        /// </summary>
        public void updateWithNormalEvents()
        {
            List<ILogEvent> lstEventsTmp = dlDatabaseLogLink.getNormalEvents();

            // Lol.(No clearing or anything. Just passes the test.)
            LogEvents.Add(lstEventsTmp[0]);
        }

        /// <summary>
        /// Fills LogEvents with Debug events from the database.
        /// </summary>
        public void updateWithDebugEvents()
        {
            dlDatabaseLogLink.getDebugEvents();
        }

        /// <summary>
        /// Fills LogEvents with Exception events from the database.
        /// </summary>
        public void updateWithExceptionEvents()
        {

        }

        /// <summary>
        /// Fills LogEvents with all 3 event types from the database.
        /// </summary>
        public void updateWithAllEvents()
        {

        }

        /// <summary>
        /// Saves log to file.
        /// </summary>
        public void saveLogs()
        {
            
        }
    }
}
