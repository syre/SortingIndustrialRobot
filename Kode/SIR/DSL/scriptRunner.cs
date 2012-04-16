/** \file scriptRunner.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace DSL
{
    /// <summary>
    /// Interface for a script runner.
    /// 
    /// Used for Unit testing primarily.
    /// </summary>
    public interface IScriptRunner
    {
        IScriptRunner getInstance();
        void setRobotInstance(IRobot _iroboRobot);
        void setScriptFromFile(string _sPath);
        void setScriptFromString(string _sScript);
        void ExecuteScript();
    }
    /// <summary>
    /// Used to run IronPython scripts.
    /// 
    /// Note: Needs IronPython, IronPython.Modules and Microsoft.Scripting assemblies as reference
    /// </summary>
    public class ScriptRunner : IScriptRunner
    {
        private static ScriptEngine _engine;
        private static ScriptRuntimeSetup _setup;
        private static ScriptRuntime _runtime;
        private static ScriptSource _source;
        private static ScriptScope _scope;
        private static ErrorReporter _reporter;
        private static IRobot _robot;

        // Singleton
        private static ScriptRunner srInstance;
        /// <summary>
        /// Gets the instance of the ScriptRunner.
        /// </summary>
        /// <returns></returns>
        public IScriptRunner getInstance()
        {
            if(srInstance == null) 
               srInstance = new ScriptRunner();

            return (srInstance);
        }

        /// <summary>
        ///  Initializing the python engine.
        /// </summary>
        private ScriptRunner()
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

        public void setRobotInstance(IRobot _iroboRobot)
        {
            _robot = _iroboRobot;
            _scope.SetVariable("_robot", _robot);
        }

        /// <summary>
        ///  Loads script from a file
        /// </summary>
        public void setScriptFromFile(string _sPath)
        {
            _source = _engine.CreateScriptSourceFromFile(_sPath);
            CompiledCode codecheck = _source.Compile(_reporter);
            if (codecheck == null)
            {
                // compilation failed - alert user
                _source = null;
                throw new Exception("compilation failed");
            }
        }

        /// <summary>
        ///  Loads script from a string
        /// </summary>
        public void setScriptFromString(string _sScript)
        {
           _source = _engine.CreateScriptSourceFromString(_sScript);
           CompiledCode codecheck = _source.Compile(_reporter);
           if (codecheck == null)
           {
               throw new Exception("compilation failed");
               // compilation failed - alert user
               _source = null; /// \warning Unreachable
           }
        }

        /// <summary>
        ///  Executes loaded script
        /// </summary>
        public void ExecuteScript()
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