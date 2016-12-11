using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TextEditor.UIControls
{
    public partial class Toolbar : UserControl
    {
        public Toolbar()
        {
            InitializeComponent();
            this.LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty NewCommandProperty =
         DependencyProperty.Register("NewCommand", typeof(ICommand),
         typeof(Toolbar));

        public ICommand NewCommand
        {
            get
            {
                return (ICommand)GetValue(NewCommandProperty);
            }
            set
            {
                SetValue(NewCommandProperty, value);
            }
        }

        public static readonly DependencyProperty OpenCommandProperty =
         DependencyProperty.Register("OpenCommand", typeof(ICommand),
         typeof(Toolbar));

        public ICommand OpenCommand
        {
            get
            {
                return (ICommand)GetValue(OpenCommandProperty);
            }
            set
            {
                SetValue(OpenCommandProperty, value);
            }
        }

        public static readonly DependencyProperty SaveCommandProperty =
         DependencyProperty.Register("SaveCommand", typeof(ICommand),
         typeof(Toolbar));

        public ICommand SaveCommand
        {
            get
            {
                return (ICommand)GetValue(SaveCommandProperty);
            }
            set
            {
                SetValue(SaveCommandProperty, value);
            }
        }

        public static readonly DependencyProperty CloseCommandProperty =
         DependencyProperty.Register("CloseCommand", typeof(ICommand),
         typeof(Toolbar));

        public ICommand CloseCommand
        {
            get
            {
                return (ICommand)GetValue(CloseCommandProperty);
            }
            set
            {
                SetValue(CloseCommandProperty, value);
            }
        }

    }
}
