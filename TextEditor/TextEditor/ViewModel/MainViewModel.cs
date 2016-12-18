using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TextEditor.Base;
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
            controller.ActiveDocumentChanged += Controller_ActiveDocumentChanged;
            controller.CurrentOpenFilesChanged += Controller_CurrentOpenFilesChanged;
        }

        private void Controller_CurrentOpenFilesChanged(object sender, PropertyChangedEventArgs e)
        {
            var updateSource = (IUIController)sender;
            Files = updateSource.CurrentOpenFiles;
            RaisePropertyChanged(nameof(Files));
        }

        private void Controller_ActiveDocumentChanged(object sender, PropertyChangedEventArgs e)
        {
            var updateSource = (IUIController)sender;
            SelectedItem = updateSource.ActiveDocument;
            RaisePropertyChanged(nameof(SelectedItem));
        }

        public ObservableCollection<IDocument> Files { get; set;}

        public IDocument SelectedItem
        {
            get { return controller.ActiveDocument; }
            set {
                    if (controller.ActiveDocument != value)
                        controller.ActiveDocument = value;
                }
        }

        public ICommand NewCommand { get { return controller.NewCommand; } }

        public ICommand OpenCommand { get { return controller.OpenCommand; } }

        public ICommand SaveCommand { get { return controller.SaveCommand; } }

        public ICommand CloseCommand { get { return controller.CloseCommand; } }

    }
}
