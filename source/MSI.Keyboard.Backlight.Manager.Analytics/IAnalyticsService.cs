using System;
using System.Collections.Generic;

namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public interface IAnalyticsService
    {
        void TrackEvent(string key, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        TrackTimedEvent TrackTimedEvent(string name);
        void TrackException(Exception exception);
    }
}