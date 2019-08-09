using Autofac;

namespace MSI.Keyboard.Backlight.Manager.Settings.IoC
{
    public class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JsonBacklightConfigurationRepository>()
                .AsSelf();

            builder.RegisterType<InMemoryCacheBacklightConfigurationRepository>()
                .As<IBacklightConfigurationRepository>()
                .SingleInstance();

            builder.RegisterType<WindowsRegistryFrontendAppSettings>()
                   .As<IFrontendAppSettings>()
                   .SingleInstance();

        }
    }
}
