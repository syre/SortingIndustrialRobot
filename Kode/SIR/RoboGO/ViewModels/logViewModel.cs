/** \file logViewModel.cs */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        private Dispatcher dUIDispatcher;
        /// <summary>
        /// Dispatcher to change this class´s dependency properties.
        /// </summary>
        public Dispatcher UIDispatcher
        {
            get { return (dUIDispatcher); }
            set { dUIDispatcher = value; }
        }

        private DatabaseLog dlDatabaseLogLink;
        /// <summary>
        /// DatabaseLog used for communication with database.
        /// </summary>
        public DatabaseLog DatabaseLogLink
        {
            get { return (dlDatabaseLogLink); }
            set { dlDatabaseLogLink = value; }
        }

        private ILogEventCollection lstLogEvents;
        /// <summary>
        /// List of events last gotten from database.
        /// </summary>
        public ILogEventCollection LogEvents
        {
            get { return (lstLogEvents); }
            set { lstLogEvents = LogEvents; }
        }
        #region Commands
        private DelegateCommand dcCmdGetNormalEvents;
        /// <summary>
        /// Command for getting all Normal events from the database.
        /// </summary>
        public DelegateCommand CmdGetNormalEvents
        {
            get { return dcCmdGetNormalEvents; }
            set { dcCmdGetNormalEvents = value; }
        }

        private DelegateCommand dcCmdGetDebugEvents;
        /// <summary>
        /// Command for getting all Debug events from the database.
        /// </summary>
        public DelegateCommand CmdGetDebugEvents
        {
            get { return dcCmdGetDebugEvents; }
            set { dcCmdGetDebugEvents = value; }
        }

        private DelegateCommand dcCmdGetExceptionEvents;
        /// <summary>
        /// Command for getting all Exception events from the database.
        /// </summary>
        public DelegateCommand CmdGetExceptionEvents
        {
            get { return dcCmdGetExceptionEvents; }
            set { dcCmdGetExceptionEvents = value; }
        }

        private DelegateCommand dcCmdGetAllEvents;
        /// <summary>
        /// Command for getting all all event types from the database.
        /// </summary>
        public DelegateCommand CmdGetAllEvents
        {
            get { return dcCmdGetAllEvents; }
            set { dcCmdGetAllEvents = value; }
        }
        #endregion
        #endregion

        // Functions
        /// <summary>
        /// Normal constructor.
        /// </summary>
        /// <param name="_dUIDispatcher">Dispatcher for the UI.</param>
        public LogViewModel(Dispatcher _dUIDispatcher)
        {
            // Init
        }

        //
        // Should use threadhandler because of getting from database.
        //
        /// <summary>
        /// Fills LogEvents with Normal events from the database.
        /// </summary>
        public void updateWithNormalEvents()
        {
            
        }

        /// <summary>
        /// Fills LogEvents with Debug events from the database.
        /// </summary>
        public void updateWithDebugEvents()
        {

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
    }
}
