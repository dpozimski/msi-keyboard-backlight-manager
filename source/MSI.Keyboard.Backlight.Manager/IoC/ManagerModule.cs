using Autofac;
using Autofac.Extras.DynamicProxy;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MSI.Keyboard.Backlight.Manager.Analytics;
using MSI.Keyboard.Backlight.Manager.Jobs.IoC;
using MSI.Keyboard.Backlight.Service;
using System;
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

            builder.Register(c => KeyboardServiceFactory.Create())
                   .AsSelf()
                   .SingleInstance()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(AnalyticsInterceptor));

            RegisterModules(builder);
        }

        private void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<BacklightJobsModule>();
        }
    }
}
