using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DSL;
using ControlSystem;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// Command class for simple linking between one command and a function.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        // Members
        private Action aMethod;

        // Functions
        public DelegateCommand(Action _aMethod)
        {
            aMethod = _aMethod;
        }
        public void Execute(object _objParam)
        {
            aMethod.Invoke();
        }

        public bool CanExecute(object parameter)
        {
            return (true);
        }

        public event EventHandler CanExecuteChanged;
    }

    /// <summary>
    /// ViewModel between IDEView and ScriptRunner.
    /// </summary>
    public class IDEViewModel
    {
        // Members and properties
        private IScriptRunner isrScriptRunner;
        /// <summary>
        /// ScriptRunner using to execute code.
        /// </summary>
        public IScriptRunner ScriptExecuter
        {
            get { return (isrScriptRunner); }
            set
            {
                if (value == null)
                    throw new Exception();
                else
                    isrScriptRunner = value;
            }
        }

        private string sCode;
        /// <summary>
        /// Code to execute.
        /// </summary>
        public string Code
        {
            get { return (sCode); }
            set { sCode = value; }
        }
        #region Commands
        #endregion

        // Functions
        public IDEViewModel()
        {
            // Members settings
            sCode = "";
            isrScriptRunner = Factory.getScriptRunnerInstance;

            #region Commands

            #endregion
        }

        /// <summary>
        /// Execute the code.
        /// </summary>
        public void executeCode()
        {
            isrScriptRunner.setScriptFromString(Code);

            isrScriptRunner.ExecuteScript();
        }
    }
}
