/** \file SQLReader.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SqlInteraction
{
    /// <summary>
    /// Class to read table data.
    /// </summary>
    public class SQLReader : ISQLReader
    {
        private SqlDataReader sqlDataReader;
        /// <summary>
        /// Core reader used for functions.
        /// </summary>
        public SqlDataReader SQLCoreReader
        {
            get { return (sqlDataReader); }
            set { sqlDataReader = value; }
        }
        
        /// <summary>
        /// Constructor which takes a SqlDataReader used for the functions.
        /// </summary>
        /// <param name="_sqlDataReader">Data reader for the functions.</param>
        public SQLReader(SqlDataReader _sqlDataReader)
        {
            SQLCoreReader = _sqlDataReader;
        }

        public List<object> readRow()
        {
            if (sqlDataReader.Read())
            {
                List<object> lstAttributes = new List<object>();
                for(int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    lstAttributes.Add(sqlDataReader[i]);
                }
                return (lstAttributes);
            }
            return (new List<object>());
        }

        /// <summary>
        /// Closes the reader and its connection with the database connection.
        /// 
        /// Use this or you can lock other functions from using the database connection.
        /// </summary>
        public void close()
        {
            sqlDataReader.Close();
        }
    }
}
