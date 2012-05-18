using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    public interface ISqlConnection
    {
        ConnectionState RobotConnectionState { get; }
        string Connectionstring { set; get; }
        void ConnectionOpen();
        void ConnectionClose();
        SqlCommand CreateCommand();
        int TimeOut { get; }
    }
}
