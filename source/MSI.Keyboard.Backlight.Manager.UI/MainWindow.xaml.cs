using Autofac;
using MahApps.Metro.Controls;
using MSI.Keyboard.Backlight.Manager.UI.ViewModels;
using System.Windows;

namespace MSI.Keyboard.Backlight.Manager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            DataContext = App.ContainerScope.Resolve<MainWindowViewModel>();
            
            InitializeComponent();
        }
    }
}
