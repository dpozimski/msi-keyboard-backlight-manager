using Autofac;
using MSI.Keyboard.Backlight.Manager.IoC;
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

            return builder.Build();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().AsSelf();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<ShellViewModel>().AsSelf();
        }

        private void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<ManagerModule>();
        }
    }
}
