/** \file scriptRunner.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using SqlInteraction;

namespace ControlSystem
{
    /// <summary>
    /// Interface for a script runner.
    /// 
    /// Used for Unit testing primarily.
    /// </summary>
    public interface IScriptRunner
    {
        /// <summary>
        /// Sets the underlying IRobot that the scripts are used on.
        /// </summary>
        /// <param name="_iroboRobot">Robot to run script on.</param>
        void setRobotInstance(IRobot _iroboRobot);
        
        /// <summary>
        /// Loads script from a file
        /// </summary>
        void setScriptFromFile(string _sPath);
        
        /// <summary>
        ///  Loads script from a string
        /// </summary>
        void setScriptFromString(string _sScript);
        
        /// <summary>
        /// Returns all input from the program.
        /// </summary>
        /// <returns>Output from program.</returns>
        string readFromOutputStream();
        
        /// <summary>
        /// Clear the output.
        /// </summary>
        void clearOutputStream();
        
        /// <summary>
        /// Executes loaded script
        /// </summary>
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
        private static ISQLHandler _sqlhandler;

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
            // sets both outputs to our output memory stream
            _runtime.IO.SetOutput(_outputstream,new StreamWriter(_outputstream));
            _runtime.IO.SetErrorOutput(_outputstream,new StreamWriter(_outputstream));
            _sqlhandler = SQLHandler.GetInstance;
            // initializing robot methods from methods.py file placed in root dir
            setScriptFromFile(@"DSLFiler\methods.py");
            ExecuteScript();
        }

        public void setRobotInstance(IRobot _iroboRobot)
        {    
            _robot = _iroboRobot;
            _scope.SetVariable("_robot", _robot);
            _scope.SetVariable("_sqlhandler", SQLHandler.GetInstance);
        }

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
        
        public void clearOutputStream()
        {
            _outputstream.SetLength(0);
        }

        public void ExecuteScript()
        {
            if (_source != null)
            {
                try
                {
                    _source.Execute(_scope);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Script error:\n" + e.Message, "Script Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                
            }
            else
               throw new Exception();
        }
    }
}