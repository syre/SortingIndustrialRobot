/** \file robotSqlConnection.cs */
/** \author Robotic Global Organization(RoboGO) */
using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    /// <summary>
    /// SQLConnection to connect to a database.
    /// </summary>
    public class RobotSqlConnection : ISqlConnection
    {
        /// <summary>
        /// Constructor that connects to a database with the information specified.
        /// </summary>
        /// <param name="connectionstring">Information for connection.</param>
        public RobotSqlConnection(string _sConnectionString)
        {
            connection = new DatabaseSQLConnection(_sConnectionString);
        }

        /// <summary>
        /// Constructor for testing when no database connection.
        /// </summary>
        /// <param name="_idscConnection">SQL connection.</param>
        public RobotSqlConnection(IDatabaseSQLConnection _idscConnection)
        {
            connection = _idscConnection;
        }

        private IDatabaseSQLConnection connection;
        /// <summary>
        /// SQL connection used for operations.
        /// </summary>
        public IDatabaseSQLConnection Connection
        {
            get { return (connection); }
            set { connection = value; }
        }
        /// <summary>
        /// State of the connection.
        /// </summary>
        public ConnectionState RobotConnectionState
        {
            get { return connection.State; }
        }

        /// <summary>
        /// String used for the connection.
        /// 
        /// Updates the connection when set.
        /// </summary>
        public string Connectionstring
        {
            get { return connection.ConnectionString; }
            set { connection.ConnectionString = value; }
        }

        public void ConnectionOpen()
        {
            connection.open();
        }

        public void ConnectionClose()
        {
            connection.close();
        }

        public SqlCommand CreateCommand()
        {
            return connection.createCommand();
        }

        public int TimeOut
        {
            get { return connection.connectionTimeout; }
        }
    }
}
