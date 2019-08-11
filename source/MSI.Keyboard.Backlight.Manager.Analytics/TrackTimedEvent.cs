using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public class TrackTimedEvent : IDisposable
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly string _key;
        private readonly Stopwatch _stopwatch;

        public TrackTimedEvent(IAnalyticsService analyticsService, string key)
        {
            _analyticsService = analyticsService;
            _key = key;
            _stopwatch = new Stopwatch();

            _stopwatch.Start();
        }

        public void Dispose()
        {
            _stopwatch.Stop();

            _analyticsService.TrackEvent(_key, new Dictionary<string, string>
            {
                { "Type", "TimedEvent" }
            },
            new Dictionary<string, double>
            {
                { "Elapsed", _stopwatch.ElapsedMilliseconds }
            });
        }
    }
}
