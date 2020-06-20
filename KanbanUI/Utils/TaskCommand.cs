using KanbanUI.Model;
using System;
using System.Windows.Input;

namespace KanbanUI.Utils
{
    public class TaskCommand : ICommand
    {
        Action<TaskModel> _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public TaskCommand(Action<TaskModel> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public TaskCommand(Action<TaskModel> executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {

            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((TaskModel)parameter);
            }
        }
    }
}
