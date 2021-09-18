using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesVM NotesVM { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public NewNoteCommand(NotesVM notesVM)
        {
            NotesVM = notesVM;
        }

        public bool CanExecute(object parameter) => parameter is Notebook;

        public void Execute(object parameter)
        {
            NotesVM.CreateNewNote();
        }
    }
}
