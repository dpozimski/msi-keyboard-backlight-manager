using Autofac;
using MSI.Keyboard.Backlight.Manager.Analytics;
using MSI.Keyboard.Backlight.Manager.Notifications;
using MSI.Keyboard.Backlight.Manager.UI.IoC;
using MSI.Keyboard.Backlight.Manager.UI.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MSI.Keyboard.Backlight.Manager.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static ILifetimeScope ContainerScope { get; private set; }

        private readonly AppContainerBuilder _appContainerBuilder;
        private IAnalyticsService _analyticsService;

        public App()
        {
            _appContainerBuilder = new AppContainerBuilder();
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            ContainerScope.Dispose();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var container = _appContainerBuilder.Build();

            ContainerScope = container.BeginLifetimeScope();

            _analyticsService = ContainerScope.Resolve<IAnalyticsService>();

            var mainWindow = ContainerScope.Resolve<MainWindow>();
            var notificationsService = ContainerScope.Resolve<INotificationService>();
            var singleInstanceValidator = ContainerScope.Resolve<ISingleInstanceValidator>();

            if(!singleInstanceValidator.Validate())
            {
                _analyticsService.TrackEvent("SingleInstanceValidator_Error");
                notificationsService.ShowError("Application is already running");

                await Task.Delay(5000);

                Current.Shutdown();
            }

            _analyticsService.TrackEvent("Load_Completed");

            mainWindow.Show();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _analyticsService.TrackException(e.Exception);

            e.Handled = true;
        }
    }
}
