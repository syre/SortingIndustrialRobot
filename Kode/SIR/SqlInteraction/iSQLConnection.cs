using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    public interface ISqlConnection
    {
        /// <summary>
        /// State of the connection.
        /// </summary>
        ConnectionState RobotConnectionState { get; }
        
        /// <summary>
        /// The connection string used for the connection.
        /// </summary>
        string Connectionstring { set; get; }
        
        /// <summary>
        /// Open the connection.
        /// </summary>
        void ConnectionOpen();
        
        /// <summary>
        /// Close the connection.
        /// </summary>
        void ConnectionClose();
        
        /// <summary>
        /// Create a SQLCommand to be able to get/set information in the database.
        /// </summary>
        /// <returns></returns>
        SqlCommand CreateCommand();
        
        /// <summary>
        /// How long it will try to connect before timing out.
        /// </summary>
        int TimeOut { get; }
    }
}
