using KanbanUI.Model;
using System;
using System.Windows.Input;

namespace KanbanUI.Utils
{
    public class ColumnCommand : ICommand
    {
        Action<ColumnModel> _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public ColumnCommand(Action<ColumnModel> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public ColumnCommand(Action<ColumnModel> executeMethod, Func<bool> canExecuteMethod)
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
                _TargetExecuteMethod((ColumnModel)parameter);
            }
        }
    }
}
