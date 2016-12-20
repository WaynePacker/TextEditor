using System.Windows;
using System.Windows.Controls;
using TextEditor.ViewModel;

namespace TextEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new MainViewModel();
        }

        public MainViewModel ViewModel { get; private set; }
    }
}
