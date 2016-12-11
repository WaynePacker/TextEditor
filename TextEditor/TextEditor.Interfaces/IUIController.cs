using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TextEditor.Interfaces
{
    public interface IUIController
    {   
        IDocument ActiveDocument { get; set; }

        ICommand NewCommand { get; }
        ICommand OpenCommand { get; }
        ICommand SaveCommand { get; }
        ICommand CloseCommand { get; }

        ObservableCollection<IDocument> CurrentOpenFiles { get; set; }
    }
}
