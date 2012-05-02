/** \file ILogEvent.cs */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlInteraction
{
    public interface ILogEvent
    {
        // If you set the values I WILL KILL YOU.
        int LogID { get; set; }
        string LogEventType { get; set; }
        DateTime LogTime { get; set; }
        string Description { get; set; }
        int PositionID { get; set; }
    }
}
