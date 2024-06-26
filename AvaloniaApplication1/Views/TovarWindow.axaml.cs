using Avalonia.Controls;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views
{
    public partial class TovarWindow : Window
    {
        public TovarWindow()
        {
            InitializeComponent();
            DataContext = new TovarViewModel();
        }
    }
}
