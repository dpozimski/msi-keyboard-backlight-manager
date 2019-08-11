using Autofac;
using MSI.Keyboard.Backlight.Manager.Analytics;
using MSI.Keyboard.Backlight.Manager.Notifications;
using MSI.Keyboard.Backlight.Manager.UI.IoC;
using MSI.Keyboard.Backlight.Manager.UI.Services;
using MSI.Keyboard.Backlight.Manager.UI.ViewModels;
using System.Threading.Tasks;
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

        protected override async void OnStartup(StartupEventArgs e)
        {
            var container = _appContainerBuilder.Build();

            ContainerScope = container.BeginLifetimeScope();

            var mainWindow = ContainerScope.Resolve<MainWindow>();
            var mainWindowViewModel = ContainerScope.Resolve<MainWindowViewModel>();
            var notificationsService = ContainerScope.Resolve<INotificationService>();
            var singleInstanceValidator = ContainerScope.Resolve<ISingleInstanceValidator>();
            var analyticsConfiguration = ContainerScope.Resolve<IAnalyticsConfiguration>();

            if(!singleInstanceValidator.Validate())
            {
                notificationsService.ShowError("Application is already running");

                await Task.Delay(5000);

                Current.Shutdown();
            }
            
            analyticsConfiguration.Configure();

            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
