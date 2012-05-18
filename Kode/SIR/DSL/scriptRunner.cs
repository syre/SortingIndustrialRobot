/** \file scriptRunner.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.IO;
using System.Text;
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
        void setRobotInstance(IRobot _iroboRobot);
        void setScriptFromFile(string _sPath);
        void setScriptFromString(string _sScript);
        string readFromOutputStream();
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
        private static MemoryStream _outputstream;
        private static IRobot _robot;

        // Singleton
        private static ScriptRunner srInstance;
        /// <summary>
        /// Gets the instance of the ScriptRunner.
        /// </summary>
        /// <returns></returns>
        public static ScriptRunner getInstance()
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
            _outputstream = new MemoryStream();
            _runtime.IO.SetOutput(_outputstream,new StreamWriter(_outputstream));
            // initializing robot methods from methods.py file placed in root dir
            setScriptFromFile("methods.py");
            ExecuteScript();
        
        }

        /// <summary>
        /// Sets the underlying IRobot that the scripts are used on.
        /// </summary>
        /// <param name="_iroboRobot">Robot to run script on.</param>
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
               _source = null;
               // compilation failed - alert user
               throw new Exception("compilation failed");
           }
        }

        public string  readFromOutputStream()
        {
            int length = (int)_outputstream.Length;

            Byte[] bytes = new Byte[length];

            _outputstream.Seek(0, SeekOrigin.Begin);
            _outputstream.Read(bytes, 0, (int)_outputstream.Length);

            string tempString = Encoding.GetEncoding("utf-8").GetString(bytes, 0, (int)_outputstream.Length);
            
            if(tempString.EndsWith("\r\n"))
            {
                tempString = tempString.Substring(0, tempString.LastIndexOf("\r"));
            }
            return tempString;
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