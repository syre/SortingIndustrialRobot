/** \file ISQLHandler.cs */
/** \author Robotic Global Organization(RoboGO) */
using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
	///	<summary>
	///	Interface for handling class of sql
	///	</summary>
    public interface ISQLHandler
    {
        /// <summary>
        /// Sets a new connection to use.
        /// </summary>
        /// <param name="_server">Paramater for ip/domain to use</param>
        /// <param name="_database">Parameter for which database to use</param>
        /// <param name="_username">Parameter for username to use</param>
        /// <param name="_password">Parameter for password to use</param>
        /// <param name="_timeout">Parameter for what timeout should be</param>
        /// <returns>Returns bool for whether connection was succesfully set</returns>
        bool setConnection(string _server, string _database, string _username, string _password, string _timeout);

        /// <summary>
        /// Creates a command that can be used for sql interaction
        /// </summary>
        /// <param name="_commandText">Parameter for the command text</param>
        /// <param name="_commandType">Parameter for what type text needs to be interpreted</param>
        /// <returns>Returns the SqlCommand made from the given parameters</returns>
        SqlCommand makeCommand(string _commandText, CommandType _commandType);

        /// <summary>
        /// Function adds a parameter to the command.
        /// </summary>
        /// <param name="_command">Parameter is the command made in makeCommand</param>
        /// <param name="_parameterName">Parameter is name of the parameter defined in makeCommand</param>
        /// <param name="_parameterValue">Parameter is value to be used in the command</param>
        /// <param name="_parameterType">Parameter is of which type the parameter is</param>
        void addParameter(SqlCommand _command, string _parameterName, object _parameterValue, SqlDbType _parameterType);

        /// <summary>
        /// Executes the supplied command on SQL server
        /// </summary>
        /// <param name="_command">Parameter is command to be executed</param>
        /// <param name="queryType">Parameter is what kind of query to execute (write/read)</param>
        /// <returns>Returns Null if write, if read returns a Datareader</returns>
        ISQLReader runQuery(SqlCommand _command, string queryType);

        /// <summary>
        /// Changes 1 specific parameter in connection info
        /// </summary>
        /// <param name="_parameter">Parameter is part of or full parameter name to change</param>
        /// <param name="_parameterValue">Parameter is value to change the connection info to</param>
        void changeConnectionparameter(string _parameter, string _parameterValue);

        /// <summary>
        /// Variable to get the connection definition
        /// </summary>
        ISqlConnection Connection { set; get; }
    }
}