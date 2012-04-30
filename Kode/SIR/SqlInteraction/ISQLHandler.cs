using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    public interface ISQLHandler
    {
        bool setConnection(string _username, string _password, string _server, string _trusted, string _database, string _timeout);

        SqlCommand makeCommand(string _commandText, CommandType _commandType);

        void addParameter(SqlCommand _command, string _parameterName, object _parameterValue, SqlDbType _parameterType);

        SqlDataReader runQuery(SqlCommand _command, string queryType);

        void changeConnectionparameter(string _parameter, string _parameterValue);
    }
}