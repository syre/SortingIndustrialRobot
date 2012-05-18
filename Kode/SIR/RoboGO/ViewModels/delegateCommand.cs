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
        public DelegateCommand(Action _aMethodToExecute)
        {
            aMethod = _aMethodToExecute;
        }

        public bool CanExecute(object _objParam)
        {
            return (true);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object _objParam)
        {
            aMethod.Invoke();
        }
    }
}
