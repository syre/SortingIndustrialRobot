/** \file errorReporter.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace ControlSystem
{
    /// <summary>
    /// Temporary error reporting class.
    /// 
    /// \warning Temp. class.
    /// </summary>
    public class ErrorReporter : ErrorListener
    {
        public static List<String> Errorlist = new List<string>();

        /// <summary>
        /// Adds error to the error list.
        /// </summary>
        /// <param name="_scrpsrcScriptSource">Script.</param>
        /// <param name="_sErrorMsg">Error message.</param>
        /// <param name="_srcspanSpanning">Span.</param>
        /// <param name="_iCode">Code.</param>
        /// <param name="sevSeverity">Severity.</param>
        public override void ErrorReported(ScriptSource _scrpsrcScriptSource, string _sErrorMsg, SourceSpan _srcspanSpanning, int _iCode, Severity sevSeverity)
        {
            Errorlist.Add(_sErrorMsg);
        }
    }
}
