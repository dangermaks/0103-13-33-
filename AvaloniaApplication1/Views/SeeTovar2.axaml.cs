using Avalonia.Controls;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views
{
    public partial class SeeTovar2 : Window
    {
        public SeeTovar2()
        {
            InitializeComponent();
            DataContext = new SeeTovar2ViewModel();
        }
    }
}
