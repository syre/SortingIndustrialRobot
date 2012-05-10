using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlInteraction
{
    public enum ConnectionState
    {
        Close,
        Open
    }
    public interface ISqlConnection
    {
        ConnectionState RobotConnectionState { set; get; }
        string Connectionstring { set; get; }
        void ConnectionOpen();
        void ConnectionClose();
        ISqlCommand CreateCommand();
        uint TimeOut { set; get; }
    }
}
