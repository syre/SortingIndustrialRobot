/** \file databaseSQLConnection.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    /// <summary>
    /// Interface for a SQL connection to a database.
    /// 
    /// Used for testing.
    /// </summary>
    public interface IDatabaseSQLConnection
    {
        /// <summary>
        /// State of the connection.
        /// </summary>
        ConnectionState State { get; }

        /// <summary>
        /// Connection string used for the connection.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Open the connection.
        /// </summary>
        void open();

        /// <summary>
        /// Close the connection.
        /// </summary>
        void close();

        /// <summary>
        /// Create a command to be used with the connection.
        /// </summary>
        /// <returns>The new command.</returns>
        SqlCommand createCommand();

        /// <summary>
        /// Timeout for the connection.
        /// </summary>
        /// <returns>Timeout in seconds when establishing connection.</returns>
        int connectionTimeout { get; }
    }

    /// <summary>
    /// Implementation of SQL connection to a database.
    /// 
    /// Uses SQLConnection.
    /// </summary>
    public class DatabaseSQLConnection : IDatabaseSQLConnection
    {
        // Members
        private SqlConnection scConnection;

        // Functions
        public DatabaseSQLConnection(string _sConnectionString)
        {
            scConnection = new SqlConnection(_sConnectionString);
        }

        public ConnectionState State
        {
            get
            {
                return(scConnection.State);
            }
        }

        public string ConnectionString
        {
            get { return (scConnection.ConnectionString); }
            set { scConnection.ConnectionString = value; }
        }

        public void open()
        {
            scConnection.Open();
        }

        public void close()
        {
            scConnection.Close();
        }

        public SqlCommand createCommand()
        {
            return (scConnection.CreateCommand());
        }

        public int connectionTimeout
        {
            get{return (scConnection.ConnectionTimeout);}
        }
    }
}
