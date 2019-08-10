using System;
using Autofac;
using MSI.Keyboard.Backlight.Manager.Analytics.IoC;
using MSI.Keyboard.Backlight.Manager.IoC;
using MSI.Keyboard.Backlight.Manager.Settings.IoC;
using MSI.Keyboard.Backlight.Manager.UI.Services;
using MSI.Keyboard.Backlight.Manager.UI.ViewModels;

namespace MSI.Keyboard.Backlight.Manager.UI.IoC
{
    public class AppContainerBuilder
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            RegisterModules(builder);
            RegisterViewModels(builder);
            RegisterViews(builder);
            RegisterServices(builder);

            return builder.Build();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<RestoreConfigurationService>().As<IRestoreConfigurationService>();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().AsSelf();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<ConfigurationViewModel>().AsSelf();
        }

        private void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<ManagerModule>();
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<AnalyticsModule>();
        }
    }
}
