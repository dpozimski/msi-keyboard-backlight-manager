using System;
using Autofac;
using MSI.Keyboard.Backlight.Manager.Jobs.Models;

namespace MSI.Keyboard.Backlight.Manager.Jobs.IoC
{
    public class BacklightJobFactory : IBacklightJobFactory
    {
        private readonly ILifetimeScope _container;

        public BacklightJobFactory(ILifetimeScope container)
        {
            _container = container;
        }

        public IBacklightJob Create(BacklightJobType jobType)
        {
            return _container.ResolveKeyed<IBacklightJob>(jobType);
        }
    }
}
