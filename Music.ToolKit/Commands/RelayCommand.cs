using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.ToolKit.Commands
{
    /// <summary>
    /// 继承链命令，将bool改为判断的委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : DelegateCommandBase
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        protected override bool CanExecute(object parameter)
        {
            return parameter != null && CanExecute((T)parameter);
        }
        protected override void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        public bool CanExecute(T parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }
        public void Execute(T parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
