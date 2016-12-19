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
using TextEditor.Base;
using TextEditor.Commands;
using TextEditor.Components;
using TextEditor.Interfaces;

namespace TextEditor
{
    public class UIController : ANotifyBase, IUIController
    {
        private static IUIController instance;
        IDocument activeDocument;


        private UIController()
        {
            instance = this;
            newCommand = new RelayCommand(p => OnNew());
            openCommand = new RelayCommand(p => OnOpen());
            saveCommand = new RelayCommand(p => OnSave(), p => CanSave());
            closeCommand = new RelayCommand(p => OnClose(p), p => CanClose());

            CurrentOpenFiles = new ObservableCollection<IDocument>();
            this.PropertyChanged += UIController_PropertyChanged;
        }

        private void UIController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ActiveDocument):
                    ActiveDocumentChanged.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
                    break;
                case nameof(CurrentOpenFiles):
                    CurrentOpenFilesChanged.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
                    break;
            }
        }

        public event PropertyChangedEventHandler ActiveDocumentChanged;
        public event PropertyChangedEventHandler CurrentOpenFilesChanged;

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
                SetAndRaisePropertyChanged(ref activeDocument, value);
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
            return true;
        }

        private void OnSave()
        {
            if (string.IsNullOrEmpty(ActiveDocument.FilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "Text | *.txt",
                    Title = "Save File"
                };
                
                if (saveFileDialog.ShowDialog().GetValueOrDefault())
                        ActiveDocument.FilePath = saveFileDialog.FileName;
            }

            File.WriteAllText(ActiveDocument.FilePath, ActiveDocument.Content);
            ActiveDocument.HasChanges = false;
        }

        private bool CanClose()
        {
            return true;
        }

        private void OnClose(object documentToClose = null)
        {
            var toClose = (IDocument)documentToClose ?? ActiveDocument;
            if (toClose.HasChanges)
            {
                var res = MessageBox.Show(string.Format("Save changes for file '{0}'?", toClose.Name), "Save Changes", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Cancel)
                    return;
                if (res == MessageBoxResult.Yes)
                {
                    OnSave();
                }
            }

            CurrentOpenFiles.Remove(toClose);
            ActiveDocument = CurrentOpenFiles.FirstOrDefault();
        }
    }

}
