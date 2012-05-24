/** \file delegateCommand.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Windows.Input;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// Command class for executing one function.
    /// 
    /// Function type: void functionName(void)
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private Action aMethod;
        /// <summary>
        /// Constructor with function to call.
        /// </summary>
        /// <param name="_aMethodToExecute">Function to call when command is used.</param>
        public DelegateCommand(Action _aMethodToExecute)
        {
            aMethod = _aMethodToExecute;
        }

        /// <summary>
        /// Able to execute.
        /// </summary>
        /// <param name="_objParam">Unused from ICommand.</param>
        /// <returns>Always true.</returns>
        public bool CanExecute(object _objParam)
        {
            return (true);
        }

        /// <summary>
        /// EventHandler for if able execute.
        /// 
        /// Note: Not used.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Execute the command from the constructor.
        /// </summary>
        /// <param name="_objParam">Unused from ICommand.</param>
        public void Execute(object _objParam)
        {
            aMethod.Invoke();
        }
    }
}
