using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using System.Reflection;

namespace MSI.Keyboard.Backlight.Manager.IoC
{
    public class ManagerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddMediatR(typeof(ManagerModule).Assembly);

            builder.RegisterType<JsonConfigurationRepository>().As<IConfigurationRepository>();
            builder.RegisterAssemblyTypes(typeof(ManagerModule).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .AsImplementedInterfaces();

            builder.Register(c => BacklightConfigurationBuilderFactory.Create()).InstancePerDependency().AsSelf();
            builder.Register(c => KeyboardServiceFactory.Create()).AsSelf();
        }
    }
}
