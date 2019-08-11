using Autofac;
using Autofac.Extras.DynamicProxy;
using MSI.Keyboard.Backlight.Manager.Analytics;
using MSI.Keyboard.Backlight.Manager.Analytics.IoC;
using MSI.Keyboard.Backlight.Manager.IoC;
using MSI.Keyboard.Backlight.Manager.Notifications.IoC;
using MSI.Keyboard.Backlight.Manager.Settings.IoC;
using MSI.Keyboard.Backlight.Manager.UI.Services;
using MSI.Keyboard.Backlight.Manager.UI.ViewModels;
using System.Windows;

namespace MSI.Keyboard.Backlight.Manager.UI.IoC
{
    public class AppContainerBuilder
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            RegisterViewModels(builder);
            RegisterViews(builder);
            RegisterServices(builder);
            RegisterModules(builder);

            return builder.Build();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<KeyboardBacklightService>().As<IKeyboardBacklightService>()
                   .EnableInterfaceInterceptors();
            builder.RegisterType<MutexBasedSingleInstanceValidator>().As<ISingleInstanceValidator>()
                   .SingleInstance()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(AnalyticsInterceptor));
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();
            builder.Register(c => Application.Current.Dispatcher);
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<ConfigurationViewModel>().AsSelf();
            builder.RegisterType<StatusBarViewModel>().AsSelf();
        }

        private void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<ManagerModule>();
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<NotificationsModule>();
            builder.RegisterModule<AnalyticsModule>();
        }
    }
}
