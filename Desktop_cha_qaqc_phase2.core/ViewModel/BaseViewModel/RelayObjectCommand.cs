using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop_cha_qaqc_phase2.Core.ViewModel.BaseViewModel
{
    class RelayObjectCommand<T>: ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayObjectCommand (Predicate<T> canExecute,Action<T> execute)
        {
            if ( execute==null )
                throw new ArgumentNullException("execute");
            _canExecute=canExecute;
            _execute=execute;
        }

        public bool CanExecute (object parameter)
        {
            return _canExecute==null ? true : _canExecute((T)parameter);
        }

        public void Execute (object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested+=value; }
            remove { CommandManager.RequerySuggested-=value; }
        }
    }

}
