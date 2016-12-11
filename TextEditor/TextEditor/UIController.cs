using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class UIController : IUIController
    {
        private static IUIController instance;

        private UIController()
        {
            instance = this;
            newCommand = new RelayCommand(p => OnNew());
            openCommand = new RelayCommand(p => OnOpen());
            saveCommand = new RelayCommand(p => OnSave(), p => CanSave());
            closeCommand = new RelayCommand(p => OnClose(), p => CanClose());

            CurrentOpenFiles = new ObservableCollection<IDocument>();

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
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
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
            throw new NotImplementedException();
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
