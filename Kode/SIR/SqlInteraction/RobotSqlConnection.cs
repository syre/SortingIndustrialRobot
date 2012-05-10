using System.Data.SqlClient;

namespace SqlInteraction
{
    public class RobotSqlConnection : ISqlConnection
    {
        private ConnectionState _state;
        public ConnectionState RobotConnectionState
        {
            get { return _state; }
            set
            {
                if(value == ConnectionState.Open && Sql.State == System.Data.ConnectionState.Open)
                {
                    _state = ConnectionState.Open;  
                }
                else
                {
                    _state = ConnectionState.Close;
                }
            }
        }

        public string Connectionstring
        {
            get { return Sql.ConnectionString; }
            set { Sql.ConnectionString = value; }
        }

        public SqlConnection Sql { get; set; }

        public void ConnectionOpen()
        {
            Sql.Open();
            RobotConnectionState = ConnectionState.Open;
        }

        public void ConnectionClose()
        {
            Sql.Close();
            RobotConnectionState = ConnectionState.Close;
        }

        public ISqlCommand CreateCommand()
        {
            throw new System.NotImplementedException();
        }

        public uint TimeOut { set; get; }
    }
}