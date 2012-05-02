/** \ISQLReader.cs */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SqlInteraction
{
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
        List<string> readRow();
    }
}
