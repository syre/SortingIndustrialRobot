using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace DSL_component
{
    static class ScriptRunner
    {
        static private ScriptRuntimeSetup _setup;
        static private ScriptRuntime _runtime;
        static private ScriptEngine _engine;
        private static ScriptSource _source;
        private static ScriptScope _scope;
        static ScriptRunner()
        {
            _setup = Python.CreateRuntimeSetup(null);
            _runtime = new ScriptRuntime(_setup);
            _engine = Python.GetEngine(_runtime);
        }
        public static ScriptEngine getEngine()
        {
            return _engine;
        }

        public static void setScriptFromFile(string path)
        {
            _source = _engine.CreateScriptSourceFromFile(path);
            _scope = _engine.CreateScope();
        }

        public static void setScriptFromString(string script)
        {
           _scope = _engine.CreateScope();
           _source = _engine.CreateScriptSourceFromString(script);
        }

        public static void RunRobotFunction(string script, Robot r)
        {
            _scope = _engine.CreateScope();
            _scope.SetVariable("robot", r);


            _source = _engine.CreateScriptSourceFromString("robot." + script, Microsoft.Scripting.SourceCodeKind.Statements);
        }

        public static void ExecuteScript()
        {
            if (_source == null)
            {
                throw new Exception();
            }
                _source.Execute(_scope);
        }
    }
}