/** \file SQLReader.cs */
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

        public List<string> readRow()
        {
            if (sqlDataReader.Read())
            {
                List<string> lstAttributes = new List<string>();
                for(int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    lstAttributes.Add((string)sqlDataReader[i]);
                }
                return (lstAttributes);
            }
            return (new List<string>());
        }
    }
}
