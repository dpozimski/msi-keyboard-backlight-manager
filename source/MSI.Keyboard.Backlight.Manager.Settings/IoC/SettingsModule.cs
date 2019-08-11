using Autofac;
using Autofac.Extras.DynamicProxy;
using MSI.Keyboard.Backlight.Manager.Analytics;

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
                   .SingleInstance()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(AnalyticsInterceptor));

            builder.RegisterType<WindowsRegistryFrontendAppSettings>()
                   .As<IFrontendAppSettings>()
                   .SingleInstance()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(AnalyticsInterceptor));

        }
    }
}
