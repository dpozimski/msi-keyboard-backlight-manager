using Autofac;
using Castle.DynamicProxy;
using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Analytics.IoC
{
    public class AnalyticsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(MetricsPipelineProcessor<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterType<ApplicationInsightsClientFactory>().As<IApplicationInsightsClientFactory>();
            builder.RegisterType<ApplicationInsightsAnalyticsService>().As<IAnalyticsService>().SingleInstance();
            builder.Register(c => c.Resolve<IApplicationInsightsClientFactory>().Create());
            builder.RegisterType<AnalyticsInterceptor>().AsSelf();
        }
    }
}
