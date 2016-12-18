using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TextEditor.Commands;
using TextEditor.Components;
using TextEditor.Interfaces;

namespace TextEditor
{
    public class UIController : IUIController, INotifyPropertyChanged
    {
        private static IUIController instance;
        IDocument activeDocument;


        private UIController()
        {
            instance = this;
            newCommand = new RelayCommand(p => OnNew());
            openCommand = new RelayCommand(p => OnOpen());
            saveCommand = new RelayCommand(p => OnSave(), p => CanSave());
            closeCommand = new RelayCommand(p => OnClose(), p => CanClose());

            CurrentOpenFiles = new ObservableCollection<IDocument>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler ActiveDocumentChanged;
        public event PropertyChangedEventHandler CurrentOpenFilesChanged;
 
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(ActiveDocument):
                    ActiveDocumentChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                    break;
                case nameof(CurrentOpenFiles):
                    CurrentOpenFilesChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                    break;
         
                default: PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                    break;
            }

            
        }

        public static IUIController Instance
        {
            get
            {
                return instance ?? new UIController();
            }
        }

        public IDocument ActiveDocument
        {
            get
            {
                return activeDocument;
            }

            set
            {
                if (activeDocument != value)
                {
                    activeDocument = value;
                    RaisePropertyChanged(nameof(ActiveDocument));
                }
            }
        }

        public ObservableCollection<IDocument> CurrentOpenFiles { get; set; }

        ICommand newCommand;
        ICommand openCommand;
        ICommand saveCommand;
        ICommand closeCommand;

        public ICommand NewCommand { get { return newCommand; } }

        public ICommand OpenCommand { get { return openCommand; } }

        public ICommand SaveCommand { get { return saveCommand; } }

        public ICommand CloseCommand { get { return closeCommand; } }

        private void OnNew()
        {
            var newDocument = new Document();
            this.CurrentOpenFiles.Add(newDocument);
            this.ActiveDocument = newDocument;
            RaisePropertyChanged(nameof(CurrentOpenFiles));
        }

        private void OnOpen()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                var fileViewModel = Open(dlg.FileName);
                ActiveDocument = fileViewModel;
            }
        }

        public IDocument Open(string filepath)
        {
            var fileViewModel = CurrentOpenFiles.FirstOrDefault(fm => fm.FilePath == filepath);
            if (fileViewModel != null)
                return fileViewModel;

            fileViewModel = new Document(filepath);
            CurrentOpenFiles.Add(fileViewModel);
            RaisePropertyChanged(nameof(CurrentOpenFiles));
            return fileViewModel;
        }



        private bool CanSave()
        {
            throw new NotImplementedException();
        }

        private void OnSave()
        {
            throw new NotImplementedException();
        }

        private bool CanClose()
        {
            throw new NotImplementedException();
        }

        private void OnClose()
        {
            throw new NotImplementedException();
        }
    }

}
