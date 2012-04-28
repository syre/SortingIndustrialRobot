using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlInteraction
{
    public class Cubes
    {
        private SQLHandler sql;
        private string username;
        private string password;
        private string server;
        private readonly string database;
        private readonly string ConnectionTimeout;
        private readonly string trustedConnection;


        /// <summary>
        /// create a cubes object for getting the stored cubes in the database
        /// </summary>
        /// <param name="_databaseName">database name</param>
        /// <param name="_server">The server name </param>
        /// <param name="_connTimeOut">connectiontimeout value</param>
        /// <param name="_trusted">if the connection to the database is trusted</param>
        public Cubes(string _databaseName, string _server, int _connTimeOut, bool _trusted)
        {
            database = _databaseName;
            ConnectionTimeout = Convert.ToString(_connTimeOut);
            trustedConnection = _trusted ? "yes" : "no";
            server = _server;

            sql = new SQLHandler();
        }

        public bool Connect(string _username, string _password)
        {
            username = _username;
            password = _password;

            return sql.setConnection(username, password, server, trustedConnection, database, ConnectionTimeout);
        }

        public List<Cube> StoredCubesInDatabase()
        {
            string command = "select * from " + database;
            sql.makeCommand(command, new CommandType());

            //commandtype?
            SqlDataReader reader = sql.runQuery(sql.makeCommand(command, new CommandType()), "read");

            reader.t
        }

    }
}
