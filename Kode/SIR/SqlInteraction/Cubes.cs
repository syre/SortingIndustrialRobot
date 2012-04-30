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

        /// <summary>
        /// Connecting with a username and password
        /// </summary>
        /// <param name="_username">Guess</param>
        /// <param name="_password">Guess</param>
        /// <returns>If it is connected</returns>
        public bool Connect(string _username, string _password)
        {
            username = _username;
            password = _password;

            return sql.setConnection(username, password, server, trustedConnection, database, ConnectionTimeout);
        }

        public IEnumerable<Cube> StoredCubesInDatabase()
        {
            string command = "select * from " + database;
            
            SqlDataReader reader = sql.runQuery(sql.makeCommand(command, CommandType.Text), "read");
            while (reader.Read())
            {

                yield return new Cube()
                                    {
                                        BoxID = Convert.ToInt32(reader[0]),
                                        PositionID = Convert.ToInt32(reader[1]),
                                        Length = Convert.ToDouble(reader[2]),
                                        Width = Convert.ToDouble(reader[3]),
                                        Depth = Convert.ToDouble(reader[4]),
                                        Weight = Convert.ToDouble(reader[5])
                                    };
            }
            
        }

    }
}
