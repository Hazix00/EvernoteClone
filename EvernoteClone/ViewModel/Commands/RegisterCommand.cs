using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginVM LoginVM { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RegisterCommand(LoginVM loginVM)
        {
            LoginVM = loginVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO : Register functionality
        }
    }
}
