using Autofac;
using MSI.Keyboard.Backlight.Manager.UI.ViewModels;
using System.Windows.Controls.Primitives;

namespace MSI.Keyboard.Backlight.Manager.UI.Views
{
    /// <summary>
    /// Interaction logic for StatusBarView.xaml
    /// </summary>
    public partial class StatusBarView : StatusBar
    {
        public StatusBarView()
        {
            DataContext = App.ContainerScope.Resolve<StatusBarViewModel>();

            InitializeComponent();
        }
    }
}
