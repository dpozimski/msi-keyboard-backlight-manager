using Autofac;
using System;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace MSI.Keyboard.Backlight.Manager.Notifications.IoC
{
    public class NotificationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationsService>().As<INotificationService>();
            builder.Register(c => CreateNotifier(c.Resolve<Dispatcher>())).SingleInstance();
        }

        private Notifier CreateNotifier(Dispatcher dispatcher)
        {
            var notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new PrimaryScreenPositionProvider(
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = dispatcher;
            });

            return notifier;
        }
    }
}
