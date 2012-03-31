/** \file errorReporter.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace DSL
{
    // temporary error reporting class
    public class ErrorReporter : ErrorListener
    {
        public static List<String> Errorlist = new List<string>();

        public override void ErrorReported(ScriptSource _scrpsrcScriptSource, string _sErrorMsg, SourceSpan _srcspanSpanning, int _iCode, Severity sevSeverity)
        {
            Errorlist.Add(_sErrorMsg);
        }
    }
}
