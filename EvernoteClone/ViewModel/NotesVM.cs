using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;
        public Notebook SelectedNotebook
        {
            get => selectedNotebook;
            set
            {
                selectedNotebook = value;
                GetNotes();
            }
        }
        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; }
        public NewNoteCommand NewNoteCommand { get; }

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);

            //GetNotebooks();
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

        public void GetNotebooks()
        {
            List<Notebook> notebooks = DatabaseHelper.Read<Notebook>();
            Notebooks?.Clear();
            Notebooks = new ObservableCollection<Notebook>(notebooks);
        }
        public void GetNotes()
        {
            if (SelectedNotebook == null) return;

            List<Note> notes = DatabaseHelper.GetByColumnName<Note>("NotebookId", SelectedNotebook.Id);
            Notes?.Clear();
            Notes = new ObservableCollection<Note>(notes);
        }

    }
}
