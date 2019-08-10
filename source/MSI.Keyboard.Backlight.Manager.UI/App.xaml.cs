using Autofac;
using MSI.Keyboard.Backlight.Manager.Analytics;
using MSI.Keyboard.Backlight.Manager.UI.IoC;
using System.Windows;

namespace MSI.Keyboard.Backlight.Manager.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static ILifetimeScope ContainerScope { get; private set; }

        private readonly AppContainerBuilder _appContainerBuilder;

        public App()
        {
            _appContainerBuilder = new AppContainerBuilder();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            ContainerScope.Dispose();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var container = _appContainerBuilder.Build();

            ContainerScope = container.BeginLifetimeScope();

            container.Resolve<IAnalyticsConfiguration>().Configure();
            var window = ContainerScope.Resolve<MainWindow>();
            window.Show();
        }
    }
}
