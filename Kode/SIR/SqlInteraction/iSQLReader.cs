/** \ISQLReader.cs */
/** \author Robotic Global Organization(RoboGO) */

using System.Collections.Generic;

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
        /// Read one table row.
        /// 
        /// For each call the next row is read.
        /// </summary>
        /// <returns>List of objects from the table. Empty list if end of table or no data in table.</returns>
        List<object> readRow();

        /// <summary>
        /// Closes the reader.
        /// </summary>
        void close();
    }
}
