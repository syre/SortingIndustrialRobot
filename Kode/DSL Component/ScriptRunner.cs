using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace DSL_component
{
    /// <summary>
    ///  needs IronPython, IronPython.Modules and Microsoft.Scripting assemblies as reference
    /// </summary>

    static class ScriptRunner
    {
        private static ScriptRuntimeSetup _setup;
        private static ScriptRuntime _runtime;
        private static ScriptEngine _engine;
        private static ScriptSource _source;
        private static ScriptScope _scope;

        /// <summary>
        ///  initializing the python engine
        /// </summary>
        static ScriptRunner()
        {
            _setup = Python.CreateRuntimeSetup(null);
            _runtime = new ScriptRuntime(_setup);
            _engine = Python.GetEngine(_runtime);
            _scope = _engine.CreateScope();
        }

        public static ScriptEngine getEngine()
        {
            return _engine;
        }

        /// <summary>
        ///  loads script from a file
        /// </summary>
        public static void setScriptFromFile(string path)
        {
            _source = _engine.CreateScriptSourceFromFile(path);
        }

        /// <summary>
        ///  loads script from a string
        /// </summary>
        public static void setScriptFromString(string script)
        {

           _source = _engine.CreateScriptSourceFromString(script);
        }

        /// <summary>
        ///  allows us to run the robot object functions from python
        /// </summary>
        public static void RunRobotFunction(string script, Robot r)
        {
            _scope = _engine.CreateScope();
            ObjectOperations objOps = _engine.Operations;
            
            // removing the need for robot scope
            foreach (string memberName in objOps.GetMemberNames(r))
            {
                _scope.SetVariable(memberName, objOps.GetMember(r, memberName));
            }

            _source = _engine.CreateScriptSourceFromString(script, Microsoft.Scripting.SourceCodeKind.Statements);
        }

        /// <summary>
        ///  executes loaded script
        /// </summary>
        public static void ExecuteScript()
        {
            if (_source != null)
            {
                _source.Execute(_scope);
                
            }
            else
                throw new Exception();
        }
    }
}