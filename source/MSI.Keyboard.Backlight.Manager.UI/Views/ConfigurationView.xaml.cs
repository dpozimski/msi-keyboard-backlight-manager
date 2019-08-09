using Autofac;
using MSI.Keyboard.Backlight.Manager.UI.ViewModels;
using System.Windows.Controls;

namespace MSI.Keyboard.Backlight.Manager.UI.Views
{
    public partial class ConfigurationView : UserControl
    {
        public ConfigurationView()
        {
            DataContext = App.ContainerScope.Resolve<ConfigurationViewModel>();

            InitializeComponent();
        }
    }
}
