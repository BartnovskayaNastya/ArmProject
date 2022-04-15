using System;
using System.Windows.Input;

namespace ArmBazaProject
{
    public class DelegateCommand : ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        public bool isVisible = true;

        // Событие, необходимое для ICommand
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        // Два конструктора
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
            canExecute = this.AlwaysCanExecute;
        }

        // Методы, необходимые для ICommand
        public void Execute(object param)
        {
            execute(param);
        }

        public bool CanExecute(object parameter)
        {
            return isVisible;
        }

        

        // Метод CanExecute по умолчанию
        private bool AlwaysCanExecute(object param)
        {
            return true;
        }

    }
}