using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
