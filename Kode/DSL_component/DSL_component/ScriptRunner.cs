using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace DSL_component
{
    /// <summary>
    ///  needs IronPython, IronPython.Modules and Microsoft.Scripting assemblies as reference
    /// </summary>

    public static class ScriptRunner
    {
        private static ScriptEngine _engine;
        private static ScriptRuntimeSetup _setup;
        private static ScriptRuntime _runtime;
        private static ScriptSource _source;
        private static ScriptScope _scope;
        private static ErrorReporter _reporter;
        private static IRobot _robot;

        /// <summary>
        ///  initializing the python engine
        /// </summary>
        static ScriptRunner()
        {
            _setup = Python.CreateRuntimeSetup(null);
            _runtime = new ScriptRuntime(_setup);
            _engine = Python.GetEngine(_runtime);
            _scope = _engine.CreateScope();
            _reporter = new ErrorReporter();
            // initializing robot methods from methods.py file placed in root dir
            setScriptFromFile("../../methods.py");
            ExecuteScript();
        }

        public static void setRobotInstance(IRobot r)
        {
            _robot = r;
            _scope.SetVariable("_robot", _robot);
        }

        /// <summary>
        ///  loads script from a file
        /// </summary>
        public static void setScriptFromFile(string path)
        {
            _source = _engine.CreateScriptSourceFromFile(path);
            CompiledCode codecheck = _source.Compile(_reporter);
            if (codecheck == null)
            {
                // compilation failed - alert user
                _source = null;
                throw new Exception("compilation failed");
            }
        }

        /// <summary>
        ///  loads script from a string
        /// </summary>
        public static void setScriptFromString(string script)
        {
           _source = _engine.CreateScriptSourceFromString(script);
           CompiledCode codecheck = _source.Compile(_reporter);
           if (codecheck == null)
           {
               throw new Exception("compilation failed");
               // compilation failed - alert user
               _source = null;
           }
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