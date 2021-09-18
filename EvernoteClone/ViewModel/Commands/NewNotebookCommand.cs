using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {
        public NotesVM NotesVM { get; }

        public event EventHandler CanExecuteChanged;

        public NewNotebookCommand(NotesVM notesVM)
        {
            NotesVM = notesVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NotesVM.CreateNewNotebook();
        }
    }
}
