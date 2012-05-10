/** \file SQLReader.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SqlInteraction
{
    public class SQLReader : ISQLReader
    {
        private SqlDataReader sqlDataReader;
        public SqlDataReader SQLCoreReader
        {
            get { return (sqlDataReader); }
            set { sqlDataReader = value; }
        }
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
    }
}
