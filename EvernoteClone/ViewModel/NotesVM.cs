using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EvernoteClone.ViewModel
{
    public class NotesVM
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set 
            { 
                selectedNotebook = value;
                //TODO get notes
            }
        }
        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; }
        public NewNoteCommand NewNoteCommand { get; }

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

        public void CreateNewNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "Untitled Notebook"
            };
            DatabaseHelper.Insert(newNotebook);
        }
        public void CreateNewNote()
        {
            DateTime time = DateTime.Now;
            Note newNote = new Note()
            {
                NotebookId = SelectedNotebook.Id,
                CreatedAt = time,
                UpdatedAt = time,
                Title = "Untitled"
            };
            DatabaseHelper.Insert(newNote);
        }

    }
}
