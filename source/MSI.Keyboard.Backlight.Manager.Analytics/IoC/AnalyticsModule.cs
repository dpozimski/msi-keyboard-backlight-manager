using Autofac;
using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Analytics.IoC
{
    public class AnalyticsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(MetricsPipelineProcessor<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterType<AppCenterAnalyticsConfiguration>().As<IAnalyticsConfiguration>();
        }
    }
}
