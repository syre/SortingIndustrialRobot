/** \file ILogEvent.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlInteraction
{
    /// <summary>
    /// Interface for event logs.
    /// 
    /// Used in conjunction with SQL database.
    /// </summary>
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
