/** \file ErrorReporter.cs */
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

        public override void ErrorReported(ScriptSource source, string errormessage, SourceSpan span, int code, Severity severity)
        {
            Errorlist.Add(errormessage);
        }
    }
}
