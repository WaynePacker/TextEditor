using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TextEditor.Base;
using TextEditor.Components;
using TextEditor.Interfaces;

namespace TextEditor.ViewModel
{
    public class MainViewModel : AViewModel
    {
        private readonly IUIController controller;
        public MainViewModel()
        {
            controller = UIController.Instance;
            Files = new ObservableCollection<IDocument>();
            controller.PropertyChanged += Controller_PropertyChanged;
        }

        private void Controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var updateSource = (IUIController)sender;
            if (e.PropertyName == nameof(updateSource.CurrentOpenFiles))
            {
                Files = updateSource.CurrentOpenFiles;
                RaisePropertyChanged(nameof(Files));
            }
            if(e.PropertyName == nameof(updateSource.ActiveDocument))
            {
                SelectedItem = updateSource.ActiveDocument;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public ObservableCollection<IDocument> Files { get; set;}

        public IDocument SelectedItem { get; set; }

        public ICommand NewCommand { get { return controller.NewCommand; } }

        public ICommand OpenCommand { get { return controller.OpenCommand; } }

        public ICommand SaveCommand { get { return controller.SaveCommand; } }

        public ICommand CloseCommand { get { return controller.CloseCommand; } }

    }
}
