using Autofac;
using MSI.Keyboard.Backlight.Manager.UI.IoC;
using System.Windows;

namespace MSI.Keyboard.Backlight.Manager.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ILifetimeScope _containerLifeTimeScope { get; private set; }

        private readonly AppContainerBuilder _appContainerBuilder;

        public App()
        {
            _appContainerBuilder = new AppContainerBuilder();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            _containerLifeTimeScope.Dispose();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var container = _appContainerBuilder.Build();

            _containerLifeTimeScope = container.BeginLifetimeScope();

            var window = _containerLifeTimeScope.Resolve<MainWindow>();
            window.Show();
        }
    }
}
