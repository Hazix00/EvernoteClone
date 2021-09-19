using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
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

        private Note selectedNote;

        public Note SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        private Visibility isVisible;
        public Visibility IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
            }
        }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public NotesVM()
        {
            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisible = Visibility.Collapsed;

            GetNotebooks();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook
            {
                Name = "Notebook"
            };

            DatabaseHelper.Insert(newNotebook);

            GetNotebooks();
        }

        public void CreateNote(int notebookId)
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

            GetNotes();
        }

        private void GetNotebooks()
        {
            List<Notebook> notebooks = DatabaseHelper.Read<Notebook>();
            Notebooks?.Clear();
            Notebooks = new ObservableCollection<Notebook>(notebooks);
        }

        private void GetNotes()
        {
            if (SelectedNotebook == null) return;

            List<Note> notes = DatabaseHelper.GetByColumnName<Note>("NotebookId", SelectedNotebook.Id);
            Notes?.Clear();
            Notes = new ObservableCollection<Note>(notes);
        }



        public void StartEditing()
        {
            IsVisible = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            IsVisible = Visibility.Collapsed;
            DatabaseHelper.Update(notebook);
            GetNotebooks();
        }
    }
}
