using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Keyboard.Backlight.Manager.Notifications
{
    public interface INotificationService
    {
        void ShowInformation(string message);
        void ShowSuccess(string message);
        void ShowWarning(string message);
        void ShowError(string message);
    }
}
