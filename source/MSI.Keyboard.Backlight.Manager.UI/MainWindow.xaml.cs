using MSI.Keyboard.Backlight.Manager.UI.ViewModels;
using System.Windows;

namespace MSI.Keyboard.Backlight.Manager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}
