using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Files = controller.CurrentOpenFiles;
        }

        public ObservableCollection<IDocument> Files { get; set;}

    }
}
