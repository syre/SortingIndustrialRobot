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
        public RobotSqlConnection(string connectionstring)
        {
            connection = new SqlConnection(connectionstring);
        }
        private SqlConnection connection;
        public ConnectionState RobotConnectionState
        {
            get { return connection.State; }
        }

        public string Connectionstring
        {
            get { return connection.ConnectionString; }
            set { connection.ConnectionString = value; }
        }

        public void ConnectionOpen()
        {
            connection.Open();
        }

        public void ConnectionClose()
        {
            connection.Close();
        }

        public SqlCommand CreateCommand()
        {
            return connection.CreateCommand();
        }

        public int TimeOut
        {
            get { return connection.ConnectionTimeout; }
        }
    }
}
