using Microsoft.ApplicationInsights;

namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public interface IApplicationInsightsClientFactory
    {
        TelemetryClient Create();
    }
}
