using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SqlInteraction
{

    public interface ISQLCommand
    {
        int CommandTimeout { get; set; }
        string CommandText { get; set; }
    }

    public class SQLCommand : ISQLCommand
    {
        private SQLCommand _sqlCommand; 

        public int CommandTimeout
        {
            get { return _sqlCommand.CommandTimeout; }
            set
            {
                if( value > 0 )
                {
                    _sqlCommand.CommandTimeout = value; 
                }
            }
        }

        public string CommandText
        {
            get { return _sqlCommand.CommandText; }
            set { _sqlCommand.CommandText = value; }
        }
    }
}
