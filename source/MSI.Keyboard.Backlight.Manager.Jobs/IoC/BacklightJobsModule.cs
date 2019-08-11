using Autofac;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Manager.Jobs.DeviceMasterPeak;
using MSI.Keyboard.Backlight.Manager.Jobs.Models;
using MSI.Keyboard.Backlight.Manager.Jobs.RgbBacklight;
using MSI.Keyboard.Backlight.Manager.Jobs.TaskbarDependentBacklight;

namespace MSI.Keyboard.Backlight.Manager.Jobs.IoC
{
    public class BacklightJobsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BacklightJobFactory>().As<IBacklightJobFactory>();
            builder.Register(c => BacklightConfigurationBuilderFactory.Create())
                   .InstancePerDependency()
                   .AsSelf();

            RegisterJob<RgbBacklightJob>(builder, BacklightJobType.Rgb);
            RegisterJob<TaskbarDependentBacklightJob>(builder, BacklightJobType.TaskbarColorDependent);
            RegisterJob<VolumeMasterPeakBacklightJob>(builder, BacklightJobType.VolumeMasterPeak);
        }

        private void RegisterJob<T>(ContainerBuilder builder, BacklightJobType type)
            where T : IBacklightJob
        {
            builder.RegisterType<T>()
                   .Keyed<IBacklightJob>(type);
        }
            
    }
}
