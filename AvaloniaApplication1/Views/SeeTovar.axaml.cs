using Avalonia.Controls;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views
{
    public partial class SeeTovar : Window
    {
        public SeeTovar()
        {
            InitializeComponent();
            DataContext = new SeeTovarViewModel();
        }
    }
}
