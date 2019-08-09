using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Manager.Jobs;
using MSI.Keyboard.Backlight.Manager.Jobs.TaskbarDependentBacklight;
using MSI.Keyboard.Backlight.Service;
using System.Reflection;

namespace MSI.Keyboard.Backlight.Manager.IoC
{
    public class ManagerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddMediatR(typeof(ManagerModule).Assembly);

            builder.RegisterAssemblyTypes(typeof(ManagerModule).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .AsImplementedInterfaces();

            builder.RegisterType<TaskbarDependentBacklightJob>()
                   .As<ITaskbarDependentBacklightJob>();

            builder.RegisterType<RgbBacklightJob>()
                   .As<IRgbBacklightJob>();

            builder.Register(c => BacklightConfigurationBuilderFactory.Create())
                   .InstancePerDependency()
                   .AsSelf();

            builder.Register(c => KeyboardServiceFactory.Create())
                   .AsSelf()
                   .SingleInstance();
        }
    }
}
