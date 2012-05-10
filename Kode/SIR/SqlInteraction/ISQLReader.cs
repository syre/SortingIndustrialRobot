/** \ISQLReader.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SqlInteraction
{
    /// <summary>
    /// Interface for class that read information from a SQL table.
    /// 
    /// Used primarily as holder for already gathered information from a database.
    /// </summary>
    public interface ISQLReader
    {
        /// <summary>
        /// The underlying reader.
        /// </summary>
        SqlDataReader SQLCoreReader {get; set; }

        /// <summary>
        /// Reads one row from the table.
        /// 
        /// On next call reads next row. If any.
        /// </summary>
        /// <returns>The row if any else empty list.</returns>
        List<object> readRow();
    }
}
