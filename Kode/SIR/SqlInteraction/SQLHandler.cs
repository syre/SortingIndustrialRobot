/**
        \brief Class that handles all of SQL interaction.
        
        \author Robotic Global Organization(RoboGO)
        \date 09-04-2012
*/

using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    /// <summary>
    /// Handles all SQL interaction
    /// </summary>
    public class SQLHandler
    {
        private static volatile SQLHandler singletonhandler;
        private static object syncobject = new Object();
        private SqlConnection connection;

        private SQLHandler()
        {
            connection = new SqlConnection("Data Source=webhotel10.iha.dk;Initial Catalog=F12I4PRJ4Gr3;Persist Security Info=True;User ID=F12I4PRJ4Gr3;Password=F12I4PRJ4Gr3");
        }

        public static SQLHandler GetInstance
        {
            get
            {
                if (singletonhandler == null)
                {
                    lock (syncobject)
                    {
                        if (singletonhandler == null)
                            singletonhandler = new SQLHandler();
                    }
                }
                return singletonhandler;
            }
        }
        /// <summary>
        /// Sets a new connection to use.
        /// </summary>
        /// <param name="_server">Paramater for ip/domain to use</param>
        /// <param name="_database">Parameter for which database to use</param>
        /// <param name="_username">Parameter for username to use</param>
        /// <param name="_password">Parameter for password to use</param>
        /// <param name="_timeout">Parameter for what timeout should be</param> 
        public bool setConnection(string _server, string _database,   string _username, string _password, string _timeout)
        {
            bool check = true;
            string tempString = "Data Source=" + _server + ";" + "Initial Catalog=" + _database + ";"
                + "User ID=" + _username + ";" + "Password=" + _password + ";" + 
                "Connection Timeout=" + _timeout + ";";

            string formerConnectionstring = connection.ConnectionString;
            connection.ConnectionString = tempString;

            /*
            try
            {
                connection.Open();
                connection.Close();
            }
            catch (Exception e)
            {
                check = false;
            }

            if (!check)
            {
                connection.ConnectionString = formerConnectionstring;
            }
            */
            return true;//check;
        }

        /// <summary>
        /// Define a SQL command to use and execute
        /// </summary>
        /// <param name="_commandText">Parameter for the command text</param>
        /// <param name="_commandType">Parameter for what type text needs to be interpreted.</param>
        /// <returns></returns>
        public SqlCommand makeCommand(string _commandText, CommandType _commandType)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandTimeout = connection.ConnectionTimeout;
            command.CommandType = _commandType;
            command.CommandText = _commandText;
            
            return command;
        }

        /// <summary>
        /// Function adds a parameter to the command.
        /// </summary>
        /// <param name="_command">Parameter is the command made in makeCommand</param>
        /// <param name="_parameterName">Parameter is name of the parameter defined in makeCommand</param>
        /// <param name="_parameterValue">Parameter is value to be used in the command</param>
        /// <param name="_parameterType">Parameter is of which type the parameter is</param>
        public void addParameter(SqlCommand _command, string _parameterName, object _parameterValue, SqlDbType _parameterType)
        {
            if (!_parameterName.StartsWith("@"))
            {
                _parameterName = "@" + _parameterName;
            }

            _command.Parameters.Add(_parameterName, _parameterType);
            _command.Parameters[_parameterName].Value = _parameterValue;
        }

        /// <summary>
        /// Executes the supplied command on SQL server
        /// </summary>
        /// <param name="_command">Parameter is command to be executed</param>
        /// <param name="queryType">Parameter is what kind of query to execute (write/read)</param>
        /// <returns>Returns Null if write, if read returns a Datareader</returns>
        public ISQLReader runQuery(SqlCommand _command, string queryType)
        {
            // Check if connection is open and open is not.
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            // Type
            if(queryType.ToLower() == "write")
            {
                int changed = _command.ExecuteNonQuery();

                if(changed == 0)
                    throw new Exception("Statement changed nothing");

            }
            else if(queryType.ToLower() == "read")
            {
                SqlDataReader reader = _command.ExecuteReader();
               
                return new SQLReader(reader);
            }
            else
            {
                throw new Exception("Incorrect query type");
            }
               
            return null; 
        }

        /// <summary>
        /// Changes 1 specific parameter in connection info.
        /// </summary>
        /// <param name="_parameter">Parameter is part of or full parameter name to change</param>
        /// <param name="_parameterValue">Parameter is value to change the connection info to</param>
        public void changeConnectionparameter(string _parameter, string _parameterValue)
        {
            int index = connection.ConnectionString.ToLower().IndexOf(_parameter.ToLower());

            if(index == -1 || _parameter == ";" || _parameter == " " || _parameter == "=")
                throw new Exception(_parameter + " parameter not found or invalid");

            int indexEqual = connection.ConnectionString.IndexOf('=', index);
            int indexEnd = connection.ConnectionString.IndexOf(';', indexEqual);

            string substringBefore = connection.ConnectionString.Substring(0, indexEqual+1);
            string substringAfter = connection.ConnectionString.Substring(indexEnd);

            string tempString = substringBefore + _parameterValue + substringAfter;
            connection.ConnectionString = tempString;
        }
    }
}
