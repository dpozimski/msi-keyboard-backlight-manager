using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;

namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public class ApplicationInsightsAnalyticsService : IAnalyticsService, IDisposable
    {
        private readonly TelemetryClient _telemetry;

        public ApplicationInsightsAnalyticsService(TelemetryClient client)
        {
            _telemetry = client;
        }

        public void TrackException(Exception exception)
        {
            _telemetry.TrackException(exception);
        }

        public void TrackEvent(string key, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _telemetry.TrackEvent(key, properties, metrics);
        }

        public TrackTimedEvent TrackTimedEvent(string name)
        {
            return new TrackTimedEvent(this, name);
        }

        public void Dispose()
        {
            _telemetry?.Flush();
        }
    }
}
