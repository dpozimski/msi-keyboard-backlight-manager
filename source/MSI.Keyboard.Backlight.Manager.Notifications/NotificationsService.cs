using ToastNotifications;
using ToastNotifications.Messages;

namespace MSI.Keyboard.Backlight.Manager.Notifications
{
    public class NotificationsService : INotificationService
    {
        private readonly Notifier _notifier;

        public NotificationsService(Notifier notifier)
        {
            _notifier = notifier;
        }

        public void ShowError(string message) => _notifier.ShowError(message);

        public void ShowInformation(string message) => _notifier.ShowInformation(message);

        public void ShowSuccess(string message) => _notifier.ShowSuccess(message);

        public void ShowWarning(string message) => _notifier.ShowWarning(message);
    }
}
