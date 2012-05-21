/** \file ISQLHandler.cs */
/** \author Robotic Global Organization(RoboGO) */

using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    /// <summary>
    /// Class that handles all SQL interaction
    /// </summary>
    public class SQLHandler : ISQLHandler
    {
        private static volatile ISQLHandler singletonhandler;
        private static object syncobject = new Object();
        private ISqlConnection connection;

        /// <summary>
        /// Singleton Constructor
        /// </summary>
        private SQLHandler()
        {
            Connection = new RobotSqlConnection("Data Source=webhotel10.iha.dk;Initial Catalog=F12I4PRJ4Gr3;Persist Security Info=True;User ID=F12I4PRJ4Gr3;Password=F12I4PRJ4Gr3");
        }

        /// <summary>
        /// Variable to get the SQLHandler singleton instance
        /// </summary>
        public static ISQLHandler GetInstance
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

        public ISqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public bool setConnection(string _server, string _database,   string _username, string _password, string _timeout)
        {
            bool check = true;
            string tempString = "Data Source=" + _server + ";" + "Initial Catalog=" + _database + ";"
                + "User ID=" + _username + ";" + "Password=" + _password + ";" + 
                "Connection Timeout=" + _timeout + ";";

            string formerConnectionstring = Connection.Connectionstring;
            Connection.Connectionstring = tempString;
            
            
            try
            {
                Connection.ConnectionOpen();
                Connection.ConnectionClose();
            }
            catch (Exception)
            {
                check = false;
            }

            Connection.Connectionstring = !check ? formerConnectionstring : tempString;
            
            return true;
        }

        public SqlCommand makeCommand(string _commandText)
        {
            SqlCommand command = Connection.CreateCommand();
            command.CommandTimeout = Connection.TimeOut;
            command.CommandType = CommandType.Text;
            command.CommandText = _commandText;
            
            return command;
        }

        public void addParameter(SqlCommand _command, string _parameterName, object _parameterValue, SqlDbType _parameterType)
        {
            if (!_parameterName.StartsWith("@"))
            {
                _parameterName = "@" + _parameterName;
            }

            _command.Parameters.Add(_parameterName, _parameterType);
            _command.Parameters[_parameterName].Value = _parameterValue;
        }

        public ISQLReader runQuery(SqlCommand _command, string queryType)
        {
            // Check if connection is open and open is not.
            if (Connection.RobotConnectionState == ConnectionState.Closed)
                Connection.ConnectionOpen();

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

        public void changeConnectionparameter(string _parameter, string _parameterValue)
        {
            int index = Connection.Connectionstring.ToLower().IndexOf(_parameter.ToLower(), StringComparison.Ordinal);

            if(index == -1 || _parameter == ";" || _parameter == " " || _parameter == "=")
                throw new Exception(_parameter + " parameter not found or invalid");

            int indexEqual = Connection.Connectionstring.IndexOf('=', index);
            int indexEnd = Connection.Connectionstring.IndexOf(';', indexEqual);

            string substringBefore = Connection.Connectionstring.Substring(0, indexEqual+1);
            string substringAfter = Connection.Connectionstring.Substring(indexEnd);

            string tempString = substringBefore + _parameterValue + substringAfter;
            Connection.Connectionstring = tempString;
        }
    }
}
