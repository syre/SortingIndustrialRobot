/** \file ISQLHandler.cs */
/** \author Robotic Global Organization(RoboGO) */
using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    public interface ISQLHandler
    {
        bool setConnection(string _server, string _database, string _username, string _password, string _timeout);

        SqlCommand makeCommand(string _commandText, CommandType _commandType);

        void addParameter(SqlCommand _command, string _parameterName, object _parameterValue, SqlDbType _parameterType);

        ISQLReader runQuery(SqlCommand _command, string queryType);

        void changeConnectionparameter(string _parameter, string _parameterValue);
    }
}