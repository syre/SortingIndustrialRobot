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

    static class ScriptRunner
    {
        private static ScriptRuntimeSetup _setup;
        private static ScriptRuntime _runtime;
        private static ScriptEngine _engine;
        private static ScriptSource _source;
        private static ScriptScope _scope;
        private static ErrorReporter _reporter;
        private static ObjectOperations _operations;
        private static Wrapper _wrapper;

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
            // initializing wrapper methods from methods.py file placed in root dir
            setScriptFromFile("../../methods.py");
            ExecuteScript();
        }

        public static void setRobotInstance(Wrapper r)
        {
            _wrapper = r;
            _operations = _engine.Operations;

            // removing the need for robot scope
            foreach (string memberName in _operations.GetMemberNames(_wrapper))
            {
                _scope.SetVariable(memberName, _operations.GetMember(_wrapper, memberName));
            }
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
               System.Console.WriteLine("compilation failed");
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